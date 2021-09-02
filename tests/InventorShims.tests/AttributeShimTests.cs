using Inventor;
using InventorShims;
using NUnit.Framework;

namespace AttributeShim_Tests
{
    [TestFixture]
    public class AttributeExists
    {
        [Test]
        public void AttDoesNotExist_returnsFalse()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            var result = AttributeShim.AttributeExists(doc, "testSet", "testAttribute");

            try
            {
                Assert.IsFalse(result);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void AttDoesExist_returnsTrue()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            //create the test attribute
            AttributeSet attributeSet = doc.AttributeSets.Add("testSet");
            Attribute _ = attributeSet.Add("testAttribute", ValueTypeEnum.kStringType, "test string");

            var result = AttributeShim.AttributeExists(doc, "testSet", "testAttribute");
            try
            {
                Assert.IsTrue(result);
            }
            finally { doc.Close(true); }
        }
    }

    [TestFixture]
    public class AttributeSetExists
    {
        [Test]
        public void DoesNotExist_returnsFalse()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            var result = AttributeShim.AttributeSetExists(doc, "testSet");
            try
            {
                Assert.IsFalse(result);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void DoesExist_returnsTrue()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            //create the test attribute
            AttributeSet attributeSet = doc.AttributeSets.Add("testSet");

            var result = AttributeShim.AttributeSetExists(doc, "testSet");
            try
            {
                Assert.IsTrue(result);
            }
            finally { doc.Close(true); }
        }
    }

    [TestFixture]
    public class ObjectIsAttributeCapable
    {
        [Test]
        public void returnsFalse()
        {
            Inventor.Application app = ApplicationShim.Instance();

            //Application object is not attribute capable.
            var result = AttributeShim.ObjectIsAttributeCapable(app);
            try
            {
                Assert.IsFalse(result);
            }
            finally { }
        }

