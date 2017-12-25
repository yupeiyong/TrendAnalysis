using System.Collections.Generic;
using TrendAnalysis.Models;
using System.Linq;
using TrendAnalysis.Data;
using System;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.Service.Trend;
using TrendAnalysis.Models.Trend;
using Newtonsoft.Json;

namespace TrendAnalysis.Service.MarkSix
{
    public class MarkSixAnalysisService
    {

        /// <summary>
        /// 分析指定位置号码
        /// </summary>
        /// <param name="location">指定的第几位</param>
        /// <param name="times">分析指定的期次</param>
        /// <returns></returns>
        public List<byte> AnalyseSpecifiedLocation(MarkSixAnalyseSpecifiedLocationDto dto)
        {
            using (var dao = new TrendDbContext())
            {
                var source = dao.Set<MarkSixRecord>().AsQueryable();
                if (!string.IsNullOrWhiteSpace(dto.Times))
                {
                    var record = dao.Set<MarkSixRecord>().FirstOrDefault(m => m.Times == dto.Times);
                    if (record == null)
                        throw new Exception("错误，指定期次的记录不存在！");
                    source = source.Where(m => m.TimesValue < record.TimesValue);
                }

                //按期次值升序排列
                source = source.OrderBy(m => m.TimesValue);
                var numbers = new List<byte>();
                switch (dto.Location)
                {
                    case 1:
                        numbers = source.Select(m => m.FirstNum).ToList();
                        break;
                    case 2:
                        numbers = source.Select(m => m.SecondNum).ToList();
                        break;
                    case 3:
                        numbers = source.Select(m => m.ThirdNum).ToList();
                        break;
                    case 4:
                        numbers = source.Select(m => m.FourthNum).ToList();
                        break;
                    case 5:
                        numbers = source.Select(m => m.FifthNum).ToList();
                        break;
                    case 6:
                        numbers = source.Select(m => m.SixthNum).ToList();
                        break;
                    case 7:
                        numbers = source.Select(m => m.SeventhNum).ToList();
                        break;
                    default:
                        throw new Exception("错误，指定的位置不是有效的号码位置！");
                }

                var FactorHistoricalTrend = new FactorTrend();
                //十位数号码列表
                var tensDigitNumbers = numbers.Select(n => n.ToString("00").Substring(0, 1)).Select(n => byte.Parse(n)).ToList();
                //十位因子
                var tensDigitFactors = FactorGenerator.Create(new List<byte>() { 0, 1, 2, 3, 4 }.ToList());

                //按数字位置分析（十位/个位）
                //十位
                var tensDigitResult = FactorHistoricalTrend.Analyse(new FactorTrendAnalyseDto<byte>
                {
                    Numbers = tensDigitNumbers,
                    Factors = tensDigitFactors,
                    AllowMinTimes = dto.TensAllowMinTimes,
                    NumbersTailCutCount = dto.TensNumbersTailCutCount,
                    AllowMinFactorCurrentConsecutiveTimes = dto.TensAllowMinFactorCurrentConsecutiveTimes,
                    AllowMaxInterval = dto.TensAllowMaxInterval
                });

                //个位数号码列表
                var onesDigitNumbers = numbers.Select(n => n.ToString("00").Substring(1)).Select(n => byte.Parse(n)).ToList();
                //个位因子
                var onesDigitFactors = FactorGenerator.Create(new List<byte>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.ToList());

                //个位
                var onesDigitResult = FactorHistoricalTrend.Analyse(new FactorTrendAnalyseDto<byte>
                {
                    Numbers = onesDigitNumbers,
                    Factors = onesDigitFactors,
                    AllowMinTimes = dto.OnesAllowMinTimes,
                    NumbersTailCutCount = dto.OnesNumbersTailCutCount,
                    AllowMinFactorCurrentConsecutiveTimes = dto.OnesAllowMinFactorCurrentConsecutiveTimes,
                    AllowMaxInterval = dto.OnesAllowMaxInterval
                });

                if (tensDigitResult.Count > 0 && onesDigitResult.Count > 0)
                {
                    //选择最多连续次数
                    var maxTens = tensDigitResult.OrderByDescending(t => t.FactorCurrentConsecutiveTimes).FirstOrDefault();
                    var maxOnes = onesDigitResult.OrderByDescending(t => t.FactorCurrentConsecutiveTimes).FirstOrDefault();
                    if (maxTens != null && maxOnes != null)
                    {
                        var tenFactor = maxTens.OppositeFactor;
                        var onesFactor = maxOnes.OppositeFactor;
                        return GetNumbers(tenFactor, onesFactor);
                    }
                }
                return new List<byte>();
            }
        }


