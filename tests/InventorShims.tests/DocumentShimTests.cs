using Inventor;
using InventorShims;
using NUnit.Framework;
using NSubstitute;
using System.Linq;
using static InventorShims.EnumerableDocuments;

namespace DocumentShim_Tests
{
    [TestFixture]
    public class Nsub_tests
    {

        [Test]
        public void Nsub_test()
        {
            Document doc = Substitute.For<Document>();
            doc.PropertySets["99"]["hhgf"].Value.Returns("hello");
            doc.NeedsMigrating.Returns(true);
            Assert.IsTrue(doc.NeedsMigrating);
            Assert.Equals(doc.PropertySets["99"]["hhgf"].Value, "hello");

            PropertySets fakePropSets = Substitute.For<PropertySets>();
            doc.PropertySets.Returns(fakePropSets);

            Assert.IsNotNull(doc.PropertySets);
        }

        [Test]
        public void testtest()
        {

            //var app = InventorShims.ApplicationShim.CurrentInstance();
            //AssemblyDocument doc = (AssemblyDocument)app.ActiveDocument;

            //var test = doc.GetAllReferencedDocuments().PartDocuments().Distinct().Count();

            //Assert.AreEqual(1, test);
        }
    }

        [TestFixture]
    public class ReturnSpecificDocumentObject
    {
        [Test]
        public void PartDocument_IsReturned()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

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

        [Test]
        public void PartDocument_IsNotReturned()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

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

        [Test]
        public void AssemblyDocument_IsReturned()
        {
            var doc = tests.TestUtilities.CreateAssemblyDocument();

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

        [Test]
        public void AssemblyDocument_IsNotReturned()
        {
            var doc = tests.TestUtilities.CreateAssemblyDocument();

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

        [Test]
        public void DrawingDocument__IsReturned()
        {
            var doc = tests.TestUtilities.CreateDrawingDocument();

            var doc2 = doc.ReturnSpecificDocumentObject();

            //only exists in DrawingDocuments
            BorderDefinitions test = null;
            try
            {
                test = doc2.BorderDefinitions;
            }
            catch { }

            try
            {
                Assert.IsNotNull(test);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void DrawingDocument__IsNotReturned()
        {
            var doc = tests.TestUtilities.CreateDrawingDocument();

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

        [Test]
        public void PresentationDocument_IsReturned()
        {
            var doc = tests.TestUtilities.CreatePresentationDocument();

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

        [Test]
        public void PresentationDocument__IsNotReturned()
        {
            var doc = tests.TestUtilities.CreatePresentationDocument();

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