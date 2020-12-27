using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorShims.TranslatorShim
{
    /// <summary>Wrapper for the ApplicationProtocolType that is used by the STEP export method.</summary>
    public enum StepProtocolEnum
    {
        /// <summary>Configuration Controlled Design</summary>
        AP203 = 2,

        /// <summary>Automotive Design</summary>
        AP214 = 3,

        /// <summary>Managed Model Based 3D Engineering</summary>
        /// <remarks>Includes PMI and meshes</remarks>
        AP242 = 5
    }
}