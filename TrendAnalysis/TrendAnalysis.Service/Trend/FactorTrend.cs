using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.DataTransferObject.Trend;
using TrendAnalysis.Models.Trend;


namespace TrendAnalysis.Service.Trend
{

    /// <summary>
    ///     因子的历史趋势
    /// </summary>
    public class FactorTrend
    {
        /// <summary>
        /// 非连续指示
        /// </summary>
        public const int DiscontinuousFlag = int.MaxValue;


        /// <summary>
        /// 统计连续次数允许的最小次数
        /// </summary>
        public const int AllowMinTimes = 1;

        public List<Factor<T>> Analyse<T>(FactorsTrendAnalyseDto<T> dto)
        {
            //预测的可能因子
            var predictiveFactors = new List<Factor<T>>();
            //分析每个因子
            foreach (var factor in dto.Factors)
            {
                //统计每个因子在记录中的趋势
                var trendResult = CountFactorConsecutiveTimes(dto.Numbers, factor.Left, factor.Right);
                //行明细结果集
                var rowDetailses = trendResult.RowDetailses;
                if (rowDetailses == null || rowDetailses.Count == 0) continue;
                var lastIndexResult = rowDetailses[rowDetailses.Count - 1];
                //因子不包含最后一个号码，（连续次数为0）
                if (lastIndexResult.ConsecutiveTimes == 0)
                    continue;
                //分析历史趋势,排除最后一位号码
                var historicalTrends = AnalyseFactorHistoricalTrend<T>(dto.Numbers.Take(dto.Numbers.Count - 1).ToList(), trendResult, dto.AnalyseHistoricalTrendCount, factor.Right);

                //筛选正确100%的历史趋势，如没有不记录
                historicalTrends = historicalTrends.Where(h => h.CorrectRate == 1).OrderBy(h => h.AllowInterval).ThenByDescending(h => h.AllowConsecutiveTimes).ToList();
                if (historicalTrends.Count == 0) continue;

                var firstHistoricalTrend = historicalTrends.FirstOrDefault();
                //当前因子是否符合筛选条件
                //最多间隔数和最大连续次数
                //可以考虑加大连续次数和间隔数
                if (lastIndexResult.ConsecutiveTimes >= firstHistoricalTrend.AllowConsecutiveTimes && lastIndexResult.MaxConsecutiveTimesInterval <= firstHistoricalTrend.AllowInterval)
                    predictiveFactors.Add(factor);

            }
            return predictiveFactors;
        }


