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
        High = 0,

        Medium = 1,

        Low = 2,

        Custom = 3,

        Brep = 4
    }
}
