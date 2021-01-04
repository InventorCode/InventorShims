using System;
using System.Collections.Generic;
using Inventor;

namespace InventorShims
{
    /// <summary>
    /// Static class to manipulate iproperties for a provided Document object.
    /// </summary>
    public static class PropertyShim
    {
            private static Dictionary<string, string> NativePropertyLookup = new Dictionary<string, string>()
        {
            {"Title", "Inventor Summary Information"},
            {"Subject", "Inventor Summary Information"},
            {"Author", "Inventor Summary Information"},
            {"Keywords", "Inventor Summary Information"},
            {"Comments", "Inventor Summary Information"},
            {"Last Saved By", "Inventor Summary Information"},
            {"Revision Number", "Inventor Summary Information"},
            {"Thumbnail", "Inventor Summary Information"},
            {"Category", "Inventor Document Summary Information"},
            {"Manager", "Inventor Document Summary Information"},
            {"Company", "Inventor Document Summary Information"},
            {"Creation Time", "Design Tracking Properties"},
            {"Part Number", "Design Tracking Properties"},
            {"Project", "Design Tracking Properties"},
            {"Cost Center", "Design Tracking Properties"},
            {"Checked By", "Design Tracking Properties"},
            {"Date Checked", "Design Tracking Properties"},
            {"Engr Approved By", "Design Tracking Properties"},
            {"Engr Date Approved", "Design Tracking Properties"},
            {"User Status", "Design Tracking Properties"},
            {"Material", "Design Tracking Properties"},
            {"Part Property Revision Id", "Design Tracking Properties"},
            {"Catalog Web Link", "Design Tracking Properties"},
            {"Part Icon", "Design Tracking Properties"},
            {"Description", "Design Tracking Properties"},
            {"Vendor", "Design Tracking Properties"},
            {"Document SubType", "Design Tracking Properties"},
            {"Document SubType Name", "Design Tracking Properties"},
            {"Proxy Refresh Date", "Design Tracking Properties"},
            {"Mfg Approved By", "Design Tracking Properties"},
            {"Mfg Date Approved", "Design Tracking Properties"},
            {"Cost", "Design Tracking Properties"},
            {"Standard", "Design Tracking Properties"},
            {"Design Status", "Design Tracking Properties"},
            {"Designer", "Design Tracking Properties"},
            {"Engineer", "Design Tracking Properties"},
            {"Authority", "Design Tracking Properties"},
            {"Parameterized Template", "Design Tracking Properties"},
            {"Template Row", "Design Tracking Properties"},
            {"External Property Revision Id", "Design Tracking Properties"},
            {"Standard Revision", "Design Tracking Properties"},
            {"Manufacturer", "Design Tracking Properties"},
            {"Standards Organization", "Design Tracking Properties"},
            {"Language", "Design Tracking Properties"},
            {"Defer Updates", "Design Tracking Properties"},
            {"Size Designation", "Design Tracking Properties"},
            {"Categories", "Design Tracking Properties"},
            {"Stock Number", "Design Tracking Properties"},
            {"Weld Material", "Design Tracking Properties"},
            {"Mass", "Design Tracking Properties"},
            {"SurfaceArea", "Design Tracking Properties"},
            {"Volume", "Design Tracking Properties"},
            {"Density", "Design Tracking Properties"},
            {"Valid MassProps", "Design Tracking Properties"},
            {"Flat Pattern Width", "Design Tracking Properties"},
            {"Flat Pattern Length", "Design Tracking Properties"},
            {"Flat Pattern Area", "Design Tracking Properties"},
            {"Sheet Metal Rule", "Design Tracking Properties"},
            {"Last Updated With", "Design Tracking Properties"},
            {"Sheet Metal Width", "Design Tracking Properties"},
            {"Sheet Metal Length", "Design Tracking Properties"},
            {"Sheet Metal Area", "Design Tracking Properties"},
            {"Material Identifier", "Design Tracking Properties"},
            {"Appearance", "Design Tracking Properties"},
            {"Flat Pattern Defer Update", "Design Tracking Properties"}
        };


            private static HashSet<string> NativePropertySetLookup = new HashSet<string>
        {
            "Inventor Summary Information",
            "Inventor Document Summary Information",
            "Design Tracking Properties",
            "Inventor User Defined Properties"
        };

        /// <summary>
        /// Returns a boolean indicating if the document contains custom PropertySets
        /// </summary>
        /// <param name="propertySets">PropertySet object</param>
        /// <returns>Boolean</returns>
        private static bool UserPropertySetsExist(PropertySets propertySets)
        {
            return propertySets.Count >= NativePropertySetLookup.Count ? true : false;

        }

        /// <summary>
        /// Returns a property object for a user-created custom property.
        /// </summary>
        /// <param name="document">Inventor.Document</param>
        /// <param name="propertyName"></param>
        /// <returns>Property</returns>
        private static Property GetSuperCustomProperty(this Inventor.Document document, string propertyName)
        {
            foreach (PropertySet i in document.PropertySets)
            {
                //ignore stock propertySets
                if (NativePropertySetLookup.Contains(i.DisplayName))
                { continue; }
                foreach (Property j in i)
                {
                    if (j.DisplayName == propertyName)
                    {
                        return j;
                    }
                }
            }
            return null;
        }

