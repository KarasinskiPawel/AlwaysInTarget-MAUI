using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysInTarget.Calculate
{
    internal class WindAngle
    {
        readonly int _DN;
        readonly int _NKDM;

        int KW = 0;

        public WindAngle(int dn, int nkdm)
        {
            _DN = dn;
            _NKDM = nkdm;

            Execute();
        }

        private void Execute()
        {
            KW = _DN - _NKDM;

            if (KW <= -180)
            {
                KW = 360 + KW;

                if (KW > 90)
                {
                    KW = KW - 90;
                    KW = 90 - KW;
                }
            }

            if (KW < -90)
            {
                KW = 90 + KW;
                KW = 90 + KW;
            }

            if (KW > 180)
            {
                KW = 360 - KW;

                if (KW > 90)
                {
                    KW = KW - 90;
                    KW = 90 - KW;
                }
            }

            if (KW > 90)
            {
                KW = 90 - KW;
                KW = 90 + KW;
            }

            if (KW < 0)
                KW = KW * (-1);
        }

        public int Output() => KW;
    }
}
