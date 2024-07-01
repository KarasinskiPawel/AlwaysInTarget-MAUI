using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlwaysInTarget.WindStrengthAndDirection
{
    public class AverageWindDirection
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

            this.minWindFrom = minWindFrom == 0 ? 360 : minWindFrom;
            this.maxWindFrom = maxWindFrom == 0 ? 360 : maxWindFrom;

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

                int tmp = 0;

                if (minWindFrom > 270 && minWindFrom < 360 && maxWindFrom > 0 && maxWindFrom < 90)
                {
                    tmp = (360 - minWindFrom) + maxWindFrom;

                    averageWind = tmp / hundredsCount;

                    if (minWindFrom <= maxWindFrom)
                    {
                        averageWindFrom = Convert.ToInt32(minWindFrom - (houndredsCountToPlane * averageWind));

                        if(averageWindFrom > 360)
                            averageWindFrom = averageWindFrom - 360;
                        
                    }
                    else
                    {
                        averageWindFrom = Convert.ToInt32((houndredsCountToPlane * averageWind) + minWindFrom);

                        if (averageWindFrom > 360)
                            averageWindFrom = averageWindFrom - 360;
                    }
                }
                else
                {
                    tmp = minWindFrom - maxWindFrom;

                    averageWind = tmp / hundredsCount;

                    if (minWindFrom <= maxWindFrom)
                    {
                        //int tmp = minWindFrom - maxWindFrom;

                        averageWindFrom = Convert.ToInt32((houndredsCountToPlane * averageWind) + minWindFrom);
                    }
                    else
                    {
                        //int tmp = minWindFrom - maxWindFrom;

                        averageWindFrom = Convert.ToInt32(minWindFrom - (houndredsCountToPlane * averageWind));
                    }
                }
            }
            catch
            {
                averageWindFrom = 0;
            }

            if(averageWindFrom == 360) averageWindFrom = 0;
        }

        static double DegreeToRadian(double degree)
        {
            return degree * Math.PI / 180;
        }

        static double RadianToDegree(double radian)
        {
            return radian * 180 / Math.PI;
        }

        public decimal Output()
        {
            return averageWindFrom;
        }
    }
}
