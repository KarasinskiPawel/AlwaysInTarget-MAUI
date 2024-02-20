using AlwaysInTarget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.DbCRUD.DbRead
{
    class RWeatherConditionsMock
    {
        List<WeatherConditionM> output = new List<WeatherConditionM>();
        public RWeatherConditionsMock()
        {
            Execute();
        }

        private void Execute()
        {
            output.Add(new WeatherConditionM { Altidute = "0m", Alt = 0, WindFrom = 255, WindStregngth = 3.0M });
            output.Add(new WeatherConditionM { Altidute = "500m", Alt = 500, WindFrom = 260, WindStregngth = 7.0M });
            output.Add(new WeatherConditionM { Altidute = "1000m", Alt = 1000, WindFrom = 265, WindStregngth = 11.0M });
            output.Add(new WeatherConditionM { Altidute = "2000m", Alt = 2000, WindFrom = 270, WindStregngth = 14.0M });
            output.Add(new WeatherConditionM { Altidute = "5000m", Alt = 5000, WindFrom = 265, WindStregngth = 21.0M });
        }

        public List<WeatherConditionM> Output() => output;
    }
}
