using PilotCalculator.Models;
using AlwaysInTarget.Calculate;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AlwaysInTarget.Calculate
{
    internal class WindRose
    {
        private readonly int _NKDM;
        private readonly int _DM;

        private int quarterCompassRose = 0;

        public WindRose(int NKDM, int DM)
        {
            _NKDM = NKDM;
            _DM = DM;

            Execute();
        }

        private void Execute()
        {
            try
            {
                List<WindRoseM> windRoseDirections = new List<WindRoseM>();

                windRoseDirections.Add(new WindRoseM { Quarter = 1, Value = _NKDM });

                int quartetTo_I = _NKDM + 90;
                if (quartetTo_I > 360)
                {
                    quartetTo_I = (quartetTo_I - 360);

                    windRoseDirections.Add(new WindRoseM { Quarter = 1, Value = 360 });
                    windRoseDirections.Add(new WindRoseM { Quarter = 1, Value = 0 });
                    windRoseDirections.Add(new WindRoseM { Quarter = 1, Value = quartetTo_I });
                }
                else
                {
                    windRoseDirections.Add(new WindRoseM { Quarter = 1, Value = quartetTo_I });
                }

                int quartetTo_II = quartetTo_I + 90;
                if (quartetTo_II > 360)
                {
                    quartetTo_II = (quartetTo_II - 360);

                    windRoseDirections.Add(new WindRoseM { Quarter = 2, Value = 360 });
                    windRoseDirections.Add(new WindRoseM { Quarter = 2, Value = 0 });
                    windRoseDirections.Add(new WindRoseM { Quarter = 2, Value = quartetTo_II });
                }
                else
                {
                    windRoseDirections.Add(new WindRoseM { Quarter = 2, Value = quartetTo_II });
                }

                int quartetTo_III = quartetTo_II + 90;
                if (quartetTo_III > 360) 
                {
                    quartetTo_III = (quartetTo_III - 360);

                    windRoseDirections.Add(new WindRoseM { Quarter = 3, Value = 360 });
                    windRoseDirections.Add(new WindRoseM { Quarter = 3, Value = 0 });
                    windRoseDirections.Add(new WindRoseM { Quarter = 3, Value = quartetTo_III });

                }
                else
                {
                    windRoseDirections.Add(new WindRoseM { Quarter = 3, Value = quartetTo_III });
                }

                int quartetTo_IV = quartetTo_III + 90;
                if (quartetTo_IV > 360)
                {
                    quartetTo_IV = (quartetTo_IV - 360);

                    windRoseDirections.Add(new WindRoseM { Quarter = 4, Value = 360 });
                    windRoseDirections.Add(new WindRoseM { Quarter = 4, Value = 0 });
                    windRoseDirections.Add(new WindRoseM { Quarter = 4, Value = quartetTo_IV });
                }
                else
                {
                    windRoseDirections.Add(new WindRoseM { Quarter = 4, Value = quartetTo_IV });
                }

                for(int i = 0; i < windRoseDirections.Count; i++)
                {
                    Debug.WriteLine($"Quarter: { windRoseDirections[i].Quarter}, Bearing: {windRoseDirections[i].Value}");

                    if (!(windRoseDirections[i+1] is null))
                    {
                        if(_DM >= windRoseDirections[i].Value && _DM <= windRoseDirections[i + 1].Value)
                        {
                            quarterCompassRose = windRoseDirections[i + 1].Quarter;
                        }
                    }
                }
            }
            catch
            {

            }
        }

        internal int Output()
        {
            return quarterCompassRose;
        }
    }
}
