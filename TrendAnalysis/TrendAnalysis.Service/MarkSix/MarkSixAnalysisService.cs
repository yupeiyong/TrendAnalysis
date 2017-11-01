using System.Collections.Generic;
using TrendAnalysis.Models;
using System.Linq;
using TrendAnalysis.Data;
using System;
using TrendAnalysis.DataTransferObject;

namespace TrendAnalysis.Service
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

                var historicalTrendAnalysis = new HistoricalTrendAnalysis();
                //十位数号码列表
                var tensDigitNumbers = numbers.Select(n => n.ToString("00").Substring(0, 1)).Select(n => byte.Parse(n)).ToList();
                //十位因子
                var tensDigitFactors = NumberCombination.CreateBinaryCombinations(new List<byte>() { 0, 1, 2, 3, 4 }.ToList());

                //按数字位置分析（十位/个位）
                //十位
                var tensDigitResult = historicalTrendAnalysis.AnalyseNumbers(new AnalyseNumbersDto<byte> {
                    Numbers =tensDigitNumbers,
                    Factors =tensDigitFactors,
                    AllowMinTimes =dto.TensAllowMinTimes,
                    NumbersTailCutCount =dto.TensNumbersTailCutCount,
                    AllowMinFactorCurrentConsecutiveTimes=dto.TensAllowMinFactorCurrentConsecutiveTimes,
                    AllowMaxInterval=dto.TensAllowMaxInterval
                });

                //个位数号码列表
                var onesDigitNumbers = numbers.Select(n => n.ToString("00").Substring(1)).Select(n => byte.Parse(n)).ToList();
                //个位因子
                var onesDigitFactors = NumberCombination.CreateBinaryCombinations(new List<byte>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.ToList());

                //个位
                var onesDigitResult = historicalTrendAnalysis.AnalyseNumbers(new AnalyseNumbersDto<byte>
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
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)),Times=m.Times,TimesValue=m.TimesValue} ).ToList();
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

                var historicalTrendAnalysis = new HistoricalTrendAnalysis();

                //个位因子
                var onesDigitFactors = NumberCombination.CreateBinaryCombinations(new List<byte>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.ToList());

                var trendDto = new AnalyseHistoricalTrendDto<byte> {
                    Numbers =records,Factors=onesDigitFactors,
                    Location =dto.Location,
                    AnalyseNumberCount=dto.AnalyseNumberCount,
                    StartAllowMaxInterval=dto.StartAllowMaxInterval,
                    EndAllowMaxInterval=dto.EndAllowMaxInterval,
                    StartAllowMinFactorCurrentConsecutiveTimes=dto.StartAllowMinFactorCurrentConsecutiveTimes,
                    EndAllowMinFactorCurrentConsecutiveTimes=dto.EndAllowMinFactorCurrentConsecutiveTimes,
                    AllowMinTimes=dto.AllowMinTimes,
                    NumbersTailCutCount=dto.NumbersTailCutCount
                };
                var historicalTrends= historicalTrendAnalysis.AnalyseHistoricalTrend(trendDto);

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
        //public List<FactorResults<byte>> AnalyseOnesDigit(List<byte> onesDigitNumbers, List<BinaryNode<byte>> onesDigitFactors, int allowMinTimes, int numbersTailCutCount)
        //{
        //    List<FactorResults<byte>> onesDigitResult;
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
        //public List<FactorResults<byte>> AnalyseTensDigit(List<byte> tensDigitNumbers, List<BinaryNode<byte>> tensDigitFactors, int allowMinTimes, int numbersTailCutCount)
        //{
        //    List<FactorResults<byte>> tensDigitResult;
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
        //public List<FactorResults<byte>> AnalyseCompositeNumber(List<byte> compositeNumbers, List<BinaryNode<byte>> factors, int allowMinTimes, int numbersTailCutCount)
        //{
        //    List<FactorResults<byte>> results;
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
        //public List<FactorResults<byte>> AnalyseTensDigitAround(List<byte> tensDigitNumbers, List<BinaryNode<byte>> tensDigitFactors, int around, int allowMinTimes, int numbersTailCutCount)
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
        //    List<FactorResults<byte>> tensDigitResult;
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
