using System.Collections.Generic;
using System.Linq;


namespace TrendAnalysis.Service.Trend
{

    /// <summary>
    ///     因子连续分布
    /// </summary>
    /// <typeparam name="T">因子类型</typeparam>
    public class FactorTrendConsecutiveDetails<T>
    {

        /// <summary>
        ///     因子
        /// </summary>
        public List<T> Factor { get; set; }


        /// <summary>
        ///     预测的可能因子（反因子）
        /// </summary>
        public List<T> PredictiveFactor { get; set; }

        /// <summary>
        ///     历史的连续次数分析结果，键为次数，值为数量  表示每个连续次数出现的次数
        /// </summary>
        public SortedDictionary<int, int> ConsecutiveDistributions { get; set; } = new SortedDictionary<int, int>();


        /// <summary>
        ///     因子分布的明细结果
        /// </summary>
        public List<FactorDistribution> FactorDistributions { get; set; }

        /// <summary>
        ///     因子最大连续次数
        /// </summary>
        public int MaxConsecutiveTimes { get; set; }


        /// <summary>
        ///     最大连续期数-指定期次此因子连续次数的间隔数，数越小，表示变化的趋势越大
        /// </summary>
        public int MaxInterval => ConsecutiveDistributions == null
            || ConsecutiveDistributions.Count == 0 ? 0 : ConsecutiveDistributions.Max(k => k.Key) - MaxConsecutiveTimes;

    }

}