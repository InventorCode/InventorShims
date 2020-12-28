using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace InventorShims.TranslatorShim
{
    /// <summary>Exports a drawing document to a PDF file</summary>
    public class PdfExporter
    {
        ///<summary>The drawing document to be exported</summary>
        public Inventor.Document Document { get; set; } = null;

        public bool AllColorsAsBlack { get; set; } = false;

        public bool RemoveLineWeights { get; set; } = false;

        /// <value>dots per inch</value>
        public int VectorResolution { get; set; } = 1200;

        /// <summary>determines which sheets to include in the exported file</summary>
        public PrintRangeEnum SheetRangeType { get; set; } = PrintRangeEnum.kPrintAllSheets;

        ///<summary>backing variable for <see cref="SheetRangeStart"/></summary>
        private int _sheetRangeStart = 1;

        /// <summary>First sheet in range to include in export</summary>
        /// <value>
        /// Modifying this value automatically changes <see cref="SheetRangeType"/> to <see cref="PrintRangeEnum.kPrintSheetRange"/>
        /// </value>
        public int SheetRangeStart
        {
            get { return _sheetRangeStart; }

            set
            {
                _sheetRangeStart = value;

                SheetRangeType = PrintRangeEnum.kPrintSheetRange;
            }
        }

        ///<summary>backing variable for <see cref="SheetRangeEnd"/></summary>
        private int _sheetRangeEnd = 1;

        /// <summary>Last sheet in range to include in export</summary>
        /// <value>
        /// Modifying this value automatically changes <see cref="SheetRangeType"/> to <see cref="PrintRangeEnum.kPrintSheetRange"/>
        /// </value>
        public int SheetRangeEnd
        {
            get { return _sheetRangeEnd; }

            set
            {
                _sheetRangeEnd = value;

                SheetRangeType = PrintRangeEnum.kPrintSheetRange;
            }
        }

        /// <summary>Opens the PDF file in its default viewer after it has been created.</summary>
        public bool OpenWhenDone { get; set; } = false;

        /// <remarks>This makes temporary edits to the file in memory, which may cause an unwanted Vault checkout prompt to appear if the drawing file is not checked out.</remarks>
        public bool PrintExcludedSheets { get; set; } = false;

        ///<summary>Initializes a new instance of <see cref="PdfExporter"/></summary>
        public PdfExporter(Inventor.DrawingDocument Document)
        {
            this.Document = (Document)Document;
        }

        ///<summary>Export drawing document to PDF file with the same folder and filename as the document.</summary>
        public void Export()
        {
            Export(System.IO.Path.ChangeExtension(this.Document.FullFileName, "pdf"));
        }

        ///<summary>Export drawing document to PDF file with the specified full file path.</summary>
        public void Export(string OutputFile)
        {
            TranslatorData oTranslatorData = new TranslatorData(addinGUID: "{0AC6FD96-2F4D-42CE-8BE0-8AEA580399E4}", fullFileName: OutputFile, doc: this.Document);

            NameValueMap op = oTranslatorData.oOptions;

            op.Value["All_Color_AS_Black"] = Convert.ToInt32(AllColorsAsBlack);
            op.Value["Remove_Line_Weights"] = Convert.ToInt32(RemoveLineWeights);
            op.Value["Vector_Resolution"] = Convert.ToInt32(VectorResolution);
            op.Value["Sheet_Range"] = SheetRangeType;
            op.Value["Custom_Begin_Sheet"] = SheetRangeStart;
            op.Value["Custom_End_Sheet"] = SheetRangeEnd;
            //op.Value["Launch_Viewer"] = Convert.ToInt32(OpenWhenDone);  //Does not work.  Workaround is at the bottom of this function.

            Inventor.Application app = (Inventor.Application)this.Document.Parent;

            Transaction tempTransaction = null;

            //This is wrapped in a try/catch block to ensure the transaction gets aborted
            try
            {
                if (PrintExcludedSheets)
                {
                    DrawingDocument dwgDoc = (DrawingDocument)this.Document;

                    List<Sheet> excludedSheets = dwgDoc.Sheets.OfType<Sheet>().Where(x => x.ExcludeFromPrinting).ToList();

                    if (excludedSheets.Count > 0)
                    {
                        tempTransaction = app.TransactionManager.StartTransaction((Inventor._Document)this.Document, "Temporary Transaction");

                        excludedSheets.ForEach(x => x.ExcludeFromPrinting = false);
                    }
                }

                //Create output directory if it does not exist
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(OutputFile));

                TranslatorAddIn oTranslatorAddIn = (TranslatorAddIn)oTranslatorData.oAppAddIn;

                oTranslatorAddIn.SaveCopyAs(this.Document, oTranslatorData.oContext, oTranslatorData.oOptions, oTranslatorData.oDataMedium);

                if (tempTransaction != null) { tempTransaction.Abort(); }
            }
            catch
            {
                if (tempTransaction != null) { tempTransaction.Abort(); }

                throw;
            }

            //Workaround for the PDF exporter's "Launch_Viewer" option, which does not work.
            if (OpenWhenDone)
            {
                if(System.IO.File.Exists(OutputFile))
                {
                    //Open pdf file in its default application
                    System.Diagnostics.Process.Start(OutputFile);
                }
            }
        }
    }
}
