using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrendAnalysis.Models.Trend;
using TrendAnalysis.Service.Trend;


namespace TrendAnalysis.Service.Test.Trend
{

    [TestClass]
    public class UnitTestPermutationFactorTrend
    {

        [TestMethod]
        public void TestPermutationFactorTrendAnalyseResult_Two_Element()
        {
            var fac1 = new Factor<int> { Left = new List<int> { 1, 2 }, Right = new List<int> { 3, 4 } };
            var fac2 = new Factor<int> { Left = new List<int> { 5, 6 }, Right = new List<int> { 7, 8 } };
            var ls = new List<Factor<int>> { fac1, fac2 };

            var lss = new List<List<Factor<int>>> { ls, ls };
            var result = PermutationFactorTrend.TraversePermutationFactor(lss);
            Assert.IsTrue(result.Count == 4 * 4);
        }


        [TestMethod]
        public void TestPermutationFactorTrendAnalyseResult_One_And_Two_Element()
        {
            var fac1 = new Factor<int> { Left = new List<int> { 1, 2 }, Right = new List<int> { 3, 4 } };
            var fac2 = new Factor<int> { Left = new List<int> { 5, 6 }, Right = new List<int> { 7, 8 } };
            var ls1 = new List<Factor<int>> { fac1 };
            var ls = new List<Factor<int>> { fac1, fac2 };

            var lss = new List<List<Factor<int>>> { ls1, ls };
            var result = PermutationFactorTrend.TraversePermutationFactor(lss);
            Assert.IsTrue(result.Count == 2 * 4);
        }


        [TestMethod]
        public void TestCountConsecutive()
        {
            var numbers = new List<byte>
            {
                1, 3, 6, 9, 1, 4, 2, 3, 1, 2, 5, 6, 8, 2, 3, 1
            };

            var factors = new List<List<byte>>
            {
                new List<byte> {1, 2},
                new List<byte> {3, 4}
            };

            var oppositeFactor = new List<byte> { 5, 6 };

            var cutCount = 6;
            var result = PermutationFactorTrend.CountConsecutive(numbers, factors, oppositeFactor, cutCount);

            //有两个连续次数
            Assert.IsTrue(result.HistoricalConsecutiveTimes.Count == 2);

            //连续次数＝1出现一次
            Assert.IsTrue(result.HistoricalConsecutiveTimes[1] == 1);

            //连续次数＝2出现一次
            Assert.IsTrue(result.HistoricalConsecutiveTimes[2] == 1);


            numbers = new List<byte>
            {
                1, 3, 9, 1, 3, 1, 3, 1, 3, 6, 9, 1, 4, 2, 3, 1, 4, 5, 6, 8, 2, 3, 1
            };

            result = PermutationFactorTrend.CountConsecutive(numbers, factors, oppositeFactor, cutCount);

            //有两个连续次数
            Assert.IsTrue(result.HistoricalConsecutiveTimes.Count == 2);

            //连续次数＝1出现一次
            Assert.IsTrue(result.HistoricalConsecutiveTimes[1] == 1);

            //连续次数＝3出现2次
            Assert.IsTrue(result.HistoricalConsecutiveTimes[3] == 2);



            numbers = new List<byte>
            {
                1, 3, 9, 1, 3, 1, 3, 1, 3, 6, 9, 1, 4, 2, 3, 1, 4,1, 4, 2, 3, 1, 4,1, 4, 2, 3, 1, 4, 5, 6, 8, 2, 3, 1
            };

            result = PermutationFactorTrend.CountConsecutive(numbers, factors, oppositeFactor, cutCount);

            //有3个连续次数
            Assert.IsTrue(result.HistoricalConsecutiveTimes.Count == 3);

            //连续次数＝1出现一次
            Assert.IsTrue(result.HistoricalConsecutiveTimes[1] == 1);

            //连续次数＝3出现一次
            Assert.IsTrue(result.HistoricalConsecutiveTimes[3] == 1);

            //连续次数＝9出现一次
            Assert.IsTrue(result.HistoricalConsecutiveTimes[9] == 1);

        }

