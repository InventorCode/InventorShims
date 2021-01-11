using System;
using Inventor;

namespace InventorShims
{
    /// <summary>
    /// A collection of static methods that accesses attributes
    /// </summary>
    public static class AttributeShim
    {

        /// <summary>
        /// Sets the value of a specified attribute in the provided object. The attribute is specified by it's
        /// name. If no such attribute exists, one is created. If the containing object is not attribute
        /// capable, the method will throw a system exception.  This signature accepts a string value.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="attributeSetName">AttributeSet name as a string</param>
        /// <param name="attributeName">Attribute name as a string</param>
        /// <param name="value">Attribute value as a string</param>
        public static void SetAttributeValue(this object obj, string attributeSetName, string attributeName, string value)
        {
            SetAttributeValueEngine(obj, attributeSetName, attributeName, value, Inventor.ValueTypeEnum.kStringType);
        }


        /// <summary>
        /// Sets the value of a specified attribute in the provided object. The attribute is specified by it's
        /// name. If no such attribute exists, one is created. If the containing object is not attribute
        /// capable, the method will throw a system exception.  This signature accepts an integer.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="attributeSetName">AttributeSet name as a string</param>
        /// <param name="attributeName">Attribute name as a string</param>
        /// <param name="value">Attribute value as an integer</param>
        public static void SetAttributeValue(this object obj, string attributeSetName, string attributeName, int value)
        {
            SetAttributeValueEngine(obj, attributeSetName, attributeName, value, Inventor.ValueTypeEnum.kIntegerType);
        }


        /// <summary>
        /// Sets the value of a specified attribute in the provided object. The attribute is specified by it's
        /// name. If no such attribute exists, one is created. If the containing object is not attribute
        /// capable, the method will throw a system exception.  This signature accepts a System.Boolean
        /// value.  Note that the Inventor.Parameter object does not utilize a true boolean value, but instead a
        /// ValueTypeEnum.kBooleanType which is actually an integer value of 0 or 1.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="attributeSetName">AttributeSet name as a string</param>
        /// <param name="attributeName">Attribute name as a string</param>
        /// <param name="value">Attribute value as a Boolean</param>
        public static void SetAttributeValue(this object obj, string attributeSetName, string attributeName, bool value)
        {
            int i = value ? 1 : 0;

            SetAttributeValueEngine(obj, attributeSetName, attributeName, i, ValueTypeEnum.kBooleanType);
        }

        /// <summary>
        /// Sets the value of a specified attribute in the provided object. The attribute is specified by it's
        /// name. If no such attribute exists, one is created. If the containing object is not attribute
        /// capable, the method will throw a system exception.  This signature accepts a double value.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="attributeSetName">AttributeSet name as a string</param>
        /// <param name="attributeName">Attribute name as a string</param>
        /// <param name="value">Attribute value as a double</param>
        public static void SetAttributeValue(this object obj, string attributeSetName, string attributeName, double value)
        {
            SetAttributeValueEngine(obj, attributeSetName, attributeName, value, Inventor.ValueTypeEnum.kDoubleType);
        }

        //    Shared Sub SetAttributeValue(obj As Object, attributeSetName As String, attribute As String, value As Byte())
        //        SetAttributeValueEngine(obj, attributeSetName, attribute, value, Inventor.ValueTypeEnum.kByteArrayType)
        //    End Sub

        private static void SetAttributeValueEngine(object obj, string attributeSetName, string attributeName, dynamic value, ValueTypeEnum valueType)
        {
            if (!ObjectIsAttributeCapable(obj))
            {
                throw new SystemException("The selected object is not attribute-capable.");
            }

            Inventor.AttributeSet attributeSets;

            attributeSets = CreateAttributeSet(obj, attributeSetName);

            if (AttributeExists(obj, attributeSetName, attributeName))
            {
                attributeSets[attributeName].Value = value;
            }
            else
            {
                attributeSets.Add(attributeName, valueType, value);
            }
        }


