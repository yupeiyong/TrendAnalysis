using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 趋势分析
{
    public class Analysis
    {
        /// <summary>
        /// 取得排列好的二维数组
        /// </summary>
        /// <param name="lNum">数字列表</param>
        /// <param name="weisu">维数</param>
        /// <param name="compareCount">比较次数</param>
        /// <returns></returns>
        public List<T[,]> GetArray<T>(List<T> lNum, int weisu, uint compareCount)
        {
            if (lNum.Count < weisu * compareCount)
            {
                throw new Exception("期数小于分析比较次数乘以维数！");
            }
            List<T[,]> lByteArray = new List<T[,]>();
            for (int c = 1; c <= compareCount; c++)
            {
                T[,] array = new T[c, weisu];
                int curCount = c;
                for (int i = 0; i < c; i++)
                {
                    for (int w = 0; w < weisu; w++)
                    {
                        //当前比较次数*（当前维数+1）-（比较次数+1）
                        array[i, w] = lNum[curCount * (w + 1) - (i + 1)];
                    }
                }
                lByteArray.Add(array);
            }
            return lByteArray;
        }
    }
}
