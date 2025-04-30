using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab5Blazor.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AlwaysPasses()
        {
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void AlwaysFails()
        {
            Assert.AreEqual(1, 2); // <- You can use this to test failure later
        }
    }
}
