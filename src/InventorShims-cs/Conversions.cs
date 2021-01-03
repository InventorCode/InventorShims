namespace InventorShims
{
    /// <summary>
    /// Conversions class handles easily converting numbers to useful things, like Inches > Centimeters, for example
    /// </summary>
    public static class Conversions
    {
        #region InchesTo
        /// <summary>
        /// Converts a (double) number from inches to centimeters.
        /// </summary>
        /// <param name="inputNum"></param>
        /// <returns>double</returns>
        public static double InchesToCentimeters(this double inputNum)
        {
            return inputNum * 2.54;
        }

        /// <summary>
        /// Converts an (integer) number from inches to centimeters.
        /// </summary>
        /// <param name="inputNum"></param>
        /// <returns>double</returns>
        public static double InchesToCentimeters(this int inputNum)
        {
            return inputNum * 2.54;
        }

        /// <summary>
        /// Converts a (double) number from inches to millimeters.
        /// </summary>
        /// <param name="inputNum">double</param>
        /// <returns>double</returns>
        public static double InchesToMillimeters(this double inputNum)
        {
            return InchesToCentimeters(inputNum) * 10;
        }

        /// <summary>
        /// Converts an (integer) number from inches to millimeters.
        /// </summary>
        /// <param name="inputNum">integer</param>
        /// <returns>double</returns>
        public static double InchesToMillimeters(this int inputNum)
        {
            return InchesToCentimeters(inputNum) * 10;
        }
        #endregion

        #region CentimetersTo
        /// <summary>
        /// Converts a (double) number from centimeters to inches.
        /// </summary>
        /// <param name="inputNum">double</param>
        /// <returns>double</returns>
        public static double CentimetersToInches(this double inputNum)
        {
            return inputNum / 2.54;
        }

        /// <summary>
        /// Converts an (integer) number from centimeters to inches.
        /// </summary>
        /// <param name="inputNum">integer</param>
        /// <returns>double</returns>
        public static double CentimetersToInches(this int inputNum)
        {
            return inputNum / 2.54;
        }

        /// <summary>
        /// Converts a (double) number from centimeters to millimeters.
        /// </summary>
        /// <param name="inputNum">double</param>
        /// <returns>double</returns>
        public static double CentimetersToMillimeters(this double inputNum)
        {
            return inputNum * 10;
        }

        /// <summary>
        /// Converts an (integer) number from centimeters to millimeters.
        /// </summary>
        /// <param name="inputNum">integer</param>
        /// <returns>double</returns>
        public static double CentimetersToMillimeters(this int inputNum)
        {
            return inputNum * 10;
        }
        #endregion

        #region MillimetersTo

        /// <summary>
        /// Converts a (double) number from millimeters to inches.
        /// </summary>
        /// <param name="inputNum">double</param>
        /// <returns>double</returns>
        public static double MillimetersToInches(this double inputNum)
        {
            return inputNum * 2.54 * 10;
        }

        /// <summary>
        /// Converts an (integer) number from millimeters to inches.
        /// </summary>
        /// <param name="inputNum">integer</param>
        /// <returns>double</returns>
        public static double MillimetersToInches(this int inputNum)
        {
            return inputNum * 2.54 * 10;
        }

        /// <summary>
        /// Converts a (double) number from millimeters to centimeters.
        /// </summary>
        /// <param name="inputNum">double</param>
        /// <returns>double</returns>
        public static double MillimetersToCentimeters(this double inputNum)
        {
            return inputNum / 10;
        }

        /// <summary>
        /// Converts a (double) number from millimeters to centimeters.
        /// </summary>
        /// <param name="inputNum">integer</param>
        /// <returns>double</returns>
        public static double MillimetersToCentimeters(this int inputNum)
        {
            return inputNum / 10;
        }
        #endregion
    }
}
