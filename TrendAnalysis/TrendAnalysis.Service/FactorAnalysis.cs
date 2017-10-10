using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendAnalysis.Service
{
    /// <summary>
    /// 因子解析
    /// </summary>
    public class FactorAnalysis
    {
        /// <summary>
        /// 解析连续在因子中的记录数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numbers">记录</param>
        /// <param name="nodes">因子结点</param>
        /// <param name="allowMinTimes">允许的最小连续数，大于等于此数才记录</param>
        /// <returns></returns>
        public static List<Results<T>> Consecutives<T>(List<T> numbers, List<BinaryNode<T>> factors, int allowMinTimes = 1)
        {
            var resultList = new List<Results<T>>();
            foreach (var factor in factors)
            {
                if (factor.Left != null && factor.Left.Count > 0)
                {
                    resultList.Add(Consecutive(numbers, factor.Left, factor.Right, allowMinTimes));
                }
                if (factor.Right != null && factor.Right.Count > 0)
                {
                    resultList.Add(Consecutive(numbers, factor.Right, factor.Left, allowMinTimes));
                }
            }
            return resultList;
        }

        /// <summary>
        /// 解析连续在因子中的记录数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numbers">记录</param>
        /// <param name="nodes">因子结点</param>
        /// <param name="compareFunc">比较因子的委托方法,参数为因子列表和当前索引，返回结果为bool</param>
        /// <param name="allowMinTimes">允许的最小连续数，大于等于此数才记录</param>
        /// <returns></returns>
        public static List<Results<T>> Consecutives<T>(IReadOnlyList<T> numbers, List<BinaryNode<T>> factors, Func<IReadOnlyList<T>, List<T>, int, bool> compareFunc, int allowMinTimes = 1)
        {
            var resultList = new List<Results<T>>();
            foreach (var factor in factors)
            {
                if (factor.Left != null && factor.Left.Count > 0)
                {
                    resultList.Add(Consecutive(numbers, factor.Left, factor.Right, compareFunc, allowMinTimes));
                }
                if (factor.Right != null && factor.Right.Count > 0)
                {
                    resultList.Add(Consecutive(numbers, factor.Right, factor.Left, compareFunc, allowMinTimes));
                }
            }
            return resultList;
        }

        /// <summary>
        /// 解析连续在因子中的记录数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numbers">记录</param>
        /// <param name="factor">判断因子</param>
        /// <param name="oppositeFactor">反因子</param>
        /// <param name="compareFunc">比较因子的委托方法</param>
        /// <param name="allowMinTimes">允许的最小连续数，大于等于此数才记录</param>
        /// <returns></returns>
        private static Results<T> Consecutive<T>(IReadOnlyList<T> numbers, List<T> factor, List<T> oppositeFactor, int allowMinTimes = 1)
        {
            return Consecutive(numbers, factor, oppositeFactor, (n,factorList, index) =>
            {
                var number = n[index];
                return factorList.Exists(m => m.Equals(number));
            }, allowMinTimes);

            //var curResult = new Results<T> { Factor = factor,OppositeFactor=oppositeFactor, ConsecutiveTimes = new SortedDictionary<int, int>() };
            //var i = 0;
            ////连续次数
            //var times = 0;
            //var length = numbers.Count;
            //while (i < length)
            //{
            //    var currentItem = numbers[i];
            //    if (factor.Exists(m => m.Equals(currentItem)))
            //    {
            //        times++;
            //    }
            //    else
            //    {
            //        if (curResult.ConsecutiveTimes.ContainsKey(times))
            //        {
            //            curResult.ConsecutiveTimes[times]++;
            //        }
            //        else if(times>= allowMinTimes)
            //        {
            //            curResult.ConsecutiveTimes.Add(times, 1);
            //        }
            //        times = 0;
            //    }
            //    i++;
            //}
            //if (curResult.ConsecutiveTimes.ContainsKey(times))
            //{
            //    curResult.ConsecutiveTimes[times]++;
            //}
            //else if(times >= allowMinTimes)
            //{
            //    curResult.ConsecutiveTimes.Add(times, 1);
            //}
            //return curResult;
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
        private static Results<T> Consecutive<T>(IReadOnlyList<T> numbers, List<T> factor, List<T> oppositeFactor, Func<IReadOnlyList<T>, List<T>, int, bool> compareFunc, int allowMinTimes = 1)
        {
            var curResult = new Results<T> { Factor = factor, OppositeFactor = oppositeFactor, ConsecutiveTimes = new SortedDictionary<int, int>() };
            var i = 0;
            //连续次数
            var times = 0;
            var length = numbers.Count;
            while (i < length)
            {
                if (compareFunc(numbers,factor, i))
                {
                    times++;
                }
                else
                {
                    if (curResult.ConsecutiveTimes.ContainsKey(times))
                    {
                        curResult.ConsecutiveTimes[times]++;
                    }
                    else if (times >= allowMinTimes)
                    {
                        curResult.ConsecutiveTimes.Add(times, 1);
                    }
                    times = 0;
                }
                i++;
            }
            if (curResult.ConsecutiveTimes.ContainsKey(times))
            {
                curResult.ConsecutiveTimes[times]++;
            }
            else if (times >= allowMinTimes)
            {
                curResult.ConsecutiveTimes.Add(times, 1);
            }
            return curResult;
        }
    }


    /// <summary>
    /// 解析因子结果
    /// </summary>
    /// <typeparam name="T">因子类型</typeparam>
    public class Results<T>
    {
        /// <summary>
        /// 因子
        /// </summary>
        public List<T> Factor { get; set; }


        /// <summary>
        /// 反因子
        /// </summary>
        public List<T> OppositeFactor { get; set; }

        /// <summary>
        /// 连续次数,键为次数，值为数量
        /// </summary>
        public SortedDictionary<int, int> ConsecutiveTimes { get; set; } = new SortedDictionary<int, int>();

        /// <summary>
        /// 指定期次此因子连续次数
        /// </summary>
        public int SpecifiedTimesConsecutiveTimes { get; set; }


        /// <summary>
        /// 最大连续期数-指定期次此因子连续次数
        /// </summary>
        public int Interval => ConsecutiveTimes.Max(k => k.Key) - SpecifiedTimesConsecutiveTimes;

    }

}
