using System.ComponentModel;


namespace TrendAnalysis.Models.Trend
{

    /// <summary>
    ///     历史趋势类型
    /// </summary>
    public enum HistoricalTrendTypeEnum
    {

        /// <summary>
        ///     MarkSix普通分析
        /// </summary>
        [Description("MarkSix普通分析")] MarkSixNormal = 1,

        /// <summary>
        ///     MarkSix多号码结合分析
        /// </summary>
        [Description("MarkSix多号码结合分析")] MarkSixMultiNumber = 2

    }

}