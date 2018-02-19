using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendAnalysis.Models.Trend
{
    /// <summary>
    /// 下一号码的因子分布状态
    /// </summary>
    public enum NextNumberFactorDistributionStateEnum
    {
        /// <summary>
        /// 未分析
        /// </summary>
        [Description("未分析")]
        None = 0,
        /// <summary>
        /// 分析成功
        /// </summary>
        [Description("分析成功")]
        Success = 1,
        /// <summary>
        /// 分析失败
        /// </summary>
        [Description("分析失败")]
        Fail = 2
    }
}
