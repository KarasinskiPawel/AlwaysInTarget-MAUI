#nullable disable

using AlwaysInTarget.Auxiliary;
using AlwaysInTarget.Models;
using AlwaysInTarget.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.Calculate
{
    internal class AccurateNavigationCalculator
    {
        NavigationCalculationResult output = new NavigationCalculationResult();
        NavigationModel navigationModel;
        DataConversion convertedData;

        int DM = default(int); //wiatr z kierunku
        int NKDM = default(int); //nakazany kąt drogi magnetycznej (kurs :-) )
        decimal U = default(decimal); //prędkość wiatru
        string system = string.Empty;

        int DN = default(int); //wiatr w kierunku
        int KW = default(int); //kąt wiatru
        int KZ = default(int); //Kąt znoszenia

        decimal sinKZ = default(decimal); //sinus kąta znoszenia
        decimal sinKW = default(decimal); //sinus kąta wiatru

        public AccurateNavigationCalculator(NavigationModel navigationModel, DataConversion convertedData)
        {
            this.navigationModel = navigationModel;
            this.convertedData = convertedData;

            DM = navigationModel.WindDirection;
            NKDM = navigationModel.Course;
            U = navigationModel.WindStrenght;
            system = navigationModel.SelectedSystem;

            Execute();
        }

        private void Execute()
        {
            try
            {
                U = U * 1.94M; //Convert wind speed from m/s to knots.

                convertedData.TAS_KM = new IasToTasConversion(convertedData.IAS_KM, convertedData.Altitude_FT, system).GetTAS();

                if(convertedData.TAS_KM > 0)
                {
                    DN = new WindTowards(DM).Output();
                    KW = new WindAngle(DN, NKDM).Output();

                    sinKW = new DetremineSin().CheckSinA(KW);
                    sinKZ = Math.Round((U * sinKW) / (convertedData.TAS_KM / 1.85M), 4);
                    sinKZ = Math.Round(sinKZ, 4);

                    KZ = new DetremineSin().CheckAngel(sinKZ);

                    decimal r = 0;

                    if (NKDM == DM || NKDM == DN)
                    {
                        output.WindCorrectionAngel = $"Strb: {KZ}";
                    }
                    else
                    {
                        switch (new WindRose(NKDM, DM).Output())
                        {
                            case 1:
                                output.WindCorrectionAngel = $"Strb: {KZ}";
                                r = NKDM + KZ;

                                if (r < 0)
                                    r = 360 + r;

                                if (r > 360)
                                    r = r - 360;

                                break;
                            case 2:
                                output.WindCorrectionAngel = $"Strb: {KZ}";
                                r = NKDM + KZ;

                                if (r < 0)
                                    r = 360 + r;

                                if (r > 360)
                                    r = r - 360;

                                break;
                            case 3:
                                output.WindCorrectionAngel = $"Port: {KZ}";
                                r = NKDM - KZ;

                                if (r < 0)
                                    r = 360 + r;

                                if (r > 360)
                                    r = r - 360;

                                break;
                            case 4:
                                output.WindCorrectionAngel = $"Port: {KZ}";
                                r = NKDM - KZ;

                                if (r < 0)
                                    r = 360 + r;

                                if (r > 360)
                                    r = r - 360;

                                break;
                            default:

                                break;
                        }
                    }

                    output.Heading = r.ToString();
                    output.Correct = true;

                    Storage.GetStorage().BombSightModel.Course = Convert.ToInt32(r);
                    Storage.GetStorage().BombSightModel.WindDirection = navigationModel.WindDirection;
                    Storage.GetStorage().NavigationModel.TAS_KM = Convert.ToInt32(convertedData.TAS_KM);


                }
                else
                {
                    output.WindCorrectionAngel = "IAS cannot be 0!";
                    output.Heading = "Incorrect data";
                    output.Correct = false;
                }
            }
            catch(Exception e)
            {
                output.ErrorMessage = e.Message.ToString();

                output.WindCorrectionAngel = "...err...";
                output.Heading = " ? ";
                output.Correct = false;
            }
        }

        public NavigationCalculationResult Output() => output;
    }
}
