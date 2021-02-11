using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace InventorShims.TranslatorShim
{
    /// <summary>Stores temporary data for import and export methods.</summary>
    internal class TranslatorData
    {
        internal readonly ApplicationAddIn oAppAddIn;
        internal readonly TranslationContext oContext;
        internal readonly NameValueMap oOptions;
        internal readonly DataMedium oDataMedium;

        ///<summary>Initializes a new instance of <see cref="TranslatorData"/></summary>
        internal TranslatorData(string addinGUID, string fullFileName, Document doc = null, Inventor.Application app = null)
        {
            if (app == null) { app = (Application)doc.Parent; }

            oAppAddIn = app.ApplicationAddIns.ItemById[addinGUID];

            TransientObjects oTo = app.TransientObjects;

            if (oAppAddIn is TranslatorAddIn)
            {
                oContext = oTo.CreateTranslationContext();
                oContext.Type = IOMechanismEnum.kFileBrowseIOMechanism;

                oDataMedium = oTo.CreateDataMedium();

                oDataMedium.FileName = fullFileName;
            }
            oOptions = oTo.CreateNameValueMap();
        }
    }
}