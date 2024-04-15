#nullable disable

using AlwaysInTarget.Auxiliary;
using AlwaysInTarget.Models;
using AlwaysInTarget.TrueHeading;
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
        NavigationOnlineModel navigationOnlineModel;
        DataConversion convertedData;
        ITrueHeading trueHeading;

        int DM = default(int); //wiatr z kierunku
        int NKDM = default(int); //nakazany kąt drogi magnetycznej (kurs :-) )
        decimal U = default(decimal); //prędkość wiatru
        string system = string.Empty;

        int DN = default(int); //wiatr w kierunku
        int KW = default(int); //kąt wiatru
        int KZ = default(int); //Kąt znoszenia

        decimal sinKZ = default(decimal); //sinus kąta znoszenia
        decimal sinKW = default(decimal); //sinus kąta wiatru

        public AccurateNavigationCalculator(NavigationModel navigationModel, DataConversion convertedData, ITrueHeading trueHeading)
        {
            this.navigationModel = navigationModel;
            this.convertedData = convertedData;

            DM = navigationModel.WindDirection;
            NKDM = navigationModel.Course;
            U = navigationModel.WindStrenght;
            system = navigationModel.SelectedSystem;

            this.trueHeading = trueHeading;

            Execute();
        }

        public AccurateNavigationCalculator(NavigationOnlineModel navigationModel, DataConversion convertedData, ITrueHeading trueHeading)
        {
            this.navigationOnlineModel = navigationModel;
            this.convertedData = convertedData;

            DM = navigationOnlineModel.WindDirection;
            NKDM = navigationOnlineModel.Course;
            U = navigationOnlineModel.WindStrenght;
            system = navigationOnlineModel.SelectedSystem;

            this.trueHeading = trueHeading;

            Execute();
        }

        private void Execute()
        {
            try
            {
                U = U * 1.94M; //Convert wind speed from m/s to knots.

                convertedData.TAS_KM = new IasToTasConversion(convertedData.IAS_KM, convertedData.Altitude_FT).GetTAS_KM_H();

                if(convertedData.TAS_KM > 0)
                {
                    DN = new WindTowards(DM).Output();
                    KW = new WindAngle(DN, NKDM).Output();

                    sinKW = new DetremineSin().CheckSinA(KW);
                    sinKZ = Math.Round((U * sinKW) / (convertedData.TAS_KM / 1.85M), 4);
                    sinKZ = Math.Round(sinKZ, 4);

                    KZ = new DetremineSin().CheckAngel(sinKZ);

                    trueHeading.SetValuesAndRun(DM, DN, NKDM, KZ);
                    output.Heading = trueHeading.GetTrueHeading().ToString();
                    output.WindCorrectionAngel = trueHeading.GetWindCorrectionAngel();
                    output.Correct = true;

                    Storage.GetStorage().BombSightModel.Course = Convert.ToInt32(trueHeading.GetTrueHeading());

                    if(!(navigationModel is null))
                        Storage.GetStorage().BombSightModel.WindDirection = navigationModel.WindDirection;

                    if(!(navigationOnlineModel is null))
                        Storage.GetStorage().BombSightModel.WindDirection = navigationOnlineModel.WindDirection;

                    Storage.GetStorage().NavigationModel.TAS_KM = Convert.ToInt32(convertedData.TAS_KM);
                }
                else
                {
                    output.WindCorrectionAngel = "- - -";
                    output.Heading = "- - -";
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
