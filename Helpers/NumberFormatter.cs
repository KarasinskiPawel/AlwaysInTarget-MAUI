using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.Helpers
{
    public static class NumberFormatter
    {
        private static readonly NumberFormatInfo _nfi;

        static NumberFormatter()
        {
            _nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            _nfi.NumberGroupSeparator = " "; // spacja jako separator tysięcy
        }

        public static string FormatWithSpaces(int value)
        {
            return value.ToString("N0", _nfi);
        }

        public static string FormatWithSpaces(float value)
        {
            return value.ToString("N0", _nfi);
        }
    }
}
