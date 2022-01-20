using Inventor;
using InventorShims;
using System;

namespace tests
{
    public static class TestUtilities
    {
        internal static Inventor.Document CreatePartDocument()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            return app.Documents.Add(DocumentTypeEnum.kPartDocumentObject, path + "Standard.ipt", true);
        }

        public static Document CreateAssemblyDocument()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kAssemblyDocumentObject, path + "Standard.iam", true);
            return doc;
        }

        internal static Document CreateDrawingDocument()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kDrawingDocumentObject, path + "Standard.idw", true);
            return doc;
        }

        internal static Document CreatePresentationDocument()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var path = app.DesignProjectManager.ActiveDesignProject.TemplatesPath;
            Document doc = app.Documents.Add(DocumentTypeEnum.kPresentationDocumentObject, path + "Standard.ipn", true);
            return doc;
        }

    }
}