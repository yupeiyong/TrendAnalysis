using System;


namespace TrendAnalysis.DataTransferObject
{

    /// <summary>
    ///     分析记录的数据传输对象
    /// </summary>
    public class MarkSixAnalyseSpecifiedLocationDto
    {

        /// <summary>
        ///     第几位数字
        /// </summary>
        public int Location { get; set; }


        /// <summary>
        ///     期次
        /// </summary>
        public string Times { get; set; }


        /// <summary>
        ///     十位数，允许最大的间隔数（最大连续期数-指定期次此因子连续次数）
        /// </summary>
        [Obsolete]
        public int TensAllowMaxInterval { get; set; } = int.MaxValue;


        /// <summary>
        ///     个位数，允许最大的间隔数（最大连续期数-指定期次此因子连续次数）
        /// </summary>
        [Obsolete]
        public int OnesAllowMaxInterval { get; set; } = int.MaxValue;


        /// <summary>
        ///     十位数，分析集合时，允许的最小连续数，大于等于此数才记录连续次数
        /// </summary>
        [Obsolete]
        public int TensAllowMinTimes { get; set; } = 1;

        /// <summary>
        ///     个位数，分析集合时，允许的最小连续数，大于等于此数才记录连续次数
        /// </summary>
        [Obsolete]
        public int OnesAllowMinTimes { get; set; } = 1;


        /// <summary>
        ///     十位数，允许的最小指定期次此因子连续次数
        /// </summary>
        [Obsolete]
        public int TensAllowMinFactorCurrentConsecutiveTimes { get; set; } = 1;


        /// <summary>
        ///     个位数，允许的最小指定期次此因子连续次数
        /// </summary>
        [Obsolete]
        public int OnesAllowMinFactorCurrentConsecutiveTimes { get; set; } = 1;


        /// <summary>
        ///     十位数，记录尾部切去数量，比如原长度100，切去10，最终保留90
        /// </summary>
        [Obsolete]
        public int TensNumbersTailCutCount { get; set; }


        /// <summary>
        ///     个位数，记录尾部切去数量，比如原长度100，切去10，最终保留90
        /// </summary>
        [Obsolete]
        public int OnesNumbersTailCutCount { get; set; }


        /// <summary>
        ///     个位累加的连续次数
        /// </summary>
        public int OnesAddConsecutiveTimes { get; set; }


        /// <summary>
        ///     十位累加的连续次数
        /// </summary>
        public int TensAddConsecutiveTimes { get; set; }


        /// <summary>
        ///     个位累加的间隔数
        /// </summary>
        public int OnesAddInterval { get; set; }


        /// <summary>
        ///     十位累加的间隔数
        /// </summary>
        public int TensAddInterval { get; set; }

        /// <summary>
        ///     取记录条数（不用取全部条数，否则连续次数会无穷递增）
        /// </summary>
        public int NumberTakeCount { get; set; } = 200;


        /// <summary>
        /// 个位和十位都必须包含
        /// </summary>
        public bool OnesAndTensMustContain { get; set; } = true;

    }

}