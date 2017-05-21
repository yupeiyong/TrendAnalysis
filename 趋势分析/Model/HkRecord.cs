using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class HkRecord
    {
        /// <summary>
        /// id号
        /// </summary>
        public long Id{get;set;}
        /// <summary>
        /// 记录日期
        /// </summary>
        public DateTime RecordDate{get;set;}
        /// <summary>
        /// 期次
        /// </summary>
        public string Times{get;set;}
        /// <summary>
        /// 第一个数字
        /// </summary>
        public byte FirstNum{get;set;}
        /// <summary>
        /// 第二个数字
        /// </summary>
        public byte SecondNum{get;set;}
        /// <summary>
        /// 第三个数字
        /// </summary>
        public byte ThirdNum{get;set;}
        /// <summary>
        /// 第四个数字
        /// </summary>
        public byte FourthNum{get;set;}
        /// <summary>
        /// 第五个数字
        /// </summary>
        public byte FifthNum{get;set;}
        /// <summary>
        /// 第六个数字
        /// </summary>
        public byte SixthNum{get;set;}
        /// <summary>
        /// 第七个数字
        /// </summary>
        public byte SeventhNum{get;set;}
    }
}
