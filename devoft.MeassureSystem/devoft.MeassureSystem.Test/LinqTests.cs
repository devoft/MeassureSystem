using devoft.MeassureSystem.Linq;
using devoft.System;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace devoft.MeassureSystem.Test
{
    [TestClass]
    public class LinqTests
    {
        private Sample[]? _sampleList;

        [TestInitialize]
        public void Inititalize()
        {
            _sampleList = new []
            {
                new Sample { Id = 0, Duration = 2.min(),    Size = 10},
                new Sample { Id = 1, Duration = 2.h(),      Size = 100},
                new Sample { Id = 2, Duration = 2.s(),      Size = 1},
                new Sample { Id = 3, Duration = 2.d(),      Size = 1000}
            };
        }

        [TestMethod]
        public void TestTimeQuery()
        {
            var time = TimeSpan.FromMinutes(3);
            var baseQuery = _sampleList.AsQueryable().AllowUnits();

            IQueryable<Sample>? result = default;
            result = baseQuery.Where(s => s.Duration > 3.min() + 2.s());
            Assert.AreEqual("1,3", ToString(result));

            result = baseQuery.Where(s => s.Duration.TotalSeconds < (3.min() + 2.s()).Seconds);
            Assert.AreEqual("0,2", ToString(result));

            result = baseQuery.Where(s => s.Point * 2.s().Milliseconds == 0);
            Assert.AreEqual("0,1,2,3", ToString(result));

        }

        [TestMethod]
        public void TestLengthQuery()
        {
            var time = TimeSpan.FromMinutes(3);
            var baseQuery = _sampleList.AsQueryable().AllowUnits();
            

            IQueryable<Sample>? result = default;
            Assert.ThrowsException<NotSupportedException>(() => baseQuery.Where(s => s.Size > 2.mm() + 1.cm()).ToArray());

            result = baseQuery.Where(s => s.Size > (2.mm() + 1.cm()).Millimeter);
            Assert.AreEqual("1,3", ToString(result));

            result = baseQuery.Where(s => s.Point * 2.s().Milliseconds == 0);
            Assert.AreEqual("0,1,2,3", ToString(result));

        }

        private static string ToString(IQueryable<Sample> samples) 
            => string.Join(',', samples.Select(s => s.Id).ToArray());

        public class Sample 
        {
            public int Id { get; set; }
            public TimeSpan Duration { get; set; }
            public decimal Seconds { get; set; }
            public decimal Size { get; set; }
            public decimal Surface { get; set; }
            public int Point { get; set; }
            public string? Name { get; set; }
        }
    }
}
