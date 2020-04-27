using devoft.MeassureSystem.Weight;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace devoft.MeassureSystem.Test
{
    [TestClass]
    public class GramTests
    {
        [TestMethod]
        public void TestParsing()
        {
            Assert.AreEqual(35000, Gram.Parse("35kg").Value);

            Gram g1 = (Gram) "3cg";
            Assert.AreEqual(0.03m, g1.Value);

            Gram g2 = (Gram)"3oz";
            Assert.AreEqual(85.04856m, g2.Value);

            Assert.ThrowsException<FormatException>(() => { Gram g2 = (Gram) "3cm"; });
        }

        [TestMethod]
        public void TestToString()
        {
            Gram g = new Gram(4, "cg");
            Assert.AreEqual("4.00cg", g);

            string str = (string) g;
            Assert.AreEqual("4.00cg", str);
            Assert.AreEqual("4cg", g.ToString("N0"));
        }

        [TestMethod]
        public void TestOperators()
        {
            Gram g1 = 5;
            
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
            Assert.AreEqual(0.004m, new Gram(4).kg);
            Assert.AreEqual(0.04m, new Gram(4).hg);
            Assert.AreEqual(0.4m, new Gram(4).dag);
            Assert.AreEqual(40m,    new Gram(4).dg);
            Assert.AreEqual(400m,   new Gram(4).cg);
            Assert.AreEqual(4000m,  new Gram(4).mg);

        }


        [TestMethod]
        public void TestMiscelanea()
        {
            Assert.AreEqual("4.00cg", (Gram) "4cg");
            Assert.IsTrue((1.g() - 1.g().lb.lb()) < 1.mg());
            Assert.IsTrue((1.g() - 1.g().oz.oz()) < 1.mg());
            Assert.AreEqual(2000.5m, (50.cg() + (Gram) "2kg").Value);
        }
    }
}
