﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrendAnalysis.Data;
using TrendAnalysis.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using TrendAnalysis.DataTransferObject;
using System.Threading.Tasks;

namespace TrendAnalysis.Service.Test
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
                var onesDigitFactors = NumberCombination.CreateBinaryCombinations(new List<byte>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.ToList());

                //十位数号码列表
                var tensDigitNumbers = numbers.Select(n => n.ToString("00").Substring(0, 1)).Select(n => byte.Parse(n)).ToList();
                //十位因子
                var tensDigitFactors = NumberCombination.CreateBinaryCombinations(new List<byte>() { 0, 1, 2, 3, 4 }.ToList());

                var result = service.AnalyseOnesDigit(onesDigitNumbers, onesDigitFactors, 9,2);
                result = result.Where(m => m.ConsecutiveTimes.Count > 0).ToList();
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
                var onesDigitFactors = NumberCombination.CreateBinaryCombinations(new List<byte>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.ToList());

                //十位数号码列表
                var tensDigitNumbers = numbers.Select(n => n.ToString("00").Substring(0, 1)).Select(n => byte.Parse(n)).ToList();
                //十位因子
                var tensDigitFactors = NumberCombination.CreateBinaryCombinations(new List<byte>() { 0, 1, 2, 3, 4 }.ToList());

                var service = new MarkSixAnalysisService();
                var result = service.AnalyseTensDigit(tensDigitNumbers, tensDigitFactors, 4,0);
                result = result.Where(m => m.ConsecutiveTimes.Count > 0).ToList();
            }
        }

        //[TestMethod]
        //public void TestMethod_AnalyseByDigit()
        //{
        //    using (var dao = new TrendDbContext())
        //    {
        //        var numbers = dao.Set<MarkSixRecord>().OrderBy(n => n.Times).Take(20).Select(n => n.SeventhNum).ToList();
        //        var service = new MarkSixAnalysisService();
        //        var result = service.AnalyseByDigit(numbers, 4);
        //        result = result.Where(m => m.ConsecutiveTimes.Count > 0).ToList();
        //    }

        //}

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
                    var dto = new MarkSixAnalyseSpecifiedLocationDto { Location = 7,Times= times, TensNumbersTailCutCount = 6,OnesAllowMinSpecifiedTimesConsecutiveTimes=8,OnesNumbersTailCutCount=10, OnesAllowMaxInterval=0};
                    //var dto = new MarkSixAnalyseSpecifiedLocationDto { Location = 7, Times = records[i].Times, TensAllowMinSpecifiedTimesConsecutiveTimes = 6, TensAllowMaxInterval = -1, TensAroundCount = 200, TensNumbersTailCutCount = 6 };
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


        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestMethod_AnalyseSpecifiedLocation_By_Parallel()
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
                Parallel.For(0, 100, i =>
                {
                    var seventhNum = records[i].SeventhNum;
                    var ones = byte.Parse(seventhNum.ToString("00").Substring(1));
                    var tens = byte.Parse(seventhNum.ToString("00").Substring(0, 1));
                    var dto = new MarkSixAnalyseSpecifiedLocationDto { Location = 7, Times = records[i].Times, TensAllowMinSpecifiedTimesConsecutiveTimes = 6, TensAllowMaxInterval = 2, TensAroundCount = 30 };
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
                });
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
                var onesLeft = new List<byte>() { 0, 1, 2, 3, 4 };
                var onesRight = new List<byte>() { 5, 6, 7, 8, 9 };

                var tensLeft = new List<byte>() { 0, 1 };
                var tensRight = new List<byte>() { 2, 3, 4 };
                var rnd = new Random();
                Parallel.For(0, 100, i =>
                {
                    var r = rnd.Next() % 2;
                    var tensFactor = new List<byte>();
                    if (r == 1)
                    {
                        tensFactor = tensLeft;
                    }
                    else
                    {
                        tensFactor = tensRight;
                    }

                    var r1 = rnd.Next() % 2;
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
        /// 通过10位和个位因子，获取最终数字
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
                    var valueStr = tenFactor[i].ToString() + onesFactor[j].ToString();
                    byte number;
                    if (!byte.TryParse(valueStr, out number))
                    {
                        throw new Exception(string.Format("错误，{0}不是有效的byte类型数据！", valueStr));
                    }
                    result.Add(number);
                }
            }
            return result;
        }
        //[TestMethod]
        //public void TestGetNumbers()
        //{
        //    var tenFactor = new List<byte>() { 1,2};
        //    var onesFactor = new List<byte>() {3,4,5,6 };
        //    var service = new MarkSixAnalysisService();
        //    var numbers = service.GetNumbers(tenFactor,onesFactor);
        //    Assert.IsTrue(numbers.Count == 8);

        //}
    }
}
