using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrendAnalysis.Data;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.Models.MarkSix;
using TrendAnalysis.Service.MarkSix;
using TrendAnalysis.Service.Trend;


namespace TrendAnalysis.Service.Test.MarkSix
{

    [TestClass]
    public class UnitTestMarkSixAnalysisService
    {

        [TestMethod]
        public void TestMethod_AnalyseByOnesDigit()
        {
            using (var dao = new TrendDbContext())
            {
                var numbers = dao.Set<MarkSixRecord>().OrderBy(n => n.Times).Take(20).Select(n => n.SeventhNum).ToList();
                var service = new MarkSixAnalysisService();

                //个位数号码列表
                var onesDigitNumbers = numbers.Select(n => n.ToString("00").Substring(1)).Select(n => byte.Parse(n)).ToList();

                //个位因子
                var onesDigitFactors = FactorGenerator.Create(new List<byte> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9}.ToList());

                //十位数号码列表
                var tensDigitNumbers = numbers.Select(n => n.ToString("00").Substring(0, 1)).Select(n => byte.Parse(n)).ToList();

                //十位因子
                var tensDigitFactors = FactorGenerator.Create(new List<byte> {0, 1, 2, 3, 4}.ToList());

                var historicalAnalysis = new FactorTrend();
                var result = historicalAnalysis.Analyse(new FactorTrendAnalyseDto<byte> {Numbers = onesDigitNumbers, Factors = onesDigitFactors, AllowMinTimes = 9, AllowMaxInterval = 2});
                result = result.Where(m => m.HistoricalConsecutiveTimes.Count > 0).ToList();
            }
        }


