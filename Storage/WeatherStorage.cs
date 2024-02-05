using AlwaysInTarget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.Storage
{
    internal static class WeatherStorage
    {
        internal static List<WeatherConditionM> WeatherConditions { get; set; } = new List<WeatherConditionM>();
    }
}
