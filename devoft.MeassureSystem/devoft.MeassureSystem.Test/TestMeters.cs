using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace devoft.MeassureSystem.Test
{
    [TestClass]
    public class TestMeters
    {
        #region  Meter

        [TestMethod]
        public void TestConstructor()
        {
            Meter m = new Meter(6);
            Assert.AreEqual(6, m.Value);
        }

        [TestMethod]
        public void Conversion()
        {
            Assert.AreEqual(5000m, new Meter(5).mm);
            Assert.AreEqual(500m, new Meter(5).cm);
            Assert.AreEqual(50m, new Meter(5).dm);
            Assert.AreEqual(5m, new Meter(5).m);
            Assert.AreEqual(0.002m, new Meter(2).km);
            Assert.AreEqual(1.093613m, new Meter(1).yd);
            Assert.AreEqual(39.37008m, new Meter(1).inch);
            Assert.AreEqual(3.28084m, new Meter(1).feet);
        }

        [TestMethod]
        public void TestCentimeter()
        {
            Meter m = 5.cm();
            Assert.AreEqual(0.05m, m.Value);
        }
        [TestMethod]
        public void TestSumMeter()
        {
            Meter m = 2.cm() + 5.dm();
            Assert.AreEqual(0.52m, m.Value);
        }

        [TestMethod]
        public void TestSubtractMeter()
        {
            Meter m = 9.dm() - 8.cm();
            Assert.AreEqual(0.82m, m.Value);
        }

        [TestMethod]
        public void TestMultiplierMeter()
        {
            Meter2 m = 5.cm() * 4.cm();
            Assert.AreEqual(0.002m,m.Value);
        }

        [TestMethod]
        public void TestMultiplierScalarMeter()
        {
            Meter m = 4.cm() * 3;
            Assert.AreEqual(0.12m, m.Value);
            Meter m2 = 6 * 7.cm();
            Assert.AreEqual(0.42m, m2.Value);
        }

        [TestMethod]
        public void TestDivMeter()
        {
            decimal m = 40.cm() / 4.cm();
            Assert.AreEqual(10m, m);
        }

        [TestMethod]
        public void TestDivMeterScalar()
        {
            Meter m = 50.cm() / 10;
            Assert.AreEqual(0.05m, m.Value);
        }


        #endregion

        #region Meter2
        [TestMethod]
        public void TestMultiplierMeterMeter2()
        {
            Meter3 m3 = 2.cm() * 3.cm() * 4.cm();
            Assert.AreEqual(0.000024m,m3.Value);

            Meter2 m2 = 5.cm() * 4.cm();
            m3 = m2 * 4.cm();
            Assert.AreEqual(0.00008m, m3.Value);

            m3 = 5.cm() * m2;
            Assert.AreEqual(0.0001m, m3.Value);
        }

        public void TestSumMeter2()
        {
            Meter2 m = 2.cm2() + 5.dm2();
            Assert.AreEqual(0.52m, m.Value);
        }

        [TestMethod]
        public void TestSubtractMeter2()
        {
            Meter2 m = 9.dm2() - 8.cm2();
            Assert.AreEqual(0.82m, m.Value);
        }

        [TestMethod]
        public void TestMultiplierScalarMeter2()
        {
            Meter2 m = 4.cm2() * 3;
            Assert.AreEqual(0.12m, m.Value);
            Meter2 m2 = 6 * 7.cm2();
            Assert.AreEqual(0.42m, m2.Value);
        }

        [TestMethod]
        public void TestDivMeter2()
        {
            decimal m = 40.cm2() / 4.cm2();
            Assert.AreEqual(10m, m);
        }

        [TestMethod]
        public void TestDivMeter2Scalar()
        {
            Meter2 m = 50.cm2() / 10;
            Assert.AreEqual(0.05m, m.Value);
        }

        #endregion

        #region Litre
        public void TestLitre()
        {
            //Litre lt = 5.cm() * 2.m() * 7.dm()  
            //    Metre * Metre = Metre2 (metros cuadrados), 
            //y Metre2 * Metre = Metre3 (metros cúbicos); Metre3 debe poderse convertir a Litre
        }
        #endregion 

    }


}
