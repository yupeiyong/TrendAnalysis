using System.Collections.Generic;


namespace TrendAnalysis.Models.Trend
{

    /// <summary>
    ///     历史趋势明细项
    /// </summary>
    public class HistoricalTrendItem<T> : BaseEntity
    {

        /// <summary>
        ///     号码
        /// </summary>
        public T Number;

        /// <summary>
        ///     期次
        /// </summary>
        public string Times { get; set; }

        /// <summary>
        ///     连续次数
        /// </summary>
        public int ResultConsecutiveTimes { set; get; }


        /// <summary>
        ///     间隔数
        /// </summary>
        public int ResultInterval { get; set; }


        /// <summary>
        ///     是否正确
        /// </summary>
        public bool Success { get; set; }


        /// <summary>
        ///     可能的因子
        /// </summary>
        public List<T> PredictiveFactor { get; set; }

    }

}