        /// <summary>
        /// 通过排列因子分析指定位置号码
        /// </summary>
        /// <param name="location">指定的第几位</param>
        /// <param name="times">分析指定的期次</param>
        /// <returns></returns>
        public List<byte> AnalyseSpecifiedLocationByPermutationFactors(MarkSixAnalyseSpecifiedLocationDto dto)
        {
            using (var dao = new TrendDbContext())
            {
                var source = dao.Set<MarkSixRecord>().AsQueryable();
                if (!string.IsNullOrWhiteSpace(dto.Times))
                {
                    var record = dao.Set<MarkSixRecord>().FirstOrDefault(m => m.Times == dto.Times);
                    if (record == null)
                        throw new Exception("错误，指定期次的记录不存在！");
                    source = source.Where(m => m.TimesValue < record.TimesValue);
                }

                //按期次值升序排列
                source = source.OrderBy(m => m.TimesValue);
                var numbers = new List<byte>();
                switch (dto.Location)
                {
                    case 1:
                        numbers = source.Select(m => m.FirstNum).ToList();
                        break;
                    case 2:
                        numbers = source.Select(m => m.SecondNum).ToList();
                        break;
                    case 3:
                        numbers = source.Select(m => m.ThirdNum).ToList();
                        break;
                    case 4:
                        numbers = source.Select(m => m.FourthNum).ToList();
                        break;
                    case 5:
                        numbers = source.Select(m => m.FifthNum).ToList();
                        break;
                    case 6:
                        numbers = source.Select(m => m.SixthNum).ToList();
                        break;
                    case 7:
                        numbers = source.Select(m => m.SeventhNum).ToList();
                        break;
                    default:
                        throw new Exception("错误，指定的位置不是有效的号码位置！");
                }

                //十位数号码列表
                var tensDigitNumbers = numbers.Select(n => n.ToString("00").Substring(0, 1)).Select(n => byte.Parse(n)).ToList();
                //十位因子
                var tensDigitFactors = FactorGenerator.Create(new List<byte>() { 0, 1, 2, 3, 4 }.ToList());
                var tensResults = new List<List<PermutationFactorTrendAnalyseResult<byte>>>();
                for (var i = 0; i < tensDigitFactors.Count; i++)
                {
                    var ls = new List<List<Factor<byte>>> { new List<Factor<byte>> { tensDigitFactors[i] }, tensDigitFactors };
                    var trend = new PermutationFactorTrend();
                    var curResult=trend.Analyse(new PermutationFactorTrendAnalyseDto<byte>
                    {
                        Numbers = tensDigitNumbers,
                        PermutationFactors = ls,
                        AllowMinTimes = dto.TensAllowMinTimes,
                        NumbersTailCutCount = dto.TensNumbersTailCutCount,
                        AllowMinFactorCurrentConsecutiveTimes = dto.TensAllowMinFactorCurrentConsecutiveTimes,
                        AllowMaxInterval = dto.TensAllowMaxInterval

                    });
                    if (curResult.Count > 0)
                    {
                        tensResults.Add(curResult);
                    }                    
                }


                //个位数号码列表
                var onesDigitNumbers = numbers.Select(n => n.ToString("00").Substring(1)).Select(n => byte.Parse(n)).ToList();


                //显示字符串，用于测试
                var numberString = string.Join(",", onesDigitNumbers);
                /*
                 4,6,9,1,8,3,6,6,6,1,7,1,4,7,8,2,0,5,2,4,6,3,4,4,2,6,1,5,1,1,5,9,7,4,9,5,2,0,0,2,5,5,4,1,2,6,8,3,5,7,5,0,8,1,4,7,8,3,6,4,4,2,5,4,7,8,9,6,9,1,5,5,5,5,2,
                 0,7,0,8,5,6,7,7,4,2,0,1,3,7,8,8,9,9,0,0,2,4,4,3,6,3,7,5,1,3,5,4,8,5,5,4,5,8,2,2,5,0,2,7,5,5,9,7,9,3,2,1,2,9,9,8,8,9,0,0,6,6,0,3,5,5,8,1,9,6,4,6,2,3,0,
                 1,8,0,0,7,1,4,7,4,2,9,6,6,5,5,1,0,0,7,4,7,2,4,8,3,3,6,9,2,1,6,1,4,0,4,3,3,4,6,1,7,1,9,4,8,5,8,3,6,0,2,5,0,8,4,0,0,5,4,9,1,2,6,5,7,3,9,9,3,6,3,7,3,7,5,
                 8,3,2,0,8,1,3,0,0,6,2,7,8,4,5,9,2,4,4,5,3,4,6,0,4,8,0,6,9,3,7,5,7,3,8,2,6,7,5,6,4,9,7,4,6,2,6,7,6,0,7,9,1,5,0,0,7,8,1,7,6,8,7,1,5,4,2,3,2,6,4,3,2,1,5,
                 0,1,4,3,7,9,5,3,6,9,1,6,0,0,1,6,5,7,0,4,3,4,9,1,6,9,3,3,3,3,9,4,7,8,5,2,9,3,1,8,4,6,9,2,7,9,4,3,5,6,6,8,5,3,0,1,1,6,0,3,6,2,8,4,4,8,1,5,2,0,2,4,0,8,8,
                 4,9,1,3,7,6,0,9,9,6,4,0,0,2,8,5,0,2,5,7,9,7,6,8,6,2,0,9,0,5,5,4,9,4,1,2,0,5,2,5,2,5,7,1,8,0,2,3,2,1,9,2,7,5,4,9,8,9,3,0,4,7,8,2,5,2,5,2,9,7,8,1,9,1,7,2,3,1,0,4,4,5,5,7,3,1,8,0,3,2,4,0,4,3,4,9,9,8,2,4,1,3,4,7,6,9,6,1,4,1,3,5,4,0,1,5,4,0,4,8,9,1,2,9,9,5,3,3,7,0,3,9,7,9,0,8,7,8,5,3,3,2,5,6,4,1,2,4,2,5,3,7,1,0,5,4,3,2,2,5,5,1,5,9,1,3,7,5,8,1,7,1,6,3,3,9,2,3,0,0,9,4,1,4,0,0,8,7,0,3,8,1,6,5,5,6,6,0,9,4,7,8,4,3,5,9,3,9,7,1,4,3,4,2,2,2,5,1,4,2,9,2,4,3,0,9,5,2,7,6,5,2,0,3,4,1,5,1,4,8,9,3,1,6,1,5,0,4,9,0,7,8,0,7,1,4,2,5,0,4,3,3,6,9,8,5,1,8,3,5,8,8,8,1,9,0,4,7,4,6,4,2,5,6,2,9,3,2,0,1,2,3,2,7,5,1,9,9,0,6,6,2,8,5,4,3,9,1,1,3,1,7,7,6,0,2,4,4,1,8,1,6,3,4,7,1,6,2,0,6,6,1,7,4,5,5,6,7,8,3,4,2,3,0,1,4,2,7,7,4,7,7,4,6,6,3,2,7,6,1,5,2,2,5,7,5,8,4,2,3,4,5,2,7,3,8,2,4,5,3,5,5,2,7,5,4,4,0,3,4,7,5,0,9,9,9,4,3,9,8,1,9,8,0,4,6,4,5,0,0,2,2,4,7,3,3,6,6,8,7,8,9,8,5,8,6,7,4,5,1,0,4,6,1,6,7,1,9,2,5,3,5,4,3,8,7,4,0,5,9,6,8,2,0,1,1,0,9,4,8,1,4,5,2,4,9,3,2,2,7,9,9,5,9,9,9,8,2,4,6,6,9,1,6,9,4,6,8,7,0,9,6,1,7,0,5,6,9,7,1,3,3,7,5,5,4,4,3,5,3,3,2,0,4,0,7,9,2,5,9,3,7,5,1,9,0,9,8,5,4,2,4,2,2,1,5,4,3,9,6,8,1,2,4,8,6,7,7,5,6,5,8,5,3,9,7,1,3,7,5,0,1,6,4,5,8,2,3,1,2,6,1,3,8,9,3,3,2,5,0,7,6,3,2,7,7,4,7,3,7,7,5,0,4,4,8,1,6,4,9,5,2,8,3,0,2,6,8,6,1,5,7,1,5,2,9,4,3,4,4,6,0,2,2,0,4,5,4,7,8,4,2,7,1,6,5,8,3,5,9,6,6,9,2,6,3,5,3,4,7,3,4,4,6,4,0,6,3,5,8,8,8,8,1,5,4,1,4,3,0,2,0,1,3,7,4,0,0,5,3,0,2,9,3,0,3,9,0,9,4,8,7,5,1,2,3,5,7,4,7,2,7,1,3,4,8,3,6,2,1,3,6,1,4,7,3,1,4,9,1,8,8,2,0,1,6,6,9,4,2,6,6,4,6,3,8,8,2,6,7,3,1,6,4,9,6,6,9,7,6,8,1,6,2,4,1,4,8,8,0,7,9,5,6,5,6,2,5,7,5,5,3,0,9,5,5,4,5,5,1,0,9,8,9,9,3,3,7,8,1,4,7,4,4,5,5,3,3,0,2,3,8,9,0,2,2,0,3,5,6,7,9,0,0,8,7,5,9,2,4,3,0,4,1,5,0,7,9,6,7,7,4,1,2,2,4,0,8,6,0,5,5,2,5,9,0,0,7,9,5,2,8,6,5,3,2,6,1,2,5,3,8,7,4,0,1,4,3,7,0,9,1,8,2,6,7,1,3,1,9,0,2,9,3,1,1,7,2,4,5,2,9,7,7,7,6,5,3,0,6,1,3,2,1,4,1,8,9,0,3,3,7,1,5,4,3,9,5,3,2,7,9,9,5,0,7,8,4,1,8,1,4,2,7,2,8,4,4,8,8,4,3,7,1,8,0,7,5,4,0,6,8,7,1,2,5,2,7,3,8,1,4,1,5,5,0,0,6,2,0,5,9,9,5,2,2,7,3,8,9,1,1,5,4,1,8,2,8,1,3,8,6,1,7,8,9,5,2,4,4,6,5,4,0,9,6,9,8,3,6,6,9,8,4,5,5,6,3,0,7,2,3,6,0,5,8,7,2,8,3,3,3,7,3,6,8,5,8,2,2,6,6,9,1,7,9,6,9,2,7,1,2,8,9,0,7,9,1,7,8,2,9,3,2,7,4,6,1,4,9,0,8,4,6,4,9,7,2,1,3,8,4,4,0,1,8,1,2,8,4,3,4,0,2,8,8,1,5,6,4,2,9,3,3,5,5,2,5,9,9,4,4,1,6,7,4,7,1,6,3,6,3,1,9,4,1,8,3,8,1,4,7,7,9,1,6,3,8,5,2,8,7,7,8,2,3,5,0,0,5,6,4,2,8,7,3,3,1,7,5,2,6,2,2,3,9,9,2,9,1,8,5,2,2,7,7,6,5,9,5,1,3,6,8,4,5,4,0,5,2,5,7,8,2,9,9,4,9,2,9,7,9,0,7,1,9,0,8,0,4,3,4,6,9,2,9,0,2,2,1,3,1,6,1,6,8,6,7,2,1,1,0,1,5,6,7,8,0,1,3,4,2,0,3,6,5,0,0,9,2,0,0,1,3,1,6,4,4,5,5,7,4,7,3,1,6,4,4,0,5,9,0,3,4,6,2,3,7,4,3,8,0,2,6,1,7,9,3,6,3,9,5,3,2,8,5,3,4,6,9,0,5,3,1,4,8,5,2,1,6,1,5,7,2,8,8,9,0,0,2,3,6,3,7,4,4,1,4,7,3,0,1,2,7,2,5,7,5,2,9,4,4,3,7,8,5,5,3,3,0,4,6,1,8,1,8,2,5,4,8,0,2,4,7,5,4,8,9,7,9,7,8,3,4,8,7,5,5,0,5,9,1,5,7,8,8,7,9,3,0,7,3,5,3,3,2,8,7,6,4,1,0,0,9,4,2,4,2,3,9,3,6,7,3,3,4,5,6,2,7,2,6,5,0,3,8,3,4,9,4,6,0,7,0,0,9,7,0,4,6,0,0,9,6,9,8,0,4,7,3,4,6,8,8,7,8,7,2,9,1,6,1,2,9,5,8,3,6,3,5,9,4,9,7,7,9,0,7,5,6,5,3,2,9,3,2,6,4,7,0,8,2,0,8,7,5,3,6,9,5,8,1,9,8,1,9,3,1,0,6,2,3,8,3,0,0,1,9,1,0,0,7,3,1,5,1,8,7,6,0,4,5,7,9,5,2,8,6,4,2,1,0,1,0,5,7,6,1,3,8,5,9,8,5,0,4,7,4,4,5,1,4,1,1,3,2,1,3,4,8,3,6,0,2,9,5,0,6,8,7,8,6,3,1,7,6,9,2,6,8,2,1,4,5,8,0,4,2,2,4,9,4,3,2,0,9,7,4,7,5,0,8,8,0,9,1,7,3,2,2,5,0,5,5,3,3,2,7,7,2,2,7,3,4,6,0,7,2,3,4,3,0,2,1,6,9,3,4,1,2,7,3,7,4,7,6,6,7,1,5,0,6,1,6,3,7,7,5,4,8,6,5,1,8,7,9,5,0,2,8,7,2,6,9,7,1,9,4,4,5,1,6,3,7,5,1,2,9,9,3,6,1,4,6,8,9,5,7,0,7,0,6,4,2,3,3,8,4,4,5,3,2,3,6,3,4,1,9,9,9,0,9,8,6,7,5,8,8,1,3,8,3,5,3,6,7,9,7,7,8,9,8,0,2,3,4,1,1,3,4,3,5,0,9,1,9,5,8,7,9,0,9,0,1,9,5,5,6,6,3,0,0,8,3,3,5,4,5,8,4,5,1,4,6,2,1,3,2,2,8,8,6,6,8,5,0,6,0,3,6,5,0,6,1,4,0,2,8,2,5,5,4,2,5,3,6,0,1,4,5,9,5,1,7,1,7,4,4,7,9,9,9,5,9,4,3,1,2,5,0,9,7,3,1,8,8,2,1,5,0,3,3,3,0,6,8,9,6,9,7,1,2,0,7,7,2,9,6,4,8,2,0,5,8,6,8,6,8,4,5,2,6,5,5,6,9,5,2,0,3,2,1,9,7,4,0,4,4,1,5,5,9,5,4,3,7,9,5,0,7,4,2,3,0,2,4,2,0,1,1,2,1,3,5,4,8,6,3,6,5,0,5,4,4,6,0,2,6,1,1,9,9,4,0,1,4,1,0,4,2,3,4,2,8,7,6,9,4,0,4,8,1,5,8,7,4,6,6,8,4,9,1,0,4,8,4,7,6,8,5,7,5,6,8,3,4,0,1,7,4,1,5,4,1,6,7,4,4,4,1,1,5,1,8,0,8,0,9,3,4,9,6,7,2,0,1,1,7,0,6,2,2,8,0,1,3,4,7,6,4,9,3,1,6,6,7,6,6,6,0,1,8,0,1,6,7,2,9,0,1,0,8,5,0,7,3,9,2,7,1,8,3,4,5,4,0,9,2,7,4,9,1,7,8,0,6,6,1,0,4,3,1,4,7,4,4,2,9,9,9,7,3,4,1,3,2,4,2,2,2,2,7,7,3,4,3,6,5,8,5,7,2,1,8,7,7,5,2,6,4,5,7,5,6,7,6,2,2,7,7,3,1,7,8,8,2,6,7,5,7,8,8,5,9,3,4,5,8,2,9,4,7,4,7,0,0,0,3,7,6,1,2,2,3,0,3,4,8,0,2,7,8,7,0,8,6,9,1,9,7,2,6,0,5,0,5,4,9,9,0,0,2,8,1,9,0,0,5,3,9,3,2,1,9,2,2,0,8,5,8,5,8,7,3,6,5,2,6,0,8,1,8,5,4,3,0,8,7,8,8,5,2,6,2,9,8,2,9,8,3,7,7,0,5,9,6,5,0,2,1,0,4,0,6,1,4,4,8,0,7,2,1,0,2,0,6,2,9,7,2,6,2,5,2,3,6,8,3,5,7,3,6,3,3,4,0,7,4,9,7,0,3,9,8,3,5,9,5,1,5,2,4,9,5,1,5,1,4,1,2,5,2,4,1,5,1,8,0,2,0,2,7,4,1,1,1,5,9,7,7,7,3,8,5,8,5,7,7,2,7,3,5,2,3,4,2,6,5,7,1,4,8,8,8,2,4,4,6,1,5,4,3,2,5,4,8,2,3,8,5,1,4,2,0,3,1,4,7,6,8,7,7,9,6,2,9,4,8,9,7,4,1,0,4,7,3,3,7,3,9,3,1,2,5,1,7,2,0,8,6,8,8,5,8,5,7,5,8,8,3,9,7,3,3,2,9,0,7,8,2,3,9,6,5,4,0,6,6,1,8,1,9,2,1,2,3,5,1,1,0,3,1,3,0,6,7,4,8,8,4,4,8,4,9,2,4,3,0,5,9,7,8,7,3,1,6,1,4,5,8,2,2,3,7,2,7,8,4,7,5,1,7,7,5,2,8,4,2,8,8,4,7,1,3,7,5,4,5,9,2,8,8,9,8,6,9,9,6,4,3,6,3,2,7,7,4,6,6,8,7,3,7,2,0,8,5,1,4,9,0,9,2,7,4,3,7,9,8,2,5,4,4,0,0,1,9,2,8,2,9,1,4,3,1,6,6,3,0,6,6,7,7,6,7,6,3,9,1,1,9,4,4,4,5,0,0,0,8,2,9,8,6,0,0,7,1,6,0,1,6,6,2,8,6,4,3,8,9,4,1,9,0,6,7,9,7,7,9,6,5,4,1,3,6,5,7,1,3,8,4,7,6,8,5,7,9,4,9,0,1,5,5,4,5,4,4,2,8,0,7,0,7,7,4,4,6,6,1,5,3,1,6,5,3,7,2,3,4,6,6,4,0,5,0,8,5,8,7,7,6,9,9,1,5,1,5,6,7,0,5
                 
                 */

                var reverseNumber = new List<byte>();
                for(var i = onesDigitNumbers.Count - 1; i >= 0; i--)
                {
                    reverseNumber.Add(onesDigitNumbers[i]);
                }
                //显示字符串，用于测试
                var reverseNumbersString = string.Join(",", reverseNumber);
                /*
                 5,0,7,6,5,1,5,1,9,9,6,7,7,8,5,8,0,5,0,4,6,6,4,3,2,7,3,5,6,1,3,5,1,6,6,4,4,7,7,0,7,0,8,2,4,4,5,4,5,5,1,0,9,4,9,7,5,8,6,7,4,8,3,1,7,5,6,3,1,4,5,6,9,7,7,9,7,
                 6,0,9,1,4,9,8,3,4,6,8,2,6,6,1,0,6,1,7,0,0,6,8,9,2,8,0,0,0,5,4,4,4,9,1,1,9,3,6,7,6,7,7,6,6,0,3,6,6,1,3,4,1,9,2,8,2,9,1,0,0,4,4,5,2,8,9,7,3,4,7,2,9,0,9,4,1,
                 5,8,0,2,7,3,7,8,6,6,4,7,7,2,3,6,3,4,6,9,9,6,8,9,8,8,2,9,5,4,5,7,3,1,7,4,8,8,2,4,8,2,5,7,7,1,5,7,4,8,7,2,7,3,2,2,8,5,4,1,6,1,3,7,8,7,9,5,0,3,4,2,9,4,8,4,4,
                 8,8,4,7,6,0,3,1,3,0,1,1,5,3,2,1,2,9,1,8,1,6,6,0,4,5,6,9,3,2,8,7,0,9,2,3,3,7,9,3,8,8,5,7,5,8,5,8,8,6,8,0,2,7,1,5,2,1,3,9,3,7,3,3,7,4,0,1,4,7,9,8,4,9,2,6,9,
                 7,7,8,6,7,4,1,3,0,2,4,1,5,8,3,2,8,4,5,2,3,4,5,1,6,4,4,2,8,8,8,4,1,7,5,6,2,4,3,2,5,3,7,2,7,7,5,8,5,8,3,7,7,7,9,5,1,1,1,4,7,2,0,2,0,8,1,5,1,4,2,5,2,1,4,1,5,
                 1,5,9,4,2,5,1,5,9,5,3,8,9,3,0,7,9,4,7,0,4,3,3,6,3,7,5,3,8,6,3,2,5,2,6,2,7,9,2,6,0,2,0,1,2,7,0,8,4,4,1,6,0,4,0,1,2,0,5,6,9,5,0,7,7,3,8,9,2,8,9,2,6,2,5,8,8,
                 7,8,0,3,4,5,8,1,8,0,6,2,5,6,3,7,8,5,8,5,8,0,2,2,9,1,2,3,9,3,5,0,0,9,1,8,2,0,0,9,9,4,5,0,5,0,6,2,7,9,1,9,6,8,0,7,8,7,2,0,8,4,3,0,3,2,2,1,6,7,3,0,0,0,7,4,7,
                 4,9,2,8,5,4,3,9,5,8,8,7,5,7,6,2,8,8,7,1,3,7,7,2,2,6,7,6,5,7,5,4,6,2,5,7,7,8,1,2,7,5,8,5,6,3,4,3,7,7,2,2,2,2,4,2,3,1,4,3,7,9,9,9,2,4,4,7,4,1,3,4,0,1,6,6,0,
                 8,7,1,9,4,7,2,9,0,4,5,4,3,8,1,7,2,9,3,7,0,5,8,0,1,0,9,2,7,6,1,0,8,1,0,6,6,6,7,6,6,1,3,9,4,6,7,4,3,1,0,8,2,2,6,0,7,1,1,0,2,7,6,9,4,3,9,0,8,0,8,1,5,1,1,4,4,
                 4,7,6,1,4,5,1,4,7,1,0,4,3,8,6,5,7,5,8,6,7,4,8,4,0,1,9,4,8,6,6,4,7,8,5,1,8,4,0,4,9,6,7,8,2,4,3,2,4,0,1,4,1,0,4,9,9,1,1,6,2,0,6,4,4,5,0,5,6,3,6,8,4,5,3,1,2,
                 1,1,0,2,4,2,0,3,2,4,7,0,5,9,7,3,4,5,9,5,5,1,4,4,0,4,7,9,1,2,3,0,2,5,9,6,5,5,6,2,5,4,8,6,8,6,8,5,0,2,8,4,6,9,2,7,7,0,2,1,7,9,6,9,8,6,0,3,3,3,0,5,1,2,8,8,1,
                 3,7,9,0,5,2,1,3,4,9,5,9,9,9,7,4,4,7,1,7,1,5,9,5,4,1,0,6,3,5,2,4,5,5,2,8,2,0,4,1,6,0,5,6,3,0,6,0,5,8,6,6,8,8,2,2,3,1,2,6,4,1,5,4,8,5,4,5,3,3,8,0,0,3,6,6,5,
                 5,9,1,0,9,0,9,7,8,5,9,1,9,0,5,3,4,3,1,1,4,3,2,0,8,9,8,7,7,9,7,6,3,5,3,8,3,1,8,8,5,7,6,8,9,0,9,9,9,1,4,3,6,3,2,3,5,4,4,8,3,3,2,4,6,0,7,0,7,5,9,8,6,4,1,6,3,
                 9,9,2,1,5,7,3,6,1,5,4,4,9,1,7,9,6,2,7,8,2,0,5,9,7,8,1,5,6,8,4,5,7,7,3,6,1,6,0,5,1,7,6,6,7,4,7,3,7,2,1,4,3,9,6,1,2,0,3,4,3,2,7,0,6,4,3,7,2,2,7,7,2,3,3,5,5,0,5,2,2,3,7,1,9,0,8,8,0,5,7,4,7,9,0,2,3,4,9,4,2,2,4,0,8,5,4,1,2,8,6,2,9,6,7,1,3,6,8,7,8,6,0,5,9,2,0,6,3,8,4,3,1,2,3,1,1,4,1,5,4,4,7,4,0,5,8,9,5,8,3,1,6,7,5,0,1,0,1,2,4,6,8,2,5,9,7,5,4,0,6,7,8,1,5,1,3,7,0,0,1,9,1,0,0,3,8,3,2,6,0,1,3,9,1,8,9,1,8,5,9,6,3,5,7,8,0,2,8,0,7,4,6,2,3,9,2,3,5,6,5,7,0,9,7,7,9,4,9,5,3,6,3,8,5,9,2,1,6,1,9,2,7,8,7,8,8,6,4,3,7,4,0,8,9,6,9,0,0,6,4,0,7,9,0,0,7,0,6,4,9,4,3,8,3,0,5,6,2,7,2,6,5,4,3,3,7,6,3,9,3,2,4,2,4,9,0,0,1,4,6,7,8,2,3,3,5,3,7,0,3,9,7,8,8,7,5,1,9,5,0,5,5,7,8,4,3,8,7,9,7,9,8,4,5,7,4,2,0,8,4,5,2,8,1,8,1,6,4,0,3,3,5,5,8,7,3,4,4,9,2,5,7,5,2,7,2,1,0,3,7,4,1,4,4,7,3,6,3,2,0,0,9,8,8,2,7,5,1,6,1,2,5,8,4,1,3,5,0,9,6,4,3,5,8,2,3,5,9,3,6,3,9,7,1,6,2,0,8,3,4,7,3,2,6,4,3,0,9,5,0,4,4,6,1,3,7,4,7,5,5,4,4,6,1,3,1,0,0,2,9,0,0,5,6,3,0,2,4,3,1,0,8,7,6,5,1,0,1,1,2,7,6,8,6,1,6,1,3,1,2,2,0,9,2,9,6,4,3,4,0,8,0,9,1,7,0,9,7,9,2,9,4,9,9,2,8,7,5,2,5,0,4,5,4,8,6,3,1,5,9,5,6,7,7,2,2,5,8,1,9,2,9,9,3,2,2,6,2,5,7,1,3,3,7,8,2,4,6,5,0,0,5,3,2,8,7,7,8,2,5,8,3,6,1,9,7,7,4,1,8,3,8,1,4,9,1,3,6,3,6,1,7,4,7,6,1,4,4,9,9,5,2,5,5,3,3,9,2,4,6,5,1,8,8,2,0,4,3,4,8,2,1,8,1,0,4,4,8,3,1,2,7,9,4,6,4,8,0,9,4,1,6,4,7,2,3,9,2,8,7,1,9,7,0,9,8,2,1,7,2,9,6,9,7,1,9,6,6,2,2,8,5,8,6,3,7,3,3,3,8,2,7,8,5,0,6,3,2,7,0,3,6,5,5,4,8,9,6,6,3,8,9,6,9,0,4,5,6,4,4,2,5,9,8,7,1,6,8,3,1,8,2,8,1,4,5,1,1,9,8,3,7,2,2,5,9,9,5,0,2,6,0,0,5,5,1,4,1,8,3,7,2,5,2,1,7,8,6,0,4,5,7,0,8,1,7,3,4,8,8,4,4,8,2,7,2,4,1,8,1,4,8,7,0,5,9,9,7,2,3,5,9,3,4,5,1,7,3,3,0,9,8,1,4,1,2,3,1,6,0,3,5,6,7,7,7,9,2,5,4,2,7,1,1,3,9,2,0,9,1,3,1,7,6,2,8,1,9,0,7,3,4,1,0,4,7,8,3,5,2,1,6,2,3,5,6,8,2,5,9,7,0,0,9,5,2,5,5,0,6,8,0,4,2,2,1,4,7,7,6,9,7,0,5,1,4,0,3,4,2,9,5,7,8,0,0,9,7,6,5,3,0,2,2,0,9,8,3,2,0,3,3,5,5,4,4,7,4,1,8,7,3,3,9,9,8,9,0,1,5,5,4,5,5,9,0,3,5,5,7,5,2,6,5,6,5,9,7,0,8,8,4,1,4,2,6,1,8,6,7,9,6,6,9,4,6,1,3,7,6,2,8,8,3,6,4,6,6,2,4,9,6,6,1,0,2,8,8,1,9,4,1,3,7,4,1,6,3,1,2,6,3,8,4,3,1,7,2,7,4,7,5,3,2,1,5,7,8,4,9,0,9,3,0,3,9,2,0,3,5,0,0,4,7,3,1,0,2,0,3,4,1,4,5,1,8,8,8,8,5,3,6,0,4,6,4,4,3,7,4,3,5,3,6,2,9,6,6,9,5,3,8,5,6,1,7,2,4,8,7,4,5,4,0,2,2,0,6,4,4,3,4,9,2,5,1,7,5,1,6,8,6,2,0,3,8,2,5,9,4,6,1,8,4,4,0,5,7,7,3,7,4,7,7,2,3,6,7,0,5,2,3,3,9,8,3,1,6,2,1,3,2,8,5,4,6,1,0,5,7,3,1,7,9,3,5,8,5,6,5,7,7,6,8,4,2,1,8,6,9,3,4,5,1,2,2,4,2,4,5,8,9,0,9,1,5,7,3,9,5,2,9,7,0,4,0,2,3,3,5,3,4,4,5,5,7,3,3,1,7,9,6,5,0,7,1,6,9,0,7,8,6,4,9,6,1,9,6,6,4,2,8,9,9,9,5,9,9,7,2,2,3,9,4,2,5,4,1,8,4,9,0,1,1,0,2,8,6,9,5,0,4,7,8,3,4,5,3,5,2,9,1,7,6,1,6,4,0,1,5,4,7,6,8,5,8,9,8,7,8,6,6,3,3,7,4,2,2,0,0,5,4,6,4,0,8,9,1,8,9,3,4,9,9,9,0,5,7,4,3,0,4,4,5,7,2,5,5,3,5,4,2,8,3,7,2,5,4,3,2,4,8,5,7,5,2,2,5,1,6,7,2,3,6,6,4,7,7,4,7,7,2,4,1,0,3,2,4,3,8,7,6,5,5,4,7,1,6,6,0,2,6,1,7,4,3,6,1,8,1,4,4,2,0,6,7,7,1,3,1,1,9,3,4,5,8,2,6,6,0,9,9,1,5,7,2,3,2,1,0,2,3,9,2,6,5,2,4,6,4,7,4,0,9,1,8,8,8,5,3,8,1,5,8,9,6,3,3,4,0,5,2,4,1,7,0,8,7,0,9,4,0,5,1,6,1,3,9,8,4,1,5,1,4,3,0,2,5,6,7,2,5,9,0,3,4,2,9,2,4,1,5,2,2,2,4,3,4,1,7,9,3,9,5,3,4,8,7,4,9,0,6,6,5,5,6,1,8,3,0,7,8,0,0,4,1,4,9,0,0,3,2,9,3,3,6,1,7,1,8,5,7,3,1,9,5,1,5,5,2,2,3,4,5,0,1,7,3,5,2,4,2,1,4,6,5,2,3,3,5,8,7,8,0,9,7,9,3,0,7,3,3,5,9,9,2,1,9,8,4,0,4,5,1,0,4,5,3,1,4,1,6,9,6,7,4,3,1,4,2,8,9,9,4,3,4,0,4,2,3,0,8,1,3,7,5,5,4,4,0,1,3,2,7,1,9,1,8,7,9,2,5,2,5,2,8,7,4,0,3,9,8,9,4,5,7,2,9,1,2,3,2,0,8,1,7,5,2,5,2,5,0,2,1,4,9,4,5,5,0,9,0,2,6,8,6,7,9,7,5,2,0,5,8,2,0,0,4,6,9,9,0,6,7,3,1,9,4,8,8,0,4,2,0,2,5,1,8,4,4,8,2,6,3,0,6,1,1,0,3,5,8,6,6,5,3,4,9,7,2,9,6,4,8,1,3,9,2,5,8,7,4,9,3,3,3,3,9,6,1,9,4,3,4,0,7,5,6,1,0,0,6,1,9,6,3,5,9,7,3,4,1,0,5,1,2,3,4,6,2,3,2,4,5,1,7,8,6,7,1,8,7,0,0,5,1,9,7,0,6,7,6,2,6,4,7,9,4,6,5,7,6,2,8,3,7,5,7,3,9,6,0,8,4,0,6,4,3,5,4,4,2,9,5,4,8,7,2,6,0,0,3,1,8,0,2,3,8,5,7,3,7,3,6,3,9,9,3,7,5,6,2,1,9,4,5,0,0,4,8,0,5,2,0,6,3,8,5,8,4,9,1,7,1,6,4,3,3,4,0,4,1,6,1,2,9,6,3,3,8,4,2,7,4,7,0,0,1,5,5,6,6,9,2,4,7,4,1,7,0,0,8,1,0,3,2,6,4,6,9,1,8,5,5,3,0,6,6,0,0,9,8,8,9,9,2,1,2,3,9,7,9,5,5,7,2,0,5,2,2,8,5,4,5,5,8,4,5,3,1,5,7,3,6,3,4,4,2,0,0,9,9,8,8,7,3,1,0,2,4,7,7,6,5,8,0,7,0,2,5,5,5,5,1,9,6,9,8,7,4,5,2,4,4,6,3,8,7,4,1,8,0,5,7,5,3,8,6,2,1,4,5,5,2,0,0,2,5,9,4,7,9,5,1,1,5,1,6,2,4,4,3,6,4,2,5,0,2,8,7,4,1,7,1,6,6,6,3,8,1,9,6,4
                 */

                //个位因子
                var onesDigitFactors = FactorGenerator.Create(new List<byte>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.ToList());

                var onesResults= new List<List<PermutationFactorTrendAnalyseResult<byte>>>();
                for (var i = 0; i < onesDigitFactors.Count; i++)
                {
                    var ls = new List<List<Factor<byte>>> { new List<Factor<byte>> { onesDigitFactors[i] }, onesDigitFactors };
                    var trend = new PermutationFactorTrend();
                    var curResult = trend.Analyse(new PermutationFactorTrendAnalyseDto<byte>
                    {
                        Numbers = onesDigitNumbers,
                        PermutationFactors = ls,
                        AllowMinTimes = dto.OnesAllowMinTimes,
                        NumbersTailCutCount = dto.OnesNumbersTailCutCount,
                        AllowMinFactorCurrentConsecutiveTimes = dto.OnesAllowMinFactorCurrentConsecutiveTimes,
                        AllowMaxInterval = dto.OnesAllowMaxInterval

                    });
                    if (curResult.Count > 0)
                    {
                        onesResults.Add(curResult);
                    }
                }

                //if (tensDigitResult.Count > 0 && onesDigitResult.Count > 0)
                //{
                //    //选择最多连续次数
                //    var maxTens = tensDigitResult.OrderByDescending(t => t.FactorCurrentConsecutiveTimes).FirstOrDefault();
                //    var maxOnes = onesDigitResult.OrderByDescending(t => t.FactorCurrentConsecutiveTimes).FirstOrDefault();
                //    if (maxTens != null && maxOnes != null)
                //    {
                //        var tenFactor = maxTens.OppositeFactor;
                //        var onesFactor = maxOnes.OppositeFactor;
                //        return GetNumbers(tenFactor, onesFactor);
                //    }
                //}
                var test=onesResults.OrderBy(p => p.First().Interval).ThenByDescending(p => p.First().FactorCurrentConsecutiveTimes).ToList();
                return new List<byte>();
            }
        }


