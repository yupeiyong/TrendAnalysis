using System;
using System.Collections.Generic;
using System.Linq;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.Models.Trend;

namespace TrendAnalysis.Service.Trend
{
    /// <summary>
    /// 排列因子的历史趋势
    /// </summary>
    public class PermutationFactorTrend
    {
        /// <summary>
        /// 分析
        /// </summary>
        /// <param name="numbers">记录集合</param>
        /// <param name="tensDigitFactors">比较因子</param>
        /// <param name="allowMinTimes">允许的最小连续次数，大于等于此数才记录</param>
        /// <returns></returns>
        public List<PermutationFactorTrendAnalyseResult<T>> Analyse<T>(PermutationFactorTrendAnalyseDto<T> dto)
        {

            List<PermutationFactorTrendAnalyseResult<T>> factorResults = null;
            if (dto.NumbersTailCutCount > 0 && dto.Numbers.Count > 0)
            {
                var nums = dto.Numbers.Skip(0).Take(dto.Numbers.Count - dto.NumbersTailCutCount).ToList();
                factorResults = AnalyseConsecutives(nums, dto.PermutationFactors, dto.AllowMinTimes);
            }
            else
            {
                factorResults = AnalyseConsecutives(dto.Numbers, dto.PermutationFactors, dto.AllowMinTimes);
            }
            factorResults = factorResults.Where(t => t.HistoricalConsecutiveTimes.Count > 0).ToList();
            foreach (var item in factorResults)
            {
                var times = 0;
                //记录集合倒序检查，因子是否包含当前号码
                for (var i = dto.Numbers.Count - 1; i >= 0; i--)
                {
                    //if (!item.Factors.Contains(dto.Numbers[i]))
                    //    break;
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
        /// 解析因子在记录中的连续次数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numbers">记录</param>
        /// <param name="nodes">因子结点</param>
        /// <param name="allowMinTimes">允许的最小连续数，大于等于此数才记录</param>
        /// <returns></returns>
        public static List<PermutationFactorTrendAnalyseResult<T>> AnalyseConsecutives<T>(List<T> numbers, List<List<Factor<T>>> permutationFactors, int allowMinTimes = 1)
        {
            /*
             因子
             12 34
             13 24
             14 23

            排列因子（两层排列，2*6=12种组合）：
            1、12
               12
            2、12
               34
            3、12
               13
            4、12
               24
            5、12
               14
            6、12
               23
            7、34
               12
            8、34
               34
            9、34
               13
            10、34
                24
            11、34
                14
            12、34
                23
             */

            /*
             如果只有两层
             for(var i=0;i<arr[0].Count;i++)
             {
                 for(var j=0;j<arr[1].Count;j++)
                 {
                     1、var factors=new List<<List<T>>(){arr[0][i].Left,arr[1][j].Left}; 反因子：arr[1][j].Right
                     2、var factors=new List<<List<T>>(){arr[0][i].Left,arr[1][j].Right};反因子：arr[1][j].Left
                     3、var factors=new List<<List<T>>(){arr[0][i].Right,arr[1][j].Left}; 反因子：arr[1][j].Right
                     4、var factors=new List<<List<T>>(){arr[0][i].Right,arr[1][j].Right};反因子：arr[1][j].Left
                 }

            }
             
             */
            var factors = TraversePermutationFactor(permutationFactors);

            var resultList = new List<PermutationFactorTrendAnalyseResult<T>>();
            foreach (var factor in factors)
            {
                if (factor != null && factor.Count > 0)
                {
                    //取保存在最后位置的反因子
                    var oppositeFactor = factor[factor.Count - 1];
                    //删除保存在最后位置的反因子
                    factor.RemoveAt(factor.Count - 1);
                    resultList.Add(AnalyseConsecutive(numbers, factor, oppositeFactor, allowMinTimes));
                }
            }


            return resultList;
        }


        /// <summary>
        /// 解析因子在记录中的连续次数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numbers">记录</param>
        /// <param name="factor">判断因子</param>
        /// <param name="oppositeFactor">反因子</param>
        /// <param name="allowMinTimes">允许的最小连续数，大于等于此数才记录</param>
        /// <returns></returns>
        private static PermutationFactorTrendAnalyseResult<T> AnalyseConsecutive<T>(IReadOnlyList<T> numbers, List<List<T>> factor, List<T> oppositeFactor, int allowMinTimes = 1)
        {
            return AnalyseConsecutive(numbers, factor, oppositeFactor, (n, factorList, index) =>
            {
                var number = n[index];
                return factorList.Exists(m => m.Equals(number));
            }, allowMinTimes);
        }

        /// <summary>
        /// 解析连续在因子中的记录数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numbers">记录</param>
        /// <param name="factor">判断因子</param>
        /// <param name="oppositeFactor">反因子</param>
        /// <param name="compareFunc">比较因子的委托方法,参数为因子列表和当前索引，返回结果为bool</param>
        /// <param name="allowMinTimes">允许的最小连续数，大于等于此数才记录</param>
        /// <returns></returns>
        private static PermutationFactorTrendAnalyseResult<T> AnalyseConsecutive<T>(IReadOnlyList<T> numbers, List<List<T>> factor, List<T> oppositeFactor, Func<IReadOnlyList<T>, List<T>, int, bool> compareFunc, int allowMinTimes = 1)
        {
            var curResult = new PermutationFactorTrendAnalyseResult<T> { Factors = factor, OppositeFactor = oppositeFactor, HistoricalConsecutiveTimes = new SortedDictionary<int, int>() };
            var i = 0;
            //连续次数
            var times = 0;
            var length = numbers.Count;
            while (i < length)
            {
                //if (compareFunc(numbers, factor, i))
                //{
                //    times++;
                //}
                //else
                //{
                //    if (curResult.HistoricalConsecutiveTimes.ContainsKey(times))
                //    {
                //        curResult.HistoricalConsecutiveTimes[times]++;
                //    }
                //    else if (times >= allowMinTimes)
                //    {
                //        curResult.HistoricalConsecutiveTimes.Add(times, 1);
                //    }
                //    times = 0;
                //}
                i++;
            }
            if (curResult.HistoricalConsecutiveTimes.ContainsKey(times))
            {
                curResult.HistoricalConsecutiveTimes[times]++;
            }
            else if (times >= allowMinTimes)
            {
                curResult.HistoricalConsecutiveTimes.Add(times, 1);
            }
            return curResult;
        }

        /// <summary>
        ///     遍历排列因子
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
        /// <returns>遍历结果，</returns>
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

    }
}
