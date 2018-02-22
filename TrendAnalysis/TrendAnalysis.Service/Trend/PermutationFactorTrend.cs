using System;
using System.Collections.Generic;
using System.Linq;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.DataTransferObject.Trend;
using TrendAnalysis.Models.Trend;


namespace TrendAnalysis.Service.Trend
{

    /// <summary>
    ///     排列因子的历史趋势
    /// </summary>
    public class PermutationFactorTrend
    {

        /// <summary>
        ///     非连续指示
        /// </summary>
        public const int DisConsecutiveFlag = int.MaxValue;


        /// <summary>
        ///     统计连续次数允许的最小次数
        /// </summary>
        public const int AllowMinTimes = 1;

        /// <summary>
        ///     分析
        /// </summary>
        /// <param name="dto">记录集合、比较因子、允许的最小连续次数，大于等于此数才记录......</param>
        /// <returns></returns>
        public Factor<T> Analyse<T>(PermutationFactorTrendAnalyseDto<T> dto)
        {
            //分析历史趋势,排除最后一位号码，（最后一位号码分析当前要分析的可能号码）
            var historicalNumbers = dto.Numbers.Take(dto.Numbers.Count - 1).ToList();

            //预测因子
            var predictiveFactor = dto.PermutationFactors[0];

            var factors = dto.PermutationFactors.Select(p => p.Left).ToList();
            //统计每个因子在记录中的趋势
            var trendResult = CountConsecutiveDistribution(dto.Numbers, factors, predictiveFactor.Right);

            //行明细结果集
            var rowDetailses = trendResult.FactorDistributions;
            if (rowDetailses == null || rowDetailses.Count == 0) return null;
            var lastIndexResult = rowDetailses[rowDetailses.Count - 1];

            //因子不包含最后一个号码，（连续次数为0）
            if (lastIndexResult.ConsecutiveTimes == 0)
                return null;

            var historicalTrends = GetCorrectRates(historicalNumbers, trendResult, dto.AnalyseHistoricalTrendEndIndex, predictiveFactor.Right);

            //筛选正确100%的历史趋势，如没有不记录
            //historicalTrends = historicalTrends.Where(h => h.CorrectRate == 1).OrderBy(h => h.AllowInterval).ThenByDescending(h => h.AllowConsecutiveTimes).ToList();
            historicalTrends = historicalTrends.Where(h => h.CorrectRate == 1).OrderByDescending(h => h.AllowConsecutiveTimes).ThenBy(h => h.AllowInterval).ToList();
            if (historicalTrends.Count == 0) return null;

            var firstHistoricalTrend = historicalTrends.FirstOrDefault();
            if (firstHistoricalTrend == null)
                return null;

            //可以考虑加大连续次数和间隔数
            if (lastIndexResult.ConsecutiveTimes >= firstHistoricalTrend.AllowConsecutiveTimes + dto.AddConsecutiveTimes && lastIndexResult.MaxConsecutiveTimesInterval <= firstHistoricalTrend.AllowInterval - dto.AddInterval)
            {
                //返回的可能因子
                return predictiveFactor;
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
        public List<FactorTrendCorrectRate> GetCorrectRates<T>(List<T> numbers, PermutationFactorTrendConsecutiveDetails<T> trendResult, int endIndex, List<T> predictiveFactor)
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
                        var distribution = distributions.FirstOrDefault(d => d.Index == i - 1);// distributions[i - 1];
                        if (distribution == null) continue;

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
        ///     解析因子在记录中的连续次数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numbers">记录</param>
        /// <param name="factors"></param>
        /// <param name="predictiveFactor">反因子</param>
        /// <returns></returns>
        public static PermutationFactorTrendConsecutiveDetails<T> CountConsecutiveDistribution<T>(IReadOnlyList<T> numbers, List<List<T>> factors, List<T> predictiveFactor)
        {
            var curResult = new PermutationFactorTrendConsecutiveDetails<T>
            {
                Factors = factors,
                PredictiveFactor = predictiveFactor,
                HistoricalConsecutiveTimes = new SortedDictionary<int, int>(),
                FactorDistributions = new List<FactorDistribution>()
            };
            var i = 0;

            //连续次数
            var times = 0;

            //统计记录的长度
            var length = numbers.Count;

            //最大连续次数
            var maxConsecutiveTimes = 0;

            //遍历所有记录
            while (i < length)
            {
                /*
                排列因子：{1,2}{3,4}
                号码索引位置：1 2 3 4 5 6 7 8 9 10 11 12 13 
                号码：        6 1 3 5 2 3 2 4 1 0  0  1  1
                              0 1 2 0 1 2 1 2 1 0  0  1  1
                （1命中第一个因子，2命中第二个因子，只有同时命中1、2才能算一次，并且索引位置跳到下一位置，比如索引位置2，3，同时命中1，2，连续次数递增1）
                 
                 */
                var n = 0;
                for (; n < factors.Count && n + i < length; n++)
                {
                    if (!factors[n].Exists(m => m.Equals(numbers[n + i])))
                    {
                        break;
                    }
                }

                //排列因子全部相等
                if (n >= factors.Count)
                {
                    //连续次数递增
                    times++;

                    //索引位置调整，指向下一索引位置的前一条记录
                    i = i + factors.Count - 1;

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
                    //是否有相同的连续次数，有则递增，否则新增一条连续次数记录
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

                    //连续次数清零，下一次重新统计
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

            //是否有相同的连续次数，有则递增，否则新增一条连续次数记录
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


        #region  遍历因子


        /// <summary>
        ///     遍历排列因子，组装分析趋势的因子时，记录反因子按最后因子取反
        ///     比如：
        ///     { 1, 2 }, { 3, 4 },
        ///     排列结果：
        ///     1,3
        ///     1,4
        ///     2,3
        ///     2,4
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="permutationFactors">要遍历的排列因子，二维列表</param>
        /// <returns>遍历结果，总条数是每一行数据条数相乘的结果</returns>
        public static List<List<List<T>>> TraversePermutationFactor<T>(List<List<Factor<T>>> permutationFactors)
        {
            var length = permutationFactors.Count;
            var result = new List<List<List<T>>>();

            //列表数组,最后一个元素为反因子
            var factors = new List<T>[length + 1];

            //每一因子索引位置数组，记录了相当每一行因子的位置
            var indexArray = new int[length];

            //记录每一因子遍历数量，记录了相当每一行遍历过的因子数量，因为每个因子有左右列表，所以每一行遍历数为每 一行元素数量*2
            var countArray = new int[length];
            var i = 0;
            while (i < length)
            {
                if (i < length - 1)
                {
                    var curLength = permutationFactors[i].Count;
                    if (indexArray[i] < curLength)
                    {
                        //取2的模如果=0，表示遍历到当前元素
                        if (countArray[i] % 2 == 0)
                        {
                            factors[i] = permutationFactors[i][indexArray[i]].Left;
                        }
                        else
                        {
                            factors[i] = permutationFactors[i][indexArray[i]].Right;

                            //可以遍历下一个元素
                            indexArray[i]++;
                        }
                        countArray[i]++;
                    }
                    else
                    {
                        if (i == 0) break;
                        indexArray[i] = 0;
                        i--;
                        continue;
                    }
                }
                else
                {
                    for (var j = 0; j < permutationFactors[i].Count; j++)
                    {
                        factors[i] = permutationFactors[i][j].Left;

                        //记录反因子
                        factors[i + 1] = permutationFactors[i][j].Right;
                        result.Add(factors.ToList());

                        factors[i] = permutationFactors[i][j].Right;

                        //记录反因子
                        factors[i + 1] = permutationFactors[i][j].Left;
                        result.Add(factors.ToList());
                    }
                    i--;
                    continue;
                }
                i++;
            }
            return result;
        }

        #endregion


        /// <summary>
        ///     分析一段日期的历史趋势，（通过号码集合分析历史趋势）
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<FactorTrendCorrectRate> AnalyseHistoricalTrend(PermutationFactorHistoricalTrendAnalyseDto<byte> dto)
        {
            var trends = new List<FactorTrendCorrectRate>();

            //if (dto.Numbers.Count < dto.AnalyseNumberCount)
            //    throw new Exception("分析历史趋势时，分析记录数量不能大于记录数量！");

            //var analyseNumbers = dto.Numbers.OrderByDescending(n => n.TimesValue).Skip(0).Take(dto.AnalyseNumberCount).ToList();
            //var factorResultDict = new Dictionary<int, List<PermutationFactorTrendConsecutiveDetails<byte>>>();

            ////先记录分析结果
            //for (int i = 0, maxCount = analyseNumbers.Count; i < maxCount; i++)
            //{
            //    var timesValue = analyseNumbers[i].TimesValue;
            //    var numbers = dto.Numbers.Where(n => n.TimesValue < timesValue).Select(n => n.Number).ToList();

            //    var factorResults = Analyse(new PermutationFactorTrendAnalyseDto<byte>
            //    {
            //        Numbers = numbers,
            //        PermutationFactors = dto.PermutationFactors,
            //        AllowMinTimes = dto.AllowMinTimes,
            //        NumbersTailCutCount = dto.NumbersTailCutCount,
            //        AllowMinFactorCurrentConsecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes,
            //        AllowMaxInterval = dto.StartAllowMaxInterval
            //    });
            //    factorResultDict.Add(i, factorResults);
            //}


            ////允许的连续次数，由小到大
            //for (var consecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes; consecutiveTimes <= dto.EndAllowMinFactorCurrentConsecutiveTimes; consecutiveTimes++)
            //{
            //    //允许的间隔数，由大到小
            //    for (var interval = dto.StartAllowMaxInterval; interval >= dto.EndAllowMaxInterval; interval--)
            //    {
            //        var resultCount = 0;
            //        var successCount = 0;

            //        var trend = new FactorTrendCorrectRate
            //        {
            //            //HistoricalTrendType = dto.HistoricalTrendType,
            //            StartTimes = analyseNumbers[0].TimesValue,
            //            //Items = new List<HistoricalTrendItem>(),
            //            Location = dto.Location,
            //            AllowConsecutiveTimes = consecutiveTimes,
            //            AllowInterval = interval,
            //            AnalyseNumberCount = dto.AnalyseNumberCount,
            //            TypeDescription = dto.TypeDescription
            //        };
            //        trends.Add(trend);
            //        for (int i = 0, maxCount = analyseNumbers.Count; i < maxCount; i++)
            //        {
            //            var number = analyseNumbers[i].Number;
            //            var times = analyseNumbers[i].Times;

            //            var factorResults = factorResultDict[i];

            //            //结果是否正确
            //            var success = false;

            //            //对结果再分析
            //            //1、按允许的最小因子当前连续次数和允许的最大间隔次数筛选
            //            //2、先按最大连续次数然后按最小间隔次数排序
            //            factorResults = factorResults
            //                .Where(m => m.FactorCurrentConsecutiveTimes >= consecutiveTimes && m.Interval <= interval)
            //                .OrderByDescending(t => t.FactorCurrentConsecutiveTimes)
            //                .ThenBy(t => t.Interval).ToList();

            //            var factorResult = factorResults.OrderByDescending(t => t.FactorCurrentConsecutiveTimes).FirstOrDefault();
            //            if (factorResult == null) continue;
            //            var resultConsecutiveTimes = factorResult.FactorCurrentConsecutiveTimes;
            //            var resultInterval = factorResult.Interval;
            //            if (factorResult.PredictiveFactor != null && factorResult.PredictiveFactor.Count > 0)
            //            {
            //                resultCount++;

            //                if (factorResult.PredictiveFactor.Contains(number))
            //                {
            //                    successCount++;
            //                    success = true;
            //                }
            //            }
            //            var trendItem = new HistoricalTrendItem
            //            {
            //                Times = times,
            //                Number = number,
            //                Success = success,
            //                ResultConsecutiveTimes = resultConsecutiveTimes,
            //                ResultInterval = resultInterval,
            //                PredictiveFactor = factorResult.PredictiveFactor
            //            };

            //            //trend.Items.Add(trendItem);


            //            /*  分析结果为也作记录
            //            var factorResult = factorResults.OrderByDescending(t => t.MaxConsecutiveTimes).FirstOrDefault();
            //            var factors = new List<byte>();
            //            var resultConsecutiveTimes = 0;
            //            var resultInterval = 0;
            //            if (factorResult != null)
            //            {
            //                factors = factorResult.PredictiveFactor;
            //                resultConsecutiveTimes = factorResult.MaxConsecutiveTimes;
            //                resultInterval = factorResult.MaxInterval;
            //                if (factorResult.PredictiveFactor != null && factorResult.PredictiveFactor.Count > 0)
            //                {
            //                    resultCount++;

            //                    if (factors.Contains(number))
            //                    {
            //                        successCount++;
            //                        success = true;
            //                    }
            //                }
            //            }

            //            var trendItem = new HistoricalTrendItem
            //            {
            //                Times = times,
            //                Number = number,
            //                Success = success,
            //                ResultConsecutiveTimes = resultConsecutiveTimes,
            //                ResultInterval = resultInterval,
            //                PredictiveFactor = factors
            //            };

            //            trend.Items.Add(trendItem);    
            //         */
            //        }
            //        trend.AnalyticalCount = resultCount;
            //        trend.CorrectCount = successCount;
            //        trend.CorrectRate = trend.AnalyticalCount == 0 ? 0 : (double)trend.CorrectCount / trend.AnalyticalCount;
            //    }
            //}
            return trends;
        }

    }

}