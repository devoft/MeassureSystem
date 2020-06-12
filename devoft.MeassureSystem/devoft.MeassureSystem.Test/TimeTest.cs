using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace devoft.MeassureSystem.Test
{
    [TestClass]
    public class TimeTest
    {
        [TestMethod]
        public void TestDeconstruction()
        {
            var (d, h, min, s, m) = 200.s();
            Assert.AreEqual((0, 0, 3, 20, 0), (d, h, min, s, m));
        }

        [TestMethod]
        public void TestParsing()
        {
            var (d,h,min,s,ms) = Time.Parse("3d:1min:30s");
            Assert.AreEqual((3, 0, 1, 30, 0), (d, h, min, s, ms));

            (d, h, min, s, ms) = Time.Parse("3.0:1:30.0");
            Assert.AreEqual((3, 0, 1, 30, 0), (d, h, min, s, ms));

        }

        [TestMethod]
        public void TestWrongParsing()
        {
            Assert.ThrowsException<FormatException>(() => Time.Parse("3d:30s:1min"));
            Assert.ThrowsException<FormatException>(() => Time.Parse("3d:30s:2s"));
            Assert.ThrowsException<FormatException>(() => Time.Parse(""));
        }

        [TestMethod]
        public void SortUsingComparable()
        {
            var jobs = new[]
            {
                new Job { Duration = 3.min() },
                new Job { Duration = 3.h() },
                new Job { Duration = 3.s() }
            };
            var sorted = string.Join(',', jobs.OrderBy(x => x.Duration).Select(x => x.Duration.ToString()));
            
            Assert.AreEqual("3s,3min,3h", sorted);
        }

        public class Job { public Time Duration { get; set; } }
    }


}
