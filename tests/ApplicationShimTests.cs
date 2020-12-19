using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;
using Inventor;
using InventorShims;

namespace tests
{
    [TestClass]
    public class ApplicationShimTests
    {
            [TestMethod]
            public void GetInstance_GetsOrStartsInstance()
            {

                Inventor.Application app = ApplicationShim.Instance();
                app.Visible = true;


                var testVariable = app.AssemblyOptions.DeferUpdate;

                Assert.IsNotNull(testVariable);

                app.Quit();
                app = null;

            }

            [TestMethod]
            public void CurrentInstance_NoInventorInstance_ReturnsNothing()
            {
                Inventor.Application app = null;
                app = ApplicationShim.CurrentInstance();

                try
                {
                    Assert.IsNull(app);
                }
                catch (AssertFailedException e)
                {
                    if (app != null)
                    {
                        app.Quit();
                        app = null;
                    }
                }

            }

            [TestMethod]
            public void NewInstance_StartsNewInventorInstance()
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
                        app.Quit();
                        app = null;
                    }
                }

            }

    }

}

