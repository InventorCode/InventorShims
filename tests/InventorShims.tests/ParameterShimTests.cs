using Inventor;
using InventorShims;
using NUnit.Framework;

namespace ParameterShim_Tests
{
    [TestFixture]
    public class GetParameter
    {
        [Test]
        public void ParameterExists_Document_returnsParameter()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            doc.SetParameterValue("testing", "16", "cm");
            Parameter test = doc.GetParameter("testing");

            try {Assert.IsNotNull(test);}
            finally { doc.Close(true); }
        }

        [Test]
        public void ParameterExists_PartDocument_returnsParameter()
        {
            var orig = tests.TestUtilities.CreatePartDocument();
            PartDocument doc = (PartDocument)orig;
            
            doc.SetParameterValue("testing", "16", "cm");
            Parameter test = doc.GetParameter("testing");

            try { Assert.IsNotNull(test); }
            finally { doc = null;  orig.Close(true); }
        }

        [Test]
        public void ParameterExists_AssemblyDocument_returnsParameter()
        {
            var orig = tests.TestUtilities.CreateAssemblyDocument();
            var doc = (AssemblyDocument)orig;

            doc.SetParameterValue("testing", "16", "cm");
            Parameter test = doc.GetParameter("testing");

            try { Assert.IsNotNull(test); }
            finally { doc = null;  orig.Close(true); }
        }

        [Test]
        public void ParameterExists_DrawingDocument_returnsParameter()
        {
            var orig = tests.TestUtilities.CreateDrawingDocument();
            var doc = (DrawingDocument)orig;

            doc.SetParameterValue("testing", "16", "cm");
            Parameter test = doc.GetParameter("testing");

            try { Assert.IsNotNull(test); }
            finally { doc = null;  orig.Close(true); }
        }

        [Test]
        public void ParameterDoesNotExist_Document_returnsNull()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            Parameter test = doc.GetParameter("testing");

