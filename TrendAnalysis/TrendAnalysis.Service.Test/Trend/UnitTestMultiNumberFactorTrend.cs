using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrendAnalysis.DataTransferObject.Trend;
using TrendAnalysis.Service.Trend;
using System.Collections.Generic;
using TrendAnalysis.Models.Trend;

namespace TrendAnalysis.Service.Test.Trend
{
    [TestClass]
    public class UnitTestMultiNumberFactorTrend
    {
        [TestMethod]
        public void TestAnalyseConsecutives()
        {
             var numbers = new List<byte>() {1,2,3,4 };
            var factors = new List<Factor<byte>> { };
            /*
              public List<T> Numbers { get; set; }

        public List<Factor<T>> Factors { get; set; }


        /// <summary>
        /// 多号码因子最大个数
        /// </summary>
        public int MultiNumberMaxCount { get; set; }


        /// <summary>
        /// 允许最大的间隔数（最大连续期数-指定期次此因子连续次数）
        /// </summary>
        public int AllowMaxInterval { get; set; } = int.MaxValue;


        /// <summary>
        /// 分析集合时，允许的最小连续数，大于等于此数才记录连续次数
        /// </summary>
        public int AllowMinTimes { get; set; } = 1;


        /// <summary>
        /// 允许的最小指定期次此因子连续次数
        /// </summary>
        public int AllowMinFactorCurrentConsecutiveTimes { get; set; } = 1;


        /// <summary>
        /// 记录尾部切去数量，比如原长度100，切去10，最终保留90
        /// </summary>
        public int NumbersTailCutCount { get; set; }


        /// <summary>
        /// 分析连续次数时的比较器
        /// </summary>
        public Func<IReadOnlyList<T>, List<T>, int, bool> AnalyseConsecutiveCompareFunc { get; set; }

        /// <summary>
        /// 分析可能因子的比较器
        /// </summary>
        public Func<IReadOnlyList<T>, List<T>, int, bool> AnalysePredictiveCompareFunc { get; set; }
             
             */
            var dto = new MultiNumberFactorTrendAnalyseDto<byte>
            {
                MultiNumberMaxCount=16,
                AllowMaxInterval=0,
                AllowMinTimes=2,
                AllowMinFactorCurrentConsecutiveTimes=6,
                NumbersTailCutCount=6
            };
            var reslutList = MultiNumberFactorTrend.AnalyseConsecutives<byte>(dto);
        }
    }
}
