using Inventor;
using InventorShims;
using NUnit.Framework;
using System.Runtime.InteropServices;

namespace PathShims_Tests
{
    [TestFixture]
    public class UpOneLevel
    {
        [TestCase(@"C:\A\Test\String\", @"C:\A\Test\")]
        [TestCase(@"C:\A\Test\String", @"C:\A\Test\")]
        [TestCase(@"C:\", @"C:")]
        [TestCase(@"C:/Users/", @"C:/")]
        [TestCase(@"C:\A\", @"C:\")]
        [TestCase("C:", null)]
        [Test]
        public void UpOneLevel_MultipleInputs(string stringA, string stringB)
        {
            Assert.AreEqual(PathShim.UpOneLevel(stringA), stringB);
        }
    }

    [TestFixture]
    public class IsLibraryPath
    {
        [Test]
        public void GoodInput()
        {
            Inventor.Application app = ApplicationShim.Instance();

            var designProject = app.DesignProjectManager.ActiveDesignProject;
            var libraryPaths = designProject.LibraryPaths;

            var i = libraryPaths.Count + 1;

            libraryPaths.Add("temporary path", @"C:\");

            string test = libraryPaths[1].Path;
            test = PathShim.TrimEndingDirectorySeparator(test);

            try
            {
                Assert.IsTrue(test.IsLibraryPath(app));
            }
            finally
            {
                libraryPaths[i].Delete();
                Marshal.ReleaseComObject(app);
                app = null;
            }
        }

        [TestCase(@"C:\Zarthastoriatarigula\the\fiverth")]
        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void BadInput_WrongPath(string testString)
        {
            Inventor.Application app = ApplicationShim.Instance();

            try
            {
                Assert.IsFalse(testString.IsLibraryPath(app));
            }
            finally
            {
                Marshal.ReleaseComObject(app);
                app = null;
            }
        }
    }

    [TestFixture]
    public class IsContentCenterPath
    {
        [Test]
        public void GoodInput()
        {
            Inventor.Application app = ApplicationShim.Instance();

            string test = app.DesignProjectManager.ActiveDesignProject.ContentCenterPath;

            try
            {
                Assert.IsTrue(test.IsContentCenterPath(app));
            }
            finally
            {
                Marshal.ReleaseComObject(app);
                app = null;
            }
}

        [Test]
        public void GoodInput_NoTrailingSlash()
        {
            Inventor.Application app = ApplicationShim.Instance();

            string test = app.DesignProjectManager.ActiveDesignProject.ContentCenterPath;
            test = PathShim.TrimEndingDirectorySeparator(test);

            try
            {
                Assert.IsTrue(test.IsContentCenterPath(app));
            }
            finally
            {
                Marshal.ReleaseComObject(app);
                app = null;
            }
        }

        [TestCase(@"C:\Windows\")]
        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void BadInput_Wrong(string testString)
        {
            Inventor.Application app = ApplicationShim.Instance();

            try
            {
                Assert.IsFalse(testString.IsContentCenterPath(app));
            }
            finally
            {
                Marshal.ReleaseComObject(app);
                app = null;
            }
        }
    }

    [TestFixture]
    public class TrimTrailingSlash
    {
        [TestCase(@"C:\Windows\SysWow\", @"C:\Windows\SysWow")]
        [TestCase(@"C:\Windows\SysWow", @"C:\Windows\SysWow")]
        [Test]
        public void GoodInput_TrailingSlash(string testA, string testB)
        {
            Assert.AreEqual(PathShim.TrimEndingDirectorySeparator(testA), testB);
        }
    }
}