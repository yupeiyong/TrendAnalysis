using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.DataTransferObject.Trend;
using TrendAnalysis.Models.Trend;
using TrendAnalysis.Service.Trend;


namespace TrendAnalysis.Service.Test.Trend
{

    [TestClass]
    public class UnitTestFactorTrend
    {

        [TestMethod]
        public void TestAnalyseConsecutives_By_String_Numbers()
        {
            var factor = new Factor<string> { Left = new List<string> { "1", "2" }, Right = new List<string> { "3", "4" } };
            var numbers = new List<string> { "3", "2", "1", "2", "0", "0", "1", "2", "3", "3", "4", "4", "4", "3", "3", "0", "3", "3", "3" };
            var result = FactorTrend.CountConsecutiveDistribution(numbers, factor.Left, factor.Right);
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.ConsecutiveDistributions.Count == 2);

            var keys = result.ConsecutiveDistributions.Keys.ToList();
            keys.Sort();
            var dict = result.ConsecutiveDistributions;
            Assert.IsTrue(keys.Count == 2);
            Assert.IsTrue(keys[0] == 2 && dict[keys[0]] == 1);
            Assert.IsTrue(keys[1] == 3 && dict[keys[0]] == 1);
        }


        [TestMethod]
        public void TestAnalyseConsecutives_By_int_Numbers()
        {
            var factor = new Factor<int> { Left = new List<int> { 1, 2 }, Right = new List<int> { 3, 4 } };
            var numbers = new List<int> { 3, 2, 1, 2, 0, 0, 1, 2, 3, 3, 4, 4, 4, 3, 3, 0, 3, 3, 3 };
            var result = FactorTrend.CountConsecutiveDistribution(numbers, factor.Left, factor.Right);
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.ConsecutiveDistributions.Count == 2);

            var keys = result.ConsecutiveDistributions.Keys.ToList();
            keys.Sort();
            var dict = result.ConsecutiveDistributions;
            Assert.IsTrue(keys.Count == 2);
            Assert.IsTrue(keys[0] == 2 && dict[keys[0]] == 1);
            Assert.IsTrue(keys[1] == 3 && dict[keys[0]] == 1);
        }


        [TestMethod]
        public void TestAnalyseFactorHistoricalTrend_By_TrendResult()
        {
            var numbers = new List<byte> { 2, 3, 3, 1, 4, 3, 3, 3, 1, 3, 3, 3 };
            /*
             索引位置：0  1  2  3  4  5  6  7  8  9  10 11
             号码列表：2, 3, 3, 1, 4, 3, 3, 3, 1, 3, 3, 3
             连续次数：0  1  2  0  1  2  3  4  0  1  2  3
             最大间隔：0  -1 -2 0  1  0  -1 -2 0  3  2  1
             */

            var factor = new Factor<byte> { Left = new List<byte> { 3, 4 }, Right = new List<byte> { 1, 2 } };
            var trendResult = FactorTrend.CountConsecutiveDistribution(numbers, factor.Left, factor.Right);

            var rows = new FactorTrend().GetCorrectRates(numbers, trendResult, 5, factor.Right);
            Assert.IsTrue(rows.Count > 0);
            rows = rows.Where(r => r.CorrectRate == 1 && r.AllowInterval == -2).ToList();
            Assert.IsTrue(rows.Count >= 1);
        }

    }

}