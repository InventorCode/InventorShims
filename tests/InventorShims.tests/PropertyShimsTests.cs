using Inventor;
using InventorShims;
using NUnit.Framework;

namespace PropertyShimTest
{
    [TestFixture]
    public class IsPropertyNative
    {
        [Test]
        public void GoodInput()
        {
            Assert.IsTrue(PropertyShim.IsPropertyNative("Title"));
        }

        [Test]
        public void BadInput()
        {
            Assert.IsFalse(PropertyShim.IsPropertyNative("ThisShouldBeFalse"));
        }

        [Test]
        public void ShortGoodInput()
        {
            Assert.IsFalse(PropertyShim.IsPropertyNative("ThisShouldBeFalse"));
        }
    }

    

    [TestFixture]
    public class SetPropertyValue
    {
        [Test]
        public void short_native()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            string test = "Bob";
            doc.SetPropertyValue("Title", test);
            string result = (string)doc.PropertySets["Inventor Summary Information"]["Title"].Value;

            try
            {
                Assert.AreEqual(test, result);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void short_custom()
        {
            var doc = tests.TestUtilities.CreatePartDocument();
            var partdoc = (PartDocument)tests.TestUtilities.CreatePartDocument();
            var assemblydoc = (AssemblyDocument)tests.TestUtilities.CreateAssemblyDocument();
            var drawingdoc = (DrawingDocument)tests.TestUtilities.CreateDrawingDocument();

            string test = "Bob";
            doc.SetPropertyValue("Stuff", test);
            partdoc.SetPropertyValue("Stuff", test);
            assemblydoc.SetPropertyValue("Stuff", test);
            drawingdoc.SetPropertyValue("Stuff", test);

            try
            {
                Assert.AreEqual(test, (string)doc.PropertySets["Inventor User Defined Properties"]["Stuff"].Value);
                Assert.AreEqual(test, (string)partdoc.PropertySets["Inventor User Defined Properties"]["Stuff"].Value);
                Assert.AreEqual(test, (string)assemblydoc.PropertySets["Inventor User Defined Properties"]["Stuff"].Value);
                Assert.AreEqual(test, (string)drawingdoc.PropertySets["Inventor User Defined Properties"]["Stuff"].Value);
            }
            finally
            {
                doc.Close(true);
                partdoc.Close(true);
                assemblydoc.Close(true);
                drawingdoc.Close(true);
            }
        }

        [Test]
        public void long_native()
        {
            var doc = tests.TestUtilities.CreatePartDocument();
            var partdoc = (PartDocument)tests.TestUtilities.CreatePartDocument();
            var assemblydoc = (AssemblyDocument)tests.TestUtilities.CreateAssemblyDocument();
            var drawingdoc = (DrawingDocument)tests.TestUtilities.CreateDrawingDocument();

            string test = "Bob";
            doc.SetPropertyValue("Inventor Summary Information", "Title", test);
            partdoc.SetPropertyValue("Inventor Summary Information", "Title", test);
            assemblydoc.SetPropertyValue("Inventor Summary Information", "Title", test);
            drawingdoc.SetPropertyValue("Inventor Summary Information", "Title", test);

            try
            {
                Assert.AreEqual(test, (string)doc.PropertySets["Inventor Summary Information"]["Title"].Value);
                Assert.AreEqual(test, (string)partdoc.PropertySets["Inventor Summary Information"]["Title"].Value);
                Assert.AreEqual(test, (string)assemblydoc.PropertySets["Inventor Summary Information"]["Title"].Value);
                Assert.AreEqual(test, (string)drawingdoc.PropertySets["Inventor Summary Information"]["Title"].Value);
            }
            finally
            {
                doc.Close(true);
                partdoc.Close(true);
                assemblydoc.Close(true);
                drawingdoc.Close(true);
            }
        }

        [Test]
        public void long_custom()
        {
            var doc = tests.TestUtilities.CreatePartDocument();
            var partdoc = (PartDocument)tests.TestUtilities.CreatePartDocument();
            var assemblydoc = (AssemblyDocument)tests.TestUtilities.CreateAssemblyDocument();
            var drawingdoc = (DrawingDocument)tests.TestUtilities.CreateDrawingDocument();

            string test = "Bob";
            doc.SetPropertyValue("Inventor User Defined Properties", "Stuff", test);
            partdoc.SetPropertyValue("Inventor User Defined Properties", "Stuff", test);
            assemblydoc.SetPropertyValue("Inventor User Defined Properties", "Stuff", test);
            drawingdoc.SetPropertyValue("Inventor User Defined Properties", "Stuff", test);

            try
            {
                Assert.AreEqual(test, (string)doc.PropertySets["Inventor User Defined Properties"]["Stuff"].Value);
                Assert.AreEqual(test, (string)partdoc.PropertySets["Inventor User Defined Properties"]["Stuff"].Value);
                Assert.AreEqual(test, (string)assemblydoc.PropertySets["Inventor User Defined Properties"]["Stuff"].Value);
                Assert.AreEqual(test, (string)drawingdoc.PropertySets["Inventor User Defined Properties"]["Stuff"].Value);
            }
            finally
            {
                doc.Close(true);
                partdoc.Close(true);
                assemblydoc.Close(true);
                drawingdoc.Close(true);
            }
        }

        [Test]
        public void long_superCustom()
        {
            var doc = tests.TestUtilities.CreatePartDocument();
            var partdoc = (PartDocument)tests.TestUtilities.CreatePartDocument();
            var assemblydoc = (AssemblyDocument)tests.TestUtilities.CreateAssemblyDocument();
            var drawingdoc = (DrawingDocument)tests.TestUtilities.CreateDrawingDocument();

            string test = "Bob";
            doc.SetPropertyValue("Custommm", "Stuff", test);
            partdoc.SetPropertyValue("Custommm", "Stuff", test);
            assemblydoc.SetPropertyValue("Custommm", "Stuff", test);
            drawingdoc.SetPropertyValue("Custommm", "Stuff", test);

            try
            {
                Assert.AreEqual(test, (string)doc.PropertySets["Custommm"]["Stuff"].Value);
                Assert.AreEqual(test, (string)partdoc.PropertySets["Custommm"]["Stuff"].Value);
                Assert.AreEqual(test, (string)assemblydoc.PropertySets["Custommm"]["Stuff"].Value);
                Assert.AreEqual(test, (string)drawingdoc.PropertySets["Custommm"]["Stuff"].Value);
            }
            finally
            {
                doc.Close(true);
                partdoc.Close(true);
                assemblydoc.Close(true);
                drawingdoc.Close(true);
            }
        }
    }

