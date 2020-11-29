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

        #region Document type booleans
        /// <summary>
        /// Returns true if document is a part
        /// </summary>
        /// <param name="documentToTest"></param>
        /// <returns></returns>
        public static bool IsPart(this Document documentToTest)
        {
            return documentToTest.DocumentType == Inventor.DocumentTypeEnum.kPartDocumentObject ? true : false;
        }

        /// <summary>
        /// Returns true if document is an assembly
        /// </summary>
        /// <param name="documentToTest"></param>
        /// <returns></returns>
        public static bool IsAssembly(this Document documentToTest)
        {
            return documentToTest.DocumentType == Inventor.DocumentTypeEnum.kAssemblyDocumentObject ? true : false;
        }

        /// <summary>
        /// Returns true if document is an drawing
        /// </summary>
        /// <param name="documentToTest"></param>
        /// <returns></returns>
        public static bool IsDrawing(this Document documentToTest)
        {
            return documentToTest.DocumentType == Inventor.DocumentTypeEnum.kDrawingDocumentObject ? true : false;
        }

        /// <summary>
        /// Returns true if document is a presentation
        /// </summary>
        /// <param name="documentToTest"></param>
        /// <returns></returns>
        public static bool IsPresentation(this Document documentToTest)
        {
            return documentToTest.DocumentType == Inventor.DocumentTypeEnum.kPresentationDocumentObject ? true : false;
        }

        /// <summary>
        /// Returns true if document is a presentation
        /// </summary>
        /// <param name="documentToTest"></param>
        /// <returns></returns>
        public static bool IsForeignModel(this Document documentToTest)
        {
            return documentToTest.DocumentType == Inventor.DocumentTypeEnum.kForeignModelDocumentObject ? true : false;
        }

        /// <summary>
        /// Returns true if document is a nest
        /// </summary>
        /// <param name="documentToTest"></param>
        /// <returns></returns>
        public static bool IsNest(this Document documentToTest)
        {
            return documentToTest.DocumentType == Inventor.DocumentTypeEnum.kNestingDocument ? true : false;
        }

        /// <summary>
        /// Returns true if document is a nest
        /// </summary>
        /// <param name="documentToTest"></param>
        /// <returns></returns>
        public static bool IsSat(this Document documentToTest)
        {
            return documentToTest.DocumentType == Inventor.DocumentTypeEnum.kSATFileDocumentObject ? true : false;
        }

        /// <summary>
        /// Returns true if document is a nest
        /// </summary>
        /// <param name="documentToTest"></param>
        /// <returns></returns>
        public static bool IsUnknown(this Document documentToTest)
        {
            return documentToTest.DocumentType == Inventor.DocumentTypeEnum.kUnknownDocumentObject ? true : false;
        }
        #endregion
    }
}
