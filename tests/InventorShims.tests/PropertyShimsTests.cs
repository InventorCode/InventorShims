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

            string test = "Bob";
            doc.SetPropertyValue("Stuff", test);
            string result = (string)doc.PropertySets["Inventor User Defined Properties"]["Stuff"].Value;

            try
            {
                Assert.AreEqual(test, result);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void long_native()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            string test = "Bob";
            doc.SetPropertyValue("Inventor Summary Information", "Title", test);
            string result = (string)doc.PropertySets["Inventor Summary Information"]["Title"].Value;

            try
            {
                Assert.AreEqual(test, result);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void long_custom()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            string test = "Bob";
            doc.SetPropertyValue("Inventor User Defined Properties", "Stuff", test);
            string result = (string)doc.PropertySets["Inventor User Defined Properties"]["Stuff"].Value;

            try
            {
                Assert.AreEqual(test, result);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void long_superCustom()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            string test = "Bob";
            doc.SetPropertyValue("Custommm", "Stuff", test);
            string result = (string)doc.PropertySets["Custommm"]["Stuff"].Value;

            try
            {
                Assert.AreEqual(test, result);
            }
            finally { doc.Close(true); }
        }
    }

    [TestFixture]
    public class GetPropertyValue
    {
        [Test]
        public void short_native()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            string test = "Bob";
            doc.PropertySets["Inventor Summary Information"]["Title"].Value = test;

            try
            {
                Assert.AreEqual(doc.GetPropertyValue("Title"), test);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void short_custom()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            string test = "Bob";
            doc.PropertySets["Inventor User Defined Properties"].Add(test, "Stuff");

            try
            {
                Assert.AreEqual(doc.GetPropertyValue("Stuff"), test);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void short_doesNotExist()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            string test = "";

            try
            {
                Assert.AreEqual(doc.GetPropertyValue("Bob"), test);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void long_native()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            string test = "Bob";
            doc.PropertySets["Inventor Summary Information"]["Title"].Value = test;

            try
            {
                Assert.AreEqual(doc.GetPropertyValue("Inventor Summary Information", "Title"), test);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void long_custom()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            string test = "Bob";
            doc.PropertySets["Inventor User Defined Properties"].Add(test, "Stuff");

            try
            {
                Assert.AreEqual(doc.GetPropertyValue("Inventor User Defined Properties", "Stuff"), test);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void long_doesNotExist()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            string test = "";

            try
            {
                Assert.AreEqual(doc.GetPropertyValue("Inventor User Defined Properties", "Bob"), test);
            }
            finally { doc.Close(true); }
        }
    }

    [TestFixture]
    public class GetProperty
    {
        [Test]
        public void short_native()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            Property test = doc.PropertySets["Inventor Summary Information"]["Title"];
            Property result = doc.GetProperty("Title");

            try
            {
                Assert.AreEqual(test, result);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void short_doesNotExist()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            Property test = doc.GetProperty("Bob");

            try
            {
                Assert.IsNull(test);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void short_custom()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            doc.PropertySets["Inventor User Defined Properties"].Add("Bob", "Stuff");
            Property test = doc.PropertySets["Inventor User Defined Properties"]["Stuff"];

            try
            {
                Assert.AreEqual(doc.GetProperty("Stuff"), test);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void long_native()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            Property test = doc.PropertySets["Inventor Summary Information"]["Title"];
            Property result = doc.GetProperty("Inventor Summary Information", "Title");

            try
            {
                Assert.AreEqual(test, result);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void long_doesNotExist()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            Property test = doc.GetProperty("Inventor Summary Information", "Bob");

            try
            {
                Assert.IsNull(test);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void long_custom()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            doc.PropertySets["Inventor User Defined Properties"].Add("Bob", "Stuff");
            Property test = doc.PropertySets["Inventor User Defined Properties"]["Stuff"];

            try
            {
                Assert.AreEqual(doc.GetProperty("Inventor User Defined Properties", "Stuff"), test);
            }
            finally { doc.Close(true); }
        }
    }
}