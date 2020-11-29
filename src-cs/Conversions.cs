using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorShims
{
    /// <summary>
    /// Conversions class handles easily converting numbers to useful things, like Inches > Centimeters, for example
    /// </summary>
    public static class Conversions
    {
        /// <summary>
        /// Handles extending a double and returns that double converted to centimeters
        /// </summary>
        /// <param name="inputNum"></param>
        /// <returns></returns>
        public static double ToCentimeters(this double inputNum)
        {
            return inputNum * 2.54;
        }

        // Int overload of the above
        public static double ToCentimeters(this int inputNum)
        {
            return inputNum * 2.54;
        }

        /// <summary>
        /// Handles extending a double and returns that double converted to inches
        /// </summary>
        /// <param name="inputNum"></param>
        /// <returns></returns>
        public static double ToInches(this double inputNum)
        {
            return inputNum / 2.54;
        }

        // Int overload of the above
        public static double ToInches(this int inputNum)
        {
            return inputNum / 2.54;
        }
    }
}
