using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrendAnalysis.DataTransferObject.Trend;
using TrendAnalysis.Models.Trend;
using TrendAnalysis.Service.Trend;


namespace TrendAnalysis.Service.Test.Trend
{

    [TestClass]
    public class UnitTestFactorsTrend
    {

        [TestMethod]
        public void TestAnalyse_PredictiveFactors_Is_Empty()
        {
            var numbers = new List<byte> {2, 3, 3, 1, 4, 3, 3, 3, 1, 3, 3, 3};
            /*
             索引位置：0  1  2  3  4  5  6  7  8  9  10 11
             号码列表：2, 3, 3, 1, 4, 3, 3, 3, 1, 3, 3, 3
             连续次数：0  1  2  0  1  2  3  4  0  1  2  3
             最大间隔：0  -1 -2 0  1  0  -1 -2 0  3  2  1
             */

            var factor = new Factor<byte> {Left = new List<byte> {3, 4}, Right = new List<byte> {1, 2}};

            var dto = new FactorsTrendAnalyseDto<byte>
            {
                Factors = new List<Factor<byte>> {factor},
                Numbers = numbers,
                AnalyseHistoricalTrendEndIndex = 5
            };

            var predictiveFactors = new FactorsTrend().Analyse(dto);
            Assert.IsTrue(predictiveFactors.Count == 0);
        }


        /// <summary>
        ///     分析结果可能的因子数大于0
        /// </summary>
        [TestMethod]
        public void TestAnalyse_PredictiveFactors_Count_Great_Than_Zero()
        {
            var numbers = new List<byte> {2, 3, 3, 1, 4, 3, 3, 3, 1, 3, 3, 3, 4, 3, 4, 3};
            /*
             索引位置：0   1   2   3   4   5   6   7   8   9   10  11  12  13  14
             号码列表：2,  3,  3,  1,  4,  3,  3,  3,  1,  3,   3,  3   4   3   4
             连续次数：0   1   2   0   1   2   3   4   0   1    2   3   4   5   6
             最大间隔：0  -1  -2   0   1   0  -1  -2   0   3    2   1   0  -1  -2
             */

            var factor = new Factor<byte> {Left = new List<byte> {3, 4}, Right = new List<byte> {1, 2}};

            var dto = new FactorsTrendAnalyseDto<byte>
            {
                Factors = new List<Factor<byte>> {factor},
                Numbers = numbers,
                AnalyseHistoricalTrendEndIndex = 5
            };

            var predictiveFactors = new FactorsTrend().Analyse(dto);
            Assert.IsTrue(predictiveFactors.Count == 1);
        }


        /// <summary>
        ///     使用随机数测试，模拟10个数（0，1，2，3，4，5，6，7，8，9）的记录
        /// </summary>
        [TestMethod]
        public void TestMethod_Analyse_Ten_Digit_By_Random()
        {
            var numbers = GetTestNumbers(0, 10, 100000);

            //因子
            var factors = FactorGenerator.Create(new List<byte> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9}.ToList());

            var watch = new Stopwatch();
            watch.Start();
            var resultString = new StringBuilder();
            var hasCount = 0;
            var resultCount = 0;
            var defaultTakeCount = 101;

            var indexs=new Dictionary<int,int>();
            var curTakeCount = defaultTakeCount;
            for (var i = 0; i < 2500; i++)
            {
                var number = numbers[i];
                var curNumbers = numbers.Skip(i + 1).Take(curTakeCount).ToList();
                curNumbers.Reverse();
                var dto = new FactorsTrendAnalyseDto<byte>
                {
                    AddConsecutiveTimes = 3,
                    AddInterval = 1,
                    Numbers = curNumbers,
                    AnalyseHistoricalTrendEndIndex = 100,
                    Factors = factors
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
                        indexs.Add(i,curTakeCount);
                    }
                }
                if (success)
                {
                    hasCount++;
                    resultString.AppendLine("期次：" + i + ",号码：" + number + ",分析结果：" + (success ? "-Yes- " : "      ") + string.Join(";", result));
                    curTakeCount = defaultTakeCount;
                }
                else
                {
                    curTakeCount++;
                }
            }
            watch.Stop();
            var usedSeconds = watch.ElapsedMilliseconds/1000;
            var str = resultString.ToString();
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
            var hasCount = 0;
            var resultCount = 0;
            var defaultTakeCount = 701;

            var indexs = new Dictionary<int, int>();
            var curTakeCount = defaultTakeCount;

            var testCount = 2500;
            for (var i = testCount; i >=0; i--)
            {
                var number = numbers[i];
                var curNumbers = numbers.Skip(i + 1).Take(curTakeCount).ToList();
                curNumbers.Reverse();
                var dto = new FactorsTrendAnalyseDto<byte>
                {
                    AddConsecutiveTimes = 7,
                    AddInterval = 1,
                    Numbers = curNumbers,
                    AnalyseHistoricalTrendEndIndex = 1,
                    Factors = factors
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
                        indexs.Add(i, curTakeCount);
                    }
                }
                if (success)
                {
                    hasCount++;
                    resultString.AppendLine("期次：" + i + ",号码：" + number + ",分析结果：" + (success ? "-Yes- " : "      ") + string.Join(";", result));
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
        ///     使用随机数测试，模拟5个数（0，1，2，3，4）的记录
        /// </summary>
        [TestMethod]
        public void TestMethod_Analyse_Five_Digit_By_Random()
        {
            var numbers = GetTestNumbers(0, 5, 100000);

            //因子
            var factors = FactorGenerator.Create(new List<byte> { 0, 1, 2, 3, 4 }.ToList());

            var watch = new Stopwatch();
            watch.Start();
            var resultString = new StringBuilder();
            var hasCount = 0;
            var resultCount = 0;
            var defaultTakeCount = 101;

            var curTakeCount = defaultTakeCount;
            for (var i = 0; i < 2500; i++)
            {
                var number = numbers[i];
                var curNumbers = numbers.Skip(i + 1).Take(curTakeCount).ToList();
                curNumbers.Reverse();
                var dto = new FactorsTrendAnalyseDto<byte>
                {
                    AddConsecutiveTimes = 2,
                    AddInterval = 1,
                    Numbers = curNumbers,
                    AnalyseHistoricalTrendEndIndex = 100,
                    Factors = factors
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
                if (success)
                {
                    hasCount++;
                    resultString.AppendLine("期次：" + i + ",号码：" + number + ",分析结果：" + (success ? "-Yes- " : "      ") + string.Join(";", result));
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
        ///     分析结果可能的因子数大于0
        /// </summary>
        [TestMethod]
        private List<byte> TestAnalyse(FactorsTrendAnalyseDto<byte> dto)
        {
            var factorHistoricalTrend = new FactorsTrend();

            var predictiveFactors = factorHistoricalTrend.Analyse(new FactorsTrendAnalyseDto<byte>
            {
                Numbers = dto.Numbers,
                Factors = dto.Factors,
                AddConsecutiveTimes = dto.AddConsecutiveTimes,
                AddInterval = dto.AddInterval,
                AnalyseHistoricalTrendEndIndex = dto.AnalyseHistoricalTrendEndIndex
            });
            if (predictiveFactors.Count > 0)
            {
                var onesFactor = new List<byte>(predictiveFactors[0].Right);
                onesFactor = predictiveFactors.Aggregate(onesFactor, (current, factor) => current.Intersect(factor.Right).ToList());

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
                numbers.Add((byte) rnd.Next(startNumber, endNumber));
            }
            return numbers;
        }

    }

}