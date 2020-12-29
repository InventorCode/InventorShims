using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Inventor;
using InventorShims;


namespace PropertyShimTest
{
    [TestClass]
    public class  IsPropertyNative
    {

        [TestMethod]
        public void GoodInput()
        {
            
            Assert.IsTrue(PropertyShim.IsPropertyNative("Title"));

        }

        [TestMethod]
        public void BadInput()
        {

            Assert.IsFalse(PropertyShim.IsPropertyNative("ThisShouldBeFalse"));

        }

        [TestMethod]
        public void ShortGoodInput()
        {
            
            Assert.IsFalse(PropertyShim.IsPropertyNative("ThisShouldBeFalse"));

        }
    }

    [TestClass]
    public class SetPropertyValue
    {
        [TestMethod]
        public void short_native()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            var doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            string test = "Bob";
            doc.SetPropertyValue("Title", test);
            string result = (string)doc.PropertySets["Inventor Summary Information"]["Title"].Value;

            Assert.AreEqual(test, result);
            doc.Close(true);
        }

        [TestMethod]
        public void short_custom()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            var doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            string test = "Bob";
            doc.SetPropertyValue("Stuff", test);
            string result = (string)doc.PropertySets["Inventor User Defined Properties"]["Stuff"].Value;

            Assert.AreEqual(test, result);
            doc.Close(true);
        }

        [TestMethod]
        public void long_native()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            var doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            string test = "Bob";
            doc.SetPropertyValue("Inventor Summary Information","Title", test);
            string result = (string)doc.PropertySets["Inventor Summary Information"]["Title"].Value;

            Assert.AreEqual(test, result);
            doc.Close(true);
        }

        [TestMethod]
        public void long_custom()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            var doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            string test = "Bob";
            doc.SetPropertyValue("Inventor User Defined Properties", "Stuff", test);
            string result = (string)doc.PropertySets["Inventor User Defined Properties"]["Stuff"].Value;

            Assert.AreEqual(test, result);
            doc.Close(true);
        }

        [TestMethod]
        public void long_superCustom()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            var doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            string test = "Bob";
            doc.SetPropertyValue("Custommm", "Stuff", test);
            string result = (string)doc.PropertySets["Custommm"]["Stuff"].Value;

            Assert.AreEqual(test, result);
            doc.Close(true);
        }
    }

    [TestClass]
    public class GetPropertyValue
    {
        [TestMethod]
        public void short_native()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            var doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            string test = "Bob";
            doc.PropertySets["Inventor Summary Information"]["Title"].Value = test;

            Assert.AreEqual(doc.GetPropertyValue("Title"), test);
            doc.Close(true);
        }


        [TestMethod]
        public void short_custom()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            var doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            string test = "Bob";
            doc.PropertySets["Inventor User Defined Properties"].Add(test, "Stuff");

            Assert.AreEqual(doc.GetPropertyValue("Stuff"), test);
            doc.Close(true);
        }

        [TestMethod]
        public void short_doesNotExist()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            var doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            string test = "";
   
            Assert.AreEqual(doc.GetPropertyValue("Bob"), test);
            doc.Close(true);
        }

        [TestMethod]
        public void long_native()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            var doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            string test = "Bob";
            doc.PropertySets["Inventor Summary Information"]["Title"].Value = test;

            Assert.AreEqual(doc.GetPropertyValue("Inventor Summary Information", "Title"), test);
            doc.Close(true);
        }


        [TestMethod]
        public void long_custom()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            var doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            string test = "Bob";
            doc.PropertySets["Inventor User Defined Properties"].Add(test, "Stuff");

            Assert.AreEqual(doc.GetPropertyValue("Inventor User Defined Properties", "Stuff"), test);
            doc.Close(true);
        }

        [TestMethod]
        public void long_doesNotExist()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            var doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            string test = "";

            Assert.AreEqual(doc.GetPropertyValue("Inventor User Defined Properties", "Bob"), test);
            doc.Close(true);
        }

    }

    [TestClass]
    public class GetProperty
    {
        [TestMethod]
        public void short_native()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            var doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            Property test = doc.PropertySets["Inventor Summary Information"]["Title"];
            Property result = doc.GetProperty("Title");

            Assert.AreEqual(test, result);
            doc.Close(true);
        }

        [TestMethod]
        public void short_doesNotExist()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            var doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            Property test = doc.GetProperty("Bob");

            Assert.IsNull(test);
            doc.Close(true);
        }

        [TestMethod]
        public void short_custom()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            var doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            doc.PropertySets["Inventor User Defined Properties"].Add("Bob", "Stuff");
            Property test = doc.PropertySets["Inventor User Defined Properties"]["Stuff"];

            Assert.AreEqual(doc.GetProperty("Stuff"), test);
            doc.Close(true);
        }

        [TestMethod]
        public void long_native()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            var doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            Property test = doc.PropertySets["Inventor Summary Information"]["Title"];
            Property result = doc.GetProperty("Inventor Summary Information", "Title");

            Assert.AreEqual(test, result);
            doc.Close(true);
        }

        [TestMethod]
        public void long_doesNotExist()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            var doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            Property test = doc.GetProperty("Inventor Summary Information", "Bob");

            Assert.IsNull(test);
            doc.Close(true);
        }

        [TestMethod]
        public void long_custom()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            var doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            doc.PropertySets["Inventor User Defined Properties"].Add("Bob", "Stuff");
            Property test = doc.PropertySets["Inventor User Defined Properties"]["Stuff"];

            Assert.AreEqual(doc.GetProperty("Inventor User Defined Properties", "Stuff"), test);
            doc.Close(true);
        }
    }
}
