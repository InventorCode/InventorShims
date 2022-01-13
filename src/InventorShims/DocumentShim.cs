using Inventor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

            if (orbitToIso)
            {
                documentToWork.OrbitToIsoFrontRightTop();
            }

            invObj.WindowState = Inventor.WindowsSizeEnum.kMaximize;

            if (setWhiteBg)
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

            if (setWhiteBg)
            {
                // Restore original theme info
                invObj.ColorSchemes[userColorScheme].Activate();
                invObj.ColorSchemes.BackgroundType = userBackgroundType;
            }
        }

        /// <summary>
        /// Saves the document without showing anything to the user
        /// </summary>
        /// <param name="document">Document object</param>
        public static void SaveSilently(this Document document)
        {
            Application app = (Inventor.Application)document.Parent;

            // Save files without prompt
            app.SilentOperation = true;

            try
            {
                document.Save();
            }
            catch (Exception e)
            {
                new SystemException("The document could not be saved silently.", e);
            }
            finally
            {
                app.SilentOperation = false;
            }
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

        #endregion Document type booleans

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
            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Debug.AutoFlush = true;
            Debug.Indent();
            Debug.WriteLine("Entering GetDocumentsFromSelectSet method...");

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

                Debug.WriteLine("item  " + (string)i.type.ToString());

                if (tempDocument is null)
                {
                    Debug.WriteLine("this object is not a document");
                    continue;
                }

                Debug.WriteLine("this object is a document.");
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
            switch (obj.type)
            {
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
        /// Tries to get an Inventor.Document object from a supplied object within a drawing.
        /// If one is found it will be returned; if not, null is returned.
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
                    try
                    {
                        //try to get single document from selected part
                        document.ProcessViewSelection((GenericObject)obj, out _, out returnObject);
                    }
                    catch { break; }

                    if (returnObject == null) break;

                    switch (returnObject)
                    {
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

                case 117478144: //kDrawingCurveSegmentObject
                    //Edge Objects and Edge Proxy Objects
                    DrawingCurveSegment drawingCurveSegment = (DrawingCurveSegment)obj;
                    DrawingCurve drawingCurve = drawingCurveSegment.Parent;

                    //get the modelGeometry, if it cannot be accessed, the file is likely unreferenced...
                    dynamic modelGeometry = null;
                    try { modelGeometry = drawingCurve.ModelGeometry; }
                    catch
                    {
                        returnDocument = null;
                        break;
                    }

                    try //for a selected DrawingCurveSegment belonging to an assembly component
                    {
                        returnDocument = (Document)modelGeometry.ContainingOccurrence.Definition.Document;
                        break;
                    }
                    catch { }

                    try //for a selected DrawingCurveSegment belonging to a part
                    {
                        returnDocument = (Document)modelGeometry.Parent.ComponentDefinition.Document;
                        break;
                    }
                    catch { }
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
        /// Returns true if an object is an Inventor Document.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Boolean</returns>
        public static bool ObjectIsDocument(dynamic obj)
        {
            //ObjectTypeEnum.kDocumentObject
            return obj.type == 50332160 ? true : false;
        }

        /// <summary>
        /// Checks if a Document is a ContentCenter part; returns a bool.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static bool IsContentCenter(this Document document) =>
            document.PropertySets.PropertySetExists("ContentCenter", out _);

        /// <summary>
        /// Checks if an AssemblyDocument is a ContentCenter part; returns false.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static bool IsContentCenter(this AssemblyDocument document) => false;

        /// <summary>
        /// Checks if a PartDocument is a ContentCenter part; returns a bool.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static bool IsContentCenter(this PartDocument document) =>
            document.PropertySets.PropertySetExists("ContentCenter", out _);

        /// <summary>
        /// Checks if a PresentationDocument is a ContentCenter part; returns false.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static bool IsContentCenter(this PresentationDocument document) => false;

        /// <summary>
        /// Checks if a Document is a Custom ContentCenter part; returns a bool.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static bool IsCustomContentCenter(this Document document)
        {
            if (document.IsContentCenter())
            {
                try
                {
                    return ((bool)document.PropertySets["ContentCenter"]["IsCustomPart"].Value);
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if a PartDocument is a Custom ContentCenter part; returns a bool.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static bool IsCustomContentCenter(this PartDocument document)
            => IsCustomContentCenter((Document)document);

        /// <summary>
        /// Checks if an AssemblyDocument is a Custom ContentCenter part; returns false.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static bool IsCustomContentCenter(this AssemblyDocument document) => false;

        /// <summary>
        /// Checks if a DrawingDocument is a Custom ContentCenter part; returns false.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static bool IsCustomContentCenter(this DrawingDocument document) => false;

        /// <summary>
        /// Checks if a PresentationDocument is a Custom ContentCenter part; returns false.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static bool IsCustomContentCenter(this PresentationDocument document) => false;

        //IsIPartFactory IsIPartMember

        
        #region IEnumerable<Document> providors

        /// <summary>
        /// Returns an IEnumerable collection of Inventor.Document from a SelectSet object.
        /// </summary>
        /// <param name="selectSet">Inventor.SelectionSet</param>
        /// <returns>IEnumerable Document</returns>
        /// <exception cref="System.ArgumentNullException">Throws an error if the selection set is empty.</exception>
        public static IEnumerable<Document> GetDocuments(this SelectSet selectSet)
        {
            if (selectSet.Count == 0)
                throw new System.ArgumentNullException("The selection set was empty.");

            foreach (dynamic i in selectSet)
            {
                Document tempDocument = DocumentShim.GetDocumentFromObject(i);

                if (tempDocument is null)
                    continue;

                yield return tempDocument;
            }
        }

        /// <summary>
        /// Returns an IEnumerable collection of Inventor.Document from a DocumentDescriptor object.
        /// </summary>
        /// <param name="documentDiscriptors"></param>
        /// <returns>IEnumerable Document</returns>
        public static IEnumerable<Document> GetDocuments(this IEnumerable<DocumentDescriptor> documentDiscriptors)
        {
            foreach (DocumentDescriptor document in documentDiscriptors)
            {
                if (document is null)
                    continue;

                yield return (Document)document.ReferencedDocument;
            }
        }

        /// <summary>
        /// Returns an IEnumerable{Document} of all referenced documents in a Document object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetAllReferencedDocuments(this Document document)
        {
            //keep calling method on document to get the next reference
            foreach (Document doc in document.AllReferencedDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable{Document} of all referenced documents in an AssemblyDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetAllReferencedDocuments(this AssemblyDocument document)
        {
            foreach (Document doc in document.AllReferencedDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable{Document} of all referenced documents in a PresentationDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetAllReferencedDocuments(this PresentationDocument document)
        {
            foreach (Document doc in document.AllReferencedDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable{Document} of all referenced documents in a PartDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetAllReferencedDocuments(this PartDocument document)
        {
            foreach (Document doc in document.AllReferencedDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable{Document} of all referenced documents in a DrawingDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetAllReferencedDocuments(this DrawingDocument document)
        {
            foreach (Document doc in document.AllReferencedDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable{Document} of referenced documents in a Document object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetReferencedDocuments(this Document document)
        {
            foreach (Document doc in document.ReferencedDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable{Document} of referenced documents in an AssemblyDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetReferencedDocuments(this AssemblyDocument document)
        {
            foreach (Document doc in document.ReferencedDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable{Document} of referenced documents in a PresentationDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetReferencedDocuments(this PresentationDocument document)
        {
            foreach (Document doc in document.ReferencedDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable{Document} of referenced documents in a PartDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetReferencedDocuments(this PartDocument document)
        {
            foreach (Document doc in document.ReferencedDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable{Document} of referenced documents in a DrawingDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetReferencedDocuments(this DrawingDocument document)
        {
            foreach (Document doc in document.ReferencedDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable{Document} of referencing documents in a Document object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetReferencingDocuments(this Document document)
        {
            foreach (Document doc in document.ReferencingDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable{Document} of referencing documents in an AssemblyDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetReferencingDocuments(this AssemblyDocument document)
        {
            foreach (Document doc in document.ReferencingDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable{Document} of referencing documents in a PresentationDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetReferencingDocuments(this PresentationDocument document)
        {
            foreach (Document doc in document.ReferencingDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable{Document} of referencing documents in a PartDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetReferencingDocuments(this PartDocument document)
        {
            foreach (Document doc in document.ReferencingDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable{Document} of referencing documents in a DrawingDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetReferencingDocuments(this DrawingDocument document)
        {
            foreach (Document doc in document.ReferencingDocuments)
                yield return doc;
        }

        #endregion IEnumerable<Document> providors

        #region IEnumerable Filters

        /// <summary>
        /// Returns an IEnumerable{AssemblyDocument} of just the AssemblyDocuments from an IEnumerable{Document} collection.
        /// </summary>
        /// <param name="documents"></param>
        /// <returns></returns>
        public static IEnumerable<AssemblyDocument> AssemblyDocuments(this IEnumerable<Document> documents)
        {
            foreach (Document document in documents)
            {
                if (document.IsAssembly())
                    yield return (AssemblyDocument)document;
            }

            yield break;
        }

        /// <summary>
        /// Returns an IEnumerable{Document} with the AssemblyDocuments removed from an IEnumerable{Document} collection.
        /// </summary>
        /// <param name="documents"></param>
        /// <returns></returns>
        public static IEnumerable<Document> RemoveAssemblyDocuments(this IEnumerable<Document> documents)
        {
            foreach (Document document in documents)
            {
                if (!document.IsAssembly())
                    yield return document;
            }

            yield break;
        }

        /// <summary>
        /// Returns an IEnumerable{AssemblyDocument} of just the DrawingDocuments from an IEnumerable{Document} collection.
        /// </summary>
        /// <param name="documents"></param>
        /// <returns></returns>
        public static IEnumerable<DrawingDocument> DrawingDocuments(this IEnumerable<Document> documents)
        {
            foreach (Document document in documents)
            {
                if (document.IsDrawing())
                    yield return (DrawingDocument)document;
            }

            yield break;
        }

        /// <summary>
        /// Returns an IEnumerable{Document} with the DrawingDocuments removed from an IEnumerable{Document} collection.
        /// </summary>
        /// <param name="documents"></param>
        /// <returns></returns>
        public static IEnumerable<Document> RemoveDrawingDocuments(this IEnumerable<Document> documents)
        {
            foreach (Document document in documents)
            {
                if (!document.IsDrawing())
                    yield return document;
            }

            yield break;
        }

        /// <summary>
        /// Returns an IEnumerable{AssemblyDocument} of just the PresentationDocuments from an IEnumerable{Document} collection.
        /// </summary>
        /// <param name="documents"></param>
        /// <returns></returns>
        public static IEnumerable<PresentationDocument> PresentationDocuments(this IEnumerable<Document> documents)
        {
            foreach (Document document in documents)
            {
                if (document.IsPresentation())
                    yield return (PresentationDocument)document;
            }

            yield break;
        }

        /// <summary>
        /// Returns an IEnumerable{Document} with the PresentationDocuments removed from an IEnumerable{Document} collection.
        /// </summary>
        /// <param name="documents"></param>
        /// <returns></returns>
        public static IEnumerable<Document> RemovePresentationDocuments(this IEnumerable<Document> documents)
        {
            foreach (Document document in documents)
            {
                if (!document.IsPresentation())
                    yield return document;
            }

            yield break;
        }

        /// <summary>
        /// Returns an IEnumerable{AssemblyDocument} of just the PartDocuments from an IEnumerable{Document} collection.
        /// </summary>
        /// <param name="documents"></param>
        /// <returns></returns>
        public static IEnumerable<PartDocument> PartDocuments(this IEnumerable<Document> documents)
        {
            foreach (Document document in documents)
            {
                if (document.IsPart())
                    yield return (PartDocument)document;
            }

            yield break;
        }

        /// <summary>
        /// Returns an IEnumerable{Document} with the PartDocuments removed from an IEnumerable{Document} collection.
        /// </summary>
        /// <param name="documents"></param>
        /// <returns></returns>
        public static IEnumerable<Document> RemovePartDocuments(this IEnumerable<Document> documents)
        {
            foreach (Document document in documents)
            {
                if (!document.IsPart())
                    yield return document;
            }

            yield break;
        }

        /// <summary>
        /// Returns an IEnumerable{Document} with the Non-Native Documents (ForeignModel, SAT, Unknown) removed from an IEnumerable{Document} collection.
        /// </summary>
        /// <param name="documents"></param>
        /// <returns></returns>
        public static IEnumerable<Document> RemoveNonNativeDocuments(this IEnumerable<Document> documents)
        {
            foreach (Document document in documents)
            {
                if (document.IsForeignModel() || document.IsSat() || document.IsUnknown())
                { }
                else
                {
                    yield return document;
                }
            }

            yield break;
        }

        #endregion IEnumerable Filters

        #region IEnumerable<DocumentDescriptors> providers

        /// <summary>
        /// Returns an IEnumerable{DocumentDescriptors} of referenced documents in a Document object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<DocumentDescriptor> GetReferencedDocumentDescriptors(this Document document)
        {
            foreach (DocumentDescriptor dd in document.ReferencedDocumentDescriptors)
                yield return dd;
        }

        /// <summary>
        /// Returns an IEnumerable{DocumentDescriptors} of referenced documents in a AssemblyDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<DocumentDescriptor> GetReferencedDocumentDescriptors(this AssemblyDocument document)
        {
            foreach (DocumentDescriptor dd in document.ReferencedDocumentDescriptors)
                yield return dd;
        }

        /// <summary>
        /// Returns an IEnumerable{DocumentDescriptors} of referenced documents in a PresentationDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<DocumentDescriptor> GetReferencedDocumentDescriptors(this PresentationDocument document)
        {
            foreach (DocumentDescriptor dd in document.ReferencedDocumentDescriptors)
                yield return dd;
        }

        /// <summary>
        /// Returns an IEnumerable{DocumentDescriptors} of referenced documents in a PartDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<DocumentDescriptor> GetReferencedDocumentDescriptors(this PartDocument document)
        {
            foreach (DocumentDescriptor dd in document.ReferencedDocumentDescriptors)
                yield return dd;
        }

        /// <summary>
        /// Returns an IEnumerable{DocumentDescriptors} of referencing documents in a DrawingDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<DocumentDescriptor> GetReferencedDocumentDescriptors(this DrawingDocument document)
        {
            foreach (DocumentDescriptor dd in document.ReferencedDocumentDescriptors)
                yield return dd;
        }

        /// <summary>
        /// Returns an IEnumerable{DocumentDescriptors} of all leaf documents in an AssemblyDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<DocumentDescriptor> GetAllLeafOccurrencesDocumentDescriptors(this AssemblyDocument document)
        {
            var componentDefinition = document.ComponentDefinition;
            var leafOccurences = componentDefinition.Occurrences.AllLeafOccurrences;

            foreach (ComponentOccurrence occurence in leafOccurences)
            {
                yield return occurence.ReferencedDocumentDescriptor;
            }
        }

        #endregion IEnumerable<DocumentDescriptors> providers

        #region IEnumerable Samples

        private static void iEnumerableSmpleCode(this Document document)
        {
            var test = document.GetAllReferencedDocuments()
                .RemoveNonNativeDocuments()
                .Where(s => s.IsModifiable)
                .Where(s => s.IsPart())
                .SkipWhile(s => s.NeedsMigrating)
                .Where(s => s.ReservedForWriteByMe);

            //apply to each in test
            foreach (var i in test)
            {
                i.SetPropertyValue("Author", "Bob");
            }

            document.SelectSet.GetDocuments()
                .Where(s => s.IsModifiable)
                .PartDocuments()
                .Distinct()
                .ToList()
                .ForEach(d => d.SetAttributeValue("MyAttribSet", "Attribute2", "Value"));

            var justTheAssemblyDocs = document.SelectSet.GetDocuments()
                .AssemblyDocuments();

            var notTheAssemblyDocs = document.GetAllReferencedDocuments()
                .Where(s => !s.IsAssembly());

            var JustCustomCCPartDocs = document.GetAllReferencedDocuments()
                .PartDocuments()
                .Where(d => d.IsCustomContentCenter());

            var JustCustomCCDocs = document.GetAllReferencedDocuments()
                .Where(d => d.IsCustomContentCenter());

            var count = document.GetReferencedDocumentDescriptors()
                .Where(s => !s.ReferenceMissing)
                .Where(s => !s.ReferenceSuppressed)
                .GetDocuments()
                .PartDocuments()
                .Count();
        }
        #endregion
    }
}