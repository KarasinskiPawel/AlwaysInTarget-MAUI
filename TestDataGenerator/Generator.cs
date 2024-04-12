using AlwaysInTarget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.TestDataGenerator
{
    public class Generator
    {
        PlaneDataM planeData = new PlaneDataM();

        public Generator()
        {
            planeData.Altitude_M = 0;
            planeData.Mmhg = 0;
            planeData.Airspeed_KM = 0;
            planeData.Heading = 0;
            planeData.VerticalSpeed = 0;
            planeData.ServerVersion = 0.65F;
            planeData.PlaneType = "A 20 Boston";

            Execute();
        }

        private void Execute()
        {
            Random random = new Random();

            planeData.Altitude_M = random.Next(0, 17000);
            planeData.Mmhg = 0;
            planeData.Airspeed_KM = random.Next(220, 500);
            planeData.Heading = random.Next(0, 359); ;
            planeData.VerticalSpeed = random.Next(0, 10);
            planeData.ServerVersion = 0.65F;
            planeData.PlaneType = "A 20 Boston";
        }

        public PlaneDataM Output() => planeData;
    }
}
