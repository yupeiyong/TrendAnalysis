using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TrendAnalysis.Service.Test
{
    [TestClass]
    public class UnitTestCompositeNumber
    {
        [TestMethod]
        public void TestCreate()
        {
            var compositeNumber = new CompositeNumber(1, 49);
            var Composites = compositeNumber.CompositeNumbers;
            var numbers = compositeNumber.GetNumbers(new List<uint>() {1,2 });
        }
    }
}
