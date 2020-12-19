using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InventorShims;

namespace PathShims_Tests
{
    [TestClass]
    public class UpOneLevel
    {

        [TestMethod]
        public void UpOneLevel_TrailingBackslash()
        {
            var stringA = @"C:\A\Test\String\";
            var stringB = @"C:\A\Test\";
            Assert.AreEqual(PathShim.UpOneLevel(stringA), stringB);
        }

        [TestMethod]
        public void UpOneLevel_NoTrailingBackslash()
        {
            var stringA = @"C:\A\Test\String";
            var stringB = @"C:\A\Test\";
            Assert.AreEqual(PathShim.UpOneLevel(stringA), stringB);
        }

        [TestMethod]
        public void UpOneLevel_NoBackslashes()
        {
            var stringA = @"C:";
            string stringB = "";
            Assert.AreNotEqual(PathShim.UpOneLevel(stringA), stringB);
        }

        [TestMethod]
        public void UpOneLevel_TwoLevelWithTrailingBackslash()
        {
            var stringA = @"C:\A\";
            var stringB = @"C:\";
            Assert.AreEqual(PathShim.UpOneLevel(stringA), stringB);
        }

        [TestMethod]
        public void UpOneLevel_TopLevel_ReturnsSelf()
        {
            var stringA = @"C:\";
            var stringB = @"C:";
            Assert.AreEqual(PathShim.UpOneLevel(stringA), stringB);
        }

        [TestMethod]
        public void UpOneLevel_ForwardSlashes_ReturnNothing()
        {
            var stringA = @"C:/Users/";
            var stringB = @"C:/";
            Assert.AreEqual(PathShim.UpOneLevel(stringA), stringB);
        }

    }


    [TestClass]
    public class IsLibraryPath
    {

        [TestMethod]
        public void GoodInput()
        {
            Inventor.Application app = ApplicationShim.Instance();

            var designProject = app.DesignProjectManager.ActiveDesignProject;
            var libraryPaths = designProject.LibraryPaths;
            bool addedFlag = false;

            if (libraryPaths.Count > 0)
            {
                libraryPaths.Add("temporary path", @"C:\");
                addedFlag = true;
            }

            var test = libraryPaths[1].Path;
            test = PathShim.TrimEndingDirectorySeparator(test);


            try
            {
                Assert.IsTrue(PathShim.IsLibraryPath(test, ref app));

            }
            finally
            {
                if (addedFlag = true)
                {
                    libraryPaths.Clear();
                    addedFlag = false;
                }
                app.Quit();
                app = null;
            }


        }

        public void BadInput_WrongPath()
        {
            Inventor.Application app = ApplicationShim.Instance();

            var test = @"C:\Windows";
            try
            {
                Assert.IsFalse(PathShim.IsLibraryPath(test, ref app));
            }
            finally
            {
                app.Quit();
                app = null;
            }
        }

        [TestMethod]
        public void BadInput_Empty()
        {
            Inventor.Application app = ApplicationShim.Instance();

            var test = "";

            try
            {
                Assert.IsFalse(PathShim.IsLibraryPath(test, ref app));
            }
            finally
            {
                app.Quit();
                app = null;
            }
        }

        [TestMethod]
        public void BadInput_Null()
        {
            Inventor.Application app = ApplicationShim.Instance();

            string test = null;

            try
            {
                Assert.IsFalse(PathShim.IsLibraryPath(test, ref app));
            }
            finally
            {
                app.Quit();
                app = null;
            }
        }

    }

    [TestClass]
    public class IsContentCenterPath
    {
        [TestMethod]
        public void GoodInput()
        {
            Inventor.Application app = ApplicationShim.Instance();

            string test = app.DesignProjectManager.ActiveDesignProject.ContentCenterPath;

            try
            {
                Assert.IsTrue(PathShim.IsContentCenterPath(test, ref app));
            }
            finally
            {
                app.Quit();
                app = null;
            }
        }

        [TestMethod]
        public void GoodInput_NoTrailingSlash()
        {
            Inventor.Application app = ApplicationShim.Instance();

            string test = app.DesignProjectManager.ActiveDesignProject.ContentCenterPath;
            test = PathShim.TrimEndingDirectorySeparator(test);

            try
            {
                Assert.IsTrue(PathShim.IsContentCenterPath(test, ref app));
            }
            finally
            {
                app.Quit();
                app = null;
            }
        }


        [TestMethod]
        public void BadInput_Wrong()
        {
            Inventor.Application app = ApplicationShim.Instance();
            string test = @"C:\Windows\";

            try
            {
                Assert.IsFalse(PathShim.IsContentCenterPath(test, ref app));
            }
            finally
            {
                app.Quit();
                app = null;
            }
        }

        [TestMethod]
        public void BadInput_Empty()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var test = string.Empty;

            try
            {
                Assert.IsFalse(PathShim.IsContentCenterPath(test, ref app));
            }
            finally
            {
                app.Quit();
                app = null;
            }
        }

        [TestMethod]
        public void BadInput_Null()
        {
            Inventor.Application app = ApplicationShim.Instance();
            var dp = app.DesignProjectManager.DesignProjects.ItemByName["Default"];
            dp.Activate();

            string test = null;

            try
            {
                Assert.IsFalse(PathShim.IsContentCenterPath(test, ref app));
            }
            finally
            {
                app.Quit();
                app = null;
            }
        }

    }

    [TestClass]
    public class TrimTrailingSlash
    {
        [TestMethod]
        public void GoodInput_TrailingSlash()
        {
            string test = @"C:\Windows\SysWow\";
            string test2 = @"C:\Windows\SysWow";
            Assert.AreEqual(PathShim.TrimEndingDirectorySeparator(test), test2);
        }

        [TestMethod]
        public void GoodInput_NoTrailingSlash()
        {
            string test = @"C:\Windows\SysWow";
            string test2 = @"C:\Windows\SysWow";
            Assert.AreEqual(PathShim.TrimEndingDirectorySeparator(test), test2);
        }



    }
}
