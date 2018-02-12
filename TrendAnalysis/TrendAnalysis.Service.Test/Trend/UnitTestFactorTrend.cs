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
            var result = FactorsTrend.CountFactorContinuousTimes(numbers, factor.Left, factor.Right);
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.ContinuousDistributions.Count == 2);

            var keys = result.ContinuousDistributions.Keys.ToList();
            keys.Sort();
            var dict = result.ContinuousDistributions;
            Assert.IsTrue(keys.Count == 2);
            Assert.IsTrue(keys[0] == 2 && dict[keys[0]] == 1);
            Assert.IsTrue(keys[1] == 3 && dict[keys[0]] == 1);
        }


        [TestMethod]
        public void TestAnalyseConsecutives_By_int_Numbers()
        {
            var factor = new Factor<int> { Left = new List<int> { 1, 2 }, Right = new List<int> { 3, 4 } };
            var numbers = new List<int> { 3, 2, 1, 2, 0, 0, 1, 2, 3, 3, 4, 4, 4, 3, 3, 0, 3, 3, 3 };
            var result = FactorsTrend.CountFactorContinuousTimes(numbers, factor.Left, factor.Right);
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.ContinuousDistributions.Count == 2);

            var keys = result.ContinuousDistributions.Keys.ToList();
            keys.Sort();
            var dict = result.ContinuousDistributions;
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
            var trendResult = FactorsTrend.CountFactorContinuousTimes(numbers, factor.Left, factor.Right);

            var rows = new FactorsTrend().AnalyseFactorHistoricalTrend(numbers, trendResult, 5, factor.Right);
            Assert.IsTrue(rows.Count > 0);
            rows = rows.Where(r => r.CorrectRate == 1 && r.AllowInterval == -2).ToList();
            Assert.IsTrue(rows.Count >= 1);
        }


        [TestMethod]
        public void TestAnalyse_PredictiveFactors_Is_Empty()
        {
            var numbers = new List<byte> { 2, 3, 3, 1, 4, 3, 3, 3, 1, 3, 3, 3 };
            /*
             索引位置：0  1  2  3  4  5  6  7  8  9  10 11
             号码列表：2, 3, 3, 1, 4, 3, 3, 3, 1, 3, 3, 3
             连续次数：0  1  2  0  1  2  3  4  0  1  2  3
             最大间隔：0  -1 -2 0  1  0  -1 -2 0  3  2  1
             */

            var factor = new Factor<byte> { Left = new List<byte> { 3, 4 }, Right = new List<byte> { 1, 2 } };

            var dto = new FactorsTrendAnalyseDto<byte>
            {
                Factors = new List<Factor<byte>> { factor },
                Numbers = numbers,
                AnalyseHistoricalTrendCount = 5
            };

            var predictiveFactors = new FactorsTrend().Analyse(dto);
            Assert.IsTrue(predictiveFactors.Count == 0);
        }

        /// <summary>
        /// 分析结果可能的因子数大于0
        /// </summary>
        [TestMethod]
        public void TestAnalyse_PredictiveFactors_Count_Great_Than_Zero()
        {
            var numbers = new List<byte> { 2, 3, 3, 1, 4, 3, 3, 3, 1, 3, 3, 3, 4, 3, 4, 3 };
            /*
             索引位置：0   1   2   3   4   5   6   7   8   9   10  11  12  13  14
             号码列表：2,  3,  3,  1,  4,  3,  3,  3,  1,  3,   3,  3   4   3   4
             连续次数：0   1   2   0   1   2   3   4   0   1    2   3   4   5   6
             最大间隔：0  -1  -2   0   1   0  -1  -2   0   3    2   1   0  -1  -2
             */

            var factor = new Factor<byte> { Left = new List<byte> { 3, 4 }, Right = new List<byte> { 1, 2 } };

            var dto = new FactorsTrendAnalyseDto<byte>
            {
                Factors = new List<Factor<byte>> { factor },
                Numbers = numbers,
                AnalyseHistoricalTrendCount = 5
            };

            var predictiveFactors = new FactorsTrend().Analyse(dto);
            Assert.IsTrue(predictiveFactors.Count == 1);
        }

    }

}