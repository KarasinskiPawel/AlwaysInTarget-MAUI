using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysInTarget.Calculate;
using AlwaysInTarget.DbCRUD.DbFake;

namespace AlwaysInTarget.Auxiliary
{
    internal class Sinus
    {
        public decimal CheckSinA(int KW)
        {
            return new TrigonometricSineTable().Output().Where(a => a.Angle == KW).First().SinA;
        }

        public int CheckAngel(decimal sin)
        {
            int output = default(int);

            var trigonometricSineTable = new TrigonometricSineTable().Output();

            for(int i = 0; i < trigonometricSineTable.Count; i++)
            {
                if(i == trigonometricSineTable.Count -1)
                {
                    output = trigonometricSineTable[i].Angle;
                }
                else if(sin >= trigonometricSineTable[i].SinA && sin <= trigonometricSineTable[i+1].SinA)
                {
                    if(Math.Round(sin, 3) == Math.Round(trigonometricSineTable[i + 1].SinA, 3))
                    {
                        output = trigonometricSineTable[i + 1].Angle;
                        break;
                    }

                    if(Math.Round(sin, 3) == Math.Round(trigonometricSineTable[i].SinA, 3))
                    {
                        output = trigonometricSineTable[i].Angle;
                        break;
                    }

                    if (Math.Round(sin, 2) == Math.Round(trigonometricSineTable[i + 1].SinA, 2))
                    {
                        output = trigonometricSineTable[i + 1].Angle;
                        break;
                    }

                    if (Math.Round(sin, 2) == Math.Round(trigonometricSineTable[i].SinA, 2))
                    {
                        output = trigonometricSineTable[i].Angle;
                        break;
                    }

                    if (Math.Round(sin, 1) == Math.Round(trigonometricSineTable[i + 1].SinA, 1))
                    {
                        output = trigonometricSineTable[i + 1].Angle;
                        break;
                    }

                    if (Math.Round(sin, 1) == Math.Round(trigonometricSineTable[i].SinA, 1))
                    {
                        output = trigonometricSineTable[i].Angle;
                        break;
                    }
                }
            }

            return output;
        }
    }
}
