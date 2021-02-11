using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace InventorShims
{
    /// <summary>
    /// Extension methods that work on an inventor application com object reference
    /// </summary>
    public static class UiShim
    {
        /// <summary>
        /// Gets the active color scheme name as a string
        /// </summary>
        /// <param name="inventorAppToWork"></param>
        /// <returns></returns>
        public static string GetActiveColorSchemeName(this Application inventorAppToWork)
        {
            return inventorAppToWork.ActiveColorScheme.Name;
        }
        /// <summary>
        /// Sets the active color scheme name, passed in as a string
        /// </summary>
        /// <param name="inventorAppToWork"></param>
        /// <returns></returns> 
        public static void SetActiveColorScheme(this Application inventorAppToWork, string colorSchemeNameToActivate)
        {
            inventorAppToWork.ColorSchemes[colorSchemeNameToActivate].Activate();
        }
        /// <summary>
        /// Gets the background type enum in a more memorable way
        /// </summary>
        /// <param name="inventorAppToWork"></param>
        /// <returns></returns>        
        public static BackgroundTypeEnum GetActiveColorSchemeBackground(this Application inventorAppToWork)
        {
            return inventorAppToWork.ColorSchemes.BackgroundType;
        }
    }
}
