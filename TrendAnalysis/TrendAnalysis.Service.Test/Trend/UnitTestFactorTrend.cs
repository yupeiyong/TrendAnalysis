using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.DataTransferObject.Trend;
using TrendAnalysis.Models.Trend;
using TrendAnalysis.Service.Trend;
using System.Diagnostics;
using System.Text;
using System;

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



        /// <summary>
        ///     使用随机数测试，模拟10个数（0，1，2，3，4，5，6，7，8，9）的记录,顺序
        /// </summary>
        [TestMethod]
        public void TestMethod_Analyse_Ten_Digit_By_Random_ASC()
        {
            var numbers = GetTestNumbers(0, 10, 100000);
            //因子
            var factors = FactorGenerator.Create(new List<byte> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.ToList());

            var watch = new Stopwatch();
            watch.Start();
            var resultString = new StringBuilder();
            var defaultTakeCount = 100;

            var indexs = new Dictionary<int, int>();

            var testCount = 2500;
            var trend = new FactorTrend();
            var trendCorrectRates = new List<FactorTrendCorrectRate>();

            foreach (var factor in factors)
            {
                var strFactor = string.Join(",", factor.Right);
                for (var c = 3; c < 19; c++)
                {

                    for (var interval = 0; interval > -9; interval--)
                    {
                        var hasCount = 0;
                        var resultCount = 0;

                        for (var i = testCount; i >= 0; i--)
                        {
                            var number = numbers[i];
                            var curNumbers = numbers.Skip(i + 1).Take(defaultTakeCount).ToList();
                            curNumbers.Reverse();

                            var dto = new FactorTrendAnalyseDto<byte>
                            {
                                AddConsecutiveTimes = c,
                                AddInterval = interval,
                                Numbers = curNumbers,
                                AnalyseHistoricalTrendEndIndex = 1,
                                Factor = factor
                            };
                            var result = trend.Analyse1(dto);
                            var success = false;
                            if (result == null) continue;
                            if (result.Count > 0)
                            {
                                resultCount++;
                                if (result.Contains(number))
                                {
                                    success = true;
                                }
                            }
                            if (success)
                            {
                                hasCount++;
                            }
                        }
                        trendCorrectRates.Add(new FactorTrendCorrectRate { AllowConsecutiveTimes = c, AllowInterval = interval, AnalyticalCount = resultCount, CorrectCount = hasCount, TypeDescription = strFactor,CorrectRate= resultCount == 0 ? 0 : (double)hasCount / resultCount });
                        //resultString.AppendLine($"{strFactor},连续次数：{c}间隔次数：{interval}，正确率：{(resultCount == 0 ? 0 : (double)hasCount / resultCount)}");
                    }
                }


                watch.Stop();
                var usedSeconds = watch.ElapsedMilliseconds / 1000;
                var str = resultString.ToString();

            }
        }

        public List<byte> GetTestNumbers(int startNumber, int endNumber, int count)
        {
            var numbers = new List<byte>();
            var rnd = new Random();
            for (var i = 0; i < count; i++)
            {
                numbers.Add((byte)rnd.Next(startNumber, endNumber));
            }
            return numbers;
        }
    }

}