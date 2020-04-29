using devoft.MeassureSystem.Length;
using devoft.MeassureSystem.Volume;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace devoft.MeassureSystem.Test
{
    [TestClass]
    public class VolumeTests
    {
        [TestMethod]
        public void TestOperators()
        {
            Assert.AreEqual(0.000024m, (2.cm() * 3.cm() * 4.cm()).Value);

            var m3 = (5.cm() * 4.cm()).Value * 4.cm();
            Assert.AreEqual(0.00008m, m3.Value);

            m3 = 5.cm() * (5.cm() * 4.cm()).Value;
            Assert.AreEqual(0.0001m, m3.Value);

            Assert.AreEqual(250, 1.km3() / 4.hm3());
            Assert.AreEqual(10m, 40.hm3() / 4.hm3());
            Assert.AreEqual(10.m3(), 40.m3() / 4);
            Assert.AreEqual(10m, 40.dm3() / 4.dm3());

            Assert.AreEqual(10m, 40.l() / 4.l());
            Assert.AreEqual(5.l(), new Meter3(100.l() / 20.l(),"l"));
            Assert.AreEqual(5.ml(), new Meter3(100.ml() / 20.ml(), "ml"));
            Assert.AreEqual(0.000005m, 100.ml() / 20m);

            Assert.AreEqual(10m, 40m.dm3() / 4m.dm3());
            Assert.AreEqual(10.m3(), 40m.m3() / 4);
            Assert.AreEqual(10m, 40m.hm3() / 4m.hm3());
            Assert.AreEqual(250, 1m.km3() / 4m.hm3());
            Assert.AreEqual(3, 4.5m.km3() / 1.5m.km3());


        }
    }
}
