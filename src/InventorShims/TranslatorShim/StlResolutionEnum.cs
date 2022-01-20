using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorShims.TranslatorShim
{
    /// <summary>Wrapper for the Resolution that is used by the STL export method.</summary>
    public enum StlResolutionEnum
    {
        /// <summary>
        /// High resolution for stl export.
        /// </summary>
        High = 0,

        /// <summary>
        /// Medium resolution for stl export.
        /// </summary>
        Medium = 1,

        /// <summary>
        /// Low resolution for stl export.
        /// </summary>
        Low = 2,

        /// <summary>
        /// Custom resolution for stl export.
        /// </summary>
        Custom = 3,

        /// <summary>
        /// Brep resolution for stl export.
        /// </summary>
        Brep = 4
    }
}
