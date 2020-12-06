using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Inventor;
using static InventorShims.Extension_Collections.PropertyShims;
using InventorShims.Extension_Collections;

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

            Inventor.ApprenticeServerComponent _apprentice = new Inventor.ApprenticeServerComponent();
            ApprenticeServerDocument document = _apprentice.Open(@"T:\$Work\Part1.ipt");
            document.Close();


            //            apprentice = null;
            //            Assert.IsFalse(PropertyShims.GetProperty("ThisShouldBeFalse"));
            Assert.IsFalse(PropertyShims.IsPropertyNative("ThisShouldBeFalse"));

        }

    }
}