    [TestFixture]
    public class GetPropertyValue
    {
        [Test]
        public void short_native()
        {
            var doc = tests.TestUtilities.CreatePartDocument();
            var partDoc = (PartDocument)tests.TestUtilities.CreatePartDocument();
            var assemblyDoc = (AssemblyDocument)tests.TestUtilities.CreateAssemblyDocument();
            var drawingDoc = (DrawingDocument)tests.TestUtilities.CreateDrawingDocument();

            string test = "Bob";
            doc.PropertySets["Inventor Summary Information"]["Title"].Value = test;
            partDoc.PropertySets["Inventor Summary Information"]["Title"].Value = test;
            assemblyDoc.PropertySets["Inventor Summary Information"]["Title"].Value = test;
            drawingDoc.PropertySets["Inventor Summary Information"]["Title"].Value = test;

            try
            {
                Assert.AreEqual(test, doc.GetPropertyValue("Title"));
                Assert.AreEqual(test, partDoc.GetPropertyValue("Title"));
                Assert.AreEqual(test, assemblyDoc.GetPropertyValue("Title"));
                Assert.AreEqual(test, drawingDoc.GetPropertyValue("Title"));
            }
            finally
            {
                doc.Close(true);
                partDoc.Close(true);
                assemblyDoc.Close(true);
                drawingDoc.Close(true);
            }
        }

