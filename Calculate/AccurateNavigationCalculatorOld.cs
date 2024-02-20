//using AlwaysInTarget.Auxiliary;
//using AlwaysInTarget.ViewModels;
//using PilotCalculator.Calculate;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace AlwaysInTarget.Calculate
//{
//    internal class AccurateNavigationCalculator
//    {
//        DataConversion _dataConversion;
//        NavigationModel _navigationModel;

//        int DM = default(int); //wiatr z kierunku
//        int NKDM = default(int); //nakazany kąt drogi magnetycznej (kurs :-) )
//        decimal U = default(decimal); //prędkość wiatru
//        string system = string.Empty;

//        int DN = default(int); //wiatr w kierunku
//        int KW = default(int); //kąt wiatru
//        int KZ = default(int); //Kąt znoszenia

//        decimal sinKZ = default(decimal); //sinus kąta znoszenia
//        decimal sinKW = default(decimal); //sinus kąta wiatru

//        public AccurateNavigationCalculator(DataConversion dataConversion, NavigationModel navigationModel)
//        {
//            _dataConversion = dataConversion;
//            _navigationModel = navigationModel;

//            DM = navigationModel.WindDirection;
//            NKDM = navigationModel.Course;
//            U = navigationModel.WindStrenght;
//            system = navigationModel.SelectedSystem;
//        }

//        public bool Execute()
//        {
//            try
//            {
//                U = U * 1.94M; //Convert wind speed from m/s to knots.

//                DataWarehouse.TAS_KM = new IasToTasConversion(DataWarehouse.IAS_KM, DataWarehouse.Altitude_FT, system).GetTAS();

//                if (DataWarehouse.TAS_KM != 0)
//                {
//                    DataWarehouse.TAS_MPH = Convert.ToInt32(DataWarehouse.TAS_KM / 1.609M);

//                    DN = new WindTowards(DM).Output();
//                    KW = new WindAngle(DN, NKDM).Output();

//                    sinKW = new DetremineSin().CheckSinA(KW);
//                    sinKZ = Math.Round((U * sinKW) / (DataWarehouse.TAS_KM / 1.85M), 4);
//                    sinKZ = Math.Round(sinKZ, 4);

//                    KZ = new DetremineSin().CheckAngel(sinKZ);

//                    decimal r = 0;

//                    if(NKDM == DM || NKDM == DN)
//                    {
//                        DataWarehouse.WindCorrectionAngel = $"{KZ}";

//                        r = DM;
//                    }
//                    else
//                    {
//                        switch (new WindRose(NKDM, DM).Output())
//                        {
//                            case 1:
//                                DataWarehouse.WindCorrectionAngel = $"Strb: {KZ}";
//                                r = NKDM + KZ;

//                                if (r < 0)
//                                    r = 360 + r;

//                                if (r > 360)
//                                    r = r - 360;

//                                break;
//                            case 2:
//                                DataWarehouse.WindCorrectionAngel = $"Strb: {KZ}";
//                                r = NKDM + KZ;

//                                if (r < 0)
//                                    r = 360 + r;

//                                if (r > 360)
//                                    r = r - 360;

//                                break;
//                            case 3:
//                                DataWarehouse.WindCorrectionAngel = $"Port: {KZ}";
//                                r = NKDM - KZ;

//                                if (r < 0)
//                                    r = 360 + r;

//                                if (r > 360)
//                                    r = r - 360;

//                                break;
//                            case 4:
//                                DataWarehouse.WindCorrectionAngel = $"Port: {KZ}";
//                                r = NKDM - KZ;

//                                if (r < 0)
//                                    r = 360 + r;

//                                if (r > 360)
//                                    r = r - 360;

//                                break;
//                            default:

//                                break;
//                        }
//                    }

//                    DataWarehouse.Heading = r.ToString();
//                }
//                else
//                {
//                    DataWarehouse.WindCorrectionAngel = "IAS cannot be 0!";
//                    DataWarehouse.Heading = "Incorrect data.";

//                    return false;
//                }
//            }
//            catch
//            {
//                DataWarehouse.WindCorrectionAngel = "...err...";
//                DataWarehouse.Heading = "Incorrect data.";

//                return false;
//            }

//            return true;
//        }
//    }
//}
