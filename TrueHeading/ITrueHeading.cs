using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.TrueHeading
{
    internal interface ITrueHeading
    {
        public void SetValuesAndRun(int DM, int DN, int NKDM, int KZ);
        public string GetWindCorrectionAngel();
        public int GetTrueHeading();
    }
}
