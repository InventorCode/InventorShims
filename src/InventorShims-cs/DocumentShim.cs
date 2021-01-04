using System;
using System.Collections.Generic;
using System.Linq;
using Inventor;

namespace InventorShims
{
    /// <summary>
    /// Methods that extended the Inventor.Document object with additional functionality.
    /// </summary>
    public static class DocumentShim
    {
        /// <summary>
        /// Zooms extents in a drawing, part, or assembly.
        /// </summary>
        /// <param name="documentToWork">Document object</param>
        public static void ZoomExtents(this Document documentToWork)
        {
            documentToWork.Activate();
            ((Inventor.Application)documentToWork.Parent).CommandManager.ControlDefinitions["AppZoomallCmd"].Execute();
        }
        
        /// <summary>
        /// Orbits around the part/assembly to show the view from a front isometric angle on the view cube
        /// </summary>
        /// <param name="documentToWork">Document object</param>
        public static void OrbitToIsoFrontRightTop(this Document documentToWork)
        {
            ((Inventor.Application)documentToWork.Parent).CommandManager.ControlDefinitions["AppIsometricViewCmd"].Execute();
        }

        /// <summary>
        /// Takes a screenshot of the document
        /// </summary>
        /// <param name="documentToWork">Document object</param>
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
        /// <param name="documentToWork">Document object</param>
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

        #region Document type booleans
        /// <summary>
        /// Returns true if document is a part
        /// </summary>
        /// <param name="documentToTest"></param>
        /// <returns></returns>
        public static bool IsPart(this Document documentToTest)
        {
            return documentToTest.DocumentType == Inventor.DocumentTypeEnum.kPartDocumentObject;
        }

        /// <summary>
        /// Returns true if document is an assembly
        /// </summary>
        /// <param name="documentToTest"></param>
        /// <returns></returns>
        public static bool IsAssembly(this Document documentToTest)
        {
            return documentToTest.DocumentType == Inventor.DocumentTypeEnum.kAssemblyDocumentObject;
        }

        /// <summary>
        /// Returns true if document is a drawing
        /// </summary>
        /// <param name="documentToTest"></param>
        /// <returns></returns>
        public static bool IsDrawing(this Document documentToTest)
        {
            return documentToTest.DocumentType == Inventor.DocumentTypeEnum.kDrawingDocumentObject;
        }

        /// <summary>
        /// Returns true if document is a presentation
        /// </summary>
        /// <param name="documentToTest"></param>
        /// <returns></returns>
        public static bool IsPresentation(this Document documentToTest)
        {
            return documentToTest.DocumentType == Inventor.DocumentTypeEnum.kPresentationDocumentObject;
        }

        /// <summary>
        /// Returns true if document is a ForeignModel
        /// </summary>
        /// <param name="documentToTest"></param>
        /// <returns></returns>
        public static bool IsForeignModel(this Document documentToTest)
        {
            return documentToTest.DocumentType == Inventor.DocumentTypeEnum.kForeignModelDocumentObject;
        }

        /// <summary>
        /// Returns true if document is an SAT reference
        /// </summary>
        /// <param name="documentToTest"></param>
        /// <returns></returns>
        public static bool IsSat(this Document documentToTest)
        {
            return documentToTest.DocumentType == Inventor.DocumentTypeEnum.kSATFileDocumentObject;
        }

        /// <summary>
        /// Returns true if document is unknown
        /// </summary>
        /// <param name="documentToTest"></param>
        /// <returns></returns>
        public static bool IsUnknown(this Document documentToTest)
        {
            return documentToTest.DocumentType == Inventor.DocumentTypeEnum.kUnknownDocumentObject;
        }
        #endregion

        /// <summary>
        /// Returns a Document Object subtype if a subtype exists.  If not, a generic Inventor.Document is returned.
        /// </summary>
        /// <param name="document">Document object</param>
        /// <returns>dynamic</returns>
        public static dynamic ReturnSpecificDocumentObject(this Inventor.Document document)
        {
            switch (document)
            {
                case PartDocument d:
                    return d;

                case AssemblyDocument d:
                    return d;

                case DrawingDocument d:
                    return d;

                case PresentationDocument d:
                    return d;

                default:
                    return document;
            }
        }

        /// <summary>
        /// Returns a list of Documents from a provided SelectSet.  If no Documents are found, an
        /// empty list is returned.  Only unique documents objects are returned.
        /// </summary>
        /// <param name="selectSet">Inventor.SelectSet</param>
        /// <returns>List(of Documents)</returns>
        public static List<Document> GetDocumentsFromSelectSet(this SelectSet selectSet)
        {

            List<Document> documentList = new List<Document>();

            if (selectSet.Count == 0)
            {       
                    //nothing is selected, return an null list!
                    return documentList;
            }

            Document tempDocument = null;

            foreach (dynamic i in selectSet)
                {
                tempDocument = GetDocumentFromObject(i);
                Console.WriteLine("item  " + i.type);
                
                if (tempDocument is null) {
                    Console.WriteLine("this object is not a document");
                    continue;
                    }
                Console.WriteLine("this object is a document.");
                documentList.Add(tempDocument);
                }

            if (documentList != null)
                documentList = documentList.Distinct().ToList();

            return documentList;
        }

