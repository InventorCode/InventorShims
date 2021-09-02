using InventorShims;
using NUnit.Framework;
using System.Collections.Generic;

namespace ExternalRuleDirectories_Tests
{
    [TestFixture]
    public class ExternalRuleDirectoriesTests
    {
        [Test]
        public void Dispose_DoesNotThrowError()
        {
            ExternalRuleDirectories shim = new ExternalRuleDirectories();
            shim.Dispose();
        }

        [Test]
        public void Using_DoesNotThrowError()
        {
            using (ExternalRuleDirectories shim = new ExternalRuleDirectories())
            {
            }
        }

        [Test]
        public void Constructor_doesNotThrowError()
        {
            ExternalRuleDirectories shim = new ExternalRuleDirectories();
        }

        [Test]
        public void ConstructorLong_doesNotThrowError()
        {
            Inventor.Application app = ApplicationShim.Instance();
            ExternalRuleDirectories shim = new ExternalRuleDirectories(app);
        }

        [Test]
        public void ExternalRuleDirectories_doesNotThrowError()
        {
            ExternalRuleDirectories shim = new ExternalRuleDirectories();
            List<string> _ = shim.Directories;
        }

        [Test]
        public void Add_Works()
        {
            using (ExternalRuleDirectories shim = new ExternalRuleDirectories())
            {
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
        }

        [Test]
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