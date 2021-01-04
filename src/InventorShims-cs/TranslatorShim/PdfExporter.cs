using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace InventorShims.TranslatorShim
{
    ///<summary>Exports a drawing document to a single PDF file</summary>
    public class PdfExporter
    {
        ///<summary>The drawing document to be exported</summary>
        public Inventor.Document Document { get; set; } = null;

        ///<summary>Returns the document's fullfilename with the extension replaced by pdf</summary>
        private string FullFileNameAsPdf
        {
            get
            {
                return System.IO.Path.ChangeExtension(this.Document.FullFileName, "pdf");
            }
        }

        ///<summary>Exports in monochrome</summary>
        public bool AllColorsAsBlack { get; set; } = false;

        ///<summary>Exports all lines to have the thinnest lineweight possible</summary>
        public bool RemoveLineWeights { get; set; } = false;

        ///<summary>Determines how closely curved edges are approximated by straight line segments (in dots per inch)</summary>
        public int VectorResolution { get; set; } = 1200;

        ///<summary>Opens the PDF file in its default viewer after it has been created</summary>
        public bool OpenWhenDone { get; set; } = false;

        ///<summary>
        ///Includes sheets in the exported file, even if they have the "Exclude from printing" option checked.<code/>
        ///This export option applies to any of the <see cref="ExportSheets()"/> methods, but not to the <see cref="ExportSheet()"/> methods.<code/>
        ///This makes temporary edits to the file in memory, which may cause an unwanted Vault checkout prompt to appear if the drawing file is not checked out.
        ///</summary>
        public bool PrintExcludedSheets { get; set; } = false;

        ///<summary>Initializes a new instance of <see cref="PdfExporter"/></summary>
        public PdfExporter(Inventor.DrawingDocument Document)
        {
            this.Document = (Document)Document;
        }

        ///<summary>Export current sheet to a single PDF file with the same folder and filename as the document.</summary>
        public void ExportSheet()
        {
            Export
            (
                OutputFile: FullFileNameAsPdf,
                SheetRangeType: PrintRangeEnum.kPrintCurrentSheet
            );
        }

        ///<summary>Export current sheet to a single PDF file with the specified full file path.</summary>
        public void ExportSheet(string OutputFile)
        {
            Export
            (
                OutputFile: OutputFile,
                SheetRangeType: PrintRangeEnum.kPrintCurrentSheet
            );
        }

        ///<summary>Export specified sheet to a single PDF file with the same folder and filename as the document.</summary>
        public void ExportSheet(int SheetNumber)
        {
            Export
            (
                OutputFile: FullFileNameAsPdf,
                SheetRangeType: PrintRangeEnum.kPrintSheetRange,
                StartSheet: SheetNumber,
                EndSheet: SheetNumber
            );
        }

        ///<summary>Export specified sheet to a single PDF file with the specified full file path.</summary>
        public void ExportSheet(int SheetNumber, string OutputFile)
        {
            Export
            (
                OutputFile: OutputFile,
                SheetRangeType: PrintRangeEnum.kPrintSheetRange,
                StartSheet: SheetNumber,
                EndSheet: SheetNumber
            );
        }

        ///<summary>Export all sheets in document to a single PDF file with the same folder and filename as the document.</summary>
        public void ExportSheets()
        {
            Export
            (
                OutputFile: FullFileNameAsPdf,
                SheetRangeType: PrintRangeEnum.kPrintAllSheets
            );
        }

        ///<summary>Export all sheets in document to a single PDF file with the specified full file path.</summary>
        public void ExportSheets(string OutputFile)
        {
            Export
            (
                OutputFile: OutputFile,
                SheetRangeType: PrintRangeEnum.kPrintAllSheets
            );
        }

        ///<summary>Export sheet range to a single PDF file with the same folder and filename as the document.</summary>
        public void ExportSheets(int StartSheet, int EndSheet)
        {
            Export
            (
                OutputFile: FullFileNameAsPdf,
                SheetRangeType: PrintRangeEnum.kPrintSheetRange,
                StartSheet: StartSheet, 
                EndSheet: EndSheet
            );
        }

        ///<summary>Export sheet range to a single PDF file with the specified full file path.</summary>
        public void ExportSheets(int StartSheet, int EndSheet, string OutputFile)
        {
            Export
            (
                OutputFile: OutputFile,
                SheetRangeType: PrintRangeEnum.kPrintSheetRange,
                StartSheet: StartSheet,
                EndSheet: EndSheet
            );
        }

        ///<summary>Export all sheets, beginning with <paramref name="StartSheet"/>, to a single PDF file with the same folder and filename as the document.</summary>
        public void ExportSheets(int StartSheet)
        {
            DrawingDocument dwgDoc = (DrawingDocument)this.Document;

            Export
            (
                OutputFile: FullFileNameAsPdf,
                SheetRangeType: PrintRangeEnum.kPrintSheetRange,
                StartSheet: StartSheet,
                EndSheet: dwgDoc.Sheets.Count
            );
        }

        ///<summary>Export all sheets, beginning with <paramref name="StartSheet"/>, to a single PDF file with the specified full file path.</summary>
        public void ExportSheets(int StartSheet, string OutputFile)
        {
            DrawingDocument dwgDoc = (DrawingDocument)this.Document;

            Export
            (
                OutputFile: OutputFile,
                SheetRangeType: PrintRangeEnum.kPrintSheetRange,
                StartSheet: StartSheet,
                EndSheet: dwgDoc.Sheets.Count
            );
        }

        ///<summary>Export specific sheets to a single PDF file with the same folder and filename as the document.<code/>
        ///The option <see cref="PrintExcludedSheets"/> still applies when using this method.<code/>
        ///This makes temporary edits to the file in memory, which may cause an unwanted Vault checkout prompt to appear if the drawing file is not checked out.
        ///</summary>
        public void ExportSheets(IEnumerable<Sheet> Sheets)
        {
            Export
            (
                OutputFile: FullFileNameAsPdf,
                SheetRangeType: PrintRangeEnum.kPrintAllSheets,
                IncludedSheets: Sheets
            );
        }

        ///<summary>Export specific sheets to a single PDF file with the specified full file path.<code/>
        ///The option <see cref="PrintExcludedSheets"/> still applies when using this method.<code/>
        ///This makes temporary edits to the file in memory, which may cause an unwanted Vault checkout prompt to appear if the drawing file is not checked out.
        ///</summary>
        public void ExportSheets(IEnumerable<Sheet> Sheets, string OutputFile)
        {
            Export
            (
                OutputFile: OutputFile,
                SheetRangeType: PrintRangeEnum.kPrintAllSheets,
                IncludedSheets: Sheets
            );
        }

        ///<summary>Export specific sheet numbers to a single PDF file with the same folder and filename as the document.<code/>
        ///The option <see cref="PrintExcludedSheets"/> still applies when using this method.<code/>
        ///This makes temporary edits to the file in memory, which may cause an unwanted Vault checkout prompt to appear if the drawing file is not checked out.
        ///</summary>
        public void ExportSheets(IEnumerable<int> SheetNumbers)
        {
            Export
            (
                OutputFile: FullFileNameAsPdf,
                SheetRangeType: PrintRangeEnum.kPrintAllSheets,
                IncludedSheetNums: SheetNumbers
            );
        }

        ///<summary>Export specific sheet numbers to a single PDF file with the specified full file path.<code/>
        ///The option <see cref="PrintExcludedSheets"/> still applies when using this method.<code/>
        ///This makes temporary edits to the file in memory, which may cause an unwanted Vault checkout prompt to appear if the drawing file is not checked out.
        ///</summary>
        public void ExportSheets(IEnumerable<int> SheetNumbers, string OutputFile)
        {
            Export
            (
                OutputFile: OutputFile,
                SheetRangeType: PrintRangeEnum.kPrintAllSheets,
                IncludedSheetNums: SheetNumbers
            );
        }

        ///<summary>Export to a single PDF file</summary>
        private void Export(
            string OutputFile,
            PrintRangeEnum SheetRangeType, 
            int StartSheet = 0, 
            int EndSheet = 0, 
            IEnumerable<Sheet> IncludedSheets = null, 
            IEnumerable<int> IncludedSheetNums = null
            )
        {
            TranslatorData oTranslatorData = new TranslatorData(addinGUID: "{0AC6FD96-2F4D-42CE-8BE0-8AEA580399E4}", fullFileName: OutputFile, doc: this.Document);

            NameValueMap op = oTranslatorData.oOptions;

            op.Value["All_Color_AS_Black"] = Convert.ToInt32(AllColorsAsBlack);
            op.Value["Remove_Line_Weights"] = Convert.ToInt32(RemoveLineWeights);
            op.Value["Vector_Resolution"] = Convert.ToInt32(VectorResolution);
            op.Value["Sheet_Range"] = SheetRangeType;
            if (StartSheet != 0) { op.Value["Custom_Begin_Sheet"] = StartSheet; }
            if (EndSheet != 0) { op.Value["Custom_End_Sheet"] = EndSheet; }
            //op.Value["Launch_Viewer"] = Convert.ToInt32(OpenWhenDone);  //Does not work.  Workaround is at the bottom of this function.

            Transaction tempTransaction = null;

            //This is wrapped in a try/catch block to ensure the transaction gets aborted
            try
            {
                Inventor.Application app = (Inventor.Application)this.Document.Parent;

                DrawingDocument dwgDoc = (DrawingDocument)this.Document;

                tempTransaction = app.TransactionManager.StartTransaction((Inventor._Document)this.Document, "Temporary Transaction");

                if (PrintExcludedSheets)
                {
                    //Temporarily include all sheets.
                    List<Sheet> excludedSheets = dwgDoc.Sheets.OfType<Sheet>().Where(x => x.ExcludeFromPrinting).ToList();

                    if (excludedSheets.Count > 0)
                    {
                        excludedSheets.ForEach(x => x.ExcludeFromPrinting = false);
                    }
                }

                //Temporarily exclude sheets that were not passed in to this function.
                if (IncludedSheets != null)
                {
                    List<Sheet> temporarilyExcludedSheets = dwgDoc.Sheets.OfType<Sheet>().Except(IncludedSheets).ToList();

                    temporarilyExcludedSheets.ForEach(x => x.ExcludeFromPrinting = true);
                }

                //Temporarily exclude sheet numbers that were not passed in to this function.
                if (IncludedSheetNums != null)
                {
                    List<Sheet> includedSheets = new List<Sheet>();

                    for(int i = 1; i <= dwgDoc.Sheets.Count; i++)
                    {
                        if (IncludedSheetNums.Contains(i))
                        {
                            includedSheets.Add(dwgDoc.Sheets[i]);
                        }
                    }

                    List<Sheet> temporarilyExcludedSheets = dwgDoc.Sheets.OfType<Sheet>().Except(includedSheets).ToList();

                    temporarilyExcludedSheets.ForEach(x => x.ExcludeFromPrinting = true);
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
            if (OpenWhenDone && System.IO.File.Exists(OutputFile))
            {
                //Open pdf file in its default application
                System.Diagnostics.Process.Start(OutputFile);
            }
        }
    }
}
