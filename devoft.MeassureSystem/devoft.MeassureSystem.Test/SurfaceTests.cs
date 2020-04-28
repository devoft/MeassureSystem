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
        public void TestOperators()
        {
            Assert.AreEqual(0.52m, (2.cm2() + 5.dm2()).Value);

            Assert.AreEqual(0.82m, (9.dm2() - 8.cm2()).Value);
            
            Assert.AreEqual(0.002m, (5.cm() * 4.cm()).Value);
            Assert.AreEqual(0.12m, (4.cm2() * 3).Value);
            Assert.AreEqual(0.42m, (6 * 7.cm2()).Value);

            Assert.AreEqual(10m, 40.cm2() / 4.cm2());
            Assert.AreEqual(0.05m, (50.cm2() / 10).Value);
        }
    }
}
