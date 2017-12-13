using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendAnalysis.Models.Trend
{
    /// <summary>
    /// 历史趋势明细项
    /// </summary>
    public class HistoricalTrendItem<T>
    {
        /// <summary>
        /// 期次
        /// </summary>
        public string Times { get; set; }

        /// <summary>
        /// 号码
        /// </summary>
        public T Number;

        /// <summary>
        /// 连续次数
        /// </summary>
        public int ResultConsecutiveTimes { set; get; }


        /// <summary>
        /// 间隔数
        /// </summary>
        public int ResultInterval { get; set; }


        /// <summary>
        /// 是否正确
        /// </summary>
        public bool Success { get; set; }


        public List<T> OppositeFactor { get; set; }
    }
}