        /// <summary>
        ///     分析因子一段日期的历史趋势，（通过号码集合分析历史趋势）
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<HistoricalTrend> AnalyseFactorHistoricalTrend<T>(List<T> numbers, FactorTrendAnalyseResult<T> trendResult, int analyseNumberCount, List<T> predictiveFactor)
        {
            var trends = new List<HistoricalTrend>();

            if (analyseNumberCount <= 0)
                throw new Exception("分析历史趋势时，分析记录数量不能小于等于0！");

            if (numbers == null || numbers.Count == 0)
                throw new Exception("分析历史趋势时，记录不能为空！");

            var numberCount = numbers.Count;

            if (numberCount < analyseNumberCount)
                throw new Exception("分析历史趋势时，分析记录数量不能大于记录数量！");

            var minConsecutiveTimes = trendResult.RowDetailses.Where(n => n.ConsecutiveTimes != 0).Min(n => n.ConsecutiveTimes);
            var maxConsecutiveTimes = trendResult.RowDetailses.Where(n => n.ConsecutiveTimes != 0).Max(n => n.ConsecutiveTimes);

            var minInterval = trendResult.RowDetailses.Where(n => n.MaxConsecutiveTimesInterval!=DiscontinuousFlag).Min(n => n.MaxConsecutiveTimesInterval);
            var maxInterval = trendResult.RowDetailses.Where(n => n.MaxConsecutiveTimesInterval != DiscontinuousFlag).Max(n => n.MaxConsecutiveTimesInterval);
            //允许的连续次数，由小到大
            for (var consecutiveTimes = minConsecutiveTimes; consecutiveTimes <= maxConsecutiveTimes; consecutiveTimes++)
            {
                //允许的间隔数，由大到小
                for (var interval = maxInterval; interval >= minInterval; interval--)
                {
                    var resultCount = 0;
                    var successCount = 0;

                    var trend = new HistoricalTrend
                    {
                        AllowConsecutiveTimes = consecutiveTimes,
                        AllowInterval = interval,
                        AnalyseNumberCount = analyseNumberCount
                    };
                    trends.Add(trend);

                    //行明细结果集
                    var rowDetailses = trendResult.RowDetailses;
                    for (int i = numberCount - 1; i >= analyseNumberCount; i--)
                    {
                        var number = numbers[i];
                        //上一索引位置的分析结果,10个号码，分析第10位（索引位置9），取第9位（索引位置8）
                        var curIndexResult = rowDetailses[i - 1];

                        //对结果再分析
                        //1、按允许的最小因子当前连续次数和允许的最大间隔次数筛选
                        //2、先按最大连续次数然后按最小间隔次数排序
                        if (curIndexResult.ConsecutiveTimes >= consecutiveTimes && curIndexResult.MaxConsecutiveTimesInterval <= interval)
                        {
                            var resultConsecutiveTimes = curIndexResult.ConsecutiveTimes;
                            var resultInterval = curIndexResult.MaxConsecutiveTimesInterval;
                            if (predictiveFactor != null && predictiveFactor.Count > 0)
                            {
                                resultCount++;

                                if (predictiveFactor.Contains(number))
                                {
                                    successCount++;
                                }
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
        ///     统计因子在记录中的连续次数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numbers">记录</param>
        /// <param name="factor">判断因子</param>
        /// <param name="predictiveFactor">反因子</param>
        /// <returns></returns>
        public static FactorTrendAnalyseResult<T> CountFactorConsecutiveTimes<T>(IReadOnlyList<T> numbers, List<T> factor, List<T> predictiveFactor)
        {
            var curResult = new FactorTrendAnalyseResult<T>
            {
                Factor = factor,
                PredictiveFactor = predictiveFactor,
                HistoricalConsecutiveTimes = new SortedDictionary<int, int>(),
                RowDetailses = new List<FactorTrendAnalyseResultRowDetails>()
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
                    curResult.RowDetailses.Add(
                        new FactorTrendAnalyseResultRowDetails
                        {
                            Index = i,
                            MaxConsecutiveTimesInterval = maxConsecutiveTimes - times,
                            ConsecutiveTimes = times
                        });
                }
                else
                {
                    if (curResult.HistoricalConsecutiveTimes.ContainsKey(times))
                    {
                        curResult.HistoricalConsecutiveTimes[times]++;
                    }
                    else if (times >= AllowMinTimes)
                    {
                        curResult.HistoricalConsecutiveTimes.Add(times, 1);
                    }
                    if (times > maxConsecutiveTimes)
                        maxConsecutiveTimes = times;
                    times = 0;
                    //因子不连续
                    curResult.RowDetailses.Add(
                        new FactorTrendAnalyseResultRowDetails
                        {
                            Index = i,
                            MaxConsecutiveTimesInterval = DiscontinuousFlag,
                            ConsecutiveTimes = times
                        });
                }
                i++;
            }
            if (curResult.HistoricalConsecutiveTimes.ContainsKey(times))
            {
                curResult.HistoricalConsecutiveTimes[times]++;
            }
            else if (times >= AllowMinTimes)
            {
                curResult.HistoricalConsecutiveTimes.Add(times, 1);
            }
            return curResult;
        }

        /// <summary>
        ///     统计多个因子在记录中的连续次数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numbers">记录</param>
        /// <param name="factors"></param>
        /// <returns></returns>
        [Obsolete]
        public static List<FactorTrendAnalyseResult<T>> CountFactorsConsecutivesTimes<T>(List<T> numbers, List<Factor<T>> factors)
        {
            var resultList = new List<FactorTrendAnalyseResult<T>>();
            foreach (var factor in factors)
            {
                if (factor.Left == null || factor.Right == null) continue;
                resultList.Add(CountFactorConsecutiveTimes(numbers, factor.Left, factor.Right));
            }
            return resultList;
        }



        /// <summary>
        ///     分析一段日期的历史趋势，（通过号码集合分析历史趋势）
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Obsolete]
        public List<HistoricalTrend> AnalyseHistoricalTrend(AnalyseHistoricalTrendDto<byte> dto)
        {
            var trends = new List<HistoricalTrend>();
            var curDto = new AnalyseFactorHistoricalTrendDto<byte>
            {
                Location = dto.Location,
                Numbers = dto.Numbers,
                AnalyseNumberCount = dto.AnalyseNumberCount,
                StartAllowMaxInterval = dto.StartAllowMaxInterval,
                EndAllowMaxInterval = dto.EndAllowMaxInterval,
                StartAllowMinFactorCurrentConsecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes,
                EndAllowMinFactorCurrentConsecutiveTimes = dto.EndAllowMinFactorCurrentConsecutiveTimes,
                AllowMinTimes = dto.AllowMinTimes,
                NumbersTailCutCount = dto.NumbersTailCutCount,
                HistoricalTrendType = dto.HistoricalTrendType,
                TypeDescription = dto.TypeDescription
            };
            foreach (var factor in dto.Factors)
            {
                curDto.Factor = factor;
                trends.AddRange(AnalyseFactorHistoricalTrend(curDto));
            }
            return trends;
        }


        /// <summary>
        ///     分析
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Obsolete]
        public List<FactorTrendAnalyseResult<T>> Analyse_Old<T>(FactorsTrendAnalyseDto<T> dto)
        {
            var factorResults = CountFactorsConsecutivesTimes(dto.Numbers, dto.Factors);
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
        ///     分析因子一段日期的历史趋势，（通过号码集合分析历史趋势）
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<HistoricalTrend> AnalyseFactorHistoricalTrend(AnalyseFactorHistoricalTrendDto<byte> dto)
        {
            var trends = new List<HistoricalTrend>();

            if (dto.AnalyseNumberCount <= 0)
                throw new Exception("分析历史趋势时，分析记录数量不能小于等于0！");

            if (dto.Numbers.Count < dto.AnalyseNumberCount)
                throw new Exception("分析历史趋势时，分析记录数量不能大于记录数量！");

            var numberCount = dto.Numbers.Count;
            var numbers = dto.Numbers.Select(n => n.Number).ToList();
            var factorTrendAnalyseResults = CountFactorConsecutiveTimes(numbers, dto.Factor.Left, dto.Factor.Right);
            var analyseNumbers = dto.Numbers.OrderByDescending(n => n.TimesValue).Skip(0).Take(dto.AnalyseNumberCount).ToList();

            //允许的连续次数，由小到大
            for (var consecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes; consecutiveTimes <= dto.EndAllowMinFactorCurrentConsecutiveTimes; consecutiveTimes++)
            {
                //允许的间隔数，由大到小
                for (var interval = dto.StartAllowMaxInterval; interval >= dto.EndAllowMaxInterval; interval--)
                {
                    var resultCount = 0;
                    var successCount = 0;

                    var trend = new HistoricalTrend
                    {
                        HistoricalTrendType = dto.HistoricalTrendType,
                        StartTimes = analyseNumbers[0].TimesValue,
                        Items = new List<HistoricalTrendItem>(),
                        Location = dto.Location,
                        AllowConsecutiveTimes = consecutiveTimes,
                        AllowInterval = interval,
                        AnalyseNumberCount = dto.AnalyseNumberCount,
                        TypeDescription = dto.TypeDescription
                    };
                    trends.Add(trend);

                    //行明细结果集
                    var rowDetailses = factorTrendAnalyseResults.RowDetailses;
                    for (int i = 0, maxCount = analyseNumbers.Count; i < maxCount; i++)
                    {
                        var number = analyseNumbers[i].Number;
                        var times = analyseNumbers[i].Times;
                        //上一索引位置的分析结果,10个号码，分析第10位（索引位置9），取第9位（索引位置8）
                        var curIndexResult = rowDetailses[numberCount - i - 2];

                        //结果是否正确
                        var success = false;

                        //对结果再分析
                        //1、按允许的最小因子当前连续次数和允许的最大间隔次数筛选
                        //2、先按最大连续次数然后按最小间隔次数排序
                        if (curIndexResult.ConsecutiveTimes >= consecutiveTimes && curIndexResult.MaxConsecutiveTimesInterval <= interval)
                        {
                            var resultConsecutiveTimes = curIndexResult.ConsecutiveTimes;
                            var resultInterval = curIndexResult.MaxConsecutiveTimesInterval;
                            var predictiveFactor = dto.Factor.Right;
                            if (predictiveFactor != null && predictiveFactor.Count > 0)
                            {
                                resultCount++;

                                if (predictiveFactor.Contains(number))
                                {
                                    successCount++;
                                    success = true;
                                }
                            }

                            var trendItem = new HistoricalTrendItem
                            {
                                Times = times,
                                Number = number,
                                Success = success,
                                ResultConsecutiveTimes = resultConsecutiveTimes,
                                ResultInterval = resultInterval,
                                PredictiveFactor = predictiveFactor
                            };
                            trend.Items.Add(trendItem);

                        }
                    }
                    trend.AnalyticalCount = resultCount;
                    trend.CorrectCount = successCount;
                    trend.CorrectRate = trend.AnalyticalCount == 0 ? 0 : (double)trend.CorrectCount / trend.AnalyticalCount;
                }
            }
            return trends;
        }


        ///// <summary>
        /////     [准备废弃的代码] 分析一段日期的历史趋势，（通过号码集合分析历史趋势）
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //[Obsolete]
        //public List<HistoricalTrend> AnalyseHistoricalTrend_Old1(AnalyseHistoricalTrendDto<byte> dto)
        //{
        //    var trends = new List<HistoricalTrend>();

        //    if (dto.Numbers.Count < dto.AnalyseNumberCount)
        //        throw new Exception("分析历史趋势时，分析记录数量不能大于记录数量！");

        //    var analyseNumbers = dto.Numbers.OrderByDescending(n => n.TimesValue).Skip(0).Take(dto.AnalyseNumberCount).ToList();

        //    //允许的连续次数，由小到大
        //    for (var consecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes; consecutiveTimes <= dto.EndAllowMinFactorCurrentConsecutiveTimes; consecutiveTimes++)
        //    {
        //        //允许的间隔数，由大到小
        //        for (var interval = dto.StartAllowMaxInterval; interval >= dto.EndAllowMaxInterval; interval--)
        //        {
        //            var resultCount = 0;
        //            var successCount = 0;

        //            var trend = new HistoricalTrend
        //            {
        //                HistoricalTrendType = dto.HistoricalTrendType,
        //                StartTimes = analyseNumbers[0].TimesValue,
        //                Items = new List<HistoricalTrendItem>(),
        //                Location = dto.Location,
        //                AllowConsecutiveTimes = consecutiveTimes,
        //                AllowInterval = interval,
        //                AnalyseNumberCount = dto.AnalyseNumberCount,
        //                TypeDescription = dto.TypeDescription
        //            };
        //            trends.Add(trend);
        //            for (int i = 0, maxCount = analyseNumbers.Count; i < maxCount; i++)
        //            {
        //                var number = analyseNumbers[i].Number;
        //                var times = analyseNumbers[i].Times;
        //                var timesValue = analyseNumbers[i].TimesValue;
        //                var numbers = dto.Numbers.Where(n => n.TimesValue < timesValue).Select(n => n.Number).ToList();

        //                var factorResults = Analyse(new FactorsTrendAnalyseDto<byte>
        //                {
        //                    Numbers = numbers,
        //                    Factors = dto.Factors,
        //                    AllowMinTimes = dto.AllowMinTimes,
        //                    NumbersTailCutCount = dto.NumbersTailCutCount,
        //                    AllowMinFactorCurrentConsecutiveTimes = consecutiveTimes,
        //                    AllowMaxInterval = interval
        //                });

        //                //结果是否正确
        //                var success = false;

        //                //对结果再分析
        //                var factorResult = factorResults.OrderByDescending(t => t.FactorCurrentConsecutiveTimes).FirstOrDefault();
        //                var factors = new List<byte>();
        //                var resultConsecutiveTimes = 0;
        //                var resultInterval = 0;
        //                if (factorResult != null)
        //                {
        //                    factors = factorResult.PredictiveFactor;
        //                    resultConsecutiveTimes = factorResult.FactorCurrentConsecutiveTimes;
        //                    resultInterval = factorResult.Interval;
        //                    if (factorResult.PredictiveFactor != null && factorResult.PredictiveFactor.Count > 0)
        //                    {
        //                        resultCount++;

        //                        if (factors.Contains(number))
        //                        {
        //                            successCount++;
        //                            success = true;
        //                        }
        //                    }
        //                }

        //                var trendItem = new HistoricalTrendItem { Times = times, Number = number, Success = success, ResultConsecutiveTimes = resultConsecutiveTimes, ResultInterval = resultInterval, PredictiveFactor = factors };

        //                trend.AnalyticalCount = resultCount;
        //                trend.CorrectCount = successCount;
        //                trend.CorrectRate = trend.AnalyticalCount == 0 ? 0 : (double)trend.CorrectCount / trend.AnalyticalCount;
        //                trend.Items.Add(trendItem);
        //            }
        //        }
        //    }
        //    return trends;
        //}


        ///// <summary>
        /////     分析一段日期的历史趋势，（通过号码集合分析历史趋势）
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //[Obsolete]
        //public List<HistoricalTrend> AnalyseHistoricalTrend_Old2(AnalyseHistoricalTrendDto<byte> dto)
        //{
        //    var trends = new List<HistoricalTrend>();

        //    if (dto.Numbers.Count < dto.AnalyseNumberCount)
        //        throw new Exception("分析历史趋势时，分析记录数量不能大于记录数量！");

        //    var analyseNumbers = dto.Numbers.OrderByDescending(n => n.TimesValue).Skip(0).Take(dto.AnalyseNumberCount).ToList();

        //    var factorResultDict = new Dictionary<int, List<FactorTrendAnalyseResult<byte>>>();

        //    //先记录分析结果
        //    for (int i = 0, maxCount = analyseNumbers.Count; i < maxCount; i++)
        //    {
        //        var timesValue = analyseNumbers[i].TimesValue;
        //        var numbers = dto.Numbers.Where(n => n.TimesValue < timesValue).Select(n => n.Number).ToList();

        //        var factorResults = Analyse(new FactorsTrendAnalyseDto<byte>
        //        {
        //            Numbers = numbers,
        //            Factors = dto.Factors,
        //            AllowMinTimes = dto.AllowMinTimes,
        //            NumbersTailCutCount = dto.NumbersTailCutCount,
        //            AllowMinFactorCurrentConsecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes,
        //            AllowMaxInterval = dto.StartAllowMaxInterval
        //        });
        //        factorResultDict.Add(i, factorResults);
        //    }


        //    //允许的连续次数，由小到大
        //    for (var consecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes; consecutiveTimes <= dto.EndAllowMinFactorCurrentConsecutiveTimes; consecutiveTimes++)
        //    {
        //        //允许的间隔数，由大到小
        //        for (var interval = dto.StartAllowMaxInterval; interval >= dto.EndAllowMaxInterval; interval--)
        //        {
        //            var resultCount = 0;
        //            var successCount = 0;

        //            var trend = new HistoricalTrend
        //            {
        //                HistoricalTrendType = dto.HistoricalTrendType,
        //                StartTimes = analyseNumbers[0].TimesValue,
        //                Items = new List<HistoricalTrendItem>(),
        //                Location = dto.Location,
        //                AllowConsecutiveTimes = consecutiveTimes,
        //                AllowInterval = interval,
        //                AnalyseNumberCount = dto.AnalyseNumberCount,
        //                TypeDescription = dto.TypeDescription
        //            };
        //            trends.Add(trend);
        //            for (int i = 0, maxCount = analyseNumbers.Count; i < maxCount; i++)
        //            {
        //                var number = analyseNumbers[i].Number;
        //                var times = analyseNumbers[i].Times;

        //                var factorResults = factorResultDict[i];

        //                //结果是否正确
        //                var success = false;

        //                //对结果再分析
        //                //1、按允许的最小因子当前连续次数和允许的最大间隔次数筛选
        //                //2、先按最大连续次数然后按最小间隔次数排序
        //                factorResults = factorResults
        //                    .Where(m => m.FactorCurrentConsecutiveTimes >= consecutiveTimes && m.Interval <= interval)
        //                    .OrderByDescending(t => t.FactorCurrentConsecutiveTimes)
        //                    .ThenBy(t => t.Interval).ToList();

        //                var factorResult = factorResults.OrderByDescending(t => t.FactorCurrentConsecutiveTimes).FirstOrDefault();

        //                //有符合条件的因子分析结果，才记录，减少无用数据以提高程序性能
        //                if (factorResult == null) continue;
        //                var resultConsecutiveTimes = factorResult.FactorCurrentConsecutiveTimes;
        //                var resultInterval = factorResult.Interval;
        //                if (factorResult.PredictiveFactor != null && factorResult.PredictiveFactor.Count > 0)
        //                {
        //                    resultCount++;

        //                    if (factorResult.PredictiveFactor.Contains(number))
        //                    {
        //                        successCount++;
        //                        success = true;
        //                    }
        //                }

        //                var trendItem = new HistoricalTrendItem
        //                {
        //                    Times = times,
        //                    Number = number,
        //                    Success = success,
        //                    ResultConsecutiveTimes = resultConsecutiveTimes,
        //                    ResultInterval = resultInterval,
        //                    PredictiveFactor = factorResult.PredictiveFactor
        //                };
        //                trend.Items.Add(trendItem);

        //                /* 记录所有分析结果，包括条件为0的 [2018-1-18]
        //                var factors = new List<byte>();
        //                var resultConsecutiveTimes = 0;
        //                var resultInterval = 0;
        //                if (factorResult != null)
        //                {
        //                    factors = factorResult.PredictiveFactor;
        //                    resultConsecutiveTimes = factorResult.FactorCurrentConsecutiveTimes;
        //                    resultInterval = factorResult.Interval;
        //                    if (factorResult.PredictiveFactor != null && factorResult.PredictiveFactor.Count > 0)
        //                    {
        //                        resultCount++;

        //                        if (factors.Contains(number))
        //                        {
        //                            successCount++;
        //                            success = true;
        //                        }
        //                    }
        //                }


        //                var trendItem = new HistoricalTrendItem { Times = times, Number = number, Success = success, ResultConsecutiveTimes = resultConsecutiveTimes, ResultInterval = resultInterval, PredictiveFactor = factors };
        //                trend.Items.Add(trendItem);    
        //             */
        //            }
        //            trend.AnalyticalCount = resultCount;
        //            trend.CorrectCount = successCount;
        //            trend.CorrectRate = trend.AnalyticalCount == 0 ? 0 : (double)trend.CorrectCount / trend.AnalyticalCount;
        //        }
        //    }
        //    return trends;
        //}

    }

}