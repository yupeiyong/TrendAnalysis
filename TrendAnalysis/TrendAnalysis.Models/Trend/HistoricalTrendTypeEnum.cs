using System.ComponentModel;


namespace TrendAnalysis.Models.Trend
{

    /// <summary>
    ///     历史趋势类型
    /// </summary>
    public enum HistoricalTrendTypeEnum
    {
        /// <summary>
        /// MarkSix个位普通分析
        /// </summary>
        [Description("MarkSix个位普通分析")]
        NormalAnalyseMarkSixOnes = 1,
        /// <summary>
        /// MarkSix十位普通分析
        /// </summary>
        [Description("MarkSix十位普通分析")]
        NormalAnalyseMarkSixTens = 2,
        /// <summary>
        /// MarkSix个位多号码结合分析
        /// </summary>
        [Description("MarkSix个位多号码结合分析")]
        MultiNumberAnalyseMarkSixOnes = 3,
        /// <summary>
        /// MarkSix个位多号码结合分析
        /// </summary>
        [Description("MarkSix十位多号码结合分析")]
        MultiNumberAnalyseMarkSixTens = 4

    }

}