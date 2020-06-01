using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace devoft.MeassureSystem.Test
{
    [TestClass]
    public class PixelTests
    {

        [TestMethod]
        public void TestConstructor()
        {
            Pixel p = new Pixel(7); 
            Assert.AreEqual(7, p.Value);
        }

        [TestMethod]
        public void TestParsing()
        {
            Assert.AreEqual(183.px(), 5.cm() - 6.px());
            Assert.AreEqual(18892.px(), 5.m() - 6.px());
            Assert.AreEqual(136.px(), 5.cm() - 14.mm());
        }

        [TestMethod]
        public void TestConversion()
        {
            Pixel p = 1.inch();
            Assert.AreEqual(96, p.Value);
        }

        [TestMethod]
        public void TestOperator()
        {
            Pixel p = new Pixel(7); //1.852083333

            Assert.AreEqual(9.px(), 4.px()+5.px());
            Assert.AreEqual(10.px(), 15.px() - 5.px());
            Assert.AreEqual(90.px(), 1.inch() - 6.px());
        }
    }
}
