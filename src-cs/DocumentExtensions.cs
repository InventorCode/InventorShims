using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace InventorShims 
{
    /// <summary>
    /// Methods having to do with working with inventor document objects
    /// </summary>
    public static class DocumentExtensions
    {
        /// <summary>
        /// Zooms extents in a drawing, part, or assembly.
        /// </summary>
        /// <param name="documentToWork"></param>
        public static void ZoomExtents(this Document documentToWork)
        {
            documentToWork.Activate();
            ((Inventor.Application)documentToWork.Parent).CommandManager.ControlDefinitions["AppZoomallCmd"].Execute();
        }
        /// <summary>
        /// Orbits around the part/assembly to show the view from a front isometric angle on the view cube
        /// </summary>
        /// <param name="documentToWork"></param>
        public static void OrbitToIsoFrontRightTop(this Document documentToWork)
        {
            ((Inventor.Application)documentToWork.Parent).CommandManager.ControlDefinitions["AppIsometricViewCmd"].Execute();
        }
        /// <summary>
        /// Takes a screenshot of the document
        /// </summary>
        /// <param name="documentToWork"></param>
        /// <param name="locationToSaveImage"></param>
        /// <param name="setWhiteBg"></param>
        /// <param name="orbitToIso"></param>
        public static void ScreenShot(this Document documentToWork, string locationToSaveImage, bool setWhiteBg = false, bool orbitToIso = false)
        {
            Inventor.Application invObj = (Inventor.Application)documentToWork.Parent;
            string userColorScheme = "";
            BackgroundTypeEnum userBackgroundType = BackgroundTypeEnum.kGradientBackgroundType;

            documentToWork.ZoomExtents();

            if(orbitToIso)
            {
                documentToWork.OrbitToIsoFrontRightTop();
            }

            invObj.WindowState = Inventor.WindowsSizeEnum.kMaximize;

            if(setWhiteBg)
            {
                // Save current color scheme info
                userColorScheme = invObj.GetActiveColorSchemeName();
                userBackgroundType = invObj.GetActiveColorSchemeBackground();

                // Set white BG color scheme
                invObj.SetActiveColorScheme("Sky");
                invObj.ColorSchemes.BackgroundType = Inventor.BackgroundTypeEnum.kImageBackgroundType;
                WinMacros.WinSleep(1);
            }

            // Take screenshot
            invObj.ActiveDocument.SaveAs(locationToSaveImage, true);

            if(setWhiteBg)
            {
                // Restore original theme info
                invObj.ColorSchemes[userColorScheme].Activate();
                invObj.ColorSchemes.BackgroundType = userBackgroundType;
            }
        }
        /// <summary>
        /// Saves the document without showing anything to the user
        /// </summary>
        /// <param name="documentToWork"></param>
        public static void SaveSilently(this Document documentToWork)
        {
            // Save files without prompt
            ((Inventor.Application)documentToWork.Parent).SilentOperation = true;
            documentToWork.Save();
            ((Inventor.Application)documentToWork.Parent).SilentOperation = false;
        }
        /// <summary>
        /// Saves, but shows the user a file dialog so they can pick where to save the document
        /// </summary>
        /// <param name="documentToWork"></param>
        public static void SaveWithFileDialog(this Document documentToWork)
        {
            // Save files without prompt
            ((Inventor.Application)documentToWork.Parent).SilentOperation = false;
            documentToWork.Save();
        }
        /// <summary>
        /// Pass a string that is the name of the parameter and the value of the parameter will be returned as a string
        /// </summary>
        /// <param name="documentToWork"></param>
        /// <param name="nameOfParameterToGet"></param>
        /// <returns></returns>
        public static string GetParameterByName(this Document documentToWork, string nameOfParameterToGet)
        {
            Parameters listOfParameters;

            if(documentToWork.DocumentType == DocumentTypeEnum.kAssemblyDocumentObject)
            {
                AssemblyDocument identifiedAssemblyDocument = (AssemblyDocument)documentToWork;

                listOfParameters = identifiedAssemblyDocument.ComponentDefinition.Parameters;
            }
            else if(documentToWork.DocumentType == DocumentTypeEnum.kPartDocumentObject)
            {
                PartDocument identifiedPartDocument = (PartDocument)documentToWork;

                listOfParameters = identifiedPartDocument.ComponentDefinition.Parameters;
            }
            else
            {
                throw new Exception("Unknown type of document passed to GetParameterByName");
            }

            return (string)listOfParameters[nameOfParameterToGet].Value.ToString();
        }
        /// <summary>
        /// Gets all names of user parameters in a document
        /// </summary>
        /// <param name="documentToWork"></param>
        /// <returns></returns>
        public static List<string> GetParameterNames(this Document documentToWork)
        {
            Parameters listOfParameters;

            if(documentToWork.DocumentType == DocumentTypeEnum.kAssemblyDocumentObject)
            {
                AssemblyDocument identifiedAssemblyDocument = (AssemblyDocument)documentToWork;

                listOfParameters = identifiedAssemblyDocument.ComponentDefinition.Parameters;
            }
            else if(documentToWork.DocumentType == DocumentTypeEnum.kPartDocumentObject)
            {
                PartDocument identifiedPartDocument = (PartDocument)documentToWork;

                listOfParameters = identifiedPartDocument.ComponentDefinition.Parameters;
            }
            else
            {
                throw new Exception("Unknown type of document passed to GetParameterNames");
            }

            var returnList = new List<string>();

            foreach(Parameter parameter in listOfParameters)
            {
                returnList.Add(parameter.Name);
            }

            return returnList;
        }
        /// <summary>
        /// Set a parameter to a new value, this is a generic so to pass a string into a parameter you would use SetParameterByName/<string/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="documentToWork"></param>
        /// <param name="parameterName"></param>
        /// <param name="newValue"></param>
        public static void SetParameterByName<T>(this Document documentToWork, string parameterName, T newValue)
        {
            Parameters listOfParameters;

            if(documentToWork.DocumentType == DocumentTypeEnum.kAssemblyDocumentObject)
            {
                AssemblyDocument identifiedAssemblyDocument = (AssemblyDocument)documentToWork;

                listOfParameters = identifiedAssemblyDocument.ComponentDefinition.Parameters;
            }
            else if(documentToWork.DocumentType == DocumentTypeEnum.kPartDocumentObject)
            {
                PartDocument identifiedPartDocument = (PartDocument)documentToWork;

                listOfParameters = identifiedPartDocument.ComponentDefinition.Parameters;
            }
            else
            {
                throw new Exception("Unknown type of document passed to GetParameterByName");
            }

            listOfParameters[parameterName].Value = newValue;
        }
        /// <summary>
        /// Pass the name of an iProperty and this method returns the value in that iProperty
        /// </summary>
        /// <param name="documentToWork"></param>
        /// <param name="nameOfiProperty"></param>
        /// <returns></returns>
        public static string GetiPropertyByName(this Document documentToWork, string nameOfiProperty)
        {
            // Get custom iProp set
            Inventor.PropertySet documentPropertySet = documentToWork.PropertySets["Inventor User Defined Properties"];

            // Change the value
            return (string)documentPropertySet[nameOfiProperty].Value;
        }
        /// <summary>
        /// Sets an iProperty of the passed name to a new value. This is a generic, so if you were setting an iProperty to hold a string value,
        /// 
        /// You would use SetiPropertyByName/<string/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="documentToWork"></param>
        /// <param name="nameOfiProperty"></param>
        /// <param name="newValue"></param>
        /// <param name="createIfNecessary"></param>
        public static void SetiPropertyByName<T>(this Document documentToWork, string nameOfiProperty, T newValue, bool createIfNecessary = true)
        {
            // Get custom iProp set
            Inventor.PropertySet documentPropertySet = documentToWork.PropertySets["Inventor User Defined Properties"];

            // If programmer wants to create it if it doesn't exist
            if(createIfNecessary)
            {
                // It will throw an exception if the property already exists that we don't care about
                try
                {
                    documentPropertySet.Add("", nameOfiProperty);
                }
                catch(System.ArgumentException) { }
            }

            // Change the value
            documentPropertySet[nameOfiProperty].Value = newValue;
        }
    }
}
