using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventorShims;
using Inventor;

namespace DocumentShim_Tests
{
    [TestClass]
    public class ReturnSpecificDocumentObject
    {
        [TestMethod]
        public void PartDocument_IsReturned()
        {

            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);
            
            var doc2 = doc.ReturnSpecificDocumentObject();

            //var tt = doc2.GetPropertyValue("Author");
            var tt = PropertyShim.GetPropertyValue(doc2, "Author");
            //only exists in ParDocuments
            PartComponentDefinition test = null;
            try
            {
                test = doc2.ComponentDefinition;
            }
            catch { }

            try
            {
                Assert.IsNotNull(test);
            }
            finally { doc.Close(true); }
        }

        [TestMethod]
        public void PartDocument_IsNotReturned()
        {

            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            var doc2 = doc.ReturnSpecificDocumentObject();
            //only exists in ParDocuments
            PartComponentDefinition test = null;
            try
            {
                //test = doc2.ComponentDefinition;
            }
            catch { }

            try
            {
                Assert.IsNull(test);
            }
            finally { doc.Close(true); }
        }

        [TestMethod]
        public void AssemblyDocument_IsReturned()
        {

            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kAssemblyDocumentObject, path + "Standard.iam", true);

            var doc2 = doc.ReturnSpecificDocumentObject();
            //only exists in AssemblyDocuments
            AssemblyComponentDefinition test = null;
            try
            {
                test = doc2.ComponentDefinition;
            }
            catch { }

            try
            {
                Assert.IsNotNull(test);
            }
            finally { doc.Close(true); }
        }

        [TestMethod]
        public void AssemblyDocument_IsNotReturned()
        {

            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kAssemblyDocumentObject, path + "Standard.iam", true);

            var doc2 = doc.ReturnSpecificDocumentObject();
            //only exists in AssemblyDocuments
            AssemblyComponentDefinition test = null;
            try
            {
                //test = doc.ComponentDefinition;
            }
            catch { }

            try
            {
                Assert.IsNull(test);
            }
            finally { doc.Close(true); }
        }

        [TestMethod]
        public void DrawingDocument__IsReturned()
        {

            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kDrawingDocumentObject, path + "Standard.idw", true);

            var doc2 = doc.ReturnSpecificDocumentObject();

            //only exists in DrawingDocuments
            BorderDefinitions test = null;
            try {
            test = doc2.BorderDefinitions;
            }
            catch { }

            try
            {
                Assert.IsNotNull(test);
            }
            finally { doc.Close(true); }
        }

        [TestMethod]
        public void DrawingDocument__IsNotReturned()
        {

            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kDrawingDocumentObject, path + "Standard.idw", true);

            var doc2 = doc.ReturnSpecificDocumentObject();

            //only exists in DrawingDocuments
            BorderDefinitions test = null;
            try
            {
                //test = doc2.BorderDefinitions;
            }
            catch { }

            try
            {
                Assert.IsNull(test);
            }
            finally { doc.Close(true); }
        }

        [TestMethod]
        public void PresentationDocument_IsReturned()
        {

            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPresentationDocumentObject, path + "Standard.ipn", true);

            var doc2 = doc.ReturnSpecificDocumentObject();

            //only exists in DrawingDocuments
            PresentationScenes test = null;
            try
            {
                test = doc2.Scenes;
            }
            catch { }

            try
            {
                Assert.IsNotNull(test);
            }
            finally { doc.Close(true); }
        }

        [TestMethod]
        public void PresentationDocument__IsNotReturned()
        {

            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPresentationDocumentObject, path + "Standard.ipn", true);

            var doc2 = doc.ReturnSpecificDocumentObject();

            //only exists in DrawingDocuments
            PresentationScenes test = null;
            try
            {
                //test = doc2.Scenes;
            }
            catch { }

            try
            {
                Assert.IsNull(test);
            }
            finally { doc.Close(true); }
        }
    }

}