        [Test]
        public void short_custom()
        {
            var doc = tests.TestUtilities.CreatePartDocument();
            var partDoc = (PartDocument)tests.TestUtilities.CreatePartDocument();
            var assemblyDoc = (AssemblyDocument)tests.TestUtilities.CreateAssemblyDocument();
            var drawingDoc = (DrawingDocument)tests.TestUtilities.CreateDrawingDocument();

            string test = "Bob";
            doc.PropertySets["Inventor User Defined Properties"].Add(test, "Stuff");
            partDoc.PropertySets["Inventor User Defined Properties"].Add(test, "Stuff");
            assemblyDoc.PropertySets["Inventor User Defined Properties"].Add(test, "Stuff");
            drawingDoc.PropertySets["Inventor User Defined Properties"].Add(test, "Stuff");

            try
            {
                Assert.AreEqual(test, doc.GetPropertyValue("Stuff"));
                Assert.AreEqual(test, partDoc.GetPropertyValue("Stuff"));
                Assert.AreEqual(test, assemblyDoc.GetPropertyValue("Stuff"));
                Assert.AreEqual(test, drawingDoc.GetPropertyValue("Stuff"));
            }
            finally
            {
                doc.Close(true);
                partDoc.Close(true);
                assemblyDoc.Close(true);
                drawingDoc.Close(true);
            }
        }

        [Test]
        public void short_doesNotExist()
        {
            var doc = tests.TestUtilities.CreatePartDocument();
            var partDoc = (PartDocument)tests.TestUtilities.CreatePartDocument();
            var assemblyDoc = (AssemblyDocument)tests.TestUtilities.CreateAssemblyDocument();
            var drawingDoc = (DrawingDocument)tests.TestUtilities.CreateDrawingDocument();

            string test = "";

            try
            {
                Assert.AreEqual(test, doc.GetPropertyValue("Bob"));
                Assert.AreEqual(test, partDoc.GetPropertyValue("Bob"));
                Assert.AreEqual(test, assemblyDoc.GetPropertyValue("Bob"));
                Assert.AreEqual(test, drawingDoc.GetPropertyValue("Bob"));
            }
            finally
            {
                doc.Close(true);
                partDoc.Close(true);
                assemblyDoc.Close(true);
                drawingDoc.Close(true);
            }
        }

        [Test]
        public void long_native()
        {
            var doc = tests.TestUtilities.CreatePartDocument();
            var partdoc = (PartDocument)tests.TestUtilities.CreatePartDocument();
            var assemblydoc = (AssemblyDocument)tests.TestUtilities.CreateAssemblyDocument();
            var drawingdoc = (DrawingDocument)tests.TestUtilities.CreateDrawingDocument();

            string test = "Bob";
            doc.PropertySets["Inventor Summary Information"]["Title"].Value = test;
            partdoc.PropertySets["Inventor Summary Information"]["Title"].Value = test;
            assemblydoc.PropertySets["Inventor Summary Information"]["Title"].Value = test;
            drawingdoc.PropertySets["Inventor Summary Information"]["Title"].Value = test;

            try
            {
                Assert.AreEqual(doc.GetPropertyValue("Inventor Summary Information", "Title"), test);
                Assert.AreEqual(partdoc.GetPropertyValue("Inventor Summary Information", "Title"), test);
                Assert.AreEqual(assemblydoc.GetPropertyValue("Inventor Summary Information", "Title"), test);
                Assert.AreEqual(drawingdoc.GetPropertyValue("Inventor Summary Information", "Title"), test);
            }
            finally
            {
                doc.Close(true);
                partdoc.Close(true);
                assemblydoc.Close(true);
                drawingdoc.Close(true);
            }
        }

