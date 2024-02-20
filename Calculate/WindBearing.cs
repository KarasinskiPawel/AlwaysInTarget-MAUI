using System;
using System.Collections.Generic;
using System.Text;

namespace PilotCalculator.Calculate
{
    internal class WindBearing
    {
        private readonly int _DM;
        private readonly int _NKDM;

        private string output;
        public WindBearing(int DM, int NKDM)
        {
            _DM = DM;
            _NKDM = NKDM;

            Execute();
        }

        private void Execute()
        {
            try
            {
                int KW = _NKDM - _DM;

                if (KW < -180)
                {
                    KW = 360 + KW;
                }

                if(KW == 0 && KW == 180)
                {
                    output = "0";
                }
                else if(KW < 0)
                {
                    output = "Strb";
                }
                else if (KW >= 270) {
                    output = "Port";
                }
                else if(KW > 0)
                {
                    output = "Port";
                }
                else
                {
                    output = "Wrong";
                }
            }
            catch { }
            {

            }
        }

        public string Output()
        {
            return output;
        }
    }
}
