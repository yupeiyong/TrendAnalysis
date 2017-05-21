using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 趋势分析
{
    /// <summary>
    /// 当前记录状态（奇、偶、大、小）
    /// </summary>
    class ReportState
    {
        private bool? _isTenOdd = null;
        /// <summary>
        /// 十位是否为奇数
        /// </summary>
        public bool? IsTenOdd
        {
            get { return _isTenOdd; }
            set { _isTenOdd = value; }
        }
        private bool? _isTenBig = null;
        /// <summary>
        /// 十位是否为大数
        /// </summary>
        public bool? IsTenBig
        {
            get { return _isTenBig; }
            set { _isTenBig = value; }
        }
        private bool? _isGoweiOdd = null;
        /// <summary>
        /// 个位是否为奇数
        /// </summary>
        public bool? IsGoweiOdd
        {
            get { return _isGoweiOdd; }
            set { _isGoweiOdd = value; }
        }
        private bool? _isGoweiBig = null;
        /// <summary>
        /// 个位是否为大数
        /// </summary>
        public bool? IsGoweiBig
        {
            get { return _isGoweiBig; }
            set { _isGoweiBig = value; }
        }
    }
}
