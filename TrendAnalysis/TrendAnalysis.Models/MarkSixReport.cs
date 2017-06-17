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

        public int FirstNum { get; set; }
        public int SecondNum { get; set; }
        public int ThirdNum { get; set; }
        public int FourthNum { get; set; }
        public int FifthNum { get; set; }
        public int SixthNum { get; set; }
        public int SeventhNum { get; set; }
        /// <summary>
        /// 开奖日期
        /// </summary>
        public DateTime AwardingDate { get; set; }
    }
}
