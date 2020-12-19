using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Inventor;
using InventorShims;


namespace InventorShimsTest
{
    [TestClass]
    public class  PropertyShimsTest
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
        public void IsPropertyGetProperty_ShortGoodInput()
        {
            
            Assert.IsFalse(PropertyShim.IsPropertyNative("ThisShouldBeFalse"));

        }

        [TestMethod]
        public void GetProperty_SetProperty_short()
        {
            Inventor.Application app = ApplicationShim.Instance();
//            DesignProject designProject = app.DesignProjectManager.DesignProjects.ItemByName["Default"];
//            designProject.Activate();

            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            var doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);
            
            doc.SetProperty("Title", "Bob");

            try
            {
                Assert.AreEqual(doc.GetProperty("Title"), "Bob");
            }
            finally
            {
                app.Quit();
                app = null;
            }
        }


    }
}
