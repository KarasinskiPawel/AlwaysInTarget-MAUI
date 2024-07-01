using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.TrueHeading
{
    public abstract class TrueHeadingBase : ITrueHeading
    {
        protected string windCorrectionAngel = string.Empty;
        protected int trueHeading = 0;

        protected int _dm; // skąd wieje wiatr
        protected int _dn; // dokąd wieje wiatr
        protected int _nkdm; // nakazany kąt drogi magnetycznej - dokąd ma lecieć samolot.
        protected int _kz; // kąt znoszenia

        protected TrueHeadingBase()
        {

        }

        public void SetValuesAndRun(int DM, int DN, int NKDM, int KZ)
        {
            _dm = DM;
            _nkdm = NKDM;
            _dn = DN;
            _kz = KZ;

            Run();
        }

        private protected abstract void Run();
        public int GetTrueHeading() => trueHeading;

        public string GetWindCorrectionAngel() => windCorrectionAngel;
    }
}
