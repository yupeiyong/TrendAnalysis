using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class HkRecordAnalysis
    {
        /// <summary>
        /// 期次
        /// </summary>
        public string Times { get; set; }
        /// <summary>
        /// 第几位记录
        /// </summary>
        public byte WhatNumber { get; set; }
        /// <summary>
        /// 倍率
        /// </summary>
        public int Multiple { get; set; }
        /// <summary>
        /// 号码及金额
        /// </summary>
        public string NumMoney { get; set; }
        /// <summary>
        /// 支出
        /// </summary>
        public double Outlay { get; set; }
        /// <summary>
        /// 预期收益
        /// </summary>
        public string ExpectIncome { get; set; }
        /// <summary>
        /// 实际记录号码
        /// </summary>
        public Int16 RecordNum { get; set; }
        /// <summary>
        /// 实际收益
        /// </summary>
        public double RelityIncome { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public double Balance { get; set; }
        /// <summary>
        /// 填写人
        /// </summary>
        public string Writer { get; set; }
        /// <summary>
        /// 填写日期
        /// </summary>
        public DateTime WriteDate { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime ModifyDate { get; set; }
    }
}
