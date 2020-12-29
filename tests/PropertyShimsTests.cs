using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Inventor;
using InventorShims;


namespace PropertyShimTest
{
    [TestClass]
    public class  PropertyShimTest
    {

        [TestMethod]
        public void IsPropertyNative_GoodInput()
        {
            
            Assert.IsTrue(PropertyShim.IsPropertyNative("Title"));

        }

        [TestMethod]
        public void IsPropertyNative_BadInput()
        {

            Assert.IsFalse(PropertyShim.IsPropertyNative("ThisShouldBeFalse"));

        }

        [TestMethod]
        public void IsPropertyNative_ShortGoodInput()
        {
            
            Assert.IsFalse(PropertyShim.IsPropertyNative("ThisShouldBeFalse"));

        }

        [TestMethod]
        public void SetPropertyValue_short_native()
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
        public void GetPropertyValue_short_native()
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
        public void SetPropertyValue_short_custom()
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
        public void GetPropertyValue_short_custom()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            var doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            string test = "Bob";
            doc.PropertySets["Inventor User Defined Properties"].Add(test, "Stuff");

            Assert.AreEqual(doc.GetPropertyValue("Stuff"), test);
            doc.Close(true);
        }
    }
}
