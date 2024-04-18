using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.Calculate
{
    public static class GroundSpeed
    {
        //Example
        //W = Vr + U* cosKW.
        //W = 100 + 5 * (-0,26)
        //W = 100 – 1,3
        //W= 99

        //W - prędkość podróżna
        //VR - TAS
        //U - prędkość wiatru (knots)

        public static decimal Calculate(int tas, int KW, decimal U)
        {
            int groundSpeed = 0;

            try
            {
                decimal cosKW = new Cosinus().CheckCosA(KW);
                groundSpeed = Convert.ToInt32(tas + U * cosKW);
            }
            catch
            {
                groundSpeed = -1;
            }

            return groundSpeed;
        }
    }
}
