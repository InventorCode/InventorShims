using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorShims
{
    /// <summary>
    /// String formatting methods
    /// </summary>
    public static class StringFormatters
    {
        /// <summary>
        /// Takes a double of inches and returns the string of a fraction equivalent.
        /// 
        /// If you want to round to 1/8" of an inch, pass 8 as roundingAccuracy
        /// 
        /// Supports up to 1/32 rounding accuracy, which would be passed in as 32
        /// </summary>
        /// <param name="inputNum"></param>
        /// <param name="roundingAccuracy"></param>
        /// <returns></returns>
        public static string ToFractionStringRoundedUp(this double inputNum, int roundingAccuracy = 8)
        {
            var decimalRepresentationOfInchFraction = 1 / roundingAccuracy;

            // check if it's evenly divisible to the passed fractional
            if(inputNum % decimalRepresentationOfInchFraction != 0)
            {
                // Round up to next 1/8 if it's not already an even 1/8
                inputNum = Math.Ceiling(inputNum / decimalRepresentationOfInchFraction) * decimalRepresentationOfInchFraction;
            }

            string inputAsString = inputNum.ToString();
            string fractionString = "";

            // Figure out what the fraction should be
            if(inputAsString.Contains("."))
            {
                // if we're not an integer
                if(inputAsString.EndsWith(".03125")) { fractionString = " 1/32"; }
                else if(inputAsString.EndsWith(".0625")) { fractionString = " 1/16"; }
                else if(inputAsString.EndsWith(".09375")) { fractionString = " 3/32"; }
                else if(inputAsString.EndsWith(".125")) { fractionString = " 1/8"; }
                else if(inputAsString.EndsWith(".15625")) { fractionString = " 5/32"; }
                else if(inputAsString.EndsWith(".1875")) { fractionString = " 3/16"; }
                else if(inputAsString.EndsWith(".21875")) { fractionString = " 7/32"; }
                else if(inputAsString.EndsWith(".25")) { fractionString = " 1/4"; }
                else if(inputAsString.EndsWith(".28125")) { fractionString = " 9/32"; }
                else if(inputAsString.EndsWith(".3125")) { fractionString = " 5/16"; }
                else if(inputAsString.EndsWith(".34375")) { fractionString = " 11/32"; }
                else if(inputAsString.EndsWith(".375")) { fractionString = " 3/8"; }
                else if(inputAsString.EndsWith(".40625")) { fractionString = " 13/32"; }
                else if(inputAsString.EndsWith(".4375")) { fractionString = " 7/16"; }
                else if(inputAsString.EndsWith(".46875")) { fractionString = " 15/32"; }
                else if(inputAsString.EndsWith(".5")) { fractionString = " 1/2"; }
                else if(inputAsString.EndsWith(".53125")) { fractionString = " 17/32"; }
                else if(inputAsString.EndsWith(".5625")) { fractionString = " 9/16"; }
                else if(inputAsString.EndsWith(".59375")) { fractionString = " 19/32"; }
                else if(inputAsString.EndsWith(".625")) { fractionString = " 5/8"; }
                else if(inputAsString.EndsWith(".65625")) { fractionString = " 21/32"; }
                else if(inputAsString.EndsWith(".6875")) { fractionString = " 11/16"; }
                else if(inputAsString.EndsWith(".71875")) { fractionString = " 23/32"; }
                else if(inputAsString.EndsWith(".75")) { fractionString = " 3/4"; }
                else if(inputAsString.EndsWith(".78125")) { fractionString = " 25/32"; }
                else if(inputAsString.EndsWith(".8125")) { fractionString = " 13/16"; }
                else if(inputAsString.EndsWith(".84375")) { fractionString = " 27/32"; }
                else if(inputAsString.EndsWith(".875")) { fractionString = " 7/8"; }
                else if(inputAsString.EndsWith(".90625")) { fractionString = " 29/32"; }
                else if(inputAsString.EndsWith(".9375")) { fractionString = " 15/16"; }
                else if(inputAsString.EndsWith(".96875")) { fractionString = " 31/32"; }
                else fractionString = inputAsString;
            }

            return ((int)inputNum).ToString() + fractionString;
        }
    }
}
