using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InventorShims;
using Inventor;
using System.Windows.Forms;

namespace ParameterShim_Tests
{
    [TestClass]
    public class GetParameter
    {
        [TestMethod]
        public void ParameterExists_returnsParameter()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            doc.SetParameterValue("testing", "16", "cm");
            Parameter test = doc.GetParameter("testing");

            try
            {
                Assert.IsNotNull(test);
            }
            finally { doc.Close(true); }

        }

        [TestMethod]
        public void ParameterDoesNotExist_returnsNull()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            Parameter test = doc.GetParameter("testing");

            try
            {
                Assert.IsNull(test);
            }
            finally { doc.Close(true); }

        }
    }


            [TestClass]
    public class SetParameterValue
    {

        [TestMethod]
        public void Numeric_CreatesNew()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            doc.SetParameterValue("testing", "16", "cm");

            PartDocument part = (PartDocument)doc;
            Parameter parameter = part.ComponentDefinition.Parameters["testing"];
            double testing = (double)parameter.Value;

            try
            {
                Assert.AreEqual(16, testing);
            }
            finally
            {
                doc.Close(true);
            }
        }

        [TestMethod]
        public void Numeric_OverwritesExisting()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            doc.SetParameterValue("testing", "16", "cm");
            doc.SetParameterValue("testing", "19", "cm");

            PartDocument part = (PartDocument)doc;
            Parameter parameter = part.ComponentDefinition.Parameters["testing"];
            double testing = (double)parameter.Value;

            try
            {
                Assert.AreEqual(19, testing);
            }
            finally
            {
                doc.Close(true);
            }
        }

        [TestMethod]
        public void Text_CreatesNew()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            doc.SetParameterValue("testing", "This is a test");

            PartDocument part = (PartDocument)doc;
            Parameter parameter = part.ComponentDefinition.Parameters["testing"];
            var testing = parameter.Value;

            try
            {
                Assert.AreEqual("This is a test", testing);
            }
            finally
            {
                doc.Close(true);
            }
        }

        [TestMethod]
        public void Text_OverwritesExisting()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            doc.SetParameterValue("testing", "This is a test");
            doc.SetParameterValue("testing", "This is a test60");

            PartDocument part = (PartDocument)doc;
            Parameter parameter = part.ComponentDefinition.Parameters["testing"];
            var testing = parameter.Value;

            try
            {
                Assert.AreEqual("This is a test60", testing);
            }
            finally
            {
                doc.Close(true);
            }
        }


        [TestMethod]
        public void Bool_CreatesNew()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            doc.SetParameterValue("testing", true);

            PartDocument part = (PartDocument)doc;
            Parameter parameter = part.ComponentDefinition.Parameters["testing"];
            var testing = parameter.Value;

            try
            {
                Assert.AreEqual(true, testing);
            }
            finally
            {
                doc.Close(true);
            }
        }

        [TestMethod]
        public void Bool_OverwritesExisting()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            doc.SetParameterValue("testing", true);
            doc.SetParameterValue("testing", false);

            PartDocument part = (PartDocument)doc;
            Parameter parameter = part.ComponentDefinition.Parameters["testing"];
            var testing = parameter.Value;

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

    [TestClass]
    public class GetParameterValue
    {
        [TestMethod]
        public void GoodInput_ReturnsValue()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            doc.SetParameterValue("testing", "16", "cm");

            var testing = doc.GetParameterValue("testing");

            try
            {
                Assert.AreEqual("16.000 cm", testing);
            }
            finally
            {
                doc.Close(true);
            }

        }


        [TestMethod]
        public void NoParameter_ReturnsEmpty()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            var testing = doc.GetParameterValue("testing");

            try
            {
                Assert.AreEqual("", testing);
            }
            finally
            {
                doc.Close(true);
            }
        }
    }

    [TestClass]
    public class RemoveParameter
    {

        [TestMethod]
        public void Works()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            doc.SetParameterValue("testing", "16", "cm");
            doc.RemoveParameter("testing");
            string testing = doc.GetParameterValue("testing");

            try
            {
                Assert.AreEqual("", testing);
            }
            finally
            {
                doc.Close(true);
            }
        }

    }

    [TestClass]
    public class ParameterIsWritable
    {
        [TestMethod]
        public void GoodInput_ReturnsTrue()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

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

        [TestMethod]
        public void NoParameter_ReturnsFalse()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

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
