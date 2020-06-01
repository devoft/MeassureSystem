using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace devoft.MeassureSystem.Test
{
    [TestClass]
    public class AreaTests
    {
        [TestMethod]
        public void TestConstructor()
        {
            Area m2 = new Area(5);
            Assert.AreEqual(5, m2.M2);
        }

        [TestMethod]
        public void TestParsing()
        {
            Assert.AreEqual(5000000, Area.Parse("5km2").M2);

            Area m1 = (Area)"20cm2";
            Assert.AreEqual(0.002m, m1.M2);

            Assert.AreEqual(0.00006m, ((Area)"60mm2").M2);
        }

        [TestMethod]
        public void TestToString()
        {
            Area m2 = new Area(7, "km2");
            Assert.AreEqual("7km2", m2.ToString());
            Assert.AreEqual("10", (40.cm2() / 4.cm2()).ToString());
            Assert.AreEqual("0.0005m2", (50.cm2() / 10).ToString());
            Assert.AreEqual("0.1m", (40.cm2() / 4.cm()).ToString());
            Assert.AreEqual("45", (90.dam2() / 2.dam2()).ToString());

            Volume m3 = new Volume(7, "km3");
            Assert.AreEqual("7km3", m3.ToString());
            Assert.AreEqual("10", (40.mm3() / 4.mm3()).ToString());
            Assert.AreEqual("0.1m", (40.cm3() / 4.cm2()).ToString());
            Assert.AreEqual("45", (90.dam3() / 2.dam3()).ToString());

            Assert.AreEqual("45", (90m.dam2() / 2m.dam2()).ToString());
            Assert.AreEqual("10", (40m.mm3() / 4m.mm3()).ToString());
            Assert.AreEqual("0.1m", (40m.cm3() / 4m.cm2()).ToString());
            Assert.AreEqual("45", (90.dam3() / 2m.dam3()).ToString());
        }

        [TestMethod]
        public void TestOperators()
        {
            Assert.AreEqual(0.0502m, (2.cm2() + 5.dm2()).M2);
            Assert.AreEqual(0.0892m, (9.dm2() - 8.cm2()).M2);
            Assert.AreEqual(0.002m, (5.cm() * 4.cm()).M2);
            Assert.AreEqual(0.0012m, (4.cm2() * 3).M2);
            Assert.AreEqual(0.0042m, (6 * 7.cm2()).M2);
            Assert.AreEqual(10m, 40.cm2() / 4.cm2());
            Assert.AreEqual(0.0005m, (50.cm2() / 10).M2);
            Assert.AreEqual(20.mm2(), 80.mm2() / 4);
            Assert.AreEqual(20.hm2(), 80.hm2() / 4);
            Assert.AreEqual(25000000.m2(), 1.km2() / 4.dm2());
            Assert.AreEqual(10.cm(), 40.cm2() / 4.cm());
            Assert.AreEqual(10, 40.m2() / 4);

            Assert.AreEqual(0.0502m, (2m.cm2() + 5m.dm2()).M2);
            Assert.AreEqual(20.mm2(), 80m.mm2() / 4);
            Assert.AreEqual(20.hm2(), 80m.hm2() / 4);
            Assert.AreEqual(10, 40m.m2() / 4);
            Assert.AreEqual(25000000.m2(), 1m.km2() / 4m.dm2());
        }
    }
}
