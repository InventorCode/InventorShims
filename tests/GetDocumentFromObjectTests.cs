using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventorShims;
using Inventor;

namespace DocumentShim_Tests
{
    [TestClass]
    public class ReturnDocumentFromObject_Tests
    {
        [TestMethod]
        public void PartDocumentIn_ReturnsDocument()
        {

            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            var doc2 = doc.GetDocumentFromObject();

            var tt = PropertyShim.GetPropertyValue(doc2, "Author");
            //only exists in ParDocuments
            PropertySets test = null;
            try
            {
                test = doc2.PropertySets;
            }
            catch { }

            try
            {
                Assert.IsNotNull(test);
            }
            finally { doc.Close(true); }
        }

        [TestMethod]
        public void AssemblyDocumentIn_ReturnsDocument()
        {

            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kAssemblyDocumentObject, path + "Standard.iam", true);

            var doc2 = doc.GetDocumentFromObject();

            var tt = PropertyShim.GetPropertyValue(doc2, "Author");
            //only exists in ParDocuments
            PropertySets test = null;
            try
            {
                test = doc2.PropertySets;
            }
            catch { }

            try
            {
                Assert.IsNotNull(test);
            }
            finally { doc.Close(true); }
        }
    }
}