using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InventorShims;
using Inventor;
using System.Windows.Forms;

namespace ParameterShim_Tests
{
    [TestClass]
    public class ParameterShimTests
    {

        [TestMethod]
        public void SetParameter_Numeric_Sucessful()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            doc.SetParameter("testing", "16", "cm");

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
        public void SetParameter_Text_Sucessful()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            doc.SetParameter("testing", "This is a test");

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
        public void SetParameter_Bool_Sucessful()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            doc.SetParameter("testing", true);

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
        public void GetParameter_GoodInput_ReturnsValue()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            doc.SetParameter("testing", "16", "cm");

            var testing = doc.GetParameter("testing");

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
        public void GetParameter_NoParameter_ReturnsEmpty()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            var testing = doc.GetParameter("testing");

            try
            {
                Assert.AreEqual("", testing);
            }
            finally
            {
                doc.Close(true);
            }
        }

        [TestMethod]
        public void RemoveParameter_Works()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            doc.SetParameter("testing", "16", "cm");
            doc.RemoveParameter("testing");
            string testing = doc.GetParameter("testing");

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
}
