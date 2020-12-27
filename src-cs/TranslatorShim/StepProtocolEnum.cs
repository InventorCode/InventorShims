using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorShims.TranslatorShim
{
    /// <summary>
    /// Wrapper for the ApplicationProtocolType that is used by the STEP export method.
    /// </summary>
    public enum StepProtocolEnum
    {
        Step203 = 2, //Configuration Controlled Design
        Step214 = 3, //Automotive Design
        Step242 = 5  //Managed Model Based 3D Engineering (Includes 3D Annotations)
    }
}