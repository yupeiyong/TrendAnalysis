using System;
using System.Collections.Generic;
using System.Linq;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.Models;

namespace TrendAnalysis.Service
{
    /// <summary>
    /// 历史趋势分析
    /// </summary>
    public class HistoricalTrendAnalysis
    {
        /// <summary>
        /// 分析列表
        /// </summary>
        /// <param name="numbers">记录集合</param>
        /// <param name="tensDigitFactors">比较因子</param>
        /// <param name="allowMinTimes">允许的最小连续次数，大于等于此数才记录</param>
        /// <returns></returns>
        public List<FactorResults<T>> AnalyseNumbers<T>(AnalyseNumbersDto<T> dto)
        {
            List<FactorResults<T>> factorResults;
            if (dto.NumbersTailCutCount > 0 && dto.Numbers.Count > 0)
            {
                var nums = dto.Numbers.Skip(0).Take(dto.Numbers.Count - dto.NumbersTailCutCount).ToList();
                factorResults = FactorAnalysis.AnalyseConsecutives(nums, dto.Factors, dto.AllowMinTimes);
            }
            else
            {
                factorResults = FactorAnalysis.AnalyseConsecutives(dto.Numbers, dto.Factors, dto.AllowMinTimes);
            }
            factorResults = factorResults.Where(t => t.HistoricalConsecutiveTimes.Count > 0).ToList();
            foreach (var item in factorResults)
            {
                var times = 0;
                //记录集合倒序检查，因子是否包含当前号码
                for (var i = dto.Numbers.Count - 1; i >= 0; i--)
                {
                    if (!item.Factor.Contains(dto.Numbers[i]))
                        break;
                    times++;
                }
                //记录因子当前连续次数
                item.FactorCurrentConsecutiveTimes = times;
            }

            //1、按允许的最小因子当前连续次数和允许的最大间隔次数筛选
            //2、先按最大连续次数然后按最小间隔次数排序
            factorResults = factorResults
                .Where(m => m.FactorCurrentConsecutiveTimes >= dto.AllowMinFactorCurrentConsecutiveTimes && m.Interval <= dto.AllowMaxInterval)
                .OrderByDescending(t => t.FactorCurrentConsecutiveTimes)
                .ThenBy(t => t.Interval).ToList();

            return factorResults;
        }


        /// <summary>
        /// 通过号码集合分析历史趋势
        /// </summary>
        /// <param name="numbers">记录集合</param>
        /// <param name="tensDigitFactors">比较因子</param>
        /// <param name="allowMinTimes">允许的最小连续次数，大于等于此数才记录</param>
        /// <returns></returns>
        public List<HistoricalTrend<T>> AnalyseHistoricalTrend<T>(AnalyseHistoricalTrendDto<T> dto)
        {
            var trends = new List<HistoricalTrend<T>>();

            if (dto.Numbers.Count < dto.AnalyseNumberCount)
                throw new Exception("分析历史趋势时，分析记录数量不能大小记录数量！");

            var analyseNumbers = dto.Numbers.OrderByDescending(n => n.TimesValue).Skip(0).Take(dto.AnalyseNumberCount).ToList();
            //允许的连续次数，由小到大
            for (var consecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes; consecutiveTimes < dto.EndAllowMinFactorCurrentConsecutiveTimes; consecutiveTimes++)
            {
                //允许的间隔数，由大到小
                for (var interval = dto.StartAllowMaxInterval; interval > dto.EndAllowMaxInterval; interval--)
                {
                    var resultCount = 0;
                    var successCount = 0;

                    var trend = new HistoricalTrend<T> { Items = new List<HistoricalTrendItem<T>>(), AllowConsecutiveTimes = consecutiveTimes, AllowInterval = interval };
                    for (int i = 0, maxCount = analyseNumbers.Count; i < maxCount; i++)
                    {
                        var number = analyseNumbers[i].Number;
                        var times = analyseNumbers[i].Times;
                        var timesValue = analyseNumbers[i].TimesValue;
                        var numbers = dto.Numbers.Where(n => n.TimesValue < timesValue).Select(n => n.Number).ToList();

                        var factorResults = AnalyseNumbers(new AnalyseNumbersDto<T>
                        {
                            Numbers = numbers,
                            Factors = dto.Factors,
                            AllowMinTimes = dto.AllowMinTimes,
                            NumbersTailCutCount = dto.NumbersTailCutCount,
                            AllowMinFactorCurrentConsecutiveTimes = consecutiveTimes,
                            AllowMaxInterval = interval
                        });

                        //结果是否正确
                        var success = false;

                        //对结果再分析
                        var factorResult = factorResults.OrderByDescending(t => t.FactorCurrentConsecutiveTimes).FirstOrDefault();
                        var factors = factorResult.OppositeFactor;
                        var resultConsecutiveTimes = 0;
                        var resultInterval = 0;
                        if (factorResult != null)
                        {
                            resultConsecutiveTimes = factorResult.FactorCurrentConsecutiveTimes;
                            resultInterval = factorResult.Interval;
                            if (factorResult.OppositeFactor != null && factorResult.OppositeFactor.Count > 0)
                            {
                                resultCount++;

                                if (factors.Contains(number))
                                {
                                    successCount++;
                                    success = true;
                                }
                            }
                        }

                        var trendItem = new HistoricalTrendItem<T> { Times = times, Number = number, Success = success, ResultConsecutiveTimes = resultConsecutiveTimes, ResultInterval = resultInterval, OppositeFactor = factors };

                        trend.AnalyticalCount = resultCount;
                        trend.CorrectCount = successCount;
                        trend.Items.Add(trendItem);
                    }
                }
            }
            return trends;
        }

    }
}