            /// <summary>
            /// Returns a string if the user propertySets contain the specified property name.
            /// </summary>
            /// <param name="document"></param>
            /// <param name="propertyName"></param>
            /// <returns>string</returns>
            private static object GetSuperCustomPropertyValue(Inventor.Document document, string propertyName)
            {

                Property temp = document.GetSuperCustomProperty(propertyName);
                //try
                //    {
                        return temp.Value ?? "";
                //    }
                //catch { return "";}
            }


            /// <summary>
            /// Returns the iProperty Value for a provided document and propertyName. Short signature.
            /// </summary>
            /// <param name="document"></param>
            /// <param name="propertyName"></param>
            /// <returns></returns>
            public static object GetPropertyValue(this Document document, string propertyName)
        {
            Property prop = document.GetProperty(propertyName);
            if (prop is null)
                return "";

            return prop.Value;

        } //End GetPropertyValue

        /// <summary>
        /// Return the property value of a the specified Property within a Document object. Uses a long-form signature, specifying the Parameter Set.
        /// </summary>
        /// <param name="document">Inventor.Document</param>
        /// <param name="setName">Parameter Set name as a string.</param>
        /// <param name="propertyName">Property Name as a string.</param>
        /// <returns>Object</returns>
        public static object GetPropertyValue(this Document document, string setName, string propertyName)
        {
            Property prop = document.GetProperty(setName, propertyName);
            if (prop is null) return "";
            
            return prop.Value;
        }

        /// <summary>
        /// Return the property value of a the specified Property within a Document object. Uses a short-form signature, specifying the Parameter Set.
        /// </summary>
        /// <param name="document">Inventor.Document</param>
        /// <param name="propertyName">Property Name as a string.</param>
        /// <returns>Object</returns>
        public static Property GetProperty(this Document document, string propertyName)
        {
            PropertySets propertySets = document.PropertySets;

            //get the propertySet for the provided propertyName (if it exists)
            if (NativePropertyLookup.TryGetValue(propertyName, out string setName))
                return propertySets[setName][propertyName];

            //not found in the standard properties, search the custom properties
            PropertySet currentPropertySet = propertySets["Inventor User Defined Properties"];
            try
            {
                return currentPropertySet[propertyName];
            }
            catch { };

            //still not found, search other custom property sets!
            if (UserPropertySetsExist(propertySets))
            {
                return GetSuperCustomProperty(document, propertyName) ?? null;
            }

            //still not found, return nothing...
            return null;
        }

