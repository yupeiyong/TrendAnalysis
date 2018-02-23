using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.DataTransferObject.Trend;
using TrendAnalysis.Models.Trend;
using TrendAnalysis.Service.Trend;
using System;
using System.Text;
using System.Diagnostics;

namespace TrendAnalysis.Service.Test.Trend
{

    [TestClass]
    public class UnitTestFactorsTrend
    {

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
                AnalyseHistoricalTrendEndIndex = 5
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
                AnalyseHistoricalTrendEndIndex = 5
            };

            var predictiveFactors = new FactorsTrend().Analyse(dto);
            Assert.IsTrue(predictiveFactors.Count == 1);
        }

        [TestMethod]
        public void TestMethod_AnalyseSpecifiedLocation()
        {
            var numbers = GetTestNumbers(0, 10, 100000);

            var watch = new Stopwatch();
            watch.Start();
            var resultString = new StringBuilder();
            var hasCount = 0;
            var resultCount = 0;
            var defaultTakeCount = 101;
            var rate = 40;
            var everyPrice = 10;
            var totalMoney = 0;
            var winMoney = 0;

            var curTakeCount = defaultTakeCount;
            for (var i = 0; i < 2500; i++)
            {
                var number = numbers[i];
                var dto = new MarkSixAnalyseSpecifiedLocationDto
                {
                    Location = 7,
                    Times = times,
                    OnesAddConsecutiveTimes = 6,
                    TensAddConsecutiveTimes = 100,//2,
                    OnesAddInterval = 1,
                    TensAddInterval = 1,
                    NumberTakeCount = curTakeCount,
                    OnesAndTensMustContain = false
                };

                //var dto = new MarkSixAnalyseSpecifiedLocationDto { Location = 7, StartTimes = records[i].StartTimes, TensAllowMinFactorCurrentConsecutiveTimes = 6, TensAllowMaxInterval = -1, TensAroundCount = 200, TensNumbersTailCutCount = 6 };
                var result = TestAnalyse(dto);
                var success = false;
                if (result.Count > 0)
                {
                    resultCount++;
                    if (result.Contains(number))
                    {
                        success = true;
                    }
                }
                totalMoney += result.Count(m => m != 0) * everyPrice;
                if (success)
                {
                    hasCount++;
                    resultString.AppendLine("期次：" + i + ",号码：" + number + ",分析结果：" + (has ? "-Yes- " : "      ") + string.Join(";", result));
                    winMoney += everyPrice * rate;
                    curTakeCount = defaultTakeCount;
                }
                else
                {
                    curTakeCount++;
                }
            }
            watch.Stop();
            var usedSeconds = watch.ElapsedMilliseconds / 1000;
            var str = resultString.ToString();
        }


        /// <summary>
        /// 分析结果可能的因子数大于0
        /// </summary>
        [TestMethod]
        private List<byte> TestAnalyse(MarkSixAnalyseSpecifiedLocationDto dto)
        {
            //个位因子
            var onesDigitFactors = FactorGenerator.Create(new List<byte> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.ToList());

            var factorHistoricalTrend = new FactorsTrend();

            //个位
            var onespredictiveFactors = factorHistoricalTrend.Analyse(new FactorsTrendAnalyseDto<byte>
            {
                Numbers = numbers,
                Factors = onesDigitFactors,
                AddConsecutiveTimes = dto.OnesAddConsecutiveTimes,
                AddInterval = dto.OnesAddInterval,
                AnalyseHistoricalTrendEndIndex = dto.AnalyseHistoricalTrendEndIndex
            });
            if (onespredictiveFactors.Count > 0)
            {
                var onesFactor = new List<byte>(onespredictiveFactors[0].Right);
                onesFactor = onespredictiveFactors.Aggregate(onesFactor, (current, factor) => current.Intersect(factor.Right).ToList());

                return onesFactor;

            }
            return new List<byte>();
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