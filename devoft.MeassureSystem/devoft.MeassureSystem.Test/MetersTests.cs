using devoft.MeassureSystem.Length;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace devoft.MeassureSystem.Test
{
    [TestClass]
    public class MetersTests
    {

        [TestMethod]
        public void TestConstructor()
        {
            Meter m = new Meter(6);
            Assert.AreEqual(6, m.Value);
        }

        [TestMethod]
        public void TestParsing()
        {
            Assert.AreEqual(5000, Meter.Parse("5km").Value);

            Meter m1 = (Meter) "10cm";
            Assert.AreEqual(0.1m, m1.Value);

            m1 = (Meter)"5yd";
            Assert.AreEqual(4.572m, m1.Value);

            m1 = (Meter)"5in";
            Assert.AreEqual(0.127m, m1.Value);

            m1 = (Meter)"5ft";
            Assert.AreEqual(1.524m, m1.Value);
        }

        [TestMethod]
        public void TestToString()
        {
            Meter m = new Meter(5, "km");
            Assert.AreEqual("5km", m.ToString());

            Assert.AreEqual("10", (40.cm() / 4.cm()).ToString());
            Assert.AreEqual("0.05m", (50.cm() / 10).ToString());
        }

        [TestMethod]
        public void Conversion()
        {
            Assert.AreEqual(5000m, new Meter(5).Mm);
            Assert.AreEqual(500m, new Meter(5).Cm);
            Assert.AreEqual(50m, new Meter(5).Dm);
            Assert.AreEqual(5m, new Meter(5).M);
            Assert.AreEqual(0.002m, new Meter(2).Km);
            Assert.AreEqual(1.093613m, new Meter(1).Yd);
            Assert.AreEqual(39.37008m, new Meter(1).Inch);
            Assert.AreEqual(3.28084m, new Meter(1).Ft);
        }

        [TestMethod]
        public void TestOperators()
        {
            Meter m = 8;
            Assert.AreEqual(8m , m);

            m = 5.cm();
            Assert.AreEqual(0.05m, m.Value);
            
            Assert.AreEqual(0.52m, 2.cm() + 5.dm());
            Assert.AreEqual(0.82m, 9.dm() - 8.cm());
            Assert.AreEqual(0.12m, 4.cm() * 3);
            Assert.AreEqual(0.42m, 6 * 7.cm());
            Assert.AreEqual(10m, 40.cm() / 4.cm());
            Assert.AreEqual(0.05m, 50.cm() / 10);
            
        }

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
