using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;
using InventorShims;

namespace GetDocumentFromObjectTest
{
    class Program
    {
        static void Main(string[] args)
        {


        Inventor.Application oApp = ApplicationShim.CurrentInstance();
        SelectSet oSSet = oApp.ActiveDocument.SelectSet;
            if (oSSet.Count == 0) {
                oSSet.Select(oApp.CommandManager.Pick(SelectionFilterEnum.kAllEntitiesFilter, "Select thing(s)..."));
            }


            if (oSSet.Count == 0) {
                return;
            }

            List<Document> documentList = new List<Document>();
    		documentList = DocumentShim.GetDocumentsFromSelectSet(oSSet);

            //Get document from document, test...
//        Console.WriteLine(DocumentShim.GetDocumentFromObject(oApp.ActiveDocument).FullDocumentName);

            Console.WriteLine("Number of documents in List: " + documentList.Count().ToString());
        foreach (Document i in documentList) {
            //Console.WriteLine(i.FullFileName);
            }
         
        Console.ReadLine();
        }
    }
}
