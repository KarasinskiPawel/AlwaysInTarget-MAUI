using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysInTarget.WindStrengthAndDirection
{
    internal class AverageWindStrenght
    {
        int minAlt { get; set; }
        int maxAlt { get; set; }
        int alt { get; set; }

        decimal minWindStrength { get; set; }
        decimal maxWindStrength { get; set; }

        decimal averageWindStrength { get; set; }

        public AverageWindStrenght(int min, int max, int alt, decimal minWindStrength, decimal maxWindStrength)
        {
            minAlt = min;
            maxAlt = max;
            this.alt = alt;
            this.minWindStrength = minWindStrength;
            this.maxWindStrength = maxWindStrength;

            Execute();
        }

        private void Execute()
        {
            try
            {
                decimal hundredsCount = 0.00M;
                int minToMaxHeightDifference = 0;
                int minToPlaneHeightDifference = 0;
                decimal windStrengthPer100 = 0.0M;

                int houndredsCountToPlane = 0;

                minToMaxHeightDifference = maxAlt - minAlt;
                hundredsCount = minToMaxHeightDifference / 100;

                if(maxWindStrength >= minWindStrength)
                {
                    windStrengthPer100 = (maxWindStrength - minWindStrength) / hundredsCount;

                    minToPlaneHeightDifference = alt - minAlt;

                    houndredsCountToPlane = minToPlaneHeightDifference / 100;

                    averageWindStrength = (houndredsCountToPlane * windStrengthPer100) + minWindStrength;
                }
                else
                {
                    windStrengthPer100 = (minWindStrength - maxWindStrength) / hundredsCount;

                    minToPlaneHeightDifference = alt - minAlt;

                    houndredsCountToPlane = minToPlaneHeightDifference / 100;

                    averageWindStrength = (houndredsCountToPlane * windStrengthPer100) + minWindStrength;
                }
            }
            catch
            {
                averageWindStrength = 0;
            }
        }

        public decimal Output()
        {
            return averageWindStrength;
        }
    }
}
