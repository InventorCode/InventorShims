using InventorShims;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ApplicationShimTests
{
    [TestFixture]
    public class Instance
    {
        [Test]
        public void GetsOrStartsInstance()
        {
            Inventor.Application app = ApplicationShim.Instance();
            app.Visible = true;

            var testVariable = app.AssemblyOptions.DeferUpdate;

            Assert.IsNotNull(testVariable);

            Process[] procs = Process.GetProcessesByName("Inventor");
            app.Quit();
            if (procs.Length == 1) { procs[0].WaitForExit(); }

            Marshal.ReleaseComObject(app);
            app = null;
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
        }
    }

    [TestFixture]
    public class NewInstance
    {
        [Test]
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

                    Marshal.ReleaseComObject(app);
                    app = null;
                    //GC.Collect();
                    //GC.WaitForPendingFinalizers();
                }
            }
        }

        [Test]
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

                    Marshal.ReleaseComObject(app);
                    app = null;
                    //GC.Collect();
                    //GC.WaitForPendingFinalizers();
                }
            }
        }
    }
}