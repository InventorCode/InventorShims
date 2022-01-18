using Inventor;
using InventorShims;
using System.Collections.Generic;
using System.Linq;

namespace tests
{
    public class TestDocuments
    {
        private Dictionary<string, DocumentTypeEnum> _documents = new Dictionary<string, DocumentTypeEnum> { };
        private Dictionary<string, DocumentTypeEnum> _files = new Dictionary<string, DocumentTypeEnum> { };
        private Inventor.Application _invApp;

        public TestDocuments()
        {
            _documents.Add("Assembly.iam", DocumentTypeEnum.kAssemblyDocumentObject);
            _documents.Add("DerivedPart.ipt", DocumentTypeEnum.kPartDocumentObject);
            _documents.Add("Part01.ipt", DocumentTypeEnum.kPartDocumentObject);
            _documents.Add("Part02.ipt", DocumentTypeEnum.kPartDocumentObject);
            _documents.Add("Part03.ipt", DocumentTypeEnum.kPartDocumentObject);
            _documents.Add("Part04.ipt", DocumentTypeEnum.kPartDocumentObject);
            _documents.Add("Part11.ipt", DocumentTypeEnum.kPartDocumentObject);
            _documents.Add("Part12.ipt", DocumentTypeEnum.kPartDocumentObject);
            _documents.Add("Part13.ipt", DocumentTypeEnum.kPartDocumentObject);
            _documents.Add("SubAssembly.iam", DocumentTypeEnum.kAssemblyDocumentObject);
            _documents.Add("Drawing.idw", DocumentTypeEnum.kDrawingDocumentObject);
            //            _documents.Add("Presentation.ipn", DocumentTypeEnum.kPresentationDocumentObject);

            CreateDocuments();
        }

        ~TestDocuments()
        {
            _invApp = null;
        }

        public void CreateDocuments()
        {
            _invApp = ApplicationShim.Instance();

            if (_invApp.DesignProjectManager.ActiveDesignProject.Name != "Default")
                throw new System.Exception("Project file should be set to Default");

            if (TestDocumentsExists())
                return;

            foreach (KeyValuePair<string, DocumentTypeEnum> entry in _documents)
            {
                if (System.IO.File.Exists(FullPath(entry.Key)))
                    continue;

                switch (entry.Value)
                {
                    case DocumentTypeEnum.kPartDocumentObject:
                        var ipt = TestUtilities.CreatePartDocument();
                        ipt.SaveAs(FullPath(entry.Key), false);
                        _files.Add(FullPath(entry.Key), entry.Value);
                        ipt.Close(true);
                        break;

                    case DocumentTypeEnum.kAssemblyDocumentObject:
                        var ass = TestUtilities.CreateAssemblyDocument();
                        ass.SaveAs(FullPath(entry.Key), false);
                        _files.Add(FullPath(entry.Key), entry.Value);
                        ass.Close(true);

                        break;

                    case DocumentTypeEnum.kDrawingDocumentObject:
                        var idw = TestUtilities.CreateDrawingDocument();
                        idw.SaveAs(FullPath(entry.Key), false);
                        _files.Add(FullPath(entry.Key), entry.Value);
                        idw.Close(true);

                        break;

                    case DocumentTypeEnum.kPresentationDocumentObject:
                        var ipn = TestUtilities.CreatePresentationDocument();
                        ipn.SaveAs(FullPath(entry.Key), false);
                        _files.Add(FullPath(entry.Key), entry.Value);
                        ipn.Close(true);

                        break;

                    default:
                        break;
                }
            }

            TransientGeometry transGeom;
            transGeom = _invApp.TransientGeometry;

            Matrix oMatrix;
            oMatrix = transGeom.CreateMatrix();

            var iam = (AssemblyDocument)_invApp.Documents.Open(FullPath("Assembly.iam"), false);
            AssemblyComponentDefinition assemblyComponentDefinition = iam.ComponentDefinition;

            //add Parts to top assembly
            var lst = from x in _documents
                      where (x.Value == DocumentTypeEnum.kPartDocumentObject)
                      select x.Key;

            foreach (var x in lst)
            {
                assemblyComponentDefinition.Occurrences.Add(FullPath(x), oMatrix);
            }
            assemblyComponentDefinition.Occurrences.Add(FullPath("SubAssembly.iam"), oMatrix);

            assemblyComponentDefinition = null;
            iam.Save2();
            iam.Close();
            iam = null;

            //add Parts to sub assembly
            iam = (AssemblyDocument)_invApp.Documents.Open(FullPath("SubAssembly.iam"), false);
            assemblyComponentDefinition = iam.ComponentDefinition;

            assemblyComponentDefinition.Occurrences.Add(FullPath("part11.ipt"), oMatrix);
            assemblyComponentDefinition.Occurrences.Add(FullPath("part12.ipt"), oMatrix);
            assemblyComponentDefinition.Occurrences.Add(FullPath("part13.ipt"), oMatrix);

            assemblyComponentDefinition = null;
            iam.Save2();
            iam.Close();
            iam = null;

            //add to drawing
            var dwg = (DrawingDocument)_invApp.Documents.Open(FullPath("Drawing.idw"), true);
            var sheet = dwg.ActiveSheet;

            lst = from x in _documents
                  where (x.Value != DocumentTypeEnum.kDrawingDocumentObject)
                  select x.Key;

            var point = transGeom.CreatePoint2d(12, 12);

            foreach (var x in lst)
            {
                var doc = _invApp.Documents.Open(FullPath(x), false);
                sheet.DrawingViews.AddBaseView(doc, Position: point, 1.0, ViewOrientationTypeEnum.kDefaultViewOrientation, DrawingViewStyleEnum.kShadedDrawingViewStyle);
                doc.Close();
                doc = null;
            }

            dwg.Save();
            dwg.Close();
            dwg = null;
        }

        private static string FullPath(string path)
        {
            var inventorShimsPath = InventorShimsPath();

            return System.IO.Path.Combine(inventorShimsPath, path);
        }

        public static bool InventorShimsPathExists()
        {
            var inventorShimsPath = InventorShimsPath();
            if (string.IsNullOrEmpty(inventorShimsPath))
                return false;

            return System.IO.Directory.Exists(inventorShimsPath);
        }

        public bool TestDocumentsExists()
        {
            if (!InventorShimsPathExists())
                return false;

            var inventorShimsPath = InventorShimsPath();

            foreach (string f in _documents.Keys)
            {
                if (System.IO.File.Exists(System.IO.Path.Combine(inventorShimsPath, f)))
                    continue;
                else
                    return false;
            }

            return true;
        }

        public static string WorkspacePath()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.WorkspacePath;
            return path;
        }

        public static string InventorShimsPath()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.FullFileName;
            var test = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path), "InventorShims");
            return test;
        }

        public AssemblyDocument Assembly
        { get => (AssemblyDocument)_invApp.Documents.Open(FullPath("Assembly.iam"), false); }

        public Document Document
        { get => _invApp.Documents.Open(FullPath("part01.ipt"), false); }

        public PartDocument Part
        { get => (PartDocument)_invApp.Documents.Open(FullPath("part01.ipt"), false); }

        public DrawingDocument Drawing
        { get => (DrawingDocument)_invApp.Documents.Open(FullPath("Drawing.idw"), false); }

        //public PresentationDocument Presentation
        //{ get => (PresentationDocument)_invApp.Documents.Open(FullPath("Presentation.ipn"), false); }
    }
}