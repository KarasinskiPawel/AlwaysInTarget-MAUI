using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysInTarget.Calculate
{
    internal class WindTowards
    {
        readonly int _DM;
        int DN;
        public WindTowards(int dm)
        {
            _DM = dm;

            Execute();
        }

        private void Execute()
        {
            DN = _DM + 180;

            if (DN > 360)
                DN = DN - 360;
        }

        public int Output() => DN;
    }
}
