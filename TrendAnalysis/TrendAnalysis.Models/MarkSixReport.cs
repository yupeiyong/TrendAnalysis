using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendAnalysis.Models
{
    /// <summary>
    /// MarkSix历史记录
    /// </summary>
    public class MarkSixReport:BaseEntity
    {
        /// <summary>
        /// 期次
        /// </summary>
        [Description("期次")]
        public string Times { get; set; }

        [Description("1")]
        public byte FirstNum { get; set; }

        [Description("2")]
        public byte SecondNum { get; set; }

        [Description("3")]
        public byte ThirdNum { get; set; }

        [Description("4")]
        public byte FourthNum { get; set; }

        [Description("5")]
        public byte FifthNum { get; set; }

        [Description("6")]
        public byte SixthNum { get; set; }

        [Description("7")]
        public byte SeventhNum { get; set; }

        [Description("开奖日期")]
        /// <summary>
        /// 开奖日期
        /// </summary>
        public DateTime AwardingDate { get; set; }
    }
}
