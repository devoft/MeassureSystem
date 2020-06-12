using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace devoft.MeassureSystem.Test
{
    [TestClass]
    public class VolumeTests
    {
        [TestMethod]
        public void TestParsing()
        {
            Assert.AreEqual(0.07m, 5.cm() * 2.m() * 7.dm());
        }

        [TestMethod]
        public void TestToString()
        {
            Assert.AreEqual("70l", (5.cm() * 2.m() * 7.dm()).l().ToString());
        }

        [TestMethod]
        public void Conversion()
        {
            Assert.AreEqual(10m, new Volume(10).Meter3);
            Assert.AreEqual(10000000000m, new Volume(10).Millimeter3);
            Assert.AreEqual(0.00000000011m, new Volume(0.110m).Kilometer3);
        }

        [TestMethod]
        public void TestOperators()
        {
            Assert.AreEqual(0.000024m, (2.cm() * 3.cm() * 4.cm()).Meter3);

            var m = (5.cm() * 4.cm()).Meter2 * 4.cm();
            Assert.AreEqual(0.00008m, m.Meter);

            m = 5.cm() * (5.cm() * 4.cm()).Meter2;
            Assert.AreEqual(0.0001m, m.Meter);

            Assert.AreEqual(250, 1.km3() / 4.hm3());
            Assert.AreEqual(10m, 40.hm3() / 4.hm3());
            Assert.AreEqual(10.m3(), 40.m3() / 4);
            Assert.AreEqual(10m, 40.dm3() / 4.dm3());

            Assert.AreEqual(10m, 40.l() / 4.l());
            Assert.AreEqual(5.l(), new Volume(100.l() / 20.l(),"l"));
            Assert.AreEqual(5.ml(), new Volume(100.ml() / 20.ml(), "ml"));
            Assert.AreEqual(0.000005m, 100.ml() / 20m);

            Assert.AreEqual(10m, 40m.dm3() / 4m.dm3());
            Assert.AreEqual(10.m3(), 40m.m3() / 4);
            Assert.AreEqual(10m, 40m.hm3() / 4m.hm3());
            Assert.AreEqual(250, 1m.km3() / 4m.hm3());
            Assert.AreEqual(3, 4.5m.km3() / 1.5m.km3());

            Assert.AreEqual(70.l(), 5.cm() * 2.m() * 7.dm());
            Assert.AreEqual(1000.l(), 1.m() * 1.m() * 1.m());
            Assert.AreEqual(0.000001m, 1.cm() * 1.cm() * 1.cm());

            Assert.IsTrue((Pixel) 200 > 2.inch());

        }
    }
}
