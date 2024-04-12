#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.Models
{
    public class PlaneDataM
    {
        public float Altitude_M { get; set; }
        public float Mmhg { get; set; }
        public float Airspeed_KM { get; set; }
        public float Heading { get; set; }
        public float VerticalSpeed { get; set; }
        public float ServerVersion { get; set; }
        public string PlaneType { get; set; }

        public PlaneDataM() { }

        public PlaneDataM(float altitude, float mmhg, float airspeed, float heading, float varticalSpeed)
        {
            ///Dane z gry:
            ///wysokość - zawsze w metrach
            ///Prędkość - zawsze w km/h
            ///kierunek - radiany

            Altitude_M = altitude;
            Mmhg = mmhg;
            Airspeed_KM = airspeed;
            Heading = RadiansToDegrees(heading);
            VerticalSpeed = varticalSpeed;
        }

        private int RadiansToDegrees(float radians)
        {
            int output;

            try
            {
                output = Convert.ToInt32((radians * Math.PI) / 180);
            }
            catch
            {
                output = 0;
            }

            return output;
        }

        public void SetPlaneData(PlaneDataM newData)
        {
            Altitude_M = newData.Altitude_M;
            Mmhg= newData.Mmhg;
            Airspeed_KM = newData.Airspeed_KM;
            Heading = newData.Heading;
            VerticalSpeed = newData.VerticalSpeed;
            ServerVersion = newData.ServerVersion;
            PlaneType = newData.PlaneType;
        }
    }
}
