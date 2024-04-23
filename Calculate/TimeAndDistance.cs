using AlwaysInTarget.Models;
using AlwaysInTarget.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.Calculate
{
    public static class TimeAndDistance
    {
        private const decimal V = 3.6M;

        public static bool Calculate(NavigationOnlineModel navigation, decimal tas, int KW, decimal U, int NKDM, int DM, ref SpeedAndDistanceM outputData)
        {
            bool output = false;

            DateTime now = DateTime.Now;

            if(now - navigation.measuringTimePoint > new TimeSpan(0, 0, 1))
            {
                navigation.measuringTimePoint = now;

                outputData.GroundSpeed = GroundSpeed.Calculate(Convert.ToInt32(tas), KW, U, NKDM, DM);

                if(outputData.GroundSpeed > -1)
                {
                    outputData.Distance = ((outputData.GroundSpeed / V) / 1000);
                }
                else
                {
                    outputData.Distance = 0;
                }

                output = true;
            }

            return output;
        }
    }
}
