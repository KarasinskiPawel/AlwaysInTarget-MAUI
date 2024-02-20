#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.Models
{
    internal class NavigationPointM
    {
        public int Lp { get; set; }
        public int Course { get; set; }
        public decimal IAS { get; set; }
        public decimal TAS { get; set; }
        public int Altitude { get; set; }
        public string WindCorrectionAngel { get; set; }
        public string Heading { get; set; }
        public string BombSightDeflection { get; set; }

        public NavigationPointM()
        {
            
        }

        public NavigationPointM(int course, decimal ias, decimal tas, int altitude, string windCorrectionAngel, string heading, string bombSightDeflection)
        {
            Course = course;
            IAS = ias;
            TAS = tas;
            Altitude = altitude;
            WindCorrectionAngel = windCorrectionAngel;
            Heading = heading;
            BombSightDeflection = bombSightDeflection;
        }
    }
}
