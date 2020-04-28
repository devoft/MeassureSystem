using Microsoft.VisualStudio.TestTools.UnitTesting;
using devoft.MeassureSystem.Time;

namespace devoft.MeassureSystem.Test
{
    [TestClass]
    public class TimeTest
    {
        [TestMethod]
        public void TestDeconstruction()
        {
            var (h, min, s, m) = 200.s();
            Assert.AreEqual((0, 3, 20, 0), (h, min, s, m));
        }
    }
}
