using TrendAnalysis.Models.Trend;

namespace TrendAnalysis.Service.Trend
{

    /// <summary>
    ///     因子分布明细
    /// </summary>
    public class FactorDistribution
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
        ///      和最大连续次数的间隔，
        ///      最大因子连续次数－当前索引位置的因子连续次数,为负数表示当前连续次数大于历史最大连续次数
        /// </summary>
        public int MaxConsecutiveTimesInterval { get; set; }       
    }

}