            try { Assert.IsNull(test); }
            finally { doc.Close(true); }
        }
    }

    [TestFixture]
    public class GetParameters
    {
        [Test]
        public void GetParametersPartDocument_returns_notNull()
        {
            var orig = tests.TestUtilities.CreatePartDocument();
            var doc = (PartDocument)orig;

            try
            {
                Assert.IsNotNull(doc.GetParameters());
            }
            finally { doc = null; orig.Close(true); }
        }

        [Test]
        public void GetParametersAssemblyDocument_returns_notNull()
        {
            var orig = tests.TestUtilities.CreateAssemblyDocument();
            var doc = (AssemblyDocument)orig;

            try
            {
                Assert.IsNotNull(doc.GetParameters());
            }
            finally { doc = null;  orig.Close(true); }
        }
        [Test]
        public void GetParametersDrawingDocument_returns_notNull()
        {
            var orig = tests.TestUtilities.CreateDrawingDocument();
            var doc = (DrawingDocument)orig;

            try
            {
                Assert.IsNotNull(doc.GetParameters());
            }
            finally {
                doc = null;
                orig.Close(true); }
        }

        [Test]
        public void GetParametersDocument_returns_notNull()
        {
            var doc = tests.TestUtilities.CreateDrawingDocument();

            try
            { Assert.IsNotNull(doc.GetParameters()); }
            finally { doc.Close(true); }
        }

        [Test]
        public void GetParametersUnsupportedDocument_returns_error()
        {
            var doc = tests.TestUtilities.CreatePresentationDocument();

            try
            { Assert.IsNull(doc.GetParameters()); }
            finally { doc.Close(true); }
            
        }
    }

    [TestFixture]
    public class SetParameterValue
    {
        [Test]
        public void Numeric_CreatesNew()
        {
            var orig = tests.TestUtilities.CreatePartDocument();

            orig.SetParameterValue("testing", "16", "cm");

            PartDocument doc = (PartDocument)orig;
            Parameter parameter = doc.ComponentDefinition.Parameters["testing"];
            double testing = (double)parameter.Value;

            try
            { Assert.AreEqual(16, testing); }
            finally
            {
                doc = null;
                orig.Close(true);
            }
        }

        [Test]
        public void Numeric_OverwritesExisting()
        {
            var orig = tests.TestUtilities.CreatePartDocument();

            orig.SetParameterValue("testing", "16", "cm");
            orig.SetParameterValue("testing", "19", "cm");

            PartDocument doc = (PartDocument)orig;
            Parameter parameter = doc.ComponentDefinition.Parameters["testing"];
            double testing = (double)parameter.Value;

            try
            {
                Assert.AreEqual(19, testing);
            }
            finally
            {
                doc = null;
                orig.Close(true);
            }
        }

        [Test]
        public void NumericPart_CreatesNew()
        {
            var orig = tests.TestUtilities.CreatePartDocument();
            PartDocument doc = (PartDocument)orig;

            doc.SetParameterValue("testing", "16", "cm");

            Parameter parameter = doc.ComponentDefinition.Parameters["testing"];
            double testing = (double)parameter.Value;

            try
            {
                Assert.AreEqual(16, testing);
            }
            finally
            {
                doc = null;
                orig.Close(true);
            }
        }
        [Test]
        public void NumericAssembly_CreatesNew()
        {
            var orig = tests.TestUtilities.CreateAssemblyDocument();
            AssemblyDocument doc = (AssemblyDocument)orig;

            doc.SetParameterValue("testing", "16", "cm");

            Parameter parameter = doc.ComponentDefinition.Parameters["testing"];
            double testing = (double)parameter.Value;

            try
            {
                Assert.AreEqual(16, testing);
            }
            finally
            {
                doc = null;
                orig.Close(true);
            }
        }
        [Test]
        public void NumericDrawing_CreatesNew()
        {
            var orig = tests.TestUtilities.CreateDrawingDocument();
            DrawingDocument doc = (DrawingDocument)orig;

            doc.SetParameterValue("testing", "16", "cm");

            Parameter parameter = doc.Parameters["testing"];
            double testing = (double)parameter.Value;

            try
            {
                Assert.AreEqual(16, testing);
            }
            finally
            {
                doc = null;
                orig.Close(true);
            }
        }

        [Test]
        public void Text_CreatesNew()
        {
            var orig = tests.TestUtilities.CreatePartDocument();

            orig.SetParameterValue("testing", "This is a test");

            PartDocument doc = (PartDocument)orig;
            Parameter parameter = doc.ComponentDefinition.Parameters["testing"];
            var testing = parameter.Value;

            try
            {
                Assert.AreEqual("This is a test", testing);
            }
            finally
            {
                doc = null;
                orig.Close(true);
            }
        }
        [Test]
        public void TextPart_CreatesNew()
        {
            var orig = tests.TestUtilities.CreatePartDocument();
            PartDocument doc = (PartDocument)orig;

            doc.SetParameterValue("testing", "This is a test");

            Parameter parameter = doc.ComponentDefinition.Parameters["testing"];
            var testing = parameter.Value;

            try
            {
                Assert.AreEqual("This is a test", testing);
            }
            finally
            {
                doc = null;
                orig.Close(true);
            }
        }
        [Test]
        public void TextAssembly_CreatesNew()
        {
            var orig = tests.TestUtilities.CreateAssemblyDocument();
            AssemblyDocument doc = (AssemblyDocument)orig;

            doc.SetParameterValue("testing", "This is a test");

            Parameter parameter = doc.ComponentDefinition.Parameters["testing"];
            var testing = parameter.Value;

            try
            {
                Assert.AreEqual("This is a test", testing);
            }
            finally
            {
                doc = null;
                orig.Close(true);
            }
        }
        [Test]
        public void TextDrawing_CreatesNew()
        {
            var orig = tests.TestUtilities.CreateDrawingDocument();
            DrawingDocument doc = (DrawingDocument)orig;

            doc.SetParameterValue("testing", "This is a test");

            Parameter parameter = doc.Parameters["testing"];
            var testing = parameter.Value;

            try
            {
                Assert.AreEqual("This is a test", testing);
            }
            finally
            {
                doc = null;
                orig.Close(true);
            }
        }

        [Test]
        public void Text_OverwritesExisting()
        {
            var orig = tests.TestUtilities.CreatePartDocument();

            orig.SetParameterValue("testing", "This is a test");
            orig.SetParameterValue("testing", "This is a test60");

            PartDocument doc = (PartDocument)orig;
            Parameter parameter = doc.ComponentDefinition.Parameters["testing"];
            var testing = parameter.Value;

            try
            {
                Assert.AreEqual("This is a test60", testing);
            }
            finally
            {
                doc = null;
                orig.Close(true);
            }
        }

        [Test]
        public void Bool_CreatesNew()
        {
            var orig = tests.TestUtilities.CreatePartDocument();

            orig.SetParameterValue("testing", true);

            PartDocument doc = (PartDocument)orig;
            Parameter parameter = doc.ComponentDefinition.Parameters["testing"];
            var testing = parameter.Value;

            try
            {
                Assert.AreEqual(true, testing);
            }
            finally
            {
                doc = null;
                orig.Close(true);
            }
        }
        [Test]
        public void BoolPart_CreatesNew()
        {
            var orig = tests.TestUtilities.CreatePartDocument();
            PartDocument doc = (PartDocument)orig;

            doc.SetParameterValue("testing", true);

            PartDocument part = (PartDocument)doc;
            Parameter parameter = part.ComponentDefinition.Parameters["testing"];
            var testing = parameter.Value;

            try {Assert.AreEqual(true, testing);}
            finally { doc = null; orig.Close(true);}
        }

        [Test]
        public void BoolAssembly_CreatesNew()
        {
            var orig = tests.TestUtilities.CreateAssemblyDocument();
            var doc = (AssemblyDocument)orig;

            doc.SetParameterValue("testing", true);

            Parameter parameter = doc.ComponentDefinition.Parameters["testing"];
            var testing = parameter.Value;

            try { Assert.AreEqual(true, testing); }
            finally { doc = null; orig.Close(true); }
        }

        [Test]
        public void BoolDrawing_CreatesNew()
        {
            var orig = tests.TestUtilities.CreateDrawingDocument();
            var doc = (DrawingDocument)orig;

            doc.SetParameterValue("testing", true);

            Parameter parameter = doc.Parameters["testing"];
            var testing = parameter.Value;

            try { Assert.AreEqual(true, testing); }
            finally { doc = null;  orig.Close(true); }
        }


        [Test]
        public void Bool_OverwritesExisting()
        {
            var orig = tests.TestUtilities.CreatePartDocument();

            orig.SetParameterValue("testing", true);
            orig.SetParameterValue("testing", false);

            PartDocument doc = (PartDocument)orig;
            Parameter parameter = doc.ComponentDefinition.Parameters["testing"];
            var testing = parameter.Value;

            try { Assert.AreEqual(false, testing); }
            finally { doc = null;  orig.Close(true); }
        }
    }

    [TestFixture]
    public class GetParameterValue
    {
        [Test]
        public void GoodInput_ReturnsValue()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            doc.SetParameterValue("testing", "16", "cm");

            var testing = doc.GetParameterValue("testing");

            try { Assert.AreEqual("16.000 cm", testing); }
            finally { doc.Close(true); }
        }

        public void PartDoc_ReturnsValue()
        {
            var orig = tests.TestUtilities.CreatePartDocument();
            var doc = (PartDocument)orig;
            doc.SetParameterValue("testing", "16", "cm");

            var testing = doc.GetParameterValue("testing");

            try { Assert.AreEqual("16.000 cm", testing);}
            finally { orig.Close(true);}
        }

        public void AssemblyDoc_ReturnsValue()
        {
            var orig = tests.TestUtilities.CreateAssemblyDocument();
            var doc = (AssemblyDocument)orig;
            doc.SetParameterValue("testing", "16", "cm");

            var testing = doc.GetParameterValue("testing");

            try { Assert.AreEqual("16.000 cm", testing); }
            finally { orig.Close(true); }
        }
        public void DrawingDoc_ReturnsValue()
        {
            var orig = tests.TestUtilities.CreateDrawingDocument();
            var doc = (DrawingDocument)orig;
            doc.SetParameterValue("testing", "16", "cm");

            var testing = doc.GetParameterValue("testing");

            try { Assert.AreEqual("16.000 cm", testing); }
            finally { orig.Close(true); }
        }

        [Test]
        public void NoParameter_ReturnsEmpty()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            var testing = doc.GetParameterValue("testing");

            try { Assert.AreEqual("", testing); }
            finally { doc.Close(true); }
        }
    }

    [TestFixture]
    public class RemoveParameter
    {
        [Test]
        public void Document_Works()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            doc.SetParameterValue("testing", "16", "cm");
            doc.RemoveParameter("testing");
            string testing = doc.GetParameterValue("testing");

            try { Assert.AreEqual("", testing); }
            finally { doc.Close(true); }
        }

        [Test]
        public void PartDocument_Works()
        {
            var orig = tests.TestUtilities.CreatePartDocument();
            var doc = (PartDocument)orig;

            doc.SetParameterValue("testing", "16", "cm");
            doc.RemoveParameter("testing");
            string testing = doc.GetParameterValue("testing");

            try { Assert.AreEqual("", testing); }
            finally { doc = null;  orig.Close(true); }
        }

        [Test]
        public void AssemblyDocument_Works()
        {
            var orig = tests.TestUtilities.CreateAssemblyDocument();
            var doc = (AssemblyDocument)orig;

            doc.SetParameterValue("testing", "16", "cm");
            doc.RemoveParameter("testing");
            string testing = doc.GetParameterValue("testing");

            try { Assert.AreEqual("", testing); }
            finally { doc = null; orig.Close(true); }
        }

        [Test]
        public void DrawingDocument_Works()
        {
            var orig = tests.TestUtilities.CreateDrawingDocument();
            var doc = (DrawingDocument)orig;

            doc.SetParameterValue("testing", "16", "cm");
            doc.RemoveParameter("testing");
            string testing = doc.GetParameterValue("testing");

            try { Assert.AreEqual("", testing); }
            finally { doc = null; orig.Close(true); }
        }
    }


    [TestFixture]
    public class ParameterIsWritable
    {
        [Test]
        public void GoodInput_ReturnsTrue()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            doc.SetParameterValue("testing", "16", "cm");

            var testing = ParameterShim.ParameterIsWritable(doc, "testing");

            try
            {
                Assert.AreEqual(true, testing);
            }
            finally
            {
                doc.Close(true);
            }
        }

        [Test]
        public void NoParameter_ReturnsFalse()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            //doc.SetParameterValue("testing", "16", "cm");

            var testing = ParameterShim.ParameterIsWritable(doc, "testing");

            try
            {
                Assert.AreEqual(false, testing);
            }
            finally
            {
                doc.Close(true);
            }
        }
    }
}