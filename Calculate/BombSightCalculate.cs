using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysInTarget.Calculate
{
    internal class BombSightCalculate
    {
        private readonly int _course;
        private readonly int _windDirection;

        private string result = default;

        public BombSightCalculate(int course, int windDirection)
        {
            _course = course;
            _windDirection = windDirection;

            Execute();
        }

        private void Execute()
        {
            try
            {
                int deflection = 0;

                switch(new WindRose(_course, _windDirection).Output())
                {
                    case 1:
                        deflection = _windDirection - _course;

                        if (deflection < 0)
                            deflection = 360 + deflection;

                        result = $"Strb: {deflection}";
                        break;
                    case 2:
                        deflection = _windDirection - _course;

                        if(deflection < -180)
                        {
                            deflection = 360 + deflection;
                            result = $"Strb: {deflection}";
                        }
                        else
                        {
                            result = $"Strb: {deflection}";
                        }
                        
                        break;
                    case 3:
                        deflection = (_course - _windDirection);

                        if(deflection == -180 || deflection == 180)
                        {
                            if (deflection < 0)
                                deflection *= -1;

                            result = $"{deflection}";
                        }
                        else if(deflection < -180)
                        {
                            deflection = 360 + deflection;
                            result = $"Port: {deflection}";
                        }
                        else
                        {
                            result = $"Port: {deflection}";
                        }

                        break;
                    case 4:
                        deflection = (_course - _windDirection);

                        if (deflection <= -270)
                            deflection = 360 + deflection;

                        if(deflection == 0)
                        {
                            result = $"{deflection}";
                        }
                        else
                        {
                            result = $"Port: {deflection}";
                        }

                        break;
                    default:
                        result = "0";
                        break;
                }
            }
            catch
            {
                result = "Error";
            }

            //port - z lewej od dziobu.
            //starboard - z prawej od dziobu.
        }

        public string Output()
        {
            return result;
        }
    }
}
