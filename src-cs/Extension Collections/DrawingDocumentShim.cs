using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace InventorShims
{
    /// <summary>
    /// Methods that extend Inventor.DrawingDocument
    /// </summary>
    public static class DrawingDocumentShim
    {
        /// <summary>
        /// Saves a drawing document with a file dialog shown to the user
        /// </summary>
        /// <param name="documentToWork"></param>
        public static void SaveWithFileDialog(this DrawingDocument documentToWork)
        {
            // Cast it to document and send it to main method that takes Document
            DocumentShim.SaveWithFileDialog((Document)documentToWork);
        }
    }
}