        /// <summary>
        /// Tries to get an Inventor.Document object from a supplied object.  If one is found it will be returned; if not, null is returned.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Inventor.Document</returns>
        public static Document GetDocumentFromObject(this Object obj)
        {
            if (ObjectIsDocument(obj))
                return (Document)obj;

            Inventor.Application app = ApplicationShim.CurrentInstance();
            if (app == null) return null;
            
            Document currentDocument = app.ActiveEditDocument;
            switch (currentDocument.DocumentType)
            {
                case DocumentTypeEnum.kPartDocumentObject:
                    return currentDocument;

                case DocumentTypeEnum.kAssemblyDocumentObject:
                    return GetDocumentFromObjectInAssembly(obj);

                case DocumentTypeEnum.kDrawingDocumentObject:
                    return GetDocumentFromObjectInDrawing(obj, (DrawingDocument)currentDocument);

                case DocumentTypeEnum.kDesignElementDocumentObject: //12294
                case DocumentTypeEnum.kForeignModelDocumentObject:  //12295
                case DocumentTypeEnum.kNoDocument:                  //12297
                case DocumentTypeEnum.kPresentationDocumentObject:  //12293
                case DocumentTypeEnum.kSATFileDocumentObject:       //12296
                case DocumentTypeEnum.kUnknownDocumentObject:       //12289
                default:
                    return null;
            }
        }

        /// <summary>
        /// Tries to get an Inventor.Document object from a supplied object.  If one is found it will be returned; if not, null is returned.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Inventor.Document</returns>
        private static Document GetDocumentFromObjectInAssembly(dynamic obj)
        {
            switch (obj.type) {

                //###   In Assembly Document [kAssemblyDocumentObject]   ###
                case 67113776: //kComponentOccurrenceObject:
                case 67113888: //kComponentOccurrenceProxyObject
                    return obj.Definition.Document;

                case 100674816: //ObjectTypeEnum.kBOMRowObject
                    return obj.ComponentDefinitions(1).Document;

                default:
                    return null;
            }
        }

        /// <summary>
        /// Tries to get an Inventor.Document object from a supplied object.  If one is found it will be returned; if not, null is returned.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="document">Inventor.DrawingDocument</param>
        /// <returns></returns>
        private static Document GetDocumentFromObjectInDrawing(dynamic obj, DrawingDocument document)
        {
            Document returnDocument = null;

            switch (obj.type)
            {
                //Drawing View, Section View, Detail View
                case 117441536: //kDrawingViewObject:
                case 117463296: //kSectionDrawingViewObject
                case 117474304: //kDetailDrawingViewObject
                    return obj.ReferencedFile.ReferencedDocument;

                case 2130706445: //kGenericObject:
                    dynamic returnObject = null;
                    try {
                        //try to get single document from selected part
                        document.ProcessViewSelection((GenericObject)obj, out _, out returnObject);
                    } catch { break;}

                    if (returnObject == null) break;

                    switch (returnObject) {
                        case Document doc:
                            returnDocument = doc;
                            break;

                        case ComponentOccurrence componentOccurrence:
                            //if this doesn't work, try to get the component occurrence instead, and then get the document from that
                            returnDocument = (Document)componentOccurrence.Definition.Document;
                            break;
                            }
                    break;
                    //There was an error at 'Set oCCdef = oCompOcc.Definition.Document'

//TODO: still not working here!!!!!!!!!!


                case 117478144: //kDrawingCurveSegmentObject
                    //try to set the drawing curve object to point at the containingOccurrence object.
                    //Edge Objects and Edge Proxy Objects   
                    
                    DrawingCurveSegment drawingCurveSegment = (DrawingCurveSegment)obj;
                    DrawingCurve drawingCurve = drawingCurveSegment.Parent;
                    dynamic modelGeometry = drawingCurve.Parent;

                    try //for a selected DrawingCurveSegment belonging to an assembly component
                    {
                        returnDocument = modelGeometry.ContainingOccurrence.Definition.Document;
                        break;
                    } catch { }

                    try //for a selected DrawingCurveSegment belonging to a part
                    {
                        returnDocument = modelGeometry.Parent.ComponentDefinition.Document;
                        break;
                    } catch { }
                    break;


                case 117444096: //kPartsListObject:
                    returnDocument = obj.ReferencedFile.ReferencedDocument;
                    break;

                case 117445120: //kPartsListRowObject:
                    returnDocument = obj.ReferencedFiles.Item(1).ReferencedDocument;
                    break;

                case 100674816: //kBOMRowObject:
                    returnDocument = obj.ComponentDefinitions(1).Document;
                    break;

                default:
                    return null;
            }

            return returnDocument ?? null;

        }

        /// <summary>
        /// Returns true if an object is a document
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Boolean</returns>
        public static bool ObjectIsDocument(dynamic obj)
        {   
            //ObjectTypeEnum.kDocumentObject
            return obj.type == 50332160 ? true : false;
        }
    }
}
