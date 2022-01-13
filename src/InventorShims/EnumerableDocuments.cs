using Inventor;
using System.Collections.Generic;
using System.Linq;

namespace InventorShims
{
    public static class EnumerableDocuments
    {
        #region IEnumerable<Document> providors

        /// <summary>
        /// Returns an IEnumerable collection of Inventor.Document from a SelectSet object.
        /// </summary>
        /// <param name="selectSet">Inventor.SelectionSet</param>
        /// <returns>IEnumerable<Document></Document></returns>
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
        /// <returns>IEnumerable<Document></returns>
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
        /// Returns an IEnumerable<Document> of all referenced documents in a Document object.
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
        /// Returns an IEnumerable<Document> of all referenced documents in an AssemblyDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetAllReferencedDocuments(this AssemblyDocument document)
        {
            foreach (Document doc in document.AllReferencedDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable<Document> of all referenced documents in a PresentationDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetAllReferencedDocuments(this PresentationDocument document)
        {
            foreach (Document doc in document.AllReferencedDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable<Document> of all referenced documents in a PartDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetAllReferencedDocuments(this PartDocument document)
        {
            foreach (Document doc in document.AllReferencedDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable<Document> of all referenced documents in a DrawingDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetAllReferencedDocuments(this DrawingDocument document)
        {
            foreach (Document doc in document.AllReferencedDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable<Document> of referenced documents in a Document object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetReferencedDocuments(this Document document)
        {
            foreach (Document doc in document.ReferencedDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable<Document> of referenced documents in an AssemblyDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetReferencedDocuments(this AssemblyDocument document)
        {
            foreach (Document doc in document.ReferencedDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable<Document> of referenced documents in a PresentationDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetReferencedDocuments(this PresentationDocument document)
        {
            foreach (Document doc in document.ReferencedDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable<Document> of referenced documents in a PartDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetReferencedDocuments(this PartDocument document)
        {
            foreach (Document doc in document.ReferencedDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable<Document> of referenced documents in a DrawingDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetReferencedDocuments(this DrawingDocument document)
        {
            foreach (Document doc in document.ReferencedDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable<Document> of referencing documents in a Document object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetReferencingDocuments(this Document document)
        {
            foreach (Document doc in document.ReferencingDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable<Document> of referencing documents in an AssemblyDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetReferencingDocuments(this AssemblyDocument document)
        {
            foreach (Document doc in document.ReferencingDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable<Document> of referencing documents in a PresentationDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetReferencingDocuments(this PresentationDocument document)
        {
            foreach (Document doc in document.ReferencingDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable<Document> of referencing documents in a PartDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Document> GetReferencingDocuments(this PartDocument document)
        {
            foreach (Document doc in document.ReferencingDocuments)
                yield return doc;
        }

        /// <summary>
        /// Returns an IEnumerable<Document> of referencing documents in a DrawingDocument object.
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
        /// Returns an IEnumerable<AssemblyDocument> of just the AssemblyDocuments from an IEnumerable<Document> collection.
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
        /// Returns an IEnumerable<Document> with the AssemblyDocuments removed from an IEnumerable<Document> collection.
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
        /// Returns an IEnumerable<AssemblyDocument> of just the DrawingDocuments from an IEnumerable<Document> collection.
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
        /// Returns an IEnumerable<Document> with the DrawingDocuments removed from an IEnumerable<Document> collection.
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
        /// Returns an IEnumerable<AssemblyDocument> of just the PresentationDocuments from an IEnumerable<Document> collection.
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
        /// Returns an IEnumerable<Document> with the PresentationDocuments removed from an IEnumerable<Document> collection.
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
        /// Returns an IEnumerable<AssemblyDocument> of just the PartDocuments from an IEnumerable<Document> collection.
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
        /// Returns an IEnumerable<Document> with the PartDocuments removed from an IEnumerable<Document> collection.
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
        /// Returns an IEnumerable<Document> with the Non-Native Documents (ForeignModel, SAT, Unknown) removed from an IEnumerable<Document> collection.
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
        /// Returns an IEnumerable<DocumentDescriptors> of referenced documents in a Document object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<DocumentDescriptor> GetReferencedDocumentDescriptors(this Document document)
        {
            foreach (DocumentDescriptor dd in document.ReferencedDocumentDescriptors)
                yield return dd;
        }

        /// <summary>
        /// Returns an IEnumerable<DocumentDescriptors> of referenced documents in a AssemblyDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<DocumentDescriptor> GetReferencedDocumentDescriptors(this AssemblyDocument document)
        {
            foreach (DocumentDescriptor dd in document.ReferencedDocumentDescriptors)
                yield return dd;
        }

        /// <summary>
        /// Returns an IEnumerable<DocumentDescriptors> of referenced documents in a PresentationDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<DocumentDescriptor> GetReferencedDocumentDescriptors(this PresentationDocument document)
        {
            foreach (DocumentDescriptor dd in document.ReferencedDocumentDescriptors)
                yield return dd;
        }

        /// <summary>
        /// Returns an IEnumerable<DocumentDescriptors> of referenced documents in a PartDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<DocumentDescriptor> GetReferencedDocumentDescriptors(this PartDocument document)
        {
            foreach (DocumentDescriptor dd in document.ReferencedDocumentDescriptors)
                yield return dd;
        }

        /// <summary>
        /// Returns an IEnumerable<DocumentDescriptors> of referencing documents in a DrawingDocument object.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<DocumentDescriptor> GetReferencedDocumentDescriptors(this DrawingDocument document)
        {
            foreach (DocumentDescriptor dd in document.ReferencedDocumentDescriptors)
                yield return dd;
        }

        /// <summary>
        /// Returns an IEnumerable<DocumentDescriptors> of all leaf documents in an AssemblyDocument object.
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

        #endregion IEnumerable Samples
    }
}