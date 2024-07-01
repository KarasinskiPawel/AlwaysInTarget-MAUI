using AlwaysInTarget.Calculate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.TrueHeading
{
    public class TrueHeadingOnline : TrueHeadingBase
    {
        public TrueHeadingOnline()
        {
            
        }
        private protected override void Run()
        {
            try
            {
                if (_nkdm == _dm || _nkdm == _dn)
                {
                    windCorrectionAngel = $"Strb: {_nkdm}";
                }
                else
                {
                    switch (new WindRose(_nkdm, _dm).Output())
                    {
                        case 1:
                            windCorrectionAngel = $"Strb: {_kz}";
                            trueHeading = _nkdm - _kz;

                            if (trueHeading < 0)
                                trueHeading = 360 + trueHeading;

                            if (trueHeading > 360)
                                trueHeading = trueHeading - 360;

                            break;
                        case 2:
                            windCorrectionAngel = $"Strb: {_kz}";
                            trueHeading = _nkdm - _kz;

                            if (trueHeading < 0)
                                trueHeading = 360 + trueHeading;

                            if (trueHeading > 360)
                                trueHeading = trueHeading - 360;

                            break;
                        case 3:
                            windCorrectionAngel = $"Port: {_kz}";
                            trueHeading = _nkdm + _kz;

                            if (trueHeading < 0)
                                trueHeading = 360 + trueHeading;

                            if (trueHeading > 360)
                                trueHeading = trueHeading - 360;

                            break;
                        case 4:
                            windCorrectionAngel = $"Port: {_kz}";
                            trueHeading = _nkdm + _kz;

                            if (trueHeading < 0)
                                trueHeading = 360 + trueHeading;

                            if (trueHeading > 360)
                                trueHeading = trueHeading - 360;

                            break;
                        default:

                            break;
                    }
                }
            }
            catch
            {
                trueHeading = -1;
                windCorrectionAngel = "err";
            }
        }
    }
}