        [Test]
        public void long_custom()
        {
            var doc = tests.TestUtilities.CreatePartDocument();
            var partdoc = (PartDocument)tests.TestUtilities.CreatePartDocument();
            var assemblydoc = (AssemblyDocument)tests.TestUtilities.CreateAssemblyDocument();
            var drawingdoc = (DrawingDocument)tests.TestUtilities.CreateDrawingDocument();

            string test = "Bob";
            doc.PropertySets["Inventor User Defined Properties"].Add(test, "Stuff");
            partdoc.PropertySets["Inventor User Defined Properties"].Add(test, "Stuff");
            assemblydoc.PropertySets["Inventor User Defined Properties"].Add(test, "Stuff");
            drawingdoc.PropertySets["Inventor User Defined Properties"].Add(test, "Stuff");

            try
            {
                Assert.AreEqual(doc.GetPropertyValue("Inventor User Defined Properties", "Stuff"), test);
            }
            finally
            {
                doc.Close(true);
                partdoc.Close(true);
                assemblydoc.Close(true);
                drawingdoc.Close(true);
            }
        }

        [Test]
        public void long_doesNotExist()
        {
            var doc = tests.TestUtilities.CreatePartDocument();
            var partdoc = (PartDocument)tests.TestUtilities.CreatePartDocument();
            var assemblydoc = (AssemblyDocument)tests.TestUtilities.CreateAssemblyDocument();
            var drawingdoc = (DrawingDocument)tests.TestUtilities.CreateDrawingDocument();

            string test = "";

            try
            {
                Assert.AreEqual(doc.GetPropertyValue("Inventor User Defined Properties", "Bob"), test);
                Assert.AreEqual(partdoc.GetPropertyValue("Inventor User Defined Properties", "Bob"), test);
                Assert.AreEqual(assemblydoc.GetPropertyValue("Inventor User Defined Properties", "Bob"), test);
                Assert.AreEqual(drawingdoc.GetPropertyValue("Inventor User Defined Properties", "Bob"), test);
            }
            finally
            {
                doc.Close(true);
                partdoc.Close(true);
                assemblydoc.Close(true);
                drawingdoc.Close(true);
            }
        }
    }

    [TestFixture]
    public class GetProperty
    {
        [Test]
        public void short_native()
        {
            var doc = tests.TestUtilities.CreatePartDocument();
            var partdoc = (PartDocument)tests.TestUtilities.CreatePartDocument();
            var assemblydoc = (AssemblyDocument)tests.TestUtilities.CreateAssemblyDocument();
            var drawingdoc = (DrawingDocument)tests.TestUtilities.CreateDrawingDocument();

            Property test1 = doc.PropertySets["Inventor Summary Information"]["Title"];
            Property test2 = partdoc.PropertySets["Inventor Summary Information"]["Title"];
            Property test3 = assemblydoc.PropertySets["Inventor Summary Information"]["Title"];
            Property test4 = drawingdoc.PropertySets["Inventor Summary Information"]["Title"];

            try
            {
                Assert.AreEqual(test1, doc.GetProperty("Title"));
                Assert.AreEqual(test2, partdoc.GetProperty("Title"));
                Assert.AreEqual(test3, assemblydoc.GetProperty("Title"));
                Assert.AreEqual(test4, drawingdoc.GetProperty("Title"));
            }
            finally
            {
                doc.Close(true);
                partdoc.Close(true);
                assemblydoc.Close(true);
                drawingdoc.Close(true);
            }
        }