        /// <summary>
        /// This static function will create an Attribute Set for the provided object if one with that name
        /// does not already exist.  The newly created AttributeSet object is returned.  If the object is not
        /// attribute capable, the function will throw a system exception.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="attributeSetName">AttributeSet name as a string</param>
        /// <returns></returns>
        public static Inventor.AttributeSet CreateAttributeSet(this object obj, string attributeSetName)
        {
            dynamic temp = (dynamic)obj;

            if (!ObjectIsAttributeCapable(obj))
            {
                throw new SystemException("The selected object is not attribute-capable.");
            }

            if (AttributeSetExists(obj, attributeSetName))
            {
                
                return temp.AttributeSets[attributeSetName];
            }
            else
            {
                Inventor.AttributeSets attributeSets = temp.AttributeSets;
                return attributeSets.Add(attributeSetName);
            }
        }

        /// <summary>
        /// This static function will return an Attribute's value for the provided object and Attribute name.
        /// If the specified attribute does not exist, an empty string is returned. If the object is not
        /// attribute capable, the function will throw a system exception.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="attributeSetName">AttributeSet name as a string</param>
        /// <param name="attributeName">Attribute name as a string</param>
        /// <returns></returns>
        public static object GetAttributeValue(this object obj, string attributeSetName, object attributeName)
        {
            dynamic temp = (dynamic)obj;

            if (AttributeExists(obj, attributeSetName, (string)attributeName))
            {
                AttributeSets attributeSets = temp.AttributeSets;
                AttributeSet attributeSet = attributeSets[attributeSetName];
                return attributeSet[attributeName].Value;
            }
            else
            {
                return "";
            }

        }

        /// <summary>
        /// This static method will remove a specified Attribute from the provided Object if one exists.
        ///  If the object is not attribute capable, the function will throw a system exception.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="attributeSetName">AttributeSet name as a string</param>
        /// <param name="attributeName">Attribute name as a string</param>
        public static void RemoveAttribute(this object obj, string attributeSetName, object attributeName)
        {
            dynamic temp = (dynamic)obj;

            if (AttributeExists(obj, attributeSetName, (string)attributeName))
            {
                AttributeSets attributeSets = temp.AttributeSets;
                AttributeSet attributeSet = attributeSets[attributeSetName];
                attributeSet[attributeName].Delete();
            }
        }


        /// <summary>
        /// This static method will remove a specified AttributeSet from the provided Object if one exists.
        ///  If the object is not attribute capable, the function will throw a system exception.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="attributeSetName">AttributeSet name as a string</param>
        public static void RemoveAttributeSet(this object obj, string attributeSetName)
        {
            dynamic temp = (dynamic)obj;

            if (AttributeSetExists(obj, attributeSetName))
            {
                AttributeSets attributeSets = temp.AttributeSets;
                attributeSets[attributeSetName].Delete();
            }
        }


        /// <summary>
        /// This static function will return boolean value indicating if the specified AttributeSet
        /// exists in the provided object. If the object is not attribute capable, the function
        /// will throw a system exception.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="attributeSetName">AttributeSet name as a string</param>
        public static bool AttributeSetExists(this object obj, string attributeSetName)
        {
            dynamic temp = (dynamic)obj;

            if (!ObjectIsAttributeCapable(obj))
            {
                return false;
            }

            AttributeSets attributeSets = temp.AttributeSets;
            return attributeSets.NameIsUsed[attributeSetName];

        }


        /// <summary>
        /// This static function will return boolean value indicating if the specified Attribute
        /// exists in the provided object. If the object is not attribute capable, the function
        /// will throw a system exception.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="attributeSetName">AttributeSet name as a string</param>
        /// <param name="attributeName">Attribute name as a string</param>
        public static bool AttributeExists(this object obj, string attributeSetName, string attributeName)
        {
            dynamic temp = (dynamic)obj;

            if (!ObjectIsAttributeCapable(obj))
            {
                return false;
            }

            if (!AttributeSetExists(obj, attributeSetName))
            {
                return false;
            }

            AttributeSets attributeSets = temp.AttributeSets;
            AttributeSet attributeSet = attributeSets[attributeSetName];

            return attributeSet.NameIsUsed[attributeName];
        }

        /// <summary>
        /// This static function will return boolean value indicating if the specified Object
        /// is Attribute capable.
        /// </summary>
        /// <param name="obj">Object</param>
        public static bool ObjectIsAttributeCapable(this object obj)
        {
            dynamic temp = (dynamic)obj;
            AttributeSets attributeSets;

            try
            {
                attributeSets = temp.AttributeSets;
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
