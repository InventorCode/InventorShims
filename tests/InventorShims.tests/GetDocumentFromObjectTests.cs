using Inventor;
using InventorShims;
using NUnit.Framework;

namespace DocumentShim_Tests
{
    [TestFixture]
    public class ReturnDocumentFromObject_Tests
    {
        [Test]
        public void PartDocumentIn_ReturnsDocument()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

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

        [Test]
        public void AssemblyDocumentIn_ReturnsDocument()
        {
            var doc = tests.TestUtilities.CreateAssemblyDocument();

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