using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;
using Inventor;
using InventorShims;
using System.Diagnostics;

namespace ApplicationShimTests
{
    [TestClass]
    public class Instance
    {
        [TestMethod]
        public void GetsOrStartsInstance()
        {

            Inventor.Application app = ApplicationShim.Instance();
            app.Visible = true;

            var testVariable = app.AssemblyOptions.DeferUpdate;

            Assert.IsNotNull(testVariable);

            Process[] procs = Process.GetProcessesByName("Inventor");
            app.Quit();
            if (procs.Length == 1) { procs[0].WaitForExit(); }

            app = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

        }
    }


    [TestClass]
    public class NewInstance
    {

        [TestMethod]
        public void Works_isVisible()
        {
            Inventor.Application app = null;
            app = ApplicationShim.NewInstance();

            try
            {
                Assert.IsNotNull(app);
            }

            finally
            {
                if (app != null)
                {
                    Process[] procs = Process.GetProcessesByName("Inventor");
                    app.Quit();
                    if (procs.Length == 1) { procs[0].WaitForExit(); }
                    app = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }

        [TestMethod]
        public void Works_isInvisible()
        {
            Inventor.Application app = null;
            app = ApplicationShim.NewInstance(false);

            try
            {
                Assert.IsNotNull(app);
            }

            finally
            {
                if (app != null)
                {
                    Process[] procs = Process.GetProcessesByName("Inventor");
                    app.Quit();
                    if (procs.Length == 1) { procs[0].WaitForExit(); }

                    app = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }

        }
    }

}

