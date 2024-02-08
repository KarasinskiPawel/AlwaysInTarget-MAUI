using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget
{
    internal class DataRecalculationRequest
    {
        public decimal IAS_KM { get; set; }
        public decimal IAS_MPH { get; set; }
        public decimal TAS_KM { get; set; }
        public decimal TAS_MPH { get; set; }
        public int Altitude_M { get; set; }
        public int Altitude_FT { get; set; }

        public DataRecalculationRequest(string system, int ias)
        {
            SetIas(ias, system);
        }

        public DataRecalculationRequest(int altitude, string system)
        {
            SetAltitude(altitude, system);
        }

        public DataRecalculationRequest(int ias, int altitude, string system)
        {
            SetIas(ias, system);
            SetAltitude(altitude, system);
        }

        private void SetIas(int ias, string system)
        {
            switch (system)
            {
                case "Metric":
                    IAS_KM = ias;
                    IAS_MPH = Convert.ToInt32(ias / 1.609M);
                    break;
                case "Imperial":
                    IAS_MPH = ias;
                    IAS_KM = Convert.ToInt32(ias * 1.609M);
                    break;
                default:
                    IAS_KM = 0;
                    IAS_MPH = 0;
                    break;
            }
        }

        private void SetAltitude(int altitude, string system)
        {
            switch (system)
            {
                case "Metric":
                    Altitude_M = altitude;
                    Altitude_FT = Convert.ToInt32(altitude * 3.045);
                    break;
                case "Imperial":
                    Altitude_FT = altitude;
                    Altitude_M = Convert.ToInt32(altitude / 3.045);
                    break;
                default:
                    Altitude_M = 0;
                    Altitude_FT = 0;
                    break;
            }
        }
    }
}
