using System;
using System.Collections.Generic;
using System.Linq;
using TrendAnalysis.DataTransferObject.Trend;


namespace TrendAnalysis.Service.Trend
{

    /// <summary>
    ///     单个因子趋势分析
    /// </summary>
    public class FactorTrend
    {

        /// <summary>
        ///     非连续指示
        /// </summary>
        public const int DisConsecutiveFlag = int.MaxValue;


        /// <summary>
        ///     统计连续次数允许的最小次数
        /// </summary>
        public const int AllowMinTimes = 1;


        public List<T> Analyse<T>(FactorTrendAnalyseDto<T> dto)
        {
            //分析历史趋势,排除最后一位号码，（最后一位号码分析当前要分析的可能号码）
            var historicalNumbers = dto.Numbers.Take(dto.Numbers.Count - 1).ToList();

            //分析每个因子
            var factor = dto.Factor;

            //统计每个因子在记录中的趋势
            var trendResult = CountConsecutiveDistribution(dto.Numbers, factor.Left, factor.Right);

            //行明细结果集
            var rowDetailses = trendResult.FactorDistributions;
            if (rowDetailses == null || rowDetailses.Count == 0) return null;
            var lastIndexResult = rowDetailses[rowDetailses.Count - 1];

            //因子不包含最后一个号码，（连续次数为0）
            if (lastIndexResult.ConsecutiveTimes == 0)
                return null;

            var historicalTrends = GetCorrectRates(historicalNumbers, trendResult, dto.AnalyseHistoricalTrendEndIndex, factor.Right);

            //筛选正确100%的历史趋势，如没有不记录
            //historicalTrends = historicalTrends.Where(h => h.CorrectRate == 1).OrderBy(h => h.AllowInterval).ThenByDescending(h => h.AllowConsecutiveTimes).ToList();
            historicalTrends = historicalTrends.Where(h => h.CorrectRate == 1).OrderByDescending(h => h.AllowConsecutiveTimes).ThenBy(h => h.AllowInterval).ToList();
            if (historicalTrends.Count == 0) return null;

            var firstHistoricalTrend = historicalTrends.FirstOrDefault();
            if (firstHistoricalTrend == null )
                return null;

            //可以考虑加大连续次数和间隔数
            if (lastIndexResult.ConsecutiveTimes >= firstHistoricalTrend.AllowConsecutiveTimes + dto.AddConsecutiveTimes && lastIndexResult.MaxConsecutiveTimesInterval <= firstHistoricalTrend.AllowInterval - dto.AddInterval)
            {
                //返回的可能因子
                return factor.Right;
            }
            return null;
        }

        public List<T>Analyse1<T>(FactorTrendAnalyseDto<T> dto)
        {
            //分析历史趋势,排除最后一位号码，（最后一位号码分析当前要分析的可能号码）
            var historicalNumbers = dto.Numbers.Take(dto.Numbers.Count - 1).ToList();

            //分析每个因子
            var factor = dto.Factor;

            //统计因子在记录中的趋势
            var trendResult = CountConsecutiveDistribution(dto.Numbers, factor.Left, factor.Right);

            //行明细结果集
            var rowDetailses = trendResult.FactorDistributions;
            if (rowDetailses == null || rowDetailses.Count == 0) return null;
            var lastIndexResult = rowDetailses[rowDetailses.Count - 1];

            //因子不包含最后一个号码，（连续次数为0）
            if (lastIndexResult.ConsecutiveTimes == 0)
                return null;

            //大于等于指定的连续次和小于等于指定的间隔数
            if (lastIndexResult.ConsecutiveTimes >= dto.AddConsecutiveTimes && lastIndexResult.MaxConsecutiveTimesInterval <= dto.AddInterval)
            {
                //返回的可能因子
                return factor.Right;
            }
            return null;
        }

