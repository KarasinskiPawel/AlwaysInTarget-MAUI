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

        public static decimal Calculate(int tas, int KW, decimal U, int NKDM, int DM)
        {
            int groundSpeed = 0;

            try
            {
                decimal cosKW = new Cosinus().CheckCosA(KW);

                //Obiczenie prędkości względem ziemi z uwzględnieniem siły i kierunku wiatru
                //switch (new WindRose(NKDM, DM).Output())
                //{
                //    case 1: //-
                //        groundSpeed = Convert.ToInt32(tas + U * (-1 * cosKW));
                //        break;
                //    case 2: //+
                //        groundSpeed = Convert.ToInt32(tas + U * cosKW);
                //        break;
                //    case 3: //+
                //        groundSpeed = Convert.ToInt32(tas + U * cosKW);
                //        break;
                //    case 4: //-
                //        groundSpeed = Convert.ToInt32(tas + U * (-1 * cosKW));
                //        break;
                //    default:
                //        groundSpeed = Convert.ToInt32(tas);
                //        break;
                //}

                //Prędkość względem ziemi == TAS - do testów (Ił 2 prawdopodobnie siłę wiatru uwzględnia już w IAS)
                groundSpeed = Convert.ToInt32(tas);
            }
            catch
            {
                groundSpeed = -1;
            }

            return groundSpeed;
        }
    }
}
