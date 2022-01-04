﻿using Inventor;
using System.Collections.Generic;
using System.Linq;

namespace InventorShims
{
    public static class Immutable
    {
        public static IEnumerable<Document> GetDocumentsFromSelectSet(this SelectSet selectSet)
        {
            List<Document> documentList = new List<Document>();

            if (selectSet.Count == 0)
                throw new System.ArgumentNullException();

            foreach (dynamic i in selectSet)
            {
                Document tempDocument = DocumentShim.GetDocumentFromObject(i);

                if (tempDocument is null)
                    continue;

                documentList.Add(tempDocument);
            }

            if (documentList != null)
            {
                return documentList.Distinct();
            }
            else
            {
                return Enumerable.Empty<Document>();
            }
        }

        #region IEnumerable<Document>
        public static IEnumerable<Document> GetAllReferencedDocuments(this Document document)
        {
            return (IEnumerable<Document>)document.AllReferencedDocuments;
        }

        public static IEnumerable<Document> GetAllReferencedDocuments(this AssemblyDocument document)
        {
            return (IEnumerable<Document>)document.AllReferencedDocuments;
        }

        public static IEnumerable<Document> GetAllReferencedDocuments(this PresentationDocument document)
        {
            return (IEnumerable<Document>)document.AllReferencedDocuments;
        }

        public static IEnumerable<Document> GetAllReferencedDocuments(this PartDocument document)
        {
            return (IEnumerable<Document>)document.AllReferencedDocuments;
        }

        public static IEnumerable<Document> GetAllReferencedDocuments(this DrawingDocument document)
        {
            return (IEnumerable<Document>)document.AllReferencedDocuments;
        }

        public static IEnumerable<Document> GetReferencedDocuments(this Document document)
        {
            return (IEnumerable<Document>)document.AllReferencedDocuments;
        }

        public static IEnumerable<Document> GetReferencedDocuments(this AssemblyDocument document)
        {
            return (IEnumerable<Document>)document.ReferencedDocuments;
        }

        public static IEnumerable<Document> GetReferencedDocuments(this PresentationDocument document)
        {
            return (IEnumerable<Document>)document.ReferencedDocuments;
        }

        public static IEnumerable<Document> GetReferencedDocuments(this PartDocument document)
        {
            return (IEnumerable<Document>)document.ReferencedDocuments;
        }

        public static IEnumerable<Document> GetReferencedDocuments(this DrawingDocument document)
        {
            return (IEnumerable<Document>)document.ReferencedDocuments;
        }

        public static IEnumerable<Document> GetReferencingDocuments(this AssemblyDocument document)
        {
            return (IEnumerable<Document>)document.ReferencingDocuments;
        }

        public static IEnumerable<Document> GetReferencingDocuments(this PresentationDocument document)
        {
            return (IEnumerable<Document>)document.ReferencingDocuments;
        }

        public static IEnumerable<Document> GetReferencingDocuments(this PartDocument document)
        {
            return (IEnumerable<Document>)document.ReferencingDocuments;
        }

        public static IEnumerable<Document> GetReferencingDocuments(this DrawingDocument document)
        {
            return (IEnumerable<Document>)document.ReferencingDocuments;
        }

        #endregion IEnumerable<Document>

        private static void temp(this Document document)
        {
            document.GetAllReferencedDocuments()
                .Where(s => s.IsModifiable)
                .Where(s => s.IsPart())
                .Where(s => s.ReservedForWriteByMe)
                .ToList();

            document.SelectSet.GetDocumentsFromSelectSet()
                .Where(s => s.IsModifiable)
                .Where(s => s.IsPart())
                .Where(s => s.ReservedForWriteByMe)
                .ToList();
        }
    }
}