using InventorShims;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ExternalRuleDirectories_Tests
{
    [TestClass]
    public class ExternalRuleDirectoriesTests
    {
        [TestMethod]
        public void Constructor_doesNotThrowError()
        {
            ExternalRuleDirectories shim = new ExternalRuleDirectories();
        }

        [TestMethod]
        public void ConstructorLong_doesNotThrowError()
        {
            Inventor.Application app = ApplicationShim.Instance();
            ExternalRuleDirectories shim = new ExternalRuleDirectories(app);
        }

        [TestMethod]
        public void ExternalRuleDirectories_doesNotThrowError()
        {
            ExternalRuleDirectories shim = new ExternalRuleDirectories();
            List<string> _ = shim.Directories;
        }

        [TestMethod]
        public void Add_Works()
        {
            ExternalRuleDirectories shim = new ExternalRuleDirectories();
            var test = @"C:\Users\Default\AppData\Local\Temp\";
            shim.Add(test);

            List<string> answer = shim.Directories;

            try
            {
                Assert.IsTrue(answer.Contains(test));
            }
            finally
            {
                shim.Remove(test);
            }
        }

        [TestMethod]
        public void Contains_Works()
        {
            ExternalRuleDirectories shim = new ExternalRuleDirectories();
            var test = @"C:\Users\Default\AppData\Local\Temp\";
            shim.Add(test);

            try
            {
                Assert.IsTrue(shim.Contains(test));
            }
            finally
            {
                shim.Remove(test);
            }
        }
    }
}