        [TestMethod]
        public void TestMethod_AnalyseByTensDigit()
        {
            using (var dao = new TrendDbContext())
            {
                var numbers = dao.Set<MarkSixRecord>().OrderBy(n => n.Times).Take(20).Select(n => n.SeventhNum).ToList();

                //个位数号码列表
                var onesDigitNumbers = numbers.Select(n => n.ToString("00").Substring(1)).Select(n => byte.Parse(n)).ToList();

                //个位因子
                var onesDigitFactors = FactorGenerator.Create(new List<byte> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9}.ToList());

                //十位数号码列表
                var tensDigitNumbers = numbers.Select(n => n.ToString("00").Substring(0, 1)).Select(n => byte.Parse(n)).ToList();

                //十位因子
                var tensDigitFactors = FactorGenerator.Create(new List<byte> {0, 1, 2, 3, 4}.ToList());

                var historicalAnalysis = new FactorTrend();
                var result = historicalAnalysis.Analyse(new FactorTrendAnalyseDto<byte> {Numbers = tensDigitNumbers, Factors = tensDigitFactors, AllowMinTimes = 4, AllowMaxInterval = 0});
                result = result.Where(m => m.HistoricalConsecutiveTimes.Count > 0).ToList();
            }
        }

        [TestMethod]
        public void TestMethod_AnalyseSpecifiedLocation()
        {
            using (var dao = new TrendDbContext())
            {
                var service = new MarkSixAnalysisService();
                var records = dao.Set<MarkSixRecord>().OrderByDescending(m => m.Times).Take(1000).ToList();
                var resultString = new StringBuilder();
                var hasCount = 0;
                var resultCount = 0;
                var tensHasCount = 0;
                var onesHasCount = 0;
                for (var i = 0; i < 100; i++)
                {
                    var seventhNum = records[i].SeventhNum;
                    var ones = byte.Parse(seventhNum.ToString("00").Substring(1));
                    var tens = byte.Parse(seventhNum.ToString("00").Substring(0, 1));
                    var times = records[i].Times;
                    var dto = new MarkSixAnalyseSpecifiedLocationDto {Location = 7, Times = times, TensNumbersTailCutCount = 6, OnesAllowMinFactorCurrentConsecutiveTimes = 8, OnesNumbersTailCutCount = 10, OnesAllowMaxInterval = 0};

                    //var dto = new MarkSixAnalyseSpecifiedLocationDto { Location = 7, StartTimes = records[i].StartTimes, TensAllowMinFactorCurrentConsecutiveTimes = 6, TensAllowMaxInterval = -1, TensAroundCount = 200, TensNumbersTailCutCount = 6 };
                    var result = service.AnalyseSpecifiedLocation(dto);
                    if (result.Count > 0)
                    {
                        resultCount++;
                        var resultSource = result.Select(r => r.ToString("00"));
                        var onesResults = resultSource.Select(r => byte.Parse(r.Substring(1))).Distinct().ToList();
                        var tensResults = resultSource.Select(r => byte.Parse(r.Substring(0, 1))).Distinct().ToList();
                        if (tensResults.Contains(tens))
                        {
                            tensHasCount++;
                        }
                        if (onesResults.Contains(ones))
                        {
                            onesHasCount++;
                        }
                    }
                    var has = result.Exists(m => m == seventhNum);
                    if (has) hasCount++;
                    resultString.AppendLine("期次：" + records[i].Times + ",第7位号码：" + seventhNum + ",分析结果：" + (has ? "-Yes- " : "      ") + string.Join(";", result));
                }
                var str = resultString.ToString();
            }
        }


        [TestMethod]
        public void TestMethod_AnalyseSpecifiedLocation_By_Random()
        {
            using (var dao = new TrendDbContext())
            {
                var service = new MarkSixAnalysisService();
                var records = dao.Set<MarkSixRecord>().OrderByDescending(m => m.Times).Take(1000).ToList();
                var resultString = new StringBuilder();
                var hasCount = 0;
                var tensHasCount = 0;
                var onesHasCount = 0;
                var onesLeft = new List<byte> {0, 1, 2, 3, 4};
                var onesRight = new List<byte> {5, 6, 7, 8, 9};

                var tensLeft = new List<byte> {0, 1};
                var tensRight = new List<byte> {2, 3, 4};
                var rnd = new Random();
                Parallel.For(0, 100, i =>
                {
                    var r = rnd.Next()%2;
                    var tensFactor = new List<byte>();
                    if (r == 1)
                    {
                        tensFactor = tensLeft;
                    }
                    else
                    {
                        tensFactor = tensRight;
                    }

                    var r1 = rnd.Next()%2;
                    var onesFactor = new List<byte>();
                    if (r == 1)
                    {
                        onesFactor = onesLeft;
                    }
                    else
                    {
                        onesFactor = onesRight;
                    }

                    var seventhNum = records[i].SeventhNum;
                    var ones = byte.Parse(seventhNum.ToString("00").Substring(1));
                    var tens = byte.Parse(seventhNum.ToString("00").Substring(0, 1));
                    if (tensFactor.Contains(tens))
                    {
                        tensHasCount++;
                    }
                    if (onesFactor.Contains(ones))
                    {
                        onesHasCount++;
                    }
                    var result = GetNumbers(tensFactor, onesFactor);
                    var has = result.Exists(m => m == seventhNum);
                    if (has) hasCount++;
                    resultString.AppendLine("期次：" + records[i].Times + ",第7位号码：" + seventhNum + ",分析结果：" + (has ? "-Yes- " : "      ") + string.Join(";", result));
                });
                var str = resultString.ToString();
            }
        }


        /// <summary>
        ///     通过10位和个位因子，获取最终数字
        /// </summary>
        /// <param name="tenFactor"></param>
        /// <param name="onesFactor"></param>
        /// <returns></returns>
        private List<byte> GetNumbers(List<byte> tenFactor, List<byte> onesFactor)
        {
            var result = new List<byte>();
            for (var i = 0; i < tenFactor.Count; i++)
            {
                for (var j = 0; j < onesFactor.Count; j++)
                {
                    var valueStr = tenFactor[i] + onesFactor[j];
                    byte number;
                    if (!byte.TryParse(valueStr.ToString(), out number))
                    {
                        throw new Exception(string.Format("错误，{0}不是有效的byte类型数据！", valueStr));
                    }
                    result.Add(number);
                }
            }
            return result;
        }


        /// <summary>
        ///     分析个位历史趋势
        /// </summary>
        [TestMethod]
        public void TestMethod_AnalyseOnesHistoricalTrend()
        {
            using (var dao = new TrendDbContext())
            {
                var service = new MarkSixAnalysisService();
                var records = dao.Set<MarkSixRecord>().OrderByDescending(m => m.Times).Take(1000).ToList();
                var trendDto = new MarkSixAnalyseHistoricalTrendDto
                {
                    Location = 7,
                    Times = records[0].Times,
                    AnalyseNumberCount = 1000,
                    StartAllowMaxInterval = 1,
                    EndAllowMaxInterval = -3,
                    StartAllowMinFactorCurrentConsecutiveTimes = 9,
                    EndAllowMinFactorCurrentConsecutiveTimes = 12,
                    AllowMinTimes = 3,
                    NumbersTailCutCount = 6
                };

                var trends = service.AnalyseOnesHistoricalTrend(trendDto);
                var content = new StringBuilder();
                trends.ForEach(item => content.Append(item.ToString()));


                var str = content.ToString();
            }
        }


        [TestMethod] //
        public void TestMethod_AnalyseCompositeHistoricalTrend()
        {
            using (var dao = new TrendDbContext())
            {
                var service = new MarkSixAnalysisService();
                var records = dao.Set<MarkSixRecord>().OrderByDescending(m => m.Times).Take(1000).ToList();
                var trendDto = new MarkSixAnalyseHistoricalTrendDto
                {
                    Location = 7,
                    Times = records[0].Times,
                    AnalyseNumberCount = 100,
                    StartAllowMaxInterval = 2,
                    EndAllowMaxInterval = -5,
                    StartAllowMinFactorCurrentConsecutiveTimes = 9,
                    EndAllowMinFactorCurrentConsecutiveTimes = 16,
                    AllowMinTimes = 3,
                    NumbersTailCutCount = 6
                };

                var trends = service.AnalyseCompositeHistoricalTrend(trendDto);
                var content = new StringBuilder();
                trends.ForEach(item => content.Append(item.ToString()));


                var str = content.ToString();
            }
        }


        #region  测试多号码结合分析历史趋势
        
        /// <summary>
        ///     用多号码结合的方法 分析个位历史趋势
        /// </summary>
        [TestMethod]
        public void TestMethod_AnalyseSpecifiedLocationByMultiNumber()
        {
            using (var dao = new TrendDbContext())
            {
                var service = new MarkSixAnalysisService();
                var records = dao.Set<MarkSixRecord>().OrderByDescending(m => m.Times).Take(1000).ToList();
                var resultString = new StringBuilder();
                var hasCount = 0;
                var resultCount = 0;
                var tensHasCount = 0;
                var onesHasCount = 0;
                for (var i = 0; i < 100; i++)
                {
                    var seventhNum = records[i].SeventhNum;
                    var ones = byte.Parse(seventhNum.ToString("00").Substring(1));
                    var tens = byte.Parse(seventhNum.ToString("00").Substring(0, 1));
                    var times = records[i].Times;
                    var dto = new MarkSixAnalyseSpecifiedLocationDto { Location = 7, Times = times, TensNumbersTailCutCount = 6, OnesAllowMinFactorCurrentConsecutiveTimes = 4, OnesNumbersTailCutCount = 10, OnesAllowMaxInterval = 0 };

                    //var dto = new MarkSixAnalyseSpecifiedLocationDto { Location = 7, StartTimes = records[i].StartTimes, TensAllowMinFactorCurrentConsecutiveTimes = 6, TensAllowMaxInterval = -1, TensAroundCount = 200, TensNumbersTailCutCount = 6 };
                    var result = service.AnalyseSpecifiedLocationByMultiNumber(dto);
                    if (result.Count > 0)
                    {
                        resultCount++;
                        var resultSource = result.Select(r => r.ToString("00"));
                        var onesResults = resultSource.Select(r => byte.Parse(r.Substring(1))).Distinct().ToList();
                        var tensResults = resultSource.Select(r => byte.Parse(r.Substring(0, 1))).Distinct().ToList();
                        if (tensResults.Contains(tens))
                        {
                            tensHasCount++;
                        }
                        if (onesResults.Contains(ones))
                        {
                            onesHasCount++;
                        }
                    }
                    var has = result.Exists(m => m == seventhNum);
                    if (has) hasCount++;
                    resultString.AppendLine("期次：" + records[i].Times + ",第7位号码：" + seventhNum + ",分析结果：" + (has ? "-Yes- " : "      ") + string.Join(";", result));
                }
                var str = resultString.ToString();
            }
        }

        /// <summary>
        /// 测试分析一段时期的趋势
        /// </summary>
        [TestMethod]
        public void TestMethod_AnalyseOnesHistoricalTrendByMultiNumber()
        {
            using (var dao = new TrendDbContext())
            {
                var service = new MarkSixAnalysisService();
                var records = dao.Set<MarkSixRecord>().OrderByDescending(m => m.Times).Take(1000).ToList();
                var trendDto = new MarkSixAnalyseHistoricalTrendDto
                {
                    Location = 7,
                    Times = records[0].Times,
                    AnalyseNumberCount = 1000,
                    StartAllowMaxInterval = 1,
                    EndAllowMaxInterval = -3,
                    StartAllowMinFactorCurrentConsecutiveTimes = 9,
                    EndAllowMinFactorCurrentConsecutiveTimes = 12,
                    AllowMinTimes = 3,
                    NumbersTailCutCount = 6
                };

                var trends = service.AnalyseOnesHistoricalTrendByMultiNumber(trendDto);
                var content = new StringBuilder();
                trends.ForEach(item => content.Append(item.ToString()));


                var str = content.ToString();
            }
        }

        #endregion


        #region  测试通过排列因子分析趋势

        /// <summary>
        ///     测试，按排列因子分析历史趋势
        /// </summary>
        [TestMethod]
        public void TestAnalyseSpecifiedLocationByPermutationFactors()
        {
            using (var dao = new TrendDbContext())
            {
                var service = new MarkSixAnalysisService();
                var records = dao.Set<MarkSixRecord>().OrderByDescending(m => m.Times).Take(1000).ToList();
                var resultString = new StringBuilder();
                var hasCount = 0;
                var resultCount = 0;
                var tensHasCount = 0;
                var onesHasCount = 0;
                for (var i = 0; i < 100; i++)
                {
                    var seventhNum = records[i].SeventhNum;
                    var ones = byte.Parse(seventhNum.ToString("00").Substring(1));
                    var tens = byte.Parse(seventhNum.ToString("00").Substring(0, 1));
                    var times = records[i].Times;
                    var dto = new MarkSixAnalyseSpecifiedLocationDto {Location = 7, Times = times, TensNumbersTailCutCount = 6, OnesAllowMinFactorCurrentConsecutiveTimes = 4, OnesNumbersTailCutCount = 10, OnesAllowMaxInterval = 0};

                    //var dto = new MarkSixAnalyseSpecifiedLocationDto { Location = 7, StartTimes = records[i].StartTimes, TensAllowMinFactorCurrentConsecutiveTimes = 6, TensAllowMaxInterval = -1, TensAroundCount = 200, TensNumbersTailCutCount = 6 };
                    var result = service.AnalyseSpecifiedLocationByPermutationFactors(dto);
                    if (result.Count > 0)
                    {
                        resultCount++;
                        var resultSource = result.Select(r => r.ToString("00"));
                        var onesResults = resultSource.Select(r => byte.Parse(r.Substring(1))).Distinct().ToList();
                        var tensResults = resultSource.Select(r => byte.Parse(r.Substring(0, 1))).Distinct().ToList();
                        if (tensResults.Contains(tens))
                        {
                            tensHasCount++;
                        }
                        if (onesResults.Contains(ones))
                        {
                            onesHasCount++;
                        }
                    }
                    var has = result.Exists(m => m == seventhNum);
                    if (has) hasCount++;
                    resultString.AppendLine("期次：" + records[i].Times + ",第7位号码：" + seventhNum + ",分析结果：" + (has ? "-Yes- " : "      ") + string.Join(";", result));
                }
                var str = resultString.ToString();
            }
        }


        /// <summary>
        ///     通过测试数据，测试，按排列因子分析历史趋势
        /// </summary>
        [TestMethod]
        public void TestAnalyseSpecifiedLocationByPermutationFactors_By_Test_Data()
        {
            using (new TransactionScope())
            {
                using (var dao = new TrendDbContext())
                {
                    //删除所有记录
                    dao.Set<MarkSixRecord>().RemoveRange(dao.Set<MarkSixRecord>());

                    //添加测试数据
                    var recordString = "01,06,01,06,01,06,01,06,01,06,22,22,33,31,35,06,01,06,01,06,01,06,01,06,01";
                    var numbers = recordString.Split(',').ToList().Select(byte.Parse).ToList();
                    var sevenNumbers = new List<MarkSixRecord>();
                    for (var i = 0; i < numbers.Count; i++)
                    {
                        var record = new MarkSixRecord {Times = i.ToString(), SeventhNum = numbers[i], AwardingDate = DateTime.Now, TimesValue = i};
                        sevenNumbers.Add(record);
                    }
                    dao.Set<MarkSixRecord>().AddRange(sevenNumbers);
                    dao.SaveChanges();
                    var service = new MarkSixAnalysisService();
                    var records = dao.Set<MarkSixRecord>().OrderByDescending(m => m.TimesValue).Take(1000).ToList();
                    var resultString = new StringBuilder();
                    var hasCount = 0;
                    var resultCount = 0;
                    var tensHasCount = 0;
                    var onesHasCount = 0;
                    for (var i = 0; i < 100; i++)
                    {
                        var seventhNum = records[i].SeventhNum;
                        var ones = byte.Parse(seventhNum.ToString("00").Substring(1));
                        var tens = byte.Parse(seventhNum.ToString("00").Substring(0, 1));
                        var times = records[i].Times;
                        var dto = new MarkSixAnalyseSpecifiedLocationDto {Location = 7, Times = times, TensNumbersTailCutCount = 6, OnesAllowMinFactorCurrentConsecutiveTimes = 8, OnesNumbersTailCutCount = 10, OnesAllowMaxInterval = 0};

                        //var dto = new MarkSixAnalyseSpecifiedLocationDto { Location = 7, StartTimes = records[i].StartTimes, TensAllowMinFactorCurrentConsecutiveTimes = 6, TensAllowMaxInterval = -1, TensAroundCount = 200, TensNumbersTailCutCount = 6 };
                        var result = service.AnalyseSpecifiedLocationByPermutationFactors(dto);
                        if (result.Count > 0)
                        {
                            resultCount++;
                            var resultSource = result.Select(r => r.ToString("00"));
                            var onesResults = resultSource.Select(r => byte.Parse(r.Substring(1))).Distinct().ToList();
                            var tensResults = resultSource.Select(r => byte.Parse(r.Substring(0, 1))).Distinct().ToList();
                            if (tensResults.Contains(tens))
                            {
                                tensHasCount++;
                            }
                            if (onesResults.Contains(ones))
                            {
                                onesHasCount++;
                            }
                        }
                        var has = result.Exists(m => m == seventhNum);
                        if (has) hasCount++;
                        resultString.AppendLine("期次：" + records[i].Times + ",第7位号码：" + seventhNum + ",分析结果：" + (has ? "-Yes- " : "      ") + string.Join(";", result));
                    }
                    var str = resultString.ToString();
                }
            }
        }

        #endregion


        #region 测试一般的因子分析方法分析历史趋势

        /// <summary>
        ///     分析个位历史趋势，因子分组
        /// </summary>
        [TestMethod]
        public void TestMethod_AnalyseOnesHistoricalTrend_Factor_Array()
        {
            using (var dao = new TrendDbContext())
            {
                var service = new MarkSixAnalysisService();
                var records = dao.Set<MarkSixRecord>().OrderByDescending(m => m.Times).Take(1000).ToList();
                var trendDto = new MarkSixAnalyseHistoricalTrendDto
                {
                    Location = 7,
                    Times = records[0].Times,
                    AnalyseNumberCount = 100,
                    StartAllowMaxInterval = 1,
                    EndAllowMaxInterval = -3,
                    StartAllowMinFactorCurrentConsecutiveTimes = 6,
                    EndAllowMinFactorCurrentConsecutiveTimes = 10,
                    AllowMinTimes = 3,
                    NumbersTailCutCount = 6
                };

                var trends = service.AnalyseOnesHistoricalTrend(trendDto);
                var content = new StringBuilder();
                trends.ForEach(item => content.Append(item.ToString()));


                var str = content.ToString();
            }
        }


        [TestMethod] //
        public void TestMethod_AnalyseTensHistoricalTrend()
        {
            using (var dao = new TrendDbContext())
            {
                var service = new MarkSixAnalysisService();
                var records = dao.Set<MarkSixRecord>().OrderByDescending(m => m.Times).Take(1000).ToList();
                var trendDto = new MarkSixAnalyseHistoricalTrendDto
                {
                    Location = 7,
                    Times = records[0].Times,
                    AnalyseNumberCount = 100,
                    StartAllowMaxInterval = 2,
                    EndAllowMaxInterval = -5,
                    StartAllowMinFactorCurrentConsecutiveTimes = 3,
                    EndAllowMinFactorCurrentConsecutiveTimes = 9,
                    AllowMinTimes = 3,
                    NumbersTailCutCount = 6
                };

                var trends = service.AnalyseTensHistoricalTrend(trendDto);
                var content = new StringBuilder();
                trends.ForEach(item => content.Append(item.ToString()));


                var str = content.ToString();
            }
        }

        #endregion
    }

}