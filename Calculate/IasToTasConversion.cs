using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysInTarget.Auxiliary
{
    internal class IasToTasConversion
    {
        private int _IAS = default;
        private readonly int _altitudeInFeet = default;
        private int speedInKM = default;
        private int TAS = default;

        private static decimal FEET = 3.045M;
        private static decimal ANGLO_SAXON_MILE = 1.604m;

        public IasToTasConversion(decimal IAS, int altitudeInFeet)
        {
            _IAS = Convert.ToInt32(IAS);
            _altitudeInFeet = altitudeInFeet;

            Execute();
        }

        private void Execute()
        {

            if (_altitudeInFeet < 5000)
            {
                TAS = Convert.ToInt32(_IAS + (_IAS * ((_altitudeInFeet / 1000) * 0.015)));
            }
            else if(_altitudeInFeet >= 5000 && _altitudeInFeet < 10000) {
                TAS = Convert.ToInt32(_IAS + (_IAS * ((_altitudeInFeet / 1000) * 0.016)));
            }
            else if(_altitudeInFeet >= 10000 && _altitudeInFeet < 15000)
            {
                TAS = Convert.ToInt32(_IAS + (_IAS * ((_altitudeInFeet / 1000) * 0.017)));
            }
            else if(_altitudeInFeet >= 15000 & _altitudeInFeet < 20000)
            {
                TAS = Convert.ToInt32(_IAS + (_IAS * ((_altitudeInFeet / 1000) * 0.0185)));
            }
            else
            {
                TAS = Convert.ToInt32(_IAS + (_IAS * ((_altitudeInFeet / 1000) * 0.02)));
            }
        }

        public int GetTAS_KM_H()
        {
            return TAS;
        }

        public int GetTAS_MPH()
        {
            return Convert.ToInt32(TAS / 1.609M);
        }
    }
}
