using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DBFactory
{
    public class CreateSQL
    {
        /// <summary>
        /// 读取配置文件中指定实例化的项目
        /// </summary>
        private static string dal = System.Configuration.ConfigurationManager.AppSettings["DAL"];
        /// <summary>
        /// 创建记录数据层实例
        /// </summary>
        /// <returns></returns>
        public static IDAL.IHkRecordSQL CreateRecord()
        {
            //实例项目中的类
            return (IDAL.IHkRecordSQL)Assembly.Load(dal).CreateInstance(dal+".HkRecordSQL");
        }
        /// <summary>
        /// 创建分析数据层实例
        /// </summary>
        /// <returns></returns>
        public static IDAL.IHkAnalysisSQL CreateAnalysis()
        {
            //实例项目中的类
            return (IDAL.IHkAnalysisSQL)Assembly.Load(dal).CreateInstance(dal + ".HkAnalysisSQL");
        }
    }
}