        /// <summary>
        /// Returns the specified Property object in a Document if one exists.  If none exists, null is returned.
        /// </summary>
        /// <param name="document">Inventor.Document</param>
        /// <param name="setName">Property Set Name as a string.</param>
        /// <param name="propertyName">Property Name as a string.</param>
        /// <returns>Property</returns>
        public static Property GetProperty(this Document document, string setName, string propertyName)
        {
            try
            {
            Property prop = document.PropertySets[setName][propertyName];
            return prop ?? null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// A simple static function that returns true if the specified property is one of Inventor's built-in iProperties.
        /// </summary>
        /// <param name="name">Property Name as a string.</param>
        /// <returns>Boolean</returns>
        public static bool IsPropertyNative(string name)
        {
            return NativePropertyLookup.ContainsKey(name);
        }

        /// <summary>
        /// Set the specified document property's value.  If the iproperty name exist it will set the value. 
        /// If the name does not exist, it will add the property with the value you have specified in the 
        /// "User Defined Properties" property set.  This is the short-form signature.
        /// </summary>
        /// <param name="document">Inventor.Document</param>
        /// <param name="propertyName">Property Name as a string</param>
        /// <param name="value">Property value</param>
        public static void SetPropertyValue(this Document document, string propertyName, Object value)
        {
            PropertySets propertySets = document.PropertySets;
            
            //If the property exists as a built-in property, set the value
            if (NativePropertyLookup.TryGetValue(propertyName, out string setName))
            {
                try
                {
                    propertySets[setName][propertyName].Value = value;
                    return;
                }
                catch {};
            }

            //The property was not found in standard properties.  Search the custom properties...
            PropertySet i = propertySets["Inventor User Defined Properties"];
            try
            {
                i[propertyName].Value = value;
                return;
            }
            catch
            {
                i.Add(value, propertyName);
            }
        }

        /// <summary>
        /// Set the specified document property's value.  If the iproperty name exist it will set the value.
        /// If the name does not exist, it will add the property with the value you have specified in the
        /// "User Defined Properties" property set.  The long signature function must be used to specify custom
        /// property groups.
        /// </summary>
        /// <param name="document">Inventor.Document</param>
        /// <param name="propertySetName">Property Set Name as a string</param>
        /// <param name="propertyName">Property Name as a string</param>
        /// <param name="value"></param>
        public static void SetPropertyValue(this Document document, string propertySetName, string propertyName, object value)
        {
            PropertySets documentPropertySets = document.PropertySets;

            //If the property set exists, set the value, or add it if needed...
            if (PropertySetExists(document, propertySetName))
            {
                try
                {
                    documentPropertySets[propertySetName][propertyName].Value = value;
                    return;
                }
                catch
                {
                    documentPropertySets[propertySetName].Add(value, propertyName);
                }
            }
            else
            {
                try
                {
                    documentPropertySets.Add(propertySetName);
                    documentPropertySets[propertySetName].Add(value, propertyName);
                }
                catch { };
            }
        }

        /// <summary>
        /// Removes the specified document property.  If the property is native, it will only delete the iproperty's value.
        /// This is the short-form signature version of this method.
        /// </summary>
        /// <param name="document">Inventor.Document</param>
        /// <param name="propertyName">Property Name as a string</param>
        public static void RemoveProperty(this Document document, string propertyName)
        {
            PropertySets propertySets = document.PropertySets;

            //If the property exists as a built-in property, remove the value
            if (NativePropertyLookup.TryGetValue(propertyName, out string setName))
            {
                try
                {
                    propertySets[setName][propertyName].Value = "";
                    return;
                }
                catch { };
            }

            //The property was not found in standard properties.  Search the custom properties...
            PropertySet i = propertySets["Inventor User Defined Properties"];
            try
            {
                i[propertyName].Delete();
                return;
            }
            catch {}

            //still not found, search other custom property sets!
            if (UserPropertySetsExist(propertySets))
                foreach (PropertySet j in document.PropertySets)
                {
                    if (NativePropertySetLookup.Contains(j.DisplayName))
                        return;

                    try
                    {
                        j[propertyName].Delete();
                        return;
                    }
                    catch { };
                }
        }

        /// <summary>
        /// Removes the specified document property.  If the property is native, it will only delete the iproperty's value.
        /// This is the long-form signature version of this method.
        /// </summary>
        /// <param name="document">Inventor.Document</param>
        /// <param name="propertySetName">Property Set Name as a string</param>
        /// <param name="propertyName">Property Name as a string</param>
        public static void RemoveProperty(this Document document, string propertySetName, string propertyName)
        {
            PropertySets documentPropertySets = document.PropertySets;

            //If the property set exists...
            if (PropertySetExists(document, propertySetName))
            {
                //remove it...
                try
                {
                    documentPropertySets[propertySetName][propertyName].Delete();
                    return;
                }
                catch {}
                
                //if property still exists...
                if (CustomPropertyExists(documentPropertySets[propertySetName], propertyName))
                {
                    //set it to ""
                    try
                    {
                        documentPropertySets[propertySetName][propertyName].Value = "";
                        return;
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// A simple function that returns true/false if the specified property exists in the document.
        /// This is the short-form signature of this function.
        /// </summary>
        /// <param name="document">Inventor.Document</param>
        /// <param name="propertyName">Property Name as a string</param>
        /// <returns>Boolean</returns>
        public static bool PropertyExists(this Document document, string propertyName)
        {

            if (IsPropertyNative(propertyName))
                return true;

            //The property was not found in standard properties.  Search the custom properties...
            PropertySet i = document.PropertySets["Inventor User Defined Properties"];
            try
            {
                var temp = i[propertyName].Value;
                return true;
            }
            catch { }

            //still not found, search other custom property sets!
            if (UserPropertySetsExist(document.PropertySets))
                foreach (PropertySet j in document.PropertySets)
                {
                    try
                    {
                        j[propertyName].Delete();
                        return true;
                    }
                    catch { };
                }
            return false;
        }

        /// <summary>
        /// A simple function that returns true/false if the specified property exists in the document.
        /// This is the short-form signature of this function.
        /// </summary>
        /// <param name="document">Inventor.Document</param>
        /// <param name="propertySetName">Property Set Name as a string</param>
        /// <param name="propertyName">Property Name as a string</param>
        /// <returns>Boolean</returns>
        public static bool PropertyExists(this Document document, string propertySetName, string propertyName)
        {
            //get the propertySet for the provided propertyName (if it exists)
            object a;
            try
            {
                a = document.PropertySets[propertySetName][propertyName].Value;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// A simple function that returns true/false if the specified custom property exists in the PropertySet.
        /// </summary>
        /// <param name="currentPropertySet">Property Set Name as a string</param>
        /// <param name="propertyName">Property Name as a string</param>
        /// <returns>Boolean</returns>
        static bool CustomPropertyExists(PropertySet currentPropertySet, string propertyName)
        {
            object a;
            try
            {
                a = currentPropertySet[propertyName];
                return true;
            }
            catch
            {
                return false;
            };
        }

        static bool PropertySetExists(Document document, string propertySetName)
        {
            foreach (PropertySet propertySet in document.PropertySets)
            {
                if (String.Equals(propertySet.Name, propertySetName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
