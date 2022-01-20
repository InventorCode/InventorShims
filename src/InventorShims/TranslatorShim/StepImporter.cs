using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace InventorShims.TranslatorShim
{
    /// <summary>Imports a STEP file</summary>
    public class StepImporter
    {
        private readonly Inventor.Application _application = null;

        /// <summary>
        /// The full file path of the STEP file to be imported.<code/>
        /// Defaults to the location of the STEP file if left blank.
        /// </summary>
        public string Filename { get; set; } = "";

        /// <summary>
        /// The folder where imported files will be saved.<code/>
        /// This just sets the location that will be used when the files are eventually saved.<code/>
        /// <see cref="SaveDuringLoad"/> actually saves the files automatically.
        /// </summary>
        public string SaveLocation { get; set; } = "";

        /// <summary>Automatically saves the imported components into <see cref="SaveLocation"/>.</summary>
        public bool SaveDuringLoad { get; set; } = false;

        /// <summary>
        /// Added to filenames of all components in imported assembly.<code/>
        /// <see cref="FilenamePrefix"/> and <see cref="FilenameSuffix"/> cannot both be used.<code/>
        /// If both variables are set, <see cref="FilenamePrefix"/> will take precedence.
        /// </summary>
        public string FilenamePrefix { get; set; } = "";

        /// <summary>Added to filenames of all components in imported assembly.<code/>
        /// <see cref="FilenamePrefix"/> and <see cref="FilenameSuffix"/> cannot both be used.<code/>
        /// If both variables are set, <see cref="FilenamePrefix"/> will take precedence.
        /// </summary>
        public string FilenameSuffix { get; set; } = "";

        /// <summary>Stores a translation report as a 3rd-party file inside the imported file.</summary>
        public bool EmbedTranslationReport { get; set; } = true;

        /// <summary>Saves a translation report to <see cref="SaveLocation"/>.</summary>
        public bool SaveTranslationReport { get; set; } = false;

        /// <summary>Includes solid bodies in import.</summary>
        public bool ImportSolids { get; set; } = true;

        /// <summary>Includes surface bodies in import.</summary>
        public bool ImportSurfaces { get; set; } = true;

        /// <summary>Includes wires in import.</summary>
        public bool ImportWires { get; set; } = true;

        /// <summary>Includes points in import.</summary>
        public bool ImportPoints { get; set; } = true;

        /// <summary>Includes meshes in import.</summary>
        public bool ImportMeshes { get; set; } = true;

        /// <summary>Includes graphical PMI in import.</summary>
        public bool ImportGraphicalPMI { get; set; } = true;

        /// <summary>Determines how surfaces will be imported.</summary>
        public ImportedSurfaceOrganizationTypeEnum SurfaceType { get; set; } = ImportedSurfaceOrganizationTypeEnum.kImportedAsSingleCompositeFeature;

        /// <summary>Sets the document units for the imported file.</summary>
        public ImportUnitsTypeEnum Units { get; set; } = ImportUnitsTypeEnum.kSourceUnitsType;

        /// <summary>
        /// Checks the quality of imported data.<code/>
        /// If a bad data is found, the composite is marked with an exclamation mark in the browser and the remaining bodies are not checked.<code/>
        /// This option may significantly increase the amount of time required to translate a file.
        /// </summary>
        public bool CheckDuringLoad { get; set; } = false;

        /// <summary>
        /// Inventor attempts to stitch surfaces into a quilt or solid.<code/>
        /// If the surfaces are stitched into a single quilt or body, the resulting quilt or body is promoted to the Part environment.<code/>
        /// Otherwise, the surfaces remain in the Construction environment.
        /// </summary>
        public bool AutoStitchAndPromote { get; set; } = true;

        /// <summary>If selected, slight alterations in the surface geometry are allowed to stitch the surfaces.</summary>
        public bool AdvancedHealing { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool EdgeSplitAndMerge { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public bool FaceSplitAndMerge { get; set; } = true;

        /// <summary>
        /// Displays the document after it has imported.<code/>
        /// Otherwise, it will be opened as a hidden document, which can be shown using <c>Document.Views.Add()</c>
        /// </summary>
        public bool DisplayWhenDone { get; set; } = true;

        /// <summary>
        /// Initializes a new instance of <see cref="StepImporter"/>.<code/>
        /// <paramref name="Filename"/> is the full file path of the STEP file to be imported.
        /// </summary>
        public StepImporter(string Filename, Inventor.Application Application)
        {
            this.Filename = Filename;

            _application = Application;
        }

        /// <summary>
        /// Import STEP file.<code/>
        /// The structure of the STEP file determines whether it imports as a part or an assembly.
        /// </summary>
        /// <returns>Imported part or assembly document</returns>
        public Document Import()
        {
            return DoImport();
        }

        /// <summary>Import STEP file as a multi-body part file</summary>
        /// 
        /// <returns>Imported part document</returns>
        public PartDocument ImportAsMultiBodyPart()
        {
            return (PartDocument)DoImport(ImportAASP: true);
        }

        /// <summary>Import STEP file as a composite part file</summary>
        /// 
        /// <returns>Imported part document</returns>
        public PartDocument ImportAsCompositePart()
        {
            return (PartDocument)DoImport(ImportAASP: true, ImportAASPIndex : 1);
        }

        /// <summary>
        /// Import STEP file associatively.<code/>
        /// The structure of the STEP file determines whether it imports as a part or an assembly.
        /// </summary>
        /// <returns>Imported part or assembly document</returns>
        public Document ImportAsReference()
        {
            return DoImport(AssociativeImport: true);
        }

        /// <summary>Import STEP file</summary>
        /// 
        /// <returns>Imported part or assembly document</returns>
        private Document DoImport(bool ImportAASP = false, bool AssociativeImport = false, int ImportAASPIndex = 0)
        {
            TranslatorData oTranslatorData = new TranslatorData(addinGUID: "{90AF7F40-0C01-11D5-8E83-0010B541CD80}", fullFileName: Filename, app: _application);

            NameValueMap op = oTranslatorData.oOptions;

            op.Value["SaveComponentDuringLoad"] = SaveDuringLoad;
            op.Value["SaveLocationIndex"] = 1; //0 would import to <Project Workspace>/Imported Components. 1 is a custom save location.
            op.Value["ComponentDestFolder"] =  SaveLocation==""? System.IO.Path.GetDirectoryName(Filename) : SaveLocation;
            op.Value["AddFilenamePrefix"] = FilenamePrefix != "";
            op.Value["AddFilenameSuffix"] = FilenameSuffix != "";
            op.Value["FilenamePrefix"] = FilenamePrefix;
            op.Value["FilenameSuffix"] = FilenameSuffix;
            op.Value["EmbedInDocument"] = EmbedTranslationReport;
            op.Value["SaveToDisk"] = SaveTranslationReport;
            op.Value["ImportSolid"] = ImportSolids;
            op.Value["ImportSurface"] = ImportSurfaces;
            op.Value["ImportWire"] = ImportWires;
            op.Value["ImportPoint"] = ImportPoints;
            op.Value["ImportMeshes"] = ImportMeshes;
            op.Value["ImportGraphicalPMI"] = ImportGraphicalPMI;
            op.Value["ImportValidationProperties"] = false; //This made Inventor crash when I set it to true.
            op.Value["CreateIFO"] = false; //I don't know what this is.
            op.Value["ImportAASP"] = ImportAASP; //Import assembly as single part
            op.Value["ImportAASPIndex"] = ImportAASPIndex; //Assembly Options - Structure: 0 = Multi-Body Part, 1 = Composite Part
            op.Value["CreateSurfIndex"] = SurfaceType - 108801; //Convert ImportedSurfaceOrganizationTypeEnum into the integer values expected by the STEP importer
            op.Value["ImportUnit"] = (int)Units - 110080; //Convert ImportUnitsTypeEnum to the integer values expected by the STEP importer
            op.Value["CheckDuringLoad"] = CheckDuringLoad;
            op.Value["AutoStitchAndPromote"] = AutoStitchAndPromote;
            op.Value["AdvanceHealing"] = AdvancedHealing;
            op.Value["EdgeSplitAndMergeDisabled"] = !EdgeSplitAndMerge;
            op.Value["FaceSplitAndMergeDisabled"] = !FaceSplitAndMerge;
            op.Value["AssociativeImport"] = AssociativeImport;
            op.Value["Selective Import"] = false; //I don't know if selective import works through the API.

            //Deprecated STEP Import Options:
            //op.Value["SaveAssemSeperateFolder"] = false; //Determines whether top-level assembly is saved in a separate folder
            //op.Value["AssemDestFolder"] = ""; //Separate location where top-level assembly will be saved
            //op.Value["GroupName"] = "";
            //op.Value["GroupNameIndex"] = 0;
            //op.Value["ExplodeMSB2Assm"] = false; //explode multiple solid bodies to assembly
            //op.Value["CEGroupLevel"] = 1; //Construction environment group level
            //op.Value["CEPrefixCk"] = false;
            //op.Value["CEPrefixString"] = "";

            TranslatorAddIn oTranslatorAddIn = (TranslatorAddIn)oTranslatorData.oAppAddIn;

            oTranslatorAddIn.Open(oTranslatorData.oDataMedium, oTranslatorData.oContext, oTranslatorData.oOptions, out object oNewDoc);

            Document newDoc = (Document)oNewDoc;

            if (DisplayWhenDone && !AssociativeImport) { newDoc.Views.Add(); } //Display the document

            if (AssociativeImport && !DisplayWhenDone) { newDoc.Views[1].Close(); } //Document is displayed by default when imported as a reference

            return (Document)oNewDoc;
        }
    }
}
