﻿using System;
using System.Collections.Generic;
using System.Linq;
using TrendAnalysis.Models.Trend;


namespace TrendAnalysis.Service.Trend
{

    /// <summary>
    ///     因子生成器
    /// </summary>
    public class FactorGenerator
    {

        /// <summary>
        ///     按输入数组生成因子集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="middleIndex">进行二分组合时，从0开始的中间位置索引</param>
        /// <param name="oddLengthAllowZeroIndex">当数组长度为奇数，是否允许从索引位置0开始</param>
        /// <returns></returns>
        public static List<Factor<T>> Create<T>(List<T> arr, int middleIndex = 0, bool oddLengthAllowZeroIndex = false)
        {
            if (arr == null || arr.Count() == 0)
                throw new Exception("错误，输入的集合为空！");

            var factors = new List<Factor<T>>();
            var length = arr.Count();
            if (length == 1)
            {
                factors.Add(new Factor<T> {Left = new List<T> {arr[0]}, Right = new List<T>()});
                return factors;
            }
            if (length == 2)
            {
                factors.Add(new Factor<T> {Left = new List<T> {arr[0]}, Right = new List<T> {arr[1]}});
                return factors;
            }
            if (middleIndex == 0)
            {
                //如果数组长度为奇数，和偶数取相同的中间位置索引，如[1,2,3,4,5]=>[1,2][3,4,5]
                middleIndex = length/2 - 1;
            }

            //声明新数组，记录数组左部索引变化
            var leftIndexArray = new int[middleIndex + 1];
            for (var i = 0; i <= middleIndex; i++)
            {
                leftIndexArray[i] = i;
            }

            //当前位置
            var currentIndex = middleIndex;

            //处理左部数组时，头部索引位置
            //var top = length % 2 == 0 ? 1 : 0;
            //统一为1，不论长度是奇数还是偶数
            var top = 1;
            if (length%2 == 1 && oddLengthAllowZeroIndex)
            {
                top = 0;
            }
            do
            {
                if (currentIndex == middleIndex)
                {
                    for (var i = leftIndexArray[middleIndex]; i < length; i++)
                    {
                        //记录组合数
                        factors.Add(CreateFactor(arr, leftIndexArray));
                        leftIndexArray[currentIndex]++;
                    }
                }
                currentIndex -= 1;
                while (currentIndex >= top)
                {
                    var indexValue = leftIndexArray[currentIndex];
                    if (indexValue < length - currentIndex) break;
                    currentIndex--;
                }
                if (currentIndex >= top)
                {
                    var indexValue = leftIndexArray[currentIndex];
                    for (var i = currentIndex; i <= middleIndex; i++)
                    {
                        leftIndexArray[i] = ++indexValue;
                    }
                    currentIndex = middleIndex;
                }
            } while (currentIndex >= top);
            return CreateFactors(factors);
        }


        /// <summary>
        ///     重新生成因子列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalFactors"></param>
        /// <returns></returns>
        private static List<Factor<T>> CreateFactors<T>(List<Factor<T>> originalFactors)
        {
            var factors = new List<Factor<T>>();
            foreach (var factor in originalFactors)
            {
                factors.Add(factor);
                factors.Add(new Factor<T> { Left = factor.Right, Right = factor.Left });
            }
            return factors;
        }


        /// <summary>
        ///     生成因子
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="leftIndexArray"></param>
        /// <returns></returns>
        private static Factor<T> CreateFactor<T>(List<T> arr, int[] leftIndexArray)
        {
            var leftArry = new List<T>();
            var rightArray = new List<T>();
            for (var i = 0; i < arr.Count; i++)
            {
                var item = arr[i];
                if (leftIndexArray.Contains(i))
                {
                    leftArry.Add(item);
                }
                else
                {
                    rightArray.Add(item);
                }
            }
            return new Factor<T> {Left = leftArry, Right = rightArray};
        }

    }

}