using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace devoft.MeassureSystem.Test
{
    [TestClass]
    public class LengthTests
    {

        [TestMethod]
        public void TestConstructor()
        {
            Length m = new Length(6);
            Assert.AreEqual(6, m.Meter);

        }

        [TestMethod]
        public void TestParsing()
        {
            Assert.AreEqual(5000, Length.Parse("5km").Meter);
            Length m1 = (Length) "10cm";
            Assert.AreEqual(0.1m, m1.Meter);
            m1 = (Length)"5yd";
            Assert.AreEqual(4.572m, m1.Meter);
            m1 = (Length)"5in";
            Assert.AreEqual(0.127m, m1.Meter);
            m1 = (Length)"5ft";
            Assert.AreEqual(1.524m, m1.Meter);
            Assert.AreEqual("3cm",Length.Parse("3cm"));
            Assert.AreEqual("20cm", (Length)"20cm");
            Assert.AreEqual("2020m", 2.km() + (Length)"20m");
        }

        [TestMethod]
        public void TestToString()
        {
            Length m = new Length(5, "km");
            Assert.AreEqual("5km", m.ToString());
            Assert.AreEqual("5.00cm", 5.cm().ToString("N02"));
            Assert.AreEqual("5.00yd", 5.yd().ToString("N02"));
            Assert.AreEqual("55m", (500.cm() + 500.dm()).ToString());
            Assert.AreEqual("10", (40.cm() / 4.cm()).ToString());
            Assert.AreEqual("0.05m", (50.cm() / 10).ToString());
        }

        [TestMethod]
        public void Conversion()
        {
            Assert.AreEqual(5000m, new Length(5).Millimeter);
            Assert.AreEqual(500m, new Length(5).Centimeter);
            Assert.AreEqual(50m, new Length(5).Decimeter);
            Assert.AreEqual(5m, new Length(5).Meter);
            Assert.AreEqual(0.8m, new Length(8).Decameter);
            Assert.AreEqual(0.08m, new Length(8).Hectometer);
            Assert.AreEqual(0.002m, new Length(2).Kilometer);
            Assert.AreEqual(1.093613m, new Length(1).Yard);
            Assert.AreEqual(39.37008m, new Length(1).Inch);
            Assert.AreEqual(3.28084m, new Length(1).Feet);
        }

        [TestMethod]
        public void TestOperators()
        {
            Length l = 8;
            Assert.AreEqual(8m , l);
            Assert.AreEqual(55m, 500.cm() + 500.dm());
            Assert.AreEqual(0.05m, 5.cm());
            Assert.AreEqual(0.502m, 2.mm() + 5.dm());
            Assert.AreEqual(0.52m, 2.cm() + 5.dm());
            Assert.AreEqual(0.82m, 9.dm() - 8.cm());
            Assert.AreEqual(5.82m, 9.dm() - 8.cm() + 5.m());
            Assert.AreEqual(50.82m, 9.dm() - 8.cm() + 5.dam());
            Assert.AreEqual(700.46m, 5.dm() - 4.cm() + 7.hm());
            Assert.AreEqual(5700m, 5.km() + 7.hm());
            Assert.AreEqual(12.8016m, 6.yd() + 8.yd());
            Assert.AreEqual(14.yd(), 6.yd() + 8.yd());
            Assert.AreEqual(0.4318m, 9.inch() + 8.inch());
            Assert.AreEqual(9.inch(), 5.inch() + 4.inch());
            Assert.AreEqual(2.4384m, 5.ft() + 3.ft());
            Assert.AreEqual(8.ft(), 5.ft() + 3.ft());
            Assert.AreEqual(0.12m, 4.cm() * 3);
            Assert.AreEqual(0.42m, 6 * 7.cm());
            Assert.AreEqual(10m, 40.cm() / 4.cm());
            Assert.AreEqual(0.05m, 50.cm() / 10);
        }

        [TestMethod]
        public void TestComparison()
        {
            var pipes = new List<Pipe> { new Pipe { Diameter = 2.m() }, 
                                         new Pipe { Diameter = 30.mm() }, 
                                         new Pipe { Diameter = 10.cm() }
                                       };
            Assert.AreEqual("30mm,10cm,2m", string.Join(',', pipes.OrderBy(p => p.Diameter).Select(p => p.Diameter)));
        }

        public class Pipe
        {
            public Length Diameter { get; set; }
        }
    }
}
