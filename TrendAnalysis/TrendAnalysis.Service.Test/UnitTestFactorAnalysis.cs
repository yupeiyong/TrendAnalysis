using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TrendAnalysis.Service.Test
{
    [TestClass]
    public class UnitTestFactorAnalysis
    {
        [TestMethod]
        public void TestMethod1()
        {
            var ls = new List<BinaryNode<string>>() { new BinaryNode<string> { Left = new List<string> { "1", "2", "3" }, Right = new List<string> { } } };
            var numbers = new List<string> { "3", "2", "1", "2", "0", "0", "1", "2", "3", "3", "4", "4", "2", "3", "3","0", "2", "3", "3" };
            var analysis = new FactorAnalysis();
            var rows=analysis.Consecutives(numbers, ls);
        }
    }
}
