using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendAnalysis.Models.Trend;

namespace TrendAnalysis.DataTransferObject.Trend
{
    /// <summary>
    /// 多号码因子趋势分析传输对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MultiNumberFactorTrendAnalyseDto<T>
    {
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
        /// 分析可能连续次数的比较器
        /// </summary>
        public Func<IReadOnlyList<T>, List<T>, int, bool> PredictiveConsecutivesCompareFunc { get; set; }


        /// <summary>
        /// 可能因子的分析器
        /// </summary>
        public Action<IReadOnlyList<T>, List<T>> PredictiveFactorAction { get; set; }
    }
}
