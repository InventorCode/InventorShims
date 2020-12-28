using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorShims.TranslatorShim
{
    /// <summary>Wrapper for the OutputFileType that is used by the STL export method.</summary>
    public enum StlFormatEnum
    {
        ///<summary>supports colors</summary>
        binary = 0,

        ///<summary>does not support colors</summary>
        ascii = 1
    }
}
