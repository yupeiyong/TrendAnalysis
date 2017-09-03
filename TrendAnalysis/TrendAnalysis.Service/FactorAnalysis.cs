﻿using System;
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
        /// <param name="numbers"></param>
        /// <param name="position"></param>
        /// <param name="nodes"></param>
        public List<Results<string>> Consecutives(List<string> numbers, List<BinaryNode<string>> nodes)
        {
            var resultList = new List<Results<string>>();
            foreach (var node in nodes)
            {
                if (node.Left != null && node.Left.Count > 0)
                {
                    resultList.Add(Consecutive(numbers, node.Left));
                }
                if (node.Right != null && node.Right.Count > 0)
                {
                    resultList.Add(Consecutive(numbers, node.Right));
                }
            }
            return resultList;
        }

        private Results<string> Consecutive(List<string> numbers, List<string> factor)
        {
            var curResult = new Results<string> { Factor = factor, ConsecutiveTimes = new SortedDictionary<int, int>() };
            var i = 0;
            //连续次数
            var times = 0;
            var length = numbers.Count;
            while (i < length)
            {
                var currentItem = numbers[i];
                if (factor.Exists(m => m.Equals(currentItem)))
                {
                    times++;
                }
                else
                {
                    if (curResult.ConsecutiveTimes.ContainsKey(times))
                    {
                        curResult.ConsecutiveTimes[times]++;
                    }
                    else if(times>0)
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
            else if(times>0)
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
        public List<T> Factor { get; set; }


        /// <summary>
        /// 连续次数,键为次数，值为数量
        /// </summary>
        public SortedDictionary<int, int> ConsecutiveTimes { get; set; }

    }

}
