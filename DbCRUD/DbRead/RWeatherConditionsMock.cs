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
            output.Add(new WeatherConditionM { Alt = 0, WindFrom = 100, WindStregngth = 5.0M });
            output.Add(new WeatherConditionM { Alt = 500, WindFrom = 105, WindStregngth = 10.0M });
            output.Add(new WeatherConditionM { Alt = 1000, WindFrom = 110, WindStregngth = 15.0M });
            output.Add(new WeatherConditionM { Alt = 2000, WindFrom = 130, WindStregngth = 25.0M });
            output.Add(new WeatherConditionM { Alt = 5000, WindFrom = 100, WindStregngth = 10.0M });
            //output.Add(new WeatherConditionM { Alt = 7000, WindFrom = 280, WindStregngth = 24.0M });
            //output.Add(new WeatherConditionM { Alt = 8000, WindFrom = 260, WindStregngth = 20.0M });
            //output.Add(new WeatherConditionM { Alt = 10000, WindFrom = 300, WindStregngth = 16.0M });
        }

        public List<WeatherConditionM> Output() => output;
    }
}
