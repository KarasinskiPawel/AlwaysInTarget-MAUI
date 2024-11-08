﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysInTarget.WindStrengthAndDirection
{
    internal class AverageWindDirection
    {
        int minAlt { get; set; }
        int maxAlt { get; set; }
        int alt { get; set; }
        int minWindFrom { get; set; } = 0;
        int maxWindFrom { get; set; } = 0;

        int averageWindFrom = 0;
        public AverageWindDirection(int min, int max, int alt, int minWindFrom, int maxWindFrom)
        { 
            this.minAlt = min;
            this.maxAlt = max;  
            this.alt = alt;
            this.minWindFrom = minWindFrom;
            this.maxWindFrom = maxWindFrom;

            Execute();
        }

        private void Execute()
        {
            try
            {
                decimal hundredsCount = 0.00M;
                int minToMaxHeightDifference = 0;
                int minToPlaneHeightDifference = 0;

                int houndredsCountToPlane = 0;

                minToMaxHeightDifference = maxAlt - minAlt;
                hundredsCount = minToMaxHeightDifference / 100;

                decimal averageWind = 0.000M;

                minToPlaneHeightDifference = alt - minAlt;

                houndredsCountToPlane = (alt - minAlt) / 100;

                if (minWindFrom <= maxWindFrom)
                {
                    int tmp = maxWindFrom - minWindFrom;
                    averageWind = tmp / hundredsCount;
                    averageWindFrom = Convert.ToInt32((houndredsCountToPlane * averageWind) + minWindFrom);
                }
                else
                {
                    int tmp = minWindFrom - maxWindFrom;
                    averageWind = tmp / hundredsCount;
                    averageWindFrom = Convert.ToInt32(minWindFrom - (houndredsCountToPlane * averageWind));
                }
            }
            catch
            {
                averageWindFrom = 0;
            }
        }

        public decimal Output()
        {
            return averageWindFrom;
        }
    }
}
