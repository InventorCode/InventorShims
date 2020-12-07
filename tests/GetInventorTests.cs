using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InventorShims;

namespace InventorShimsTest
{
    [TestClass]
    public class GetInventorTests
    {
        [TestMethod]
        public void GetInventor_StartApplication()
        {

            GetInventor getInventor = GetInventor.Instance;
            Inventor.Application app = GetInventor.Application;
            
           // Assert.IsTrue(app.Ready);

            getInventor = null;
            app = null;

            getInventor = GetInventor.Instance;
            app = GetInventor.Application;

            Assert.IsTrue(app.Ready);


            //Code to open Apprentice Server below...
            //Inventor.ApprenticeServerComponent oSvr = new Inventor.ApprenticeServerComponent();
            //Inventor.ApprenticeServerDocument oAppDoc;
            //Inventor.ApprenticeServerDrawingDocument oDwgDoc;

            //    ApprenticeServerDocument document = _apprentice.Open(@"T:\$Work\Part1.ipt");
            //    document.Close();
            //    _apprentice = null;
            //}
            //catch (System.Exception e) { }
        }
    }
}
