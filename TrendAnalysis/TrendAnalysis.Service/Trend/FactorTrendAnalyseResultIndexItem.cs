namespace TrendAnalysis.Service.Trend
{

    /// <summary>
    ///     因子分析结果，索引明细
    /// </summary>
    public class FactorTrendConsecutiveDistributionRowDetails
    {

        /// <summary>
        ///     号码索引位置
        /// </summary>
        public int Index { get; set; }


        /// <summary>
        ///     当前索引位置的因子连续次数
        /// </summary>
        public int ConsecutiveTimes { get; set; }


        /// <summary>
        ///     最大因子连续次数－当前索引位置的因子连续次数
        /// </summary>
        public int MaxConsecutiveTimesInterval { get; set; }

    }

}