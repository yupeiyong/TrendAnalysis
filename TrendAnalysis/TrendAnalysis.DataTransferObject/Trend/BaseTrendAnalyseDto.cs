using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendAnalysis.DataTransferObject.Trend
{
    /// <summary>
    /// 趋势分析传输对象基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseTrendAnalyseDto<T>
    {
        /// <summary>
        /// 号码列表
        /// </summary>
        public List<T> Numbers { get; set; }

        /// <summary>
        /// 允许最大的间隔数（最大连续期数-指定期次此因子连续次数）
        /// </summary>
        [Obsolete]
        public int AllowMaxInterval { get; set; } = int.MaxValue;


        /// <summary>
        /// 分析集合时，允许的最小连续数，大于等于此数才记录连续次数
        /// </summary>
        public int AllowMinTimes { get; set; } = 1;


        /// <summary>
        /// 允许的最小指定期次此因子连续次数
        /// </summary>
        [Obsolete]
        public int AllowMinFactorCurrentConsecutiveTimes { get; set; } = 1;


        /// <summary>
        /// 记录尾部切去数量，比如原长度100，切去10，最终保留90
        /// </summary>
        [Obsolete]
        public int NumbersTailCutCount { get; set; }

        /// <summary>
        ///     分析多少位历史趋势
        /// </summary>
        public int AnalyseHistoricalTrendCount { get; set; } = 100;

        /// <summary>
        ///     累加的连续次数
        /// </summary>
        public int AddConsecutiveTimes { get; set; }


        /// <summary>
        ///     累加的间隔数
        /// </summary>
        public int AddInterval { get; set; }

    }
}
