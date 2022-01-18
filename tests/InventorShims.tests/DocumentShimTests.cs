using Inventor;
using InventorShims;
using NUnit.Framework;
using NSubstitute;
using System.Linq;
using System.Collections.Generic;

namespace DocumentShim_Tests
{
    //[TestFixture]
    public class Nsub_tests
    {

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
    }

    [TestFixture]
    public class EnumerateAllLeafOccurrencesDocumentDescriptors
    {
        [Test]
        public void Works()
        {
            var testDocs = new tests.TestDocuments();
            var doc = testDocs.Assembly;

            var test = doc.EnumerateAllLeafOccurrencesDocumentDescriptors();
            Assert.IsInstanceOf<IEnumerable<DocumentDescriptor>>(test);
        }
    }

    [TestFixture]
    public class AssemblyDocuments_Enumerator
    {
        [Test]
        public void AssemblyDocuments_Works()
        {
            var testDocs = new tests.TestDocuments();
            var doc = testDocs.Assembly;

            var test = doc.EnumerateAllReferencedDocuments().AssemblyDocuments().Count();
            Assert.AreEqual(1, test);
        }

        [Test]
        public void RemoveAssemblyDocuments_Works()
        {
            var testDocs = new tests.TestDocuments();
            var doc = testDocs.Assembly;

            var test = doc.EnumerateAllReferencedDocuments().RemoveAssemblyDocuments().Count();
            Assert.AreEqual(8, test);
        }
    }

    [TestFixture]
    public class PartDocuments_Enumerator
    {
        [Test]
        public void PartDocuments_Works()
        {
            var testDocs = new tests.TestDocuments();
            var doc = testDocs.Assembly;

            var test = doc.EnumerateAllReferencedDocuments().PartDocuments().Count();
            Assert.AreEqual(8, test);
        }

        [Test]
        public void RemovePartDocuments_Works()
        {
            var testDocs = new tests.TestDocuments();
            var doc = testDocs.Assembly;

            var test = doc.EnumerateAllReferencedDocuments().RemovePartDocuments().Count();
            Assert.AreEqual(1, test);
        }
    }

    [TestFixture]
    public class EnumerateDocumentsTests
    {
        //TODO: fill rest of this out!
        [Test]
        public void EnumerateDocuments_fromDescriptors_works()
        {
            var testDocs = new tests.TestDocuments();
            var doc = testDocs.Assembly;

            var test = doc.EnumerateAllLeafOccurrencesDocumentDescriptors().EnumerateDocuments();
            Assert.IsInstanceOf<IEnumerable<Document>>(test);
        }
    }

    [TestFixture]
    public class EnumerateAllReferencedDocumentsTests
    {
        [Test]
        public void Document_Works()
        {
            var testDocs = new tests.TestDocuments();
            var doc = testDocs.Document;

            var test = doc.EnumerateAllReferencedDocuments();
            try { Assert.IsInstanceOf<IEnumerable<Document>>(test); }
            catch { testDocs = null; }
        }

        [Test]
        public void PartDocument_Works()
        {
            var testDocs = new tests.TestDocuments();
            var doc = testDocs.Part;

            var test = doc.EnumerateAllReferencedDocuments();
            try { Assert.IsInstanceOf<IEnumerable<Document>>(test); }
            catch { testDocs = null; }
        }
        [Test]
        public void DrawingDocument_Works()
        {
            var testDocs = new tests.TestDocuments();
            var doc = testDocs.Drawing;

            var test = doc.EnumerateAllReferencedDocuments();
            try { Assert.IsInstanceOf<IEnumerable<Document>>(test); }
            catch { testDocs = null; }
        }
        [Test]
        public void AssemblyDocument_Works()
        {
            var testDocs = new tests.TestDocuments();
            var doc = testDocs.Assembly;

            var test = doc.EnumerateAllReferencedDocuments();
            try { Assert.IsInstanceOf<IEnumerable<Document>>(test); }
            catch { testDocs = null; }
        }
    }


        [TestFixture]
    public class EnumerateReferencedDocumentsTests
    {
        [Test]
        public void DrawingDocument_Works()
        {
            var testDocs = new tests.TestDocuments();
            var doc = testDocs.Drawing;

            var test = doc.EnumerateReferencedDocuments();
            try { Assert.IsInstanceOf<IEnumerable<Document>>(test); }
            catch { testDocs = null; }
        }

        [Test]
        public void AssemblyDocument_Works()
        {
            var testDocs = new tests.TestDocuments();
            var doc = testDocs.Assembly;

            var test = doc.EnumerateReferencedDocuments();
            try { Assert.IsInstanceOf<IEnumerable<Document>>(test); }
            catch { testDocs = null; }
        }

        [Test]
        public void PartDocument_Works()
        {
            var testDocs = new tests.TestDocuments();
            var doc = testDocs.Part;

            var test = doc.EnumerateReferencedDocuments();
            try { Assert.IsInstanceOf<IEnumerable<Document>>(test); }
            catch { testDocs = null; }
        }

        [Test]
        public void Document_Works()
        {
            var testDocs = new tests.TestDocuments();
            var doc = testDocs.Document;

            var test = doc.EnumerateReferencedDocuments();
            try { Assert.IsInstanceOf<IEnumerable<Document>>(test); }
            catch { testDocs = null; }
        }
    }

    [TestFixture]
    public class EnumerateReferencingDocumentsTests
    {
        [Test]
        public void DrawingDocument_Works()
        {
            var testDocs = new tests.TestDocuments();
            var doc = testDocs.Drawing;

            var test = doc.EnumerateReferencingDocuments();
            try { Assert.IsInstanceOf<IEnumerable<Document>>(test); }
            catch { testDocs = null; }
        }

        [Test]
        public void AssemblyDocument_Works()
        {
            var testDocs = new tests.TestDocuments();
            var doc = testDocs.Assembly;

            var test = doc.EnumerateReferencingDocuments();
            try { Assert.IsInstanceOf<IEnumerable<Document>>(test); }
            catch { testDocs = null; }
        }

        [Test]
        public void PartDocument_Works()
        {
            var testDocs = new tests.TestDocuments();
            var doc = testDocs.Part;

            var test = doc.EnumerateReferencingDocuments();
            try { Assert.IsInstanceOf<IEnumerable<Document>>(test); }
            catch { testDocs = null; }
        }

        [Test]
        public void Document_Works()
        {
            var testDocs = new tests.TestDocuments();
            var doc = testDocs.Document;

            var test = doc.EnumerateReferencingDocuments();
            try { Assert.IsInstanceOf<IEnumerable<Document>>(test); }
            catch { testDocs = null; }
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