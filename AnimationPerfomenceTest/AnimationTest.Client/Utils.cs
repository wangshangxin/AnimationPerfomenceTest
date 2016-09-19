using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnimationTest.Client
{
    public static class Utils
    {
        public static bool IsNumber(string strNumber)
        {
            if (string.IsNullOrEmpty(strNumber))
                return false;
            else
            {
                Regex regex = new Regex("[^0-9]");
                return !regex.IsMatch(strNumber);
            }
            return false;
        }

        public static Int32 ToInt(this string value)
        {
            Int32 result;
            if (Int32.TryParse(value, out result))
            {
                return result;
            }
            throw new InvalidCastException("Cannot convert "+ value + " to Int.");
        }

        public static double ToDouble(this string value)
        {
            double result;
            if (double.TryParse(value, out result))
            {
                return result;
            }
            throw new InvalidCastException("Cannot convert " + value + " to Double.");
        }
    }
}