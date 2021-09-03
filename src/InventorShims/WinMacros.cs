using System;
using System.Linq;
using System.Threading;

namespace InventorShims
{
    /// <summary>
    /// Windows helper methods, such as a sleep that accounts for doEvents so you can use it in a GUI and not have the user experience blocked.
    /// </summary>
    internal static class WinMacros
    {
        /// <summary>
        /// Pass int seconds to sleep a number of seconds.
        ///
        /// Do not use this method where accurate time is important. Accuracy will get worse with values of many seconds.
        /// </summary>
        /// <param name="seconds"></param>
        public static void WinSleep(int seconds)
        {
            WinMacros.WinSleepLong(seconds);
        }
        /// <summary>
        /// Returns a string of random A - Z 0 - 9 characters.
        /// </summary>
        /// <param name="lengthOfString"></param>
        /// <returns></returns>
        public static string GetRandomString(int lengthOfString = 10)
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string randChars = new string(System.Linq.Enumerable.Repeat(chars, lengthOfString).Select(s => s[random.Next(s.Length)]).ToArray());

            return randChars;
        }
        /// <summary>
        /// Pass a double to sleep for fractions of a second. (0.1 sleeps for around 1/10 second)
        ///
        /// Do not use this method where accurate time is important. Accuracy will get worse with values of many seconds.
        /// </summary>
        /// <param name="seconds"></param>
        public static void WinSleep(double seconds)
        {
            // Handle very very short
            if(seconds < 0.002)
            {
                WinMacros.WinSleepShort(2);
            }
            else if(seconds <= 1)
            {
                WinMacros.WinSleepShort((int)(seconds * 1000));
            }
            else
            {
                WinMacros.WinSleepLong((int)(seconds * 100));
            }
        }
        private static void WinSleepLong(int tenthsOfSeconds)
        {
            for(int i = 0; i < tenthsOfSeconds * 10; i++)
            {
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(100);
            }
        }
        private static void WinSleepShort(int milliSeconds)
        {
            for(int i = 0; i < milliSeconds; i++)
            {
                System.Windows.Forms.Application.DoEvents();
                Thread.Sleep(1);
            }
        }
    }
}
