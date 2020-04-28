using devoft.MeassureSystem.Length;
using devoft.MeassureSystem.Surface;
using devoft.MeassureSystem.Volume;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

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
        }
    }
}
