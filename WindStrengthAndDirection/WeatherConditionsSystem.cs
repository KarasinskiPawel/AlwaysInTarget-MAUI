using AlwaysInTarget.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AlwaysInTarget.WindStrengthAndDirection
{
    internal class WeatherConditionsSystem
    {
        readonly DataRecalculationRequest data;

        public List<WeatherConditionM> WeatherConditions { get; }

        public WeatherConditionsSystem(DataRecalculationRequest dataRecalculationRequest, List<WeatherConditionM> WeatherConditions)
        {
            data = dataRecalculationRequest;
            this.WeatherConditions = WeatherConditions;
        }

        public decimal GetWindStrength()
        {
            //wysokość w metrach

            decimal output = default;

            try
            {
                for (int i = 0; i < WeatherConditions.Count; i++)
                {
                    if (data.Altitude_M == WeatherConditions[i].Alt)
                    {
                        output = WeatherConditions[i].WindStregngth;
                        break;
                    }
                    else if (i + 1 < WeatherConditions.Count && data.Altitude_M > WeatherConditions[i].Alt && data.Altitude_M < WeatherConditions[i + 1].Alt)
                    {
                        output = new AverageWindStrenght(WeatherConditions[i].Alt, WeatherConditions[i + 1].Alt, Convert.ToInt32(data.Altitude_M), WeatherConditions[i].WindStregngth, WeatherConditions[i + 1].WindStregngth).Output();
                        break;
                    }
                    else if (i + 1 >= WeatherConditions.Count && data.Altitude_M > WeatherConditions[i].Alt)
                    {
                        output = WeatherConditions[i].WindStregngth;
                        break;
                    }
                }
            }
            catch
            {

            }

            return output;
        }

        public int GetWindDirection()
        {
            //wysokość w metrach

            decimal output = default;

            try
            {
                for (int i = 0; i < WeatherConditions.Count; i++)
                {
                    if (data.Altitude_M == WeatherConditions[i].Alt)
                    {
                        output = WeatherConditions[i].WindFrom;
                        break;
                    }
                    else if (i + 1 < WeatherConditions.Count && data.Altitude_M > WeatherConditions[i].Alt && data.Altitude_M < WeatherConditions[i + 1].Alt)
                    {
                        output = new AverageWindDirection(WeatherConditions[i].Alt, WeatherConditions[i + 1].Alt, Convert.ToInt32(data.Altitude_M), WeatherConditions[i].WindFrom, WeatherConditions[i + 1].WindFrom).Output();
                        break;
                    }
                    else if (i + 1 >= WeatherConditions.Count && data.Altitude_M > WeatherConditions[i].Alt)
                    {
                        output = WeatherConditions[i].WindFrom;
                        break;
                    }
                }
            }
            catch
            {

            }

            return Convert.ToInt32(output);
        }
    }
}
