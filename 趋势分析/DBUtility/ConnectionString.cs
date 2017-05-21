using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBUtility
{
    internal class ConnectionString
    {
        public static string GetConnString()
        {
            //当前程序集路径
            string appDomainPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            //获取实例化数据操作对象
            string dal = System.Configuration.ConfigurationManager.AppSettings["DAL"];
            string connStr = string.Empty;
            switch (dal)
            {
                case "SQLServerDAL":
                default:
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServerDB"].ConnectionString;
                    break;
                case "OleDAL":
                    //拼接连接字符
                    connStr = "data source" + appDomainPath;
                    //最后一个字符是否为路径分隔符
                    if (connStr[connStr.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                    {
                        connStr += System.IO.Path.DirectorySeparatorChar;
                    }
                    connStr += System.Configuration.ConfigurationManager.ConnectionStrings["OldDB"].ConnectionString;
                    break;
                case "SQLiteDAL":
                    //拼接连接字符
                    connStr = "provider=Microsoft.jet.oledb.4.0;data source=" + appDomainPath;
                    //最后一个字符是否为路径分隔符
                    if (connStr[connStr.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                    {
                        connStr += System.IO.Path.DirectorySeparatorChar;
                    }
                    connStr += System.Configuration.ConfigurationManager.ConnectionStrings["SqLiteDB"].ConnectionString;
                    break;
            }
            return connStr;
        }
    }
}
