using System.Collections.Generic;
using TrendAnalysis.Models;
using System.Linq;
using TrendAnalysis.Data;
using System;

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
        public List<byte> AnalyseSpecifiedLocation(int location, string times)
        {
            using (var dao = new TrendDbContext())
            {
                var source = dao.Set<MarkSixRecord>().AsQueryable();
                if (!string.IsNullOrWhiteSpace(times))
                {
                    var record = dao.Set<MarkSixRecord>().FirstOrDefault(m => m.Times == times);
                    if (record == null)
                        throw new Exception("错误，指定期次的记录不存在！");
                    source = source.Where(m => m.TimesValue < record.TimesValue);
                }

                source = source.OrderBy(m => m.Times);
                var numbers = new List<byte>();
                switch (location)
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
                //按数字位置分析（十位/个位）
                //十位
                var tensDigitResult = AnalyseTensDigit(numbers);

                var tensDigitResult2 = AnalyseTensDigitAround(numbers, (n, factor, index) =>
                {
                    var length = n.Count;
                    if (index >= length - 1)
                    {
                        return false;
                    }
                    var currentItem = (byte)((n[index] + n[index + 1]) % 4);
                    var exists = factor.Exists(m => m.Equals(currentItem));
                    return exists;
                }, (n, factor, index) =>
                {
                    var sum = (byte)((n[index] + n[index - 1]) % 4);
                    return factor.Contains(sum);
                });
                var tensDigitResult3 = AnalyseTensDigitAround(numbers, (n, factor, index) =>
                {
                    var length = n.Count;
                    if (index >= length - 2)
                    {
                        return false;
                    }
                    var currentItem = (byte)((n[index] + n[index + 1] + n[index + 2]) % 4);
                    var exists = factor.Exists(m => m.Equals(currentItem));
                    return exists;
                }, (n, factor, index) =>
                {
                    var sum = (byte)((n[index] + n[index - 1] + n[index - 2]) % 4);
                    return factor.Contains(sum);
                });
                var tensDigitResult4 = AnalyseTensDigitAround(numbers, (n, factor, index) =>
                {
                    var length = n.Count;
                    if (index >= length - 3)
                    {
                        return false;
                    }
                    var currentItem = (byte)((n[index] + n[index + 1] + n[index + 2] + n[index + 3]) % 4);
                    var exists = factor.Exists(m => m.Equals(currentItem));
                    return exists;
                }, (n, factor, index) =>
                {
                    var sum = (byte)((n[index] + n[index - 1] + n[index - 2] + n[index - 3]) % 4);
                    return factor.Contains(sum);
                });
                //个位
                var onesDigitResult = AnalyseOnesDigit(numbers, 4);

                ////分析数字本身
                //var digitResult = AnalyseByDigit(numbers, 5);

                //var max = onesDigitResult.Select(r => r.ConsecutiveTimes.Max(m => m.Key)).OrderByDescending(m => m);

                //digitResult = digitResult.OrderBy(d => d.Interval).ThenByDescending(d=>d.SpecifiedTimesConsecutiveTimes).ToList();

                //var r1 = digitResult.Where(d => d.Interval<0).ToList();
                //var r2 = digitResult.Where(d => d.OppositeFactor.Count <40).ToList();
                //tensDigitResult[0].Factor;
                return null;
            }
        }

        /// <summary>
        /// 分析列表个位数
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="nodes"></param>
        /// <param name="allowConsecutiveMinTimes">允许记录的从指定期次倒序在因子中的最小连续次数</param>
        /// <returns></returns>
        public List<Results<string>> AnalyseOnesDigit(List<byte> numbers, int allowConsecutiveMinTimes = 1, int allowMinTimes = 1)
        {
            var onesDigitFactors = NumberCombination.CreateBinaryCombinations(new List<byte>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Select(n => n.ToString()).ToList());

            //个位数号码列表
            var onesDigitNumbers = numbers.Select(n => n.ToString("00").Substring(1)).ToList();
            var onesDigitResult = FactorAnalysis.Consecutives(onesDigitNumbers, onesDigitFactors, allowMinTimes);
            onesDigitResult = onesDigitResult.Where(t => t.ConsecutiveTimes.Count > 0).ToList();
            foreach (var item in onesDigitResult)
            {
                var times = 0;
                for (var i = onesDigitNumbers.Count - 1; i >= 0; i--)
                {
                    if (!item.Factor.Contains(onesDigitNumbers[i]))
                        break;
                    times++;
                }
                item.SpecifiedTimesConsecutiveTimes = times;
            }
            onesDigitResult = onesDigitResult.Where(t => t.SpecifiedTimesConsecutiveTimes >= allowConsecutiveMinTimes).ToList();
            //先按间隔数升序再按最大连续数降序排列
            //onesDigitResult = onesDigitResult.OrderBy(r => r.Interval).OrderByDescending(r => r.ConsecutiveTimes.Max(k => k.Key)).ToList();
            onesDigitResult = onesDigitResult.OrderBy(r => r.Interval).ToList();
            return onesDigitResult;
        }


        /// <summary>
        /// 分析列表十位数
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public List<Results<string>> AnalyseTensDigit(List<byte> numbers, int allowMinTimes = 1)
        {
            //十位因子
            var tensDigitFactors = NumberCombination.CreateBinaryCombinations(new List<byte>() { 0, 1, 2, 3, 4 }.Select(n => n.ToString()).ToList());

            //十位数号码列表
            var tensDigitNumbers = numbers.Select(n => n.ToString("00").Substring(0, 1)).ToList();
            var tensDigitResult = FactorAnalysis.Consecutives(tensDigitNumbers, tensDigitFactors, allowMinTimes);
            tensDigitResult = tensDigitResult.Where(t => t.ConsecutiveTimes.Count > 0).ToList();
            foreach (var item in tensDigitResult)
            {
                var times = 0;
                for (var i = tensDigitNumbers.Count - 1; i >= 0; i--)
                {
                    if (!item.Factor.Contains(tensDigitNumbers[i]))
                        break;
                    times++;
                }
                item.SpecifiedTimesConsecutiveTimes = times;
            }
            tensDigitResult = tensDigitResult.Where(t => t.SpecifiedTimesConsecutiveTimes > 0).ToList();
            //先按间隔数升序再按最大连续数降序排列
            tensDigitResult = tensDigitResult.OrderBy(r => r.Interval).OrderByDescending(r => r.ConsecutiveTimes.Max(k => k.Key)).ToList();
            return tensDigitResult;
        }
        /// <summary>
        /// 分析列表十位数(前后几期一起分析)
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="compareFunc">用于分析历史记录的比较因子的委托方法,参数为历史记录列表，因子列表和当前索引，返回结果为bool</param>
        /// <param name="curTimesCompareFunc">用于预测当前期次的比较因子的委托方法,参数为历史记录列表，因子列表和当前索引，返回结果为bool</param>
        /// <param name="around"></param>
        /// <param name="allowMinTimes"></param>
        /// <returns></returns>
        public List<Results<byte>> AnalyseTensDigitAround(IReadOnlyList<byte> numbers, Func<IReadOnlyList<byte>, List<byte>, int, bool> compareFunc, Func<IReadOnlyList<byte>, List<byte>, int, bool> curTimesCompareFunc, int around = 1, int allowMinTimes = 1)
        {
            //十位数号码列表
            var tensDigitNumbers = numbers.Select(n => n.ToString("00").Substring(0, 1)).Select(n => byte.Parse(n)).ToList();

            //十位因子
            var tensDigitFactors = NumberCombination.CreateBinaryCombinations(new List<byte>() { 0, 1, 2, 3, 4 }).ToList();

            //分析结果
            var tensDigitResult = FactorAnalysis.Consecutives(tensDigitNumbers, tensDigitFactors, compareFunc, allowMinTimes);
            tensDigitResult = tensDigitResult.Where(t => t.ConsecutiveTimes.Count > 0).ToList();
            foreach (var item in tensDigitResult)
            {
                var times = 0;
                for (var i = tensDigitNumbers.Count - 1; i >= 0; i--)
                {
                    if (!curTimesCompareFunc(tensDigitNumbers, item.Factor, i))
                        break;

                    //if (!item.Factor.Contains(tensDigitNumbers[i]))
                    //    break;
                    times++;
                }
                item.SpecifiedTimesConsecutiveTimes = times;
            }
            tensDigitResult = tensDigitResult.Where(t => t.SpecifiedTimesConsecutiveTimes > 0).ToList();
            //先按间隔数升序再按最大连续数降序排列
            tensDigitResult = tensDigitResult.OrderBy(r => r.Interval).OrderByDescending(r => r.ConsecutiveTimes.Max(k => k.Key)).ToList();
            return tensDigitResult;
        }

        //around

        ///// <summary>
        ///// 分析号码
        ///// </summary>
        ///// <param name="numbers"></param>
        ///// <param name="nodes"></param>
        ///// <returns></returns>
        //public List<Results<byte>> AnalyseByDigit(List<byte> numbers, int allowMinTimes = 1)
        //{
        //    //生成因子组合
        //    var arr = new List<byte>();
        //    for (byte i = 1; i <= 49; i++)
        //    {
        //        arr.Add(i);
        //    }
        //    //组合节点
        //    var nodes = NumberCombination.CreateBinaryCombinations(arr, 2, true);
        //    //返回数字结果
        //    var digitResult = FactorAnalysis.Consecutives(numbers, nodes, allowMinTimes);
        //    digitResult = digitResult.Where(t => t.ConsecutiveTimes.Count > 0).ToList();
        //    foreach (var item in digitResult)
        //    {
        //        var times = 0;
        //        for (var i = numbers.Count - 1; i >= 0; i--)
        //        {
        //            if (!item.Factor.Contains(numbers[i]))
        //                break;
        //            times++;
        //        }
        //        item.SpecifiedTimesConsecutiveTimes = times;
        //    }
        //    digitResult = digitResult.Where(t => t.SpecifiedTimesConsecutiveTimes > 0).ToList();
        //    //先按间隔数升序再按最大连续数降序排列
        //    digitResult = digitResult.OrderBy(r => r.Interval).OrderByDescending(r => r.ConsecutiveTimes.Max(k => k.Key)).ToList();
        //    return digitResult;
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
