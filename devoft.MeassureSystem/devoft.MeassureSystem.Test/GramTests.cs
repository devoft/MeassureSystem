using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace devoft.MeassureSystem.Test
{
    [TestClass]
    public class GramTests
    {
        [TestMethod]
        public void TestParsing()
        {
            Assert.AreEqual(35000, Weight.Parse("35kg").g);

            Weight g1 = (Weight) "3cg";
            Assert.AreEqual(0.03m, g1.g);

            Weight g2 = (Weight)"3oz";
            Assert.AreEqual(85.04856m, g2.g);

            Assert.ThrowsException<FormatException>(() => { Weight g2 = (Weight) "3cm"; });
        }

        [TestMethod]
        public void TestToString()
        {
            Weight g = new Weight(4, "cg");
            Assert.AreEqual("4cg", g);

            string str = g;
            Assert.AreEqual("4cg", str);
            Assert.AreEqual("4.00cg", g.ToString("N02"));

            Assert.AreEqual("3.14159265358979g", new Weight((decimal)Math.PI));
        }

        [TestMethod]
        public void TestOperators()
        {
            Weight g1 = 5;
            
            Assert.AreEqual(5m,     g1                  );
            Assert.AreEqual(9m,     500.cg() + 400.cg() );
            Assert.AreEqual(1m,     500.cg() - 400.cg() );
            Assert.AreEqual(-5m,    -50.dg()            );
            Assert.AreEqual(10m,    2 * 50.dg()         );
            Assert.AreEqual(10m,    50.dg() * 2         );
            Assert.AreEqual(2m,     4.dg() / 20.cg()    );
            Assert.AreEqual(0.005m, 50.mg() / 10m       );

            g1 += 2.cg();

            Assert.AreEqual(5.02m, g1);

            Assert.IsTrue(500.cg() > 5.dg());
        }

        [TestMethod]
        public void Conversion()
        {
            Assert.AreEqual(0.004m, new Weight(4).kg);
            Assert.AreEqual(0.04m, new Weight(4).hg);
            Assert.AreEqual(0.4m, new Weight(4).dag);
            Assert.AreEqual(40m,    new Weight(4).dg);
            Assert.AreEqual(400m,   new Weight(4).cg);
            Assert.AreEqual(4000m,  new Weight(4).mg);

        }


        [TestMethod]
        public void TestMiscelanea()
        {
            Assert.AreEqual("4cg", (Weight) "4cg");
            Assert.IsTrue((1.g() - 1.g().lb.lb()) < 1.mg());
            Assert.IsTrue((1.g() - 1.g().oz.oz()) < 1.mg());
            Assert.AreEqual(2000.5m, (50.cg() + (Weight) "2kg").g);
        }
    }
}
