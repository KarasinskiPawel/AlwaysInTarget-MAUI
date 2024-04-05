#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.Models
{
    internal class PlaneDataM
    {
        public float Altitude { get; set; }
        public float Mmhg { get; set; }
        public float Airspeed { get; set; }
        public float Heading { get; set; }
        public float VerticalSpeed { get; set; }
        public float ServerVersion { get; set; }
        public string PlaneType { get; set; }

        public PlaneDataM() { }

        public PlaneDataM(float altitude, float mmhg, float airspeed, float heading, float varticalSpeed)
        {
            Altitude = altitude;
            Mmhg = mmhg;
            Airspeed = airspeed;
            Heading = heading;
            VerticalSpeed = varticalSpeed;
        }
    }
}
