using AlwaysInTarget.DbCRUD.DbFake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.Calculate
{
    class Cosinus
    {
        public decimal CheckCosA(int KW)
        {
            return new TrigonometricCosTable().Output().Where(a => a.Angle == KW).First().CosA;
        }

    }
}
