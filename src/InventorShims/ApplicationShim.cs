using System;
using System.Runtime.InteropServices;
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
        /// VB.net/iLogic example: <code>Dim oApp As Application = ApplicationShim.Instance()</code>
        /// </summary>
        /// <remarks>
        /// C# example: <code>Application app = ApplicationShim.Instance()</code>
        /// </remarks>
        /// <returns>Inventor.Application</returns>
        public static Inventor.Application Instance()
        {
            Inventor.Application app = null;

            try
            {
                return (Inventor.Application) Marshal.GetActiveObject("Inventor.Application");
            }
            catch
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
                    throw new SystemException("Unable to start Inventor.  Error message: " + e2.Message, e2);
                }
            }

            return null;
        }

        /// <summary>
        /// Gets a current Inventor.Application instance.  This will not create a new instance if one cannot be found.
        /// VB.net/iLogic example: <code>Dim oApp As Application = ApplicationShim.CurrentInstance()</code>
        /// </summary>
        /// <remarks>
        /// C# example: <code>Application app = ApplicationShim.CurrentInstance()</code>
        /// </remarks>
        /// <returns>Inventor.Application</returns>
        public static Inventor.Application CurrentInstance()
        {
            try
            {
                return (Inventor.Application)Marshal.GetActiveObject("Inventor.Application");
            }
            catch (Exception e)
            {
                throw new SystemException("Unable to get Inventor instance.  Error message: " + e.Message, e);
            }
        }

        /// <summary>
        /// Creates a new Inventor Application instance.
        /// </summary>
        /// <param name="visibility">An optional boolean value indicates the visibility of the new Inventor instance: true = visible.</param>
        /// VB.net/iLogic example: <code>Dim oApp As Application = ApplicationShim.NewInstance()</code>
        /// <remarks>
        /// C# example: <code>Application app = ApplicationShim.NewInstance()</code>
        /// </remarks>
        /// <returns>Inventor.Application</returns>
        public static Inventor.Application NewInstance(bool visibility = true)
        {
            Inventor.Application app;

                try //to start inventor
                {
                    Type appType = Type.GetTypeFromProgID("Inventor.Application");
                    app = (Inventor.Application)Activator.CreateInstance(appType);
                    app.Visible = visibility;
                    return app;
                }
                catch (Exception e2)
                {
                    throw new SystemException("Unable to start Inventor.  Error message: " + e2.Message, e2);
                }
        }
    }
}

