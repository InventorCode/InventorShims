using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;
using InventorShims;

namespace GetDocumentFromObjectTest
{
    static class Program
    {
        static void Main(string[] args)
        {
            test_GetDocumentsFromSelectSet();
            //wip_kDrawingCurveSegmentObject();
        }

        private static void test_GetDocumentsFromSelectSet()
        {
            Inventor.Application oApp = ApplicationShim.CurrentInstance();
            SelectSet oSSet = oApp.ActiveDocument.SelectSet;
            if (oSSet.Count == 0)
            {
                oSSet.Select(oApp.CommandManager.Pick(SelectionFilterEnum.kAllEntitiesFilter, "Select thing(s)..."));
            }


            if (oSSet.Count == 0)
            {
                return;
            }

            List<Document> documentList = new List<Document>();
            documentList = DocumentShim.GetDocumentsFromSelectSet(oSSet);

            Console.WriteLine("Number of documents in List: " + documentList.Count().ToString());
            foreach (Document i in documentList)
            {
            //    Console.WriteLine(i.FullFileName);
            }

            Console.WriteLine("Press Enter to Exit!");
            Console.ReadLine();
        }

        private static void wip_kDrawingCurveSegmentObject()
        {

            Inventor.Application oApp = ApplicationShim.CurrentInstance();
            SelectSet oSSet = oApp.ActiveDocument.SelectSet;
            if (oSSet.Count == 0)
            {
                return;
            }

            Inventor.Document returnDocument = null;

            dynamic obj = oSSet[2];
            switch (obj.type)
            {
                case 117478144: //kDrawingCurveSegmentObject
                                //try to set the drawing curve object to point at the containingOccurrence object.
                                //Edge Objects and Edge Proxy Objects

                    DrawingCurveSegment drawingCurveSegment = (DrawingCurveSegment)obj;
                    DrawingCurve drawingCurve = drawingCurveSegment.Parent;


                    dynamic modelGeometry;
                    try
                    {
                        modelGeometry = drawingCurve.ModelGeometry;
                    }
                    catch
                    {
                        Console.WriteLine("Are you sure the object can be found?  The ModelGeometry cannot be located. exiting...");
                        Console.ReadLine();
                        return;
                    }

                    ComponentOccurrence componentOccurrence;

                    try //for a selected DrawingCurveSegment belonging to an assembly component
                    {
                        componentOccurrence = modelGeometry.ContainingOccurrence;
                        returnDocument = (Document)componentOccurrence.Definition.Document;
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

            }

            if (returnDocument == null)
            {
                Console.WriteLine("The document could not be found!");
            }
            else
            {
                Console.WriteLine("Document Type: " + returnDocument.DocumentSubType);
                Console.WriteLine(returnDocument.FullFileName);
            }

            Console.ReadLine();
        }
    }
}
