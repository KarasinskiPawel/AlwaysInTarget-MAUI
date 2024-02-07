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
        public static string SystemM { get; set; } = "Metric";
        public static int Course { get; set; }
        public static decimal IAS_KM { get; set; }
        public static decimal IAS_MPH { get; set; }
        public static decimal TAS_KM { get; set; }
        public static decimal TAS_MPH { get; set; }
        public static int Altitude_M { get; set; }
        public static int Altitude_FT { get; set; }
        public static int WindDirection { get; set; }
        public static decimal WindStrenght { get; set; }
        public static string? WindCorrectionAngel { get; set; }
        public static string? Heading { get; set; }
        public static string? BombSightDeflection { get; set; }

        public IList<SystemM> MeasureSystems { get; set; } = new List<SystemM>();

        public NavigationModel()
        {
            MeasureSystems.Add(new SystemM { Id = 1, Name = "Metric" });
            MeasureSystems.Add(new SystemM { Id = 2, Name = "Imperial" });
        }
    }
}
