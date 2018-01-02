using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrendAnalysis.DataTransferObject.Trend;
using TrendAnalysis.Models.Trend;
using TrendAnalysis.Service.Trend;


namespace TrendAnalysis.Service.Test.Trend
{
    [TestClass]
    public class UnitTestMultiNumberFactorTrend
    {
        [TestMethod]
        public void TestAnalyseConsecutives()
        {
            var maxCount = 1;
            var dto = new MultiNumberFactorTrendAnalyseDto<byte>
            {
                Numbers = new List<byte>() { 1, 3, 9, 3, 8, 4, 0, 1, 9, 6, 5, 4, 6, 3, 0, 0, 6, 6, 9, 9, 2, 2, 4, 4, 0 },
                Factors = new List<Factor<byte>>() { new Factor<byte>() { Left = new List<byte>() { 1, 2 }, Right = new List<byte>() { 3, 4 } } },
                AllowMaxInterval = 0,
                AllowMinFactorCurrentConsecutiveTimes = 2,
                AllowMinTimes = 1,
                NumbersTailCutCount = 6,
                AnalyseConsecutiveCompareFunc = (nums, factors, i) =>
                {
                    var sum = nums[i] + nums[i - 1];
                    var curNumber = (byte)(sum % 10);
                    return factors.Contains(curNumber);
                },
                MultiNumberMaxCount = maxCount
            };
            var resultList = MultiNumberFactorTrend.AnalyseConsecutives(dto);
        }
    }
}
