using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace InventorShims.TranslatorShim
{
    /// <summary>Exports a document to a DWG file</summary>
    public class DwgExporter
    {
        ///<summary>The document to be exported</summary>
        public Inventor.Document Document { get; set; } = null;

        /// <summary>ini file specifying the export configuration</summary>
        public string ConfigurationFile { get; set; } = "";

        ///<summary>Initializes a new instance of <see cref="DwgExporter"/> with a custom export configuration file.</summary>
        public DwgExporter(Inventor.Document Document, string ConfigurationFile)
        {
            this.Document = Document;

            this.ConfigurationFile = ConfigurationFile;
        }

        ///<summary>Initializes a new instance of <see cref="DwgExporter"/> with the default export configuration.</summary>
        public DwgExporter(Inventor.Document Document)
        {
            this.Document = Document;
        }

        ///<summary>Export to DWG file with the same folder and filename as the document.</summary>
        public void Export()
        {
            Export(System.IO.Path.ChangeExtension(this.Document.FullFileName, "dwg"));
        }

        ///<summary>Export to DWG file with the specified full file path.</summary>
        public void Export(string OutputFile)
        {
            TranslatorData oTranslatorData = new TranslatorData(addinGUID: "{C24E3AC2-122E-11D5-8E91-0010B541CD80}", fullFileName: OutputFile, doc: this.Document);

            oTranslatorData.oOptions.Value["Export_Acad_IniFile"] = ConfigurationFile;

            TranslatorAddIn oTranslatorAddIn = (TranslatorAddIn)oTranslatorData.oAppAddIn;

            oTranslatorAddIn.SaveCopyAs(this.Document, oTranslatorData.oContext, oTranslatorData.oOptions, oTranslatorData.oDataMedium);
        }
    }
}
