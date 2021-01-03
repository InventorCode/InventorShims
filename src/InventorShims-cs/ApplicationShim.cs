using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Inventor;

namespace InventorShims
{
    /// <summary>
    /// A simple set of static methods to access the Inventor Application.
    /// </summary>
    public static class ApplicationShim
    {
        /// <summary>
        /// Gets an existing Inventor.Application instance, and creates a new instance if one cannot be retrieved.
        /// </summary>
        /// <remarks>
        /// VB.net/iLogic example: <code>Dim oApp As Application = ApplicationShim.Instance()</code>
        /// C# example: <code>Application app = ApplicationShim.Instance()</code>
        /// </remarks>
        /// <returns>Inventor.Application</returns>
        public static Inventor.Application Instance()
        {
            Inventor.Application app = null;

            try
            {
                app = (Inventor.Application) Marshal.GetActiveObject("Inventor.Application");
                return app;
            }
            catch (Exception e)
            {
                // is inventor not visible/in interactive mode?
            }

            //perhaps inventor needs to be started...
            if (app == null)
            {

                try //to start inventor
                {
                    Type appType = Type.GetTypeFromProgID("Inventor.Application");
                    app = (Inventor.Application)Activator.CreateInstance(appType);
                    app.Visible = true;
                    return app;
                }
                catch (Exception e2)
                {
                    MessageBox.Show("Unable to start Inventor.  Error message: " + e2.Message);
                }

            }

            return null;
        }

        /// <summary>
        /// Gets a current Inventor.Application instance.  This will not create a new instance if one cannot be found.
        /// </summary>
        /// <remarks>
        /// VB.net/iLogic example: <code>Dim oApp As Application = ApplicationShim.CurrentInstance()</code>
        /// C# example: <code>Application app = ApplicationShim.CurrentInstance()</code>
        /// </remarks>
        /// <returns>Inventor.Application</returns>
        public static Inventor.Application CurrentInstance()
        {
            Inventor.Application app = null;

            try
            {
                app = (Inventor.Application)Marshal.GetActiveObject("Inventor.Application");
                return app;
            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to get Inventor instance.  Error message: " + e.Message);
            }

            return null;
        }

        /// <summary>
        /// Creates a new Inventor Application instance.
        /// </summary>
        /// <param name="visibility">An optional boolean value indicates the visibility of the new Inventor instance: true = visible.</param>
        /// <remarks>
        /// VB.net/iLogic example: <code>Dim oApp As Application = ApplicationShim.NewInstance()</code>
        /// C# example: <code>Application app = ApplicationShim.NewInstance()</code>
        /// </remarks>
        /// <returns>Inventor.Application</returns>
        public static Inventor.Application NewInstance(bool visibility = true)
        {
            Inventor.Application app = null;

                try //to start inventor
                {
                    Type appType = Type.GetTypeFromProgID("Inventor.Application");
                    app = (Inventor.Application)Activator.CreateInstance(appType);
                    app.Visible = visibility;
                    return app;
                }
                catch (Exception e2)
                {
                    MessageBox.Show("Unable to start Inventor.  Error message: " + e2.Message);
                }


            return null;
        }


        //Code to open Apprentice Server below...
        //Inventor.ApprenticeServerComponent oSvr = new Inventor.ApprenticeServerComponent();
        //Inventor.ApprenticeServerDocument oAppDoc;
        //Inventor.ApprenticeServerDrawingDocument oDwgDoc;

        //    ApprenticeServerDocument document = _apprentice.Open(@"T:\$Work\Part1.ipt");
        //    document.Close();
        //    _apprentice = null;
        //}
        //catch (System.Exception e) { }
    }

}

