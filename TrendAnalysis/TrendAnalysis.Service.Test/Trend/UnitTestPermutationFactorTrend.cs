using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrendAnalysis.Service.Trend;
using TrendAnalysis.Models.Trend;
using System.Collections.Generic;

namespace TrendAnalysis.Service.Test.Trend
{
    [TestClass]
    public class UnitTestPermutationFactorTrend
    {
        [TestMethod]
        public void TestPermutationFactorTrendAnalyseResult()
        {
            var fac1 = new Factor<int> { Left = new List<int> { 1, 2 }, Right = new List<int> { 3, 4 } };
            var fac2 = new Factor<int> { Left = new List<int> { 5, 6 }, Right = new List<int> { 7, 8 } };
            var ls = new List<Factor<int>> { fac1, fac2 };

            var lss = new List<List<Factor<int>>> { ls, ls };
            var result=PermutationFactorTrend.TraversePermutationFactor<int>(lss);

        }
    }
}