        [Test]
        public void returnsTrue()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            var result = AttributeShim.ObjectIsAttributeCapable(doc);
            try
            {
                Assert.IsTrue(result);
            }
            finally { doc.Close(true); }
        }
    }

    [TestFixture]
    public class RemoveAttributeSet
    {
        [Test]
        public void RemovesSetThatExists()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            //create the test AttributeSet
            AttributeSet attributeSet = doc.AttributeSets.Add("testSet");
            AttributeShim.RemoveAttributeSet(doc, "testSet");

            bool result = false;
            foreach (AttributeSet i in doc.AttributeSets)
            {
                if (i.Name.Equals("testSet")) { result = true; }
            }

            try
            {
                Assert.IsFalse(result);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void RemovesSetThatDoesntExist_noCrash()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            //try to remove an attributeSet that does not exist.  Will this crash?
            AttributeShim.RemoveAttributeSet(doc, "testSet");

            bool result = false;
            foreach (AttributeSet i in doc.AttributeSets)
            {
                if (i.Name.Equals("testSet")) { result = true; }
            }
            try
            {
                Assert.IsFalse(result);
            }
            finally { doc.Close(true); }
        }
    }

    [TestFixture]
    public class CreateAttributeSet
    {
        [Test]
        public void CreateSetThatExists_noCrash()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            //create the test AttributeSet
            AttributeSet attributeSet = doc.AttributeSets.Add("testSet");
            AttributeShim.CreateAttributeSet(doc, "testSet");

            bool result = false;
            foreach (AttributeSet i in doc.AttributeSets)
            {
                if (i.Name.Equals("testSet")) { result = true; }
            }

            try
            {
                Assert.IsTrue(result);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void CreateSetThatDoesntExist()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            //try to remove an attributeSet that does not exist.  Will this crash?
            AttributeShim.CreateAttributeSet(doc, "testSet");

            bool result = false;
            foreach (AttributeSet i in doc.AttributeSets)
            {
                if (i.Name.Equals("testSet")) { result = true; }
            }
            try
            {
                Assert.IsTrue(result);
            }
            finally { doc.Close(true); }
        }
    }

    [TestFixture]
    public class RemoveAttribute
    {
        [Test]
        public void RemoveExisting_works()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            //create the test AttributeSet
            AttributeSet attributeSet = doc.AttributeSets.Add("testSet");
            Inventor.Attribute attribute = attributeSet.Add("testAttribute", ValueTypeEnum.kStringType, "test string");

            AttributeShim.RemoveAttribute(doc, "testSet", "testAttribute");

            bool result = false;
            foreach (Inventor.Attribute i in doc.AttributeSets["testSet"])
            {
                if (i.Name.Equals("testAttribute")) { result = true; }
            }

            try
            {
                Assert.IsFalse(result);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void RemovesNonExisting_noCrash()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            //create the test AttributeSet
            AttributeSet attributeSet = doc.AttributeSets.Add("testSet");
            //            Inventor.Attribute attribute = attributeSet.Add("testAttribute", ValueTypeEnum.kStringType, "test string");

            AttributeShim.RemoveAttribute(doc, "testSet", "testAttribute");

            bool result = false;
            foreach (Inventor.Attribute i in doc.AttributeSets["testSet"])
            {
                if (i.Name.Equals("testAttribute")) { result = true; }
            }

            try
            {
                Assert.IsFalse(result);
            }
            finally { doc.Close(true); }
        }
    }

    [TestFixture]
    public class GetAttributeValue
    {
        [Test]
        public void GetExisting_works()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            //create the test Attribute
            var test = "test string";
            AttributeSet attributeSet = doc.AttributeSets.Add("testSet");
            Inventor.Attribute attribute = attributeSet.Add("testAttribute", ValueTypeEnum.kStringType, test);

            var result = AttributeShim.GetAttributeValue(doc, "testSet", "testAttribute");

            try
            {
                Assert.AreEqual(result, test);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void ModifiesExisting_works()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            //create the test Attribute
            var test = "test string";
            AttributeSet attributeSet = doc.AttributeSets.Add("testSet");
            Inventor.Attribute attribute = attributeSet.Add("testAttribute", ValueTypeEnum.kStringType, "this is a thing");

            attribute.Value = test;

            var result = AttributeShim.GetAttributeValue(doc, "testSet", "testAttribute");

            try
            {
                Assert.AreEqual(result, test);
            }
            finally { doc.Close(true); }
        }
    }

    [TestFixture]
    public class SetAttributeValue
    {
        [Test]
        public void String_SetNew()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            //create the test Attribute
            var test = "test string";

            AttributeShim.SetAttributeValue(doc, "testSet", "testAttribute", test);
            var result = doc.AttributeSets["testSet"]["testAttribute"].Value;

            try
            {
                Assert.AreEqual(result, test);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void String_SetExisting()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            //create the test Attribute
            var test = "test string";

            AttributeShim.SetAttributeValue(doc, "testSet", "testAttribute", "things stuff");
            AttributeShim.SetAttributeValue(doc, "testSet", "testAttribute", test);
            var result = doc.AttributeSets["testSet"]["testAttribute"].Value;

            try
            {
                Assert.AreEqual(result, test);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void Bool_SetNew()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            //create the test Attribute
            var test = true;
            AttributeShim.SetAttributeValue(doc, "testSet", "testAttribute", test);
            var result = doc.AttributeSets["testSet"]["testAttribute"].Value;

            try
            {
                Assert.AreEqual(result, 1);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void Bool_SetExisting()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            //create the test Attribute
            var test = true;
            AttributeShim.SetAttributeValue(doc, "testSet", "testAttribute", false);
            //change the value
            AttributeShim.SetAttributeValue(doc, "testSet", "testAttribute", test);
            var result = doc.AttributeSets["testSet"]["testAttribute"].Value;

            try
            {
                Assert.AreEqual(result, 1);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void Integer_SetNew()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            //create the test Attribute
            var test = 16;

            AttributeShim.SetAttributeValue(doc, "testSet", "testAttribute", test);
            var result = doc.AttributeSets["testSet"]["testAttribute"].Value;

            try
            {
                Assert.AreEqual(result, test);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void Integer_SetExisting()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            //create the test Attribute
            var test = 16;

            AttributeShim.SetAttributeValue(doc, "testSet", "testAttribute", 19);
            AttributeShim.SetAttributeValue(doc, "testSet", "testAttribute", test);
            var result = doc.AttributeSets["testSet"]["testAttribute"].Value;

            try
            {
                Assert.AreEqual(result, test);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void Double_SetNew()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            //create the test Attribute
            double test = 16.00;

            AttributeShim.SetAttributeValue(doc, "testSet", "testAttribute", test);
            var result = doc.AttributeSets["testSet"]["testAttribute"].Value;

            try
            {
                Assert.AreEqual(result, test);
            }
            finally { doc.Close(true); }
        }

        [Test]
        public void Double_SetExisting()
        {
            var doc = tests.TestUtilities.CreatePartDocument();

            //create the test Attribute
            double test = 16.00;

            AttributeShim.SetAttributeValue(doc, "testSet", "testAttribute", 19.25);
            AttributeShim.SetAttributeValue(doc, "testSet", "testAttribute", test);
            var result = doc.AttributeSets["testSet"]["testAttribute"].Value;

            try
            {
                Assert.AreEqual(result, test);
            }
            finally { doc.Close(true); }
        }
    }
}