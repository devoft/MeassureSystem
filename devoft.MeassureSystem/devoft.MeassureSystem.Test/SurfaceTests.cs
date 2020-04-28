using devoft.MeassureSystem.Length;
using devoft.MeassureSystem.Surface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace devoft.MeassureSystem.Test
{
    [TestClass]
    public class SurfaceTests
    {
        [TestMethod]
        public void TestConstructor()
        {
            Meter2 m2 = new Meter2(5);
            Assert.AreEqual(5, m2.Value);
        }

        [TestMethod]
        public void TestParsing()
        {
            Assert.AreEqual(5000000, Meter2.Parse("5km2").Value);

            Meter2 m1 = (Meter2)"20cm2";
            Assert.AreEqual(0.002m, m1.Value);

            Assert.AreEqual(0.00006m, ((Meter2)"60mm2").Value);
        }

        [TestMethod]
        public void TestToString()
        {
            Meter2 m = new Meter2(7, "km2");
            Assert.AreEqual("7km2", m.ToString());

            Assert.AreEqual("10", (40.cm2() / 4.cm2()).ToString());
            Assert.AreEqual("0.0005m2", (50.cm2() / 10).ToString());
            Assert.AreEqual("0.1m", (40.cm2() / 4.cm()).ToString());
            Assert.AreEqual(10.cm(), 40.cm2() / 4.cm());
        }

        [TestMethod]
        public void TestOperators()
        {
            Assert.AreEqual(0.0502m, (2.cm2() + 5.dm2()).Value);

            Assert.AreEqual(0.0892m, (9.dm2() - 8.cm2()).Value);
            
            Assert.AreEqual(0.002m, (5.cm() * 4.cm()).Value);
            Assert.AreEqual(0.0012m, (4.cm2() * 3).Value);
            Assert.AreEqual(0.0042m, (6 * 7.cm2()).Value);

            Assert.AreEqual(10m, 40.cm2() / 4.cm2());
            Assert.AreEqual(0.0005m, (50.cm2() / 10).Value);
        }
    }
}
