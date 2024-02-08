using AlwaysInTarget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.ViewModels
{
    public class NavigationModel
    {
        public string SelectedSystem { get; set; }
        public int Course { get; set; }
        public int IAS { get; set; }
        public  int Altitude { get; set; }    
        public int WindDirection { get; set; }
        public decimal WindStrenght { get; set; }
        public string? WindCorrectionAngel { get; set; }
        public string? Heading { get; set; }
        public string? BombSightDeflection { get; set; }

        public string[] MeasureSystems { get; set; } = ["Metric", "Imperial"];

        public NavigationModel()
        {
            SelectedSystem = "Metric";
        }
    }
}
