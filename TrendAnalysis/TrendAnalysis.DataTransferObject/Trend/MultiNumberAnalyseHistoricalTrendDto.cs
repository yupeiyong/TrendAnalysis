using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendAnalysis.Models.Trend;

namespace TrendAnalysis.DataTransferObject.Trend
{
    public class MultiNumberAnalyseHistoricalTrendDto<T>
    {

        /// <summary>
        ///     第几位数
        /// </summary>
        public int Location { get; set; }


        /// <summary>
        ///     号码记录
        /// </summary>
        public List<TemporaryRecord<T>> Numbers { get; set; }


        /// <summary>
        ///     分析因子
        /// </summary>
        public List<Factor<T>> Factors { get; set; }


        /// <summary>
        ///     分析多少位号码
        /// </summary>
        public int AnalyseNumberCount { get; set; }

        /// <summary>
        ///     开始的允许最大的间隔数（最大连续期数-指定期次此因子连续次数），由小到大
        /// </summary>
        public int StartAllowMaxInterval { get; set; } = int.MaxValue;

        /// <summary>
        ///     结束的允许最大的间隔数（最大连续期数-指定期次此因子连续次数），由小到大
        /// </summary>
        public int EndAllowMaxInterval { get; set; } = int.MaxValue;


        /// <summary>
        ///     开始的允许的最小指定期次此因子连续次数，由大到小
        /// </summary>
        public int StartAllowMinFactorCurrentConsecutiveTimes { get; set; } = 1;


        /// <summary>
        ///     结束的允许的最小指定期次此因子连续次数，由大到小
        /// </summary>
        public int EndAllowMinFactorCurrentConsecutiveTimes { get; set; } = 1;


        /// <summary>
        ///     分析集合时，允许的最小连续数，大于等于此数才记录连续次数
        /// </summary>
        public int AllowMinTimes { get; set; } = 1;


        /// <summary>
        ///     记录尾部切去数量，比如原长度100，切去10，最终保留90
        /// </summary>
        public int NumbersTailCutCount { get; set; }

        public int MultiNumberMaxCount { get; set; }


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

        /// <summary>
        /// 分析类型
        /// </summary>
        public HistoricalTrendTypeEnum HistoricalTrendType { get; set; }

        /// <summary>
        ///     类型说明
        /// </summary>
        public string TypeDescription { get; set; }
    }

}