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
        public static void Calculate(NavigationOnlineModel navigation)
        {
            decimal groundSpeed = 0;
            decimal distance = 0;

            DateTime now = DateTime.Now;

            if(now - navigation.measuringTimePoint > new TimeSpan(0, 0, 1))
            {
                navigation.measuringTimePoint = now;

            }
        }
    }
}
