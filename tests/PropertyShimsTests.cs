using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Inventor;
using static InventorShims.PropertyShims;
using InventorShims;


namespace InventorShimsTest
{
    [TestClass]
    public class  PropertyShimsTest
    {


        [TestMethod]
        public void IsPropertyNative_GoodInput()
        {
            
            Assert.IsTrue(PropertyShims.IsPropertyNative("Title"));

        }

        [TestMethod]
        public void IsPropertyNative_BadInput()
        {

            Assert.IsFalse(PropertyShims.IsPropertyNative("ThisShouldBeFalse"));

        }

        [TestMethod]
        public void IsPropertyGetProperty_ShortGoodInput()
        {
            
            Assert.IsFalse(PropertyShims.IsPropertyNative("ThisShouldBeFalse"));

        }

        [TestMethod]
        public void GetProperty_SetProperty_short()
        {
            var getInventor = GetInventor.Instance;
            var app = GetInventor.Application;
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            var doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);
            
            doc.SetProperty("Title", "Bob");
            
            Assert.AreEqual(doc.GetProperty("Title"), "Bob");
        }


    }
}