        [Test]
        public void short_doesNotExist()
        {
            var doc = tests.TestUtilities.CreatePartDocument();
            var partdoc = (PartDocument)tests.TestUtilities.CreatePartDocument();
            var assemblydoc = (AssemblyDocument)tests.TestUtilities.CreateAssemblyDocument();
            var drawingdoc = (DrawingDocument)tests.TestUtilities.CreateDrawingDocument();

            try
            {
                Assert.IsNull(doc.GetProperty("Bob"));
                Assert.IsNull(partdoc.GetProperty("Bob"));
                Assert.IsNull(assemblydoc.GetProperty("Bob"));
                Assert.IsNull(drawingdoc.GetProperty("Bob"));
            }
            finally
            {
                doc.Close(true);
                partdoc.Close(true);
                assemblydoc.Close(true);
                drawingdoc.Close(true);
            }
        }

        [Test]
        public void short_custom()
        {
            var doc = tests.TestUtilities.CreatePartDocument();
            var partdoc = (PartDocument)tests.TestUtilities.CreatePartDocument();
            var assemblydoc = (AssemblyDocument)tests.TestUtilities.CreateAssemblyDocument();
            var drawingdoc = (DrawingDocument)tests.TestUtilities.CreateDrawingDocument();

            doc.PropertySets["Inventor User Defined Properties"].Add("Bob", "Stuff");
            partdoc.PropertySets["Inventor User Defined Properties"].Add("Bob", "Stuff");
            assemblydoc.PropertySets["Inventor User Defined Properties"].Add("Bob", "Stuff");
            drawingdoc.PropertySets["Inventor User Defined Properties"].Add("Bob", "Stuff");
            
            try
            {
                Assert.AreEqual(doc.GetProperty("Stuff"), doc.PropertySets["Inventor User Defined Properties"]["Stuff"]);
                Assert.AreEqual(partdoc.GetProperty("Stuff"), partdoc.PropertySets["Inventor User Defined Properties"]["Stuff"]);
                Assert.AreEqual(assemblydoc.GetProperty("Stuff"), assemblydoc.PropertySets["Inventor User Defined Properties"]["Stuff"]);
                Assert.AreEqual(drawingdoc.GetProperty("Stuff"), drawingdoc.PropertySets["Inventor User Defined Properties"]["Stuff"]);
            }
            finally
            {
                doc.Close(true);
                partdoc.Close(true);
                assemblydoc.Close(true);
                drawingdoc.Close(true);
            }
        }

        [Test]
        public void long_native()
        {
            var doc = tests.TestUtilities.CreatePartDocument();
            var partdoc = (PartDocument)tests.TestUtilities.CreatePartDocument();
            var assemblydoc = (AssemblyDocument)tests.TestUtilities.CreateAssemblyDocument();
            var drawingdoc = (DrawingDocument)tests.TestUtilities.CreateDrawingDocument();

            try
            {
                Assert.AreEqual(doc.PropertySets["Inventor Summary Information"]["Title"], doc.GetProperty("Inventor Summary Information", "Title"));
                Assert.AreEqual(partdoc.PropertySets["Inventor Summary Information"]["Title"], partdoc.GetProperty("Inventor Summary Information", "Title"));
                Assert.AreEqual(assemblydoc.PropertySets["Inventor Summary Information"]["Title"], assemblydoc.GetProperty("Inventor Summary Information", "Title"));
                Assert.AreEqual(drawingdoc.PropertySets["Inventor Summary Information"]["Title"], drawingdoc.GetProperty("Inventor Summary Information", "Title"));
            }
            finally
            {
                doc.Close(true);
                partdoc.Close(true);
                assemblydoc.Close(true);
                drawingdoc.Close(true);
            }
        }

        [Test]
        public void long_doesNotExist()
        {
            var doc = tests.TestUtilities.CreatePartDocument();
            var partdoc = (PartDocument)tests.TestUtilities.CreatePartDocument();
            var assemblydoc = (AssemblyDocument)tests.TestUtilities.CreateAssemblyDocument();
            var drawingdoc = (DrawingDocument)tests.TestUtilities.CreateDrawingDocument();

            try
            {
                Assert.IsNull(doc.GetProperty("Inventor Summary Information", "Bob"));
                Assert.IsNull(partdoc.GetProperty("Inventor Summary Information", "Bob"));
                Assert.IsNull(assemblydoc.GetProperty("Inventor Summary Information", "Bob"));
                Assert.IsNull(drawingdoc.GetProperty("Inventor Summary Information", "Bob"));
            }
            finally
            {
                doc.Close(true);
                partdoc.Close(true);
                assemblydoc.Close(true);
                drawingdoc.Close(true);
            }
        }

