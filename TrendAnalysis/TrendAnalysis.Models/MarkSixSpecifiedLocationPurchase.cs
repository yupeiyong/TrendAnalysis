using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendAnalysis.Models
{
    /// <summary>
    /// MarkSix指定位置购买
    /// </summary>
    public class MarkSixSpecifiedLocationPurchase : BaseEntity
    {
        public string Times { get; set; }

        /// <summary>
        /// 赔率
        /// </summary>
        public int Odds { get; set; }
        /// <summary>
        /// 指定的位置
        /// </summary>
        public byte Location { get; set; }

        /// <summary>
        /// 购买清单
        /// </summary>
        public string PurchaseList { get; set; }


        /// <summary>
        /// 购买金额
        /// </summary>
        public int PurchaseAmount { get; set; }


        /// <summary>
        /// 开奖号码
        /// </summary>
        public byte AwardingNum { get; set; }

        /// <summary>
        /// 当次收益
        /// </summary>
        public int Profit { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public int Balance { get; set; }

        public DateTime OnCreated { get; set; }

        public DateTime OnModified { get; set; }
    }
}
