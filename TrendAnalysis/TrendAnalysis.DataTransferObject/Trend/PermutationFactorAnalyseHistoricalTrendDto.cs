using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendAnalysis.Models.Trend;

namespace TrendAnalysis.DataTransferObject.Trend
{
    /// <summary>
    /// 排列因子分析一段时期的历史趋势
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PermutationFactorAnalyseHistoricalTrendDto<T> : BaseAnalyseHistoricalTrendDto<T>
    {

        public List<Factor<T>> Factors { get; set; }


        /// <summary>
        /// 允许最大的间隔数（最大连续期数-指定期次此因子连续次数）
        /// </summary>
        public int AllowMaxInterval { get; set; } = int.MaxValue;


        /// <summary>
        /// 允许的最小指定期次此因子连续次数
        /// </summary>
        public int AllowMinFactorCurrentConsecutiveTimes { get; set; } = 1;


    }
}