        [TestMethod]
        public void TestCountCountConsecutivesByPermutationFactors()
        {
            var numbers = new List<byte>
            {
                1, 3, 6, 9, 1, 4, 2, 3, 1, 2, 5, 6, 8, 2, 3, 1
            };

            var fac1 = new Factor<byte> { Left = new List<byte> { 1, 2 }, Right = new List<byte> { 3, 4 } };
            var fac2 = new Factor<byte> { Left = new List<byte> { 5, 6 }, Right = new List<byte> { 7, 8 } };
            //var fac3 = new Factor<byte> { Left = new List<byte> { 1, 2 }, Right = new List<byte> { 3, 4 } };
            var permutationFactors = new List<List<Factor<byte>>> { new List<Factor<byte>> { fac1 }, new List<Factor<byte>> { fac2 } };


            var cutCount = 6;
            var result = PermutationFactorTrend.CountConsecutives(numbers, permutationFactors, cutCount);

            Assert.IsTrue(result.Count == 4);
            var result1 = result[0];
            Assert.IsTrue(result1.Factors.Count == 2);
            Assert.IsTrue(string.Join(",", result1.Factors[0]) == "1,2");
            Assert.IsTrue(string.Join(",", result1.Factors[1]) == "5,6");
            //有0个连续次数
            Assert.IsTrue(result1.HistoricalConsecutiveTimes.Count == 0);


            var result2 = result[1];
            Assert.IsTrue(result2.Factors.Count == 2);
            Assert.IsTrue(string.Join(",", result2.Factors[0]) == "1,2");
            Assert.IsTrue(string.Join(",", result2.Factors[1]) == "7,8");
            //有0个连续次数
            Assert.IsTrue(result2.HistoricalConsecutiveTimes.Count == 0);


            var result3 = result[2];
            Assert.IsTrue(result3.Factors.Count == 2);
            Assert.IsTrue(string.Join(",", result3.Factors[0]) == "3,4");
            Assert.IsTrue(string.Join(",", result3.Factors[1]) == "5,6");
            //有0个连续次数
            Assert.IsTrue(result3.HistoricalConsecutiveTimes.Count == 1);

            //连续次数＝1出现一次
            Assert.IsTrue(result3.HistoricalConsecutiveTimes[1] == 1);


            var result4 = result[3];
            Assert.IsTrue(result4.Factors.Count == 2);
            Assert.IsTrue(string.Join(",", result4.Factors[0]) == "3,4");
            Assert.IsTrue(string.Join(",", result4.Factors[1]) == "7,8");
            //有0个连续次数
            Assert.IsTrue(result4.HistoricalConsecutiveTimes.Count == 0);


            ////连续次数＝2出现一次
            //Assert.IsTrue(result1.HistoricalConsecutiveTimes[2] == 1);


            //numbers = new List<byte>
            //{
            //    1, 3, 9, 1, 3, 1, 3, 1, 3, 6, 9, 1, 4, 2, 3, 1, 4, 5, 6, 8, 2, 3, 1
            //};

            //result = PermutationFactorTrend.CountConsecutive(numbers, factors, oppositeFactor, cutCount);

            ////有两个连续次数
            //Assert.IsTrue(result.HistoricalConsecutiveTimes.Count == 2);

            ////连续次数＝1出现一次
            //Assert.IsTrue(result.HistoricalConsecutiveTimes[1] == 1);

            ////连续次数＝3出现2次
            //Assert.IsTrue(result.HistoricalConsecutiveTimes[3] == 2);



            //numbers = new List<byte>
            //{
            //    1, 3, 9, 1, 3, 1, 3, 1, 3, 6, 9, 1, 4, 2, 3, 1, 4,1, 4, 2, 3, 1, 4,1, 4, 2, 3, 1, 4, 5, 6, 8, 2, 3, 1
            //};

            //result = PermutationFactorTrend.CountConsecutive(numbers, factors, oppositeFactor, cutCount);

            ////有两个连续次数
            //Assert.IsTrue(result.HistoricalConsecutiveTimes.Count == 3);

            ////连续次数＝1出现一次
            //Assert.IsTrue(result.HistoricalConsecutiveTimes[1] == 1);

            ////连续次数＝3出现一次
            //Assert.IsTrue(result.HistoricalConsecutiveTimes[3] == 1);

            ////连续次数＝9出现一次
            //Assert.IsTrue(result.HistoricalConsecutiveTimes[9] == 1);
        }
    }

}