        [Test]
        public void long_custom()
        {
            var doc = tests.TestUtilities.CreatePartDocument();
            var partdoc = (PartDocument)tests.TestUtilities.CreatePartDocument();
            var assemblydoc = (AssemblyDocument)tests.TestUtilities.CreateAssemblyDocument();
            var drawingdoc = (DrawingDocument)tests.TestUtilities.CreateDrawingDocument();

            doc.PropertySets["Inventor User Defined Properties"].Add("Bob", "Stuff");
            partdoc.PropertySets["Inventor User Defined Properties"].Add("Bob", "Stuff");
            assemblydoc.PropertySets["Inventor User Defined Properties"].Add("Bob", "Stuff");
            drawingdoc.PropertySets["Inventor User Defined Properties"].Add("Bob", "Stuff");

            try
            {
                Assert.AreEqual(doc.GetProperty("Inventor User Defined Properties", "Stuff"), doc.PropertySets["Inventor User Defined Properties"]["Stuff"]);
                Assert.AreEqual(partdoc.GetProperty("Inventor User Defined Properties", "Stuff"), partdoc.PropertySets["Inventor User Defined Properties"]["Stuff"]);
                Assert.AreEqual(assemblydoc.GetProperty("Inventor User Defined Properties", "Stuff"), assemblydoc.PropertySets["Inventor User Defined Properties"]["Stuff"]);
                Assert.AreEqual(drawingdoc.GetProperty("Inventor User Defined Properties", "Stuff"), drawingdoc.PropertySets["Inventor User Defined Properties"]["Stuff"]);
            }
            finally
            {
                doc.Close(true);
                partdoc.Close(true);
                assemblydoc.Close(true);
                drawingdoc.Close(true);
            }
        }

        
    }

    [TestFixture]
    public class GetSuperCustomPropertyValue
    {
        [Test]
        public void Works()
        {
            var doc = tests.TestUtilities.CreatePartDocument();
            var partdoc = (PartDocument)tests.TestUtilities.CreatePartDocument();
            var assemblydoc = (AssemblyDocument)tests.TestUtilities.CreateAssemblyDocument();
            var drawingdoc = (DrawingDocument)tests.TestUtilities.CreateDrawingDocument();

            var prop1 = doc.PropertySets.Add("custom thang");
            var prop2 = partdoc.PropertySets.Add("custom thang");
            var prop3 = assemblydoc.PropertySets.Add("custom thang");
            var prop4 = drawingdoc.PropertySets.Add("custom thang");

            doc.PropertySets["custom thang"].Add("Bob", "Stuff");
            partdoc.PropertySets["custom thang"].Add("Bob", "Stuff");
            assemblydoc.PropertySets["custom thang"].Add("Bob", "Stuff");
            drawingdoc.PropertySets["custom thang"].Add("Bob", "Stuff");

            try
            {
                Assert.AreEqual(doc.GetPropertyValue("custom thang", "Stuff"), prop1["Stuff"].Value);
                Assert.AreEqual(partdoc.GetPropertyValue("custom thang", "Stuff"), prop2["Stuff"].Value);
                Assert.AreEqual(assemblydoc.GetPropertyValue("custom thang", "Stuff"), prop3["Stuff"].Value);
                Assert.AreEqual(drawingdoc.GetPropertyValue("custom thang", "Stuff"), prop4["Stuff"].Value);
            }
            finally
            {
                doc.Close(true);
                partdoc.Close(true);
                assemblydoc.Close(true);
                drawingdoc.Close(true);
            }
        }
    }
}