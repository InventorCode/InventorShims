﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;
using Inventor;
using InventorShims;

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

            app.Quit();
            app = null;

        }
    }


    [TestClass]
    public class NewInstance {

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

