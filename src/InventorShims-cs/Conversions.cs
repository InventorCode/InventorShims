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
        #region InchesTo
        /// <summary>
        /// Handles extending an inches double and returns that double converted to centimeters
        /// </summary>
        /// <param name="inputNum"></param>
        /// <returns></returns>
        public static double InchesToCentimeters(this double inputNum)
        {
            return inputNum * 2.54;
        }

        // Int overload of the above
        public static double InchesToCentimeters(this int inputNum)
        {
            return inputNum * 2.54;
        }

        /// <summary>
        /// Handles extending an inches double and returns that double converted to millimeters
        /// </summary>
        /// <param name="inputNum"></param>
        /// <returns></returns>
        public static double InchesToMillimeters(this double inputNum)
        {
            return InchesToCentimeters(inputNum) * 10;
        }

        // Int overload of the above
        public static double InchesToMillimeters(this int inputNum)
        {
            return InchesToCentimeters(inputNum) * 10;
        }
        #endregion

        #region CentimetersTo
        /// <summary>
        /// Handles extending a double and returns that double converted to inches
        /// </summary>
        /// <param name="inputNum"></param>
        /// <returns></returns>
        public static double CentimetersToInches(this double inputNum)
        {
            return inputNum / 2.54;
        }

        // Int overload of the above
        public static double CentimetersToInches(this int inputNum)
        {
            return inputNum / 2.54;
        }

        /// <summary>
        /// Handles extending a double and returns that double converted to millimeters
        /// </summary>
        /// <param name="inputNum"></param>
        /// <returns></returns>
        public static double CentimetersToMillimeters(this double inputNum)
        {
            return inputNum * 10;
        }

        // Int overload of the above
        public static double CentimetersToMillimeters(this int inputNum)
        {
            return inputNum * 10;
        }
        #endregion

        #region MillimetersTo

        /// <summary>
        /// Handles extending a double and returns that double converted to millimeters
        /// </summary>
        /// <param name="inputNum"></param>
        /// <returns></returns>
        public static double MillimetersToInches(this double inputNum)
        {
            return inputNum * 2.54 * 10;
        }

        // Int overload of the above
        public static double MillimetersToInches(this int inputNum)
        {
            return inputNum * 2.54 * 10;
        }

        /// <summary>
        /// Handles extending a double and returns that double converted to millimeters
        /// </summary>
        /// <param name="inputNum"></param>
        /// <returns></returns>
        public static double MillimetersToCentimeters(this double inputNum)
        {
            return inputNum / 10;
        }

        // Int overload of the above
        public static double MillimetersToCentimeters(this int inputNum)
        {
            return inputNum / 10;
        }
        #endregion
    }
}
