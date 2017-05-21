using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analysis
{
    /// <summary>
    /// 当前位置的数字奇偶与左右状态
    /// </summary>
    public struct State
    {
        /// <summary>
        /// 是否为奇数
        /// </summary>
        public bool? IsOdd { get; set; }
        /// <summary>
        /// 是否为右数
        /// </summary>
        public bool? IsInRight { get; set; }
    }
}
