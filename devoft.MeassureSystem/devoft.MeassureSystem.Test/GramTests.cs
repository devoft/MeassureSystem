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

            Gram g2 = (Gram) "3cg";
            Assert.AreEqual(0.03m, g2.Value);

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
            Assert.AreEqual(5, g1.Value);

            Assert.AreEqual(5, (decimal)g1);

            Assert.AreEqual(9, (500.cg() + 400.cg()).Value);

            Assert.AreEqual(1, (500.cg() - 400.cg()).Value);

            Assert.AreEqual(-5, (-50.dg()).Value);

            Assert.AreEqual(10, (2 * 50.dg()).Value);

            Assert.AreEqual(10, (50.dg() * 2).Value);

            Assert.AreEqual(2, 4.dg() / 20.cg());

            Assert.AreEqual(0.005m, (50.mg() / 10m).Value);

            Assert.IsTrue(500.cg() > 5.dg());
        }

        [TestMethod]
        public void TestMiscelanea()
        {
            Assert.AreEqual("4.00cg", (Gram) "4cg");

            Assert.AreEqual(2000.5m, (50.cg() + (Gram) "2kg").Value);
        }
    }
}
