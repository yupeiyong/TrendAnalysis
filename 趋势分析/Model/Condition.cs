using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Model
{
    public class Condition
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string paramName { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        public string paramValue { get; set; }
        /// <summary>
        /// 字段数据类型
        /// </summary>
        public FieldDbType FieldType { get; set; }
        public enum FieldDbType
        {
            Number,//数字
            String,//字符串
            DateTime//日期时间
        }
    }
}
