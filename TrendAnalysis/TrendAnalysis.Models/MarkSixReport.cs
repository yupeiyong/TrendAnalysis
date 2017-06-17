using System;
using System.Collections.Generic;
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
        public string Times { get; set; }

        public byte FirstNum { get; set; }

        public byte SecondNum { get; set; }

        public byte ThirdNum { get; set; }

        public byte FourthNum { get; set; }

        public byte FifthNum { get; set; }

        public byte SixthNum { get; set; }

        public byte SeventhNum { get; set; }
        /// <summary>
        /// 开奖日期
        /// </summary>
        public DateTime AwardingDate { get; set; }
    }
}
