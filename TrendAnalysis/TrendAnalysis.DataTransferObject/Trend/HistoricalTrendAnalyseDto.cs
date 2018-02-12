using System;
using System.Collections.Generic;
using TrendAnalysis.Models.Trend;


namespace TrendAnalysis.DataTransferObject.Trend
{

    /// <summary>
    ///     分析一段时期历史趋势的传输对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Obsolete]
    public class HistoricalTrendAnalyseDto<T> : BaseHistoricalTrendAnalyseDto<T>
    {

        /// <summary>
        ///     分析因子
        /// </summary>
        public List<Factor<T>> Factors { get; set; }
    }

}