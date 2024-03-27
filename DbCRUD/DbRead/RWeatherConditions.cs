using AlwaysInTarget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.DbCRUD.DbRead
{
    class RWeatherConditions
    {
        List<WeatherConditionM> output = new List<WeatherConditionM>();
        public RWeatherConditions()
        {
            Execute();
        }

        private void Execute()
        {
            output.Add(new WeatherConditionM { Alt = 0, WindFrom = 0, WindStregngth = 0.0M });
            output.Add(new WeatherConditionM { Alt = 500, WindFrom = 0, WindStregngth = 0.0M });
            output.Add(new WeatherConditionM { Alt = 1000, WindFrom = 0, WindStregngth = 0.0M });
            output.Add(new WeatherConditionM { Alt = 2000, WindFrom = 0, WindStregngth = 0.0M });
            output.Add(new WeatherConditionM { Alt = 5000, WindFrom = 0, WindStregngth = 0.0M });
        }

        public List<WeatherConditionM> Output() => output;
    }
}
