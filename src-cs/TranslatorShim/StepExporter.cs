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
        private Inventor.Document _document;

        ///<summary>The document to be exported</summary>
        public Inventor.Document Document
        {
            get {return _document;}

            set {
                _document = value;

                Description = (string)_document.PropertySets["Design Tracking Properties"]["Description"].Value;

                Organization = (string)_document.PropertySets["Inventor Document Summary Information"]["Company"].Value;
            }
        }

        /// <remarks>Does not include 2d or 3d sketch points.</remarks>
        public bool IncludeSketches { get; set; } = false;

        ///<summary>The type of the STEP file to be created: AP203 / AP214 / AP242</summary>
        ///<value>Defaults to AP214 for compatibility with other software.</value>
        public StepProtocolEnum ApplicationProtocol { get; set; } = StepProtocolEnum.AP214;

        /// <value>Defaults to the current username that is defined in Inventor's Application Options</value>
        public string Author { get; set; } = "";

        /// <value>Defaults to the document's Company iProperty</value>
        public string Organization { get; set; } = "";

        /// <value>Not set by default.</value>
        /// <remarks>from ISO 10303-21: The name/mailing address of the person who authorized this file.</remarks>
        public string Authorization { get; set; } = "";

        /// <value>Defaults to the document's Description iProperty</value>
        public string Description { get; set; } = "";

        ///<summary>How far curves can deviate from their true value (also known as ExportFitTolerance)</summary>
        ///<value>Accepted range of 0.00001 to 0.001 (centimeters)</value>
        public float SplineFitAccuracy { get; set; } = .001f;

        ///<summary>Initializes a new instance of <see cref="StepExporter"/>r</summary>
        public StepExporter(Inventor.Document Document)
        {
            this.Document = Document;

            Application app = (Application)Document.Parent;

            Author = app.UserName;
        }

        ///<summary>Export to STP file with the same folder and filename as the document.</summary>
        public void Export()
        {
            Export(System.IO.Path.ChangeExtension(_document.FullFileName, "stp"));
        }

        ///<summary>Export to STP file with the specified full file path.</summary>
        public void Export(string OutputFile)
        {
            TranslatorData oTranslatorData = new TranslatorData(addinGUID: "{90AF7F40-0C01-11D5-8E83-0010B541CD80}", fullFileName: OutputFile, doc: _document);

            NameValueMap op = oTranslatorData.oOptions;

            op.Value["IncludeSketches"] = IncludeSketches;
            op.Value["ApplicationProtocolType"] = ApplicationProtocol;
            op.Value["Author"] = Author;
            op.Value["Organization"] = Organization;
            op.Value["Authorization"] = Authorization;
            op.Value["Description"] = Description;
            op.Value["export_fit_tolerance"] = SplineFitAccuracy;

            TranslatorAddIn oTranslatorAddIn = (TranslatorAddIn)oTranslatorData.oAppAddIn;

            oTranslatorAddIn.SaveCopyAs(this.Document, oTranslatorData.oContext, oTranslatorData.oOptions, oTranslatorData.oDataMedium);
        }
    }
}
