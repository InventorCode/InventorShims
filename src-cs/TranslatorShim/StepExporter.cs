using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace InventorShims.TranslatorShim
{
    /// <summary>Exports a document to a STP file</summary>
    public class StepExporter
    {
        private TranslatorData oTranslatorData = null;

        ///<value>The document to be exported</value>
        public Document Document { get; set; } = null;
        public bool IncludeSketches { get; set; } = false;
        public StepProtocolEnum ApplicationProtocol { get; set; } = StepProtocolEnum.Step214;
        public string Author { get; set; } = "";
        public string Organization { get; set; } = "";
        public string Authorization { get; set; } = "";
        public string Description { get; set; } = "";

        ///<summary>How far curves can deviate from their true value (also known as Spline Fit Accuracy)</summary>
        ///<remarks>This is probably in centimeters.</remarks>
        public float ExportFitTolerance { get; set; } = .001f;
        
        public StepExporter(Document Document)
        {
            this.Document = Document;
        }

        ///<summary>Export to STP file with the same folder and filename as the document.</summary>
        public void Export()
        {
            Export(System.IO.Path.ChangeExtension(this.Document.FullFileName, "stp"));
        }

        ///<summary>Export to STP file with the specified full file path.</summary>
        public void Export(string OutputFile)
        {
            oTranslatorData = new TranslatorData(addinGUID: "{90AF7F40-0C01-11D5-8E83-0010B541CD80}", fullFileName: OutputFile, doc: Document);

            NameValueMap op = oTranslatorData.oOptions;

            op.Value["IncludeSketches"] = IncludeSketches;
            op.Value["ApplicationProtocolType"] = ApplicationProtocol;
            op.Value["Author"] = Author;
            op.Value["Organization"] = Organization;
            op.Value["Authorization"] = Authorization;
            op.Value["Description"] = Description;
            op.Value["export_fit_tolerance"] = ExportFitTolerance;

            TranslatorAddIn oTranslatorAddIn = (TranslatorAddIn)oTranslatorData.oAppAddIn;

            oTranslatorAddIn.SaveCopyAs(this.Document, oTranslatorData.oContext, oTranslatorData.oOptions, oTranslatorData.oDataMedium);
        }
    }
}
