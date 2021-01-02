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

                Description = (string)_document.GetPropertyValue("Description");

                Organization = (string)_document.GetPropertyValue("Company");
            }
        }

        /// <summary>Include 2d and 3d sketch lines and curves in the export.  Does not include 2d or 3d sketch points.</summary>
        public bool IncludeSketches { get; set; } = false;

        ///<summary>
        ///The type of STEP file to be created: AP203 / AP214 / AP242. <br/>
        ///Defaults to AP214 for compatibility with other software.
        ///</summary>
        public StepProtocolEnum ApplicationProtocol { get; set; } = StepProtocolEnum.AP214;

        /// <summary>Defaults to the current username that is defined in Inventor's Application Options</summary>
        public string Author { get; set; } = "";

        /// <summary>Defaults to the document's Company iProperty</summary>
        public string Organization { get; set; } = "";

        /// <summary>Not set by default. From ISO 10303-21: The name/mailing address of the person who authorized this file.</summary>
        public string Authorization { get; set; } = "";

        /// <summary>Defaults to the document's Description iProperty</summary>
        public string Description { get; set; } = "";

        ///<summary>How far curves can deviate from their true value (also known as ExportFitTolerance). <br/>
        ///Accepted range of 0.00001 to 0.001 (centimeters)
        ///</summary>
        public float SplineFitAccuracy { get; set; } = .001f;

        ///<summary>Initializes a new instance of <see cref="StepExporter"/></summary>
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