        /// <summary>
        ///     分析因子的历史正确率分布等，（通过号码集合分析历史趋势）
        /// </summary>
        /// <param name="numbers">号码集合</param>
        /// <param name="trendResult">统计结果</param>
        /// <param name="endIndex">分析记录的最后索引位置</param>
        /// <param name="predictiveFactor">可能的因子</param>
        /// <returns></returns>
        public List<FactorTrendCorrectRate> GetCorrectRates<T>(List<T> numbers, FactorTrendConsecutiveDetails<T> trendResult, int endIndex, List<T> predictiveFactor)
        {

            var trends = new List<FactorTrendCorrectRate>();

            if (endIndex <= 0)
                throw new Exception("分析历史趋势时，分析记录数量不能小于等于0！");

            if (numbers == null || numbers.Count == 0)
                throw new Exception("分析历史趋势时，记录不能为空！");

            var numberCount = numbers.Count;

            if (numberCount < endIndex)
                throw new Exception("分析历史趋势时，分析记录数量不能大于记录数量！");

            var minConsecutiveTimes = trendResult.FactorDistributions.Where(n => n.ConsecutiveTimes != 0).Min(n => n.ConsecutiveTimes);
            var maxConsecutiveTimes = trendResult.FactorDistributions.Where(n => n.ConsecutiveTimes != 0).Max(n => n.ConsecutiveTimes);

            var minInterval = trendResult.FactorDistributions.Where(n => n.MaxConsecutiveTimesInterval != DisConsecutiveFlag).Min(n => n.MaxConsecutiveTimesInterval);
            var maxInterval = trendResult.FactorDistributions.Where(n => n.MaxConsecutiveTimesInterval != DisConsecutiveFlag).Max(n => n.MaxConsecutiveTimesInterval);

            //允许的连续次数，由小到大
            for (var consecutiveTimes = minConsecutiveTimes; consecutiveTimes <= maxConsecutiveTimes; consecutiveTimes++)
            {
                //允许的间隔数，由大到小
                for (var interval = maxInterval; interval >= minInterval; interval--)
                {
                    var resultCount = 0;
                    var successCount = 0;

                    var trend = new FactorTrendCorrectRate
                    {
                        AllowConsecutiveTimes = consecutiveTimes,
                        AllowInterval = interval,
                        AnalyseNumberCount = endIndex
                    };
                    trends.Add(trend);

                    //行明细结果集
                    var distributions = trendResult.FactorDistributions;
                    for (var i = numberCount - 1; i >= endIndex; i--)
                    {
                        var number = numbers[i];

                        //上一索引位置的分析结果,10个号码，分析第10位（索引位置9），取第9位（索引位置8）
                        var distribution = distributions[i - 1];

                        //对结果再分析
                        //1、按允许的最小因子当前连续次数和允许的最大间隔次数筛选
                        //2、先按最大连续次数然后按最小间隔次数排序

                        ////历史最大连续次数
                        //var historicalMaxConsecutiveTimes = curIndexResult.ConsecutiveTimes + interval;
                        if (distribution.ConsecutiveTimes == consecutiveTimes && distribution.MaxConsecutiveTimesInterval == interval)
                        {
                            resultCount++;

                            if (predictiveFactor.Contains(number))
                            {
                                successCount++;
                            }
                        }
                    }
                    trend.AnalyticalCount = resultCount;
                    trend.CorrectCount = successCount;
                    trend.CorrectRate = trend.AnalyticalCount == 0 ? 0 : (double)trend.CorrectCount / trend.AnalyticalCount;
                }
            }
            return trends;
        }


        /// <summary>
        ///     统计因子在记录中的连续次数等分布情况
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numbers">记录</param>
        /// <param name="factor">判断因子</param>
        /// <param name="predictiveFactor">反因子</param>
        /// <returns></returns>
        public static FactorTrendConsecutiveDetails<T> CountConsecutiveDistribution<T>(IReadOnlyList<T> numbers, List<T> factor, List<T> predictiveFactor)
        {
            var curResult = new FactorTrendConsecutiveDetails<T>
            {
                Factor = factor,
                PredictiveFactor = predictiveFactor,
                ConsecutiveDistributions = new SortedDictionary<int, int>(),
                FactorDistributions = new List<FactorDistribution>()
            };
            var i = 0;

            //最大连续次数
            var maxConsecutiveTimes = 0;

            //连续次数
            var times = 0;
            var length = numbers.Count;
            while (i < length)
            {
                if (factor.Contains(numbers[i]))
                {
                    times++;

                    //因子连续，最大连续次数－当前连续次数
                    curResult.FactorDistributions.Add(
                        new FactorDistribution
                        {
                            Index = i,
                            MaxConsecutiveTimesInterval = maxConsecutiveTimes - times,
                            ConsecutiveTimes = times
                        });
                }
                else
                {
                    if (curResult.ConsecutiveDistributions.ContainsKey(times))
                    {
                        curResult.ConsecutiveDistributions[times]++;
                    }
                    else if (times >= AllowMinTimes)
                    {
                        curResult.ConsecutiveDistributions.Add(times, 1);
                    }
                    if (times > maxConsecutiveTimes)
                        maxConsecutiveTimes = times;
                    times = 0;

                    //因子不连续
                    curResult.FactorDistributions.Add(
                        new FactorDistribution
                        {
                            Index = i,
                            MaxConsecutiveTimesInterval = DisConsecutiveFlag,
                            ConsecutiveTimes = times
                        });
                }
                i++;
            }
            if (curResult.ConsecutiveDistributions.ContainsKey(times))
            {
                curResult.ConsecutiveDistributions[times]++;
            }
            else if (times >= AllowMinTimes)
            {
                curResult.ConsecutiveDistributions.Add(times, 1);
            }
            return curResult;
        }

    }

}