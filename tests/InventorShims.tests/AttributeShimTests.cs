using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventorShims;
using Inventor;

namespace AttributeShim_Tests
{
    [TestClass]
    public class AttributeExistss
    {
        [TestMethod]
        public void AttDoesNotExist_returnsFalse()
        {

            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            var result = AttributeShim.AttributeExists(doc, "testSet", "testAttribute");
            try
            {
                Assert.IsFalse(result);
            }
            finally { doc.Close(true); }

        }

        [TestMethod]
        public void AttDoesExist_returnsTrue()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

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

    [TestClass]
    public class AttributeSetExists
    {
        [TestMethod]
        public void DoesNotExist_returnsFalse()
        {

            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            var result = AttributeShim.AttributeSetExists(doc, "testSet");
            try
            {
                Assert.IsFalse(result);
            }
            finally { doc.Close(true); }

        }

        [TestMethod]
        public void DoesExist_returnsTrue()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

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

    [TestClass]
    public class ObjectIsAttributeCapable
    {
        [TestMethod]
        public void returnsFalse()
        {
            Inventor.Application app = ApplicationShim.Instance();

            //Application object is not attribute capable.
            var result = AttributeShim.ObjectIsAttributeCapable(app);
            try
            {
                Assert.IsFalse(result);
            }
            finally {  }

        }

        [TestMethod]
        public void returnsTrue()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;

            //Document object is attribute capable.
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            var result = AttributeShim.ObjectIsAttributeCapable(doc);
            try
            {
                Assert.IsTrue(result);
            }
            finally { doc.Close(true); }

        }
    }

    [TestClass]
    public class RemoveAttributeSet
    {
        [TestMethod]
        public void RemovesSetThatExists()
        {

            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

            //create the test AttributeSet
            AttributeSet attributeSet = doc.AttributeSets.Add("testSet");
            AttributeShim.RemoveAttributeSet(doc, "testSet");

            bool result = false;
            foreach (AttributeSet i in doc.AttributeSets)
            {
                if (i.Name.Equals("testSet")) {result = true;}
            }

            try
            {
                Assert.IsFalse(result);
            }
            finally { doc.Close(true); }

        }

        [TestMethod]
        public void RemovesSetThatDoesntExist_noCrash()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

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

    [TestClass]
    public class CreateAttributeSet
    {
        [TestMethod]
        public void CreateSetThatExists_noCrash()
        {

            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

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

        [TestMethod]
        public void CreateSetThatDoesntExist()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

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

    [TestClass]
    public class RemoveAttribute
    {
        [TestMethod]
        public void RemoveExisting_works()
        {

            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

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

        [TestMethod]
        public void RemovesNonExisting_noCrash()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

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

    [TestClass]
    public class GetAttributeValue
    {
        [TestMethod]
        public void GetExisting_works()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);


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

        [TestMethod]
        public void ModifiesExisting_works()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);


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

    [TestClass]
    public class SetAttributeValue
    {
        [TestMethod]
        public void String_SetNew()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);


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

        [TestMethod]
        public void String_SetExisting()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);


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


        [TestMethod]
        public void Bool_SetNew()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

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

        [TestMethod]
        public void Bool_SetExisting()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);

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

        [TestMethod]
        public void Integer_SetNew()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);


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

        [TestMethod]
        public void Integer_SetExisting()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);


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


        [TestMethod]
        public void Double_SetNew()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);


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

        [TestMethod]
        public void Double_SetExisting()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);


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

