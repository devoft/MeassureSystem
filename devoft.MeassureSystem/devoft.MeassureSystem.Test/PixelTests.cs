using devoft.MeassureSystem;
using devoft.MeassureSystem.UI;
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
            Assert.AreEqual(0.03598288m, (Length)"136px");
        }

        [TestMethod]
        public void TestString()
        {
            var p = new Length(136, "px");
            Assert.AreEqual("135.99999999999116px", p.ToString());
        }

        [TestMethod]
        public void TestConversion()
        {
            Assert.AreEqual(4999.999999999675m, new Length(1.3229m).Px);
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
