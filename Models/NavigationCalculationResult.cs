using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.Models
{
    internal class NavigationCalculationResult
    {
        public string? WindCorrectionAngel { get; set; }
        public string? Heading { get; set; }
        public bool Correct { get; internal set; }
        internal string? ErrorMessage { get; set; }
        public decimal GroundSpeed { get; set; }
        public decimal Distance { get; set; }
    }
}