        public static int FactorIndex { get; set; }
        /// <summary>
        /// 单独分析指定位置号码个位数
        /// </summary>
        /// <param name="location">指定的第几位</param>
        /// <param name="times">分析指定的期次</param>
        /// <returns></returns>
        public List<HistoricalTrend<byte>> AnalyseOnesHistoricalTrend(MarkSixAnalyseHistoricalTrendDto dto)
        {
            using (var dao = new TrendDbContext())
            {
                var source = dao.Set<MarkSixRecord>().AsQueryable();
                if (!string.IsNullOrWhiteSpace(dto.Times))
                {
                    var record = dao.Set<MarkSixRecord>().FirstOrDefault(m => m.Times == dto.Times);
                    if (record == null)
                        throw new Exception("错误，指定期次的记录不存在！");
                    source = source.Where(m => m.TimesValue < record.TimesValue);
                }

                //按期次值升序排列
                source = source.OrderBy(m => m.TimesValue);
                var records = new List<TemporaryRecord<byte>>();
                switch (dto.Location)
                {
                    case 1:
                        records = source.Select(m => new { Number = m.FirstNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 2:
                        records = source.Select(m => new { Number = m.SecondNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 3:
                        records = source.Select(m => new { Number = m.ThirdNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 4:
                        records = source.Select(m => new { Number = m.FourthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 5:
                        records = source.Select(m => new { Number = m.FifthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 6:
                        records = source.Select(m => new { Number = m.SixthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 7:
                        records = source.Select(m => new { Number = m.SeventhNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    default:
                        throw new Exception("错误，指定的位置不是有效的号码位置！");
                }

                var FactorHistoricalTrend = new FactorTrend();

                //个位因子
                var onesDigitFactors = FactorGenerator.Create(new List<byte>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.ToList());

                //onesDigitFactors = onesDigitFactors.Skip(FactorIndex * 20).Take(20).ToList();
                var trendDto = new AnalyseHistoricalTrendDto<byte>
                {
                    Numbers = records,
                    Factors = onesDigitFactors,
                    Location = dto.Location,
                    AnalyseNumberCount = dto.AnalyseNumberCount,
                    StartAllowMaxInterval = dto.StartAllowMaxInterval,
                    EndAllowMaxInterval = dto.EndAllowMaxInterval,
                    StartAllowMinFactorCurrentConsecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes,
                    EndAllowMinFactorCurrentConsecutiveTimes = dto.EndAllowMinFactorCurrentConsecutiveTimes,
                    AllowMinTimes = dto.AllowMinTimes,
                    NumbersTailCutCount = dto.NumbersTailCutCount
                };
                var historicalTrends = FactorHistoricalTrend.AnalyseHistoricalTrend(trendDto);

                return historicalTrends;
            }
        }


        /// <summary>
        /// 单独分析指定位置号码十位数
        /// </summary>
        /// <param name="location">指定的第几位</param>
        /// <param name="times">分析指定的期次</param>
        /// <returns></returns>
        public List<HistoricalTrend<byte>> AnalyseTensHistoricalTrend(MarkSixAnalyseHistoricalTrendDto dto)
        {
            using (var dao = new TrendDbContext())
            {
                var source = dao.Set<MarkSixRecord>().AsQueryable();
                if (!string.IsNullOrWhiteSpace(dto.Times))
                {
                    var record = dao.Set<MarkSixRecord>().FirstOrDefault(m => m.Times == dto.Times);
                    if (record == null)
                        throw new Exception("错误，指定期次的记录不存在！");
                    source = source.Where(m => m.TimesValue < record.TimesValue);
                }

                //按期次值升序排列
                source = source.OrderBy(m => m.TimesValue);
                var records = new List<TemporaryRecord<byte>>();
                switch (dto.Location)
                {
                    case 1:
                        records = source.Select(m => new { Number = m.FirstNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 2:
                        records = source.Select(m => new { Number = m.SecondNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 3:
                        records = source.Select(m => new { Number = m.ThirdNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 4:
                        records = source.Select(m => new { Number = m.FourthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 5:
                        records = source.Select(m => new { Number = m.FifthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 6:
                        records = source.Select(m => new { Number = m.SixthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 7:
                        records = source.Select(m => new { Number = m.SeventhNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    default:
                        throw new Exception("错误，指定的位置不是有效的号码位置！");
                }

                var FactorHistoricalTrend = new FactorTrend();

                //十位因子
                var tensDigitFactors = FactorGenerator.Create(new List<byte>() { 0, 1, 2, 3, 4 }.ToList());

                var trendDto = new AnalyseHistoricalTrendDto<byte>
                {
                    Numbers = records,
                    Factors = tensDigitFactors,
                    Location = dto.Location,
                    AnalyseNumberCount = dto.AnalyseNumberCount,
                    StartAllowMaxInterval = dto.StartAllowMaxInterval,
                    EndAllowMaxInterval = dto.EndAllowMaxInterval,
                    StartAllowMinFactorCurrentConsecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes,
                    EndAllowMinFactorCurrentConsecutiveTimes = dto.EndAllowMinFactorCurrentConsecutiveTimes,
                    AllowMinTimes = dto.AllowMinTimes,
                    NumbersTailCutCount = dto.NumbersTailCutCount
                };
                var historicalTrends = FactorHistoricalTrend.AnalyseHistoricalTrend(trendDto);

                return historicalTrends;
            }
        }


        /// <summary>
        /// 单独分析指定位置号码合数
        /// </summary>
        /// <param name="location">指定的第几位</param>
        /// <param name="times">分析指定的期次</param>
        /// <returns></returns>
        public List<HistoricalTrend<byte>> AnalyseCompositeHistoricalTrend(MarkSixAnalyseHistoricalTrendDto dto)
        {
            using (var dao = new TrendDbContext())
            {
                var source = dao.Set<MarkSixRecord>().AsQueryable();
                if (!string.IsNullOrWhiteSpace(dto.Times))
                {
                    var record = dao.Set<MarkSixRecord>().FirstOrDefault(m => m.Times == dto.Times);
                    if (record == null)
                        throw new Exception("错误，指定期次的记录不存在！");
                    source = source.Where(m => m.TimesValue < record.TimesValue);
                }

                //按期次值升序排列
                source = source.OrderBy(m => m.TimesValue);
                var records = new List<TemporaryRecord<byte>>();
                switch (dto.Location)
                {
                    case 1:
                        records = source.Select(m => new { Number = m.FirstNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = (byte)(byte.Parse(m.Number.ToString("00").Substring(0, 1)) + byte.Parse(m.Number.ToString("00").Substring(1))), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 2:
                        records = source.Select(m => new { Number = m.SecondNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = (byte)(byte.Parse(m.Number.ToString("00").Substring(0, 1)) + byte.Parse(m.Number.ToString("00").Substring(1))), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 3:
                        records = source.Select(m => new { Number = m.ThirdNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = (byte)(byte.Parse(m.Number.ToString("00").Substring(0, 1)) + byte.Parse(m.Number.ToString("00").Substring(1))), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 4:
                        records = source.Select(m => new { Number = m.FourthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = (byte)(byte.Parse(m.Number.ToString("00").Substring(0, 1)) + byte.Parse(m.Number.ToString("00").Substring(1))), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 5:
                        records = source.Select(m => new { Number = m.FifthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = (byte)(byte.Parse(m.Number.ToString("00").Substring(0, 1)) + byte.Parse(m.Number.ToString("00").Substring(1))), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 6:
                        records = source.Select(m => new { Number = m.SixthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = (byte)(byte.Parse(m.Number.ToString("00").Substring(0, 1)) + byte.Parse(m.Number.ToString("00").Substring(1))), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 7:
                        records = source.Select(m => new { Number = m.SeventhNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = (byte)(byte.Parse(m.Number.ToString("00").Substring(0, 1)) + byte.Parse(m.Number.ToString("00").Substring(1))), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    default:
                        throw new Exception("错误，指定的位置不是有效的号码位置！");
                }

                var FactorHistoricalTrend = new FactorTrend();

                var compositeService = new CompositeNumber(1, 49);
                var compositeNumber = compositeService.CompositeNumbers.Select(n => (byte)n).ToList();
                //合数因子
                var compositeDigitFactors = FactorGenerator.Create(compositeNumber);

                var trendDto = new AnalyseHistoricalTrendDto<byte>
                {
                    Numbers = records,
                    Factors = compositeDigitFactors,
                    Location = dto.Location,
                    AnalyseNumberCount = dto.AnalyseNumberCount,
                    StartAllowMaxInterval = dto.StartAllowMaxInterval,
                    EndAllowMaxInterval = dto.EndAllowMaxInterval,
                    StartAllowMinFactorCurrentConsecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes,
                    EndAllowMinFactorCurrentConsecutiveTimes = dto.EndAllowMinFactorCurrentConsecutiveTimes,
                    AllowMinTimes = dto.AllowMinTimes,
                    NumbersTailCutCount = dto.NumbersTailCutCount
                };
                var historicalTrends = FactorHistoricalTrend.AnalyseHistoricalTrend(trendDto);

                return historicalTrends;
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


        ///// <summary>
        ///// 分析列表个位数
        ///// </summary>
        ///// <param name="numbers">记录集合</param>
        ///// <param name="tensDigitFactors">比较因子</param>
        ///// <param name="allowMinTimes">允许的最小连续次数，大于等于此数才记录</param>
        ///// <returns></returns>
        //public List<FactorTrendAnalyseResult<byte>> AnalyseOnesDigit(List<byte> onesDigitNumbers, List<Factor<byte>> onesDigitFactors, int allowMinTimes, int numbersTailCutCount)
        //{
        //    List<FactorTrendAnalyseResult<byte>> onesDigitResult;
        //    if (numbersTailCutCount > 0 && numbersTailCutCount < onesDigitNumbers.Count)
        //    {
        //        var numbers = onesDigitNumbers.Skip(0).Take(onesDigitNumbers.Count - numbersTailCutCount).ToList();
        //        onesDigitResult = FactorAnalysis.AnalyseConsecutives(numbers, onesDigitFactors, allowMinTimes);
        //    }
        //    else
        //    {
        //        onesDigitResult = FactorAnalysis.AnalyseConsecutives(onesDigitNumbers, onesDigitFactors, allowMinTimes);
        //    }
        //    onesDigitResult = onesDigitResult.Where(t => t.HistoricalConsecutiveTimes.Count > 0).ToList();
        //    foreach (var item in onesDigitResult)
        //    {
        //        var times = 0;
        //        for (var i = onesDigitNumbers.Count - 1; i >= 0; i--)
        //        {
        //            if (!item.Factor.Contains(onesDigitNumbers[i]))
        //                break;
        //            times++;
        //        }
        //        item.FactorCurrentConsecutiveTimes = times;
        //    }
        //    //先按最大连续次数然后按最小间隔次数排序
        //    onesDigitResult = onesDigitResult
        //        .OrderByDescending(t => t.FactorCurrentConsecutiveTimes)
        //        .ThenBy(t => t.Interval).ToList();

        //    return onesDigitResult;
        //}


        ///// <summary>
        ///// 分析列表十位数
        ///// </summary>
        ///// <param name="numbers">记录集合</param>
        ///// <param name="tensDigitFactors">比较因子</param>
        ///// <param name="allowMinTimes">允许的最小连续次数，大于等于此数才记录</param>
        ///// <returns></returns>
        //public List<FactorTrendAnalyseResult<byte>> AnalyseTensDigit(List<byte> tensDigitNumbers, List<Factor<byte>> tensDigitFactors, int allowMinTimes, int numbersTailCutCount)
        //{
        //    List<FactorTrendAnalyseResult<byte>> tensDigitResult;
        //    if (numbersTailCutCount > 0 && tensDigitNumbers.Count > 0)
        //    {
        //        var numbers = tensDigitNumbers.Skip(0).Take(tensDigitNumbers.Count - numbersTailCutCount).ToList();
        //        tensDigitResult = FactorAnalysis.AnalyseConsecutives(numbers, tensDigitFactors, allowMinTimes);
        //    }
        //    else
        //    {
        //        tensDigitResult = FactorAnalysis.AnalyseConsecutives(tensDigitNumbers, tensDigitFactors, allowMinTimes);
        //    }
        //    tensDigitResult = tensDigitResult.Where(t => t.HistoricalConsecutiveTimes.Count > 0).ToList();
        //    foreach (var item in tensDigitResult)
        //    {
        //        var times = 0;
        //        for (var i = tensDigitNumbers.Count - 1; i >= 0; i--)
        //        {
        //            if (!item.Factor.Contains(tensDigitNumbers[i]))
        //                break;
        //            times++;
        //        }
        //        item.FactorCurrentConsecutiveTimes = times;
        //    }

        //    //先按最大连续次数然后按最小间隔次数排序
        //    tensDigitResult = tensDigitResult
        //        .OrderByDescending(t => t.FactorCurrentConsecutiveTimes)
        //        .ThenBy(t => t.Interval).ToList();

        //    return tensDigitResult;
        //}


        ///// <summary>
        ///// 分析合数
        ///// </summary>
        ///// <param name="compositeNumbers"></param>
        ///// <param name="factors"></param>
        ///// <param name="allowMinTimes"></param>
        ///// <param name="numbersTailCutCount"></param>
        ///// <returns></returns>
        //public List<FactorTrendAnalyseResult<byte>> AnalyseCompositeNumber(List<byte> compositeNumbers, List<Factor<byte>> factors, int allowMinTimes, int numbersTailCutCount)
        //{
        //    List<FactorTrendAnalyseResult<byte>> results;
        //    if (numbersTailCutCount > 0 && compositeNumbers.Count > 0)
        //    {
        //        var numbers = compositeNumbers.Skip(0).Take(compositeNumbers.Count - numbersTailCutCount).ToList();
        //        results = FactorAnalysis.AnalyseConsecutives(numbers, factors, allowMinTimes);
        //    }
        //    else
        //    {
        //        results = FactorAnalysis.AnalyseConsecutives(compositeNumbers, factors, allowMinTimes);
        //    }
        //    results = results.Where(t => t.HistoricalConsecutiveTimes.Count > 0).ToList();
        //    foreach (var item in results)
        //    {
        //        var times = 0;
        //        for (var i = compositeNumbers.Count - 1; i >= 0; i--)
        //        {
        //            if (!item.Factor.Contains(compositeNumbers[i]))
        //                break;
        //            times++;
        //        }
        //        item.FactorCurrentConsecutiveTimes = times;
        //    }

        //    //先按最大连续次数然后按最小间隔次数排序
        //    results = results
        //        .OrderByDescending(t => t.FactorCurrentConsecutiveTimes)
        //        .ThenBy(t => t.Interval).ToList();

        //    return results;
        //}

        ///// <summary>
        ///// 分析列表十位数(前后几期一起分析)
        ///// </summary>
        ///// <param name="numbers">记录集合</param>
        ///// <param name="tensDigitFactors">比较因子</param>
        ///// <param name="around">后面连续期次</param>
        ///// <param name="allowMinTimes">允许的最小连续次数，大于等于此数才记录</param>
        ///// <returns></returns>
        //public List<FactorTrendAnalyseResult<byte>> AnalyseTensDigitAround(List<byte> tensDigitNumbers, List<Factor<byte>> tensDigitFactors, int around, int allowMinTimes, int numbersTailCutCount)
        //{
        //    /*
        //     十位数相加组合
        //     0+0=0，0+1=1，0+2=2，0+3=3，0+4=4
        //     1+0=1，1+1=2，1+2=3，1+3=4，1+4=0
        //     2+0=2，2+1=3，2+2=4，2+3=0，2+4=1
        //     3+0=3，3+1=4，3+2=0，3+3=1，3+4=2
        //     4+0=4，4+1=0，4+2=1，4+3=2，4+4=3
        //     */
        //    //用于分析历史记录的比较因子的委托方法,参数为历史记录列表，因子列表和当前索引，返回结果为bool
        //    Func<IReadOnlyList<byte>, List<byte>, int, bool> compareFunc = (tenNumbers, factor, index) =>
        //     {
        //         var length = tenNumbers.Count;
        //         if (index > length - around)
        //         {
        //             return false;
        //         }
        //         var currentSum = 0;
        //         for (var i = 0; i < around; i++)
        //         {
        //             currentSum += tenNumbers[index + i];
        //         }
        //         //取5的模
        //         var currentItem = (byte)(currentSum % 5);
        //         var exists = factor.Exists(m => m.Equals(currentItem));
        //         return exists;
        //     };
        //    //用于预测当前期次的比较因子的委托方法,参数为历史记录列表，因子列表和当前索引，返回结果为bool
        //    Func<IReadOnlyList<byte>, List<byte>, int, bool> curTimesCompareFunc = (tenNumbers, factor, index) =>
        //   {
        //       var currentSum = 0;
        //       for (var i = 0; i < around; i++)
        //       {
        //           currentSum += tenNumbers[index - i];
        //       }
        //       //取5的模
        //       var sum = (byte)(currentSum % 5);
        //       return factor.Contains(sum);
        //   };
        //    //分析结果
        //    List<FactorTrendAnalyseResult<byte>> tensDigitResult;
        //    if (numbersTailCutCount > 0 && tensDigitNumbers.Count > 0)
        //    {
        //        var numbers = tensDigitNumbers.Skip(0).Take(tensDigitNumbers.Count - numbersTailCutCount).ToList();
        //        tensDigitResult = FactorAnalysis.Consecutives(numbers, tensDigitFactors, compareFunc, allowMinTimes);

        //    }
        //    else
        //    {
        //        tensDigitResult = FactorAnalysis.Consecutives(tensDigitNumbers, tensDigitFactors, compareFunc, allowMinTimes);
        //    }
        //    tensDigitResult = tensDigitResult.Where(t => t.HistoricalConsecutiveTimes.Count > 0).ToList();
        //    foreach (var item in tensDigitResult)
        //    {
        //        var times = 0;
        //        for (var i = tensDigitNumbers.Count - 1; i >= 0; i--)
        //        {
        //            if (!curTimesCompareFunc(tensDigitNumbers, item.Factor, i))
        //                break;
        //            times++;
        //        }
        //        item.FactorCurrentConsecutiveTimes = times;
        //    }
        //    tensDigitResult = tensDigitResult
        //        .OrderByDescending(t => t.FactorCurrentConsecutiveTimes)
        //        .ThenBy(t => t.Interval).ToList();

        //    return tensDigitResult;
        //}


        /// <summary>
        /// 分析指定位置当前期之前的号码
        /// </summary>
        /// <param name="location">指定位置</param>
        /// <param name="times">期次</param>
        /// <param name="beforeCount">之前多少期</param>
        /// <returns></returns>
        public List<AnalysisBeforeResult> AnalyseBeforeSpecifiedLocation(int location, string times, int beforeCount)
        {
            return null;
        }
    }
}
