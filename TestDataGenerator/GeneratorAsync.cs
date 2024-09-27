using AlwaysInTarget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.TestDataGenerator
{
    public static class GeneratorAsync
    {
        private static int heading = 100;

        public static async Task<PlaneDataM> GetTestData()
        {
            PlaneDataM planeData = new PlaneDataM();

            planeData.Altitude_M = 1500;
            planeData.Mmhg = 0;
            planeData.Airspeed_KM = 300;
            planeData.Heading = heading;
            planeData.VerticalSpeed = 0;
            planeData.ServerVersion = 0.65F;
            planeData.PlaneType = "A 20 Boston";

            heading += 1;

            if (heading >= 355)
                heading = 0;

            await Task.Delay(1000);

            return planeData;
        }
    }
}
