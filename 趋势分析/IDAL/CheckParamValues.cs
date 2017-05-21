using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IDAL
{
    public class CheckParamValues
    {
        /// <summary>
        /// 检查是否有非法查询字符
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool CheckKeywords(string value)
        {
            if (Regex.IsMatch(value.ToLower(), "declare|exec|execute|truncate|drop|delete|insert|update|select|count|min|max|in|exists|or|and|between|--"))
            {
                return true;
            }
            return false;
        }
        public static string GetConditions(Model.Condition condition)
        {
            string paramValue = condition.paramValue;
            string[] values = paramValue.Split(new char[] {';','；' },StringSplitOptions.RemoveEmptyEntries);
            //最后拼接的条件语句
            string strWhere = string.Empty;
            foreach (var value in values)
            {
                if (value == "=")
                {
                    strWhere +=" or "+ condition + " is null ";
                }
                else if (value == "<>")
                {
                    strWhere +=" or "+ condition + " is not null ";
                }
                else
                {
                    switch (condition.FieldType)
                    {
                        case Model.Condition.FieldDbType.String:
                        default:
                            strWhere += GetFromText(condition.paramName, value);
                            break;
                        case Model.Condition.FieldDbType.DateTime:
                            strWhere +=GetFromDate(condition.paramName, value);
                            break;
                        case Model.Condition.FieldDbType.Number:
                            strWhere +=GetFromNumber(condition.paramName, value);
                            break;
                    }
                }
            }
            if (strWhere.Length > 4)
            {
                strWhere = "(" + strWhere.Substring(4) + ")";
            }
            return strWhere;
        }
        private static string GetFromText(string fieldName, string content)
        {
            return " or " + fieldName + " like '" + content
                .Replace("'", "''")
                .Replace("_", "[_]")
                .Replace("%", "[%]")
                .Replace("\\", "[\\]")
                .Replace("\"", "[\"]")
                .Replace("*", "%")
                .Replace("?", "_")
                .Replace("[", "[[]")
                .Replace("]", "[]]")
                .Replace("~%", "*")
                .Replace("~_", "?")
                +"'";
        }
        private static string GetFromDate(string fieldName, string content)
        {
            //条件查询语句
            string strWhere = string.Empty;
            if (content.StartsWith(">=") || content.StartsWith("<=") || content.StartsWith("<>"))
            {
                DateTime dt;
                if (DateTime.TryParse(content.Substring(2), out dt))
                {
                    strWhere += " or "+fieldName+content.Substring(0,2)+"'"+dt.ToString("yyyy-MM-dd HH:mm:ss")+"'";
                }
                else
                {
                    strWhere += GetFromText("convert(nvarchar(max),"+fieldName+")", content);
                }
            }
            else if (content.StartsWith("<") || content.StartsWith(">") || content.StartsWith("="))
            {
                DateTime dt;
                if (DateTime.TryParse(content.Substring(1), out dt))
                {
                    strWhere += " or " + fieldName + content.Substring(0, 1) + "'" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                }
                else
                {
                    strWhere += GetFromText("convert(nvarchar(max)," + fieldName + ")", content);
                }
            }
            else if (content.Contains("..."))
            {
                DateTime dt1, dt2;
                int index = content.IndexOf("...");
                if (DateTime.TryParse(content.Substring(0, index), out dt1) && DateTime.TryParse(content.Substring(index + 3), out dt2))
                {
                    strWhere += " or " + fieldName +  "between '" + dt1.ToString("yyyy-MM-dd HH:mm:ss") + "' and '"+dt2.ToString("yyyy-MM-dd HH:mm:ss")+"'";
                }
                else
                {
                    strWhere += GetFromText("convert(nvarchar(max)," + fieldName + ")", content);
                }
            }
            else
            {
                strWhere += " or " + fieldName + "='" + content + "'";
            }
            return strWhere;
        }
        private static string GetFromNumber(string fieldName, string content)
        {
            //条件查询语句
            string strWhere = string.Empty;
            if (content.StartsWith(">=") || content.StartsWith("<=") || content.StartsWith("<>"))
            {
                decimal d;
                if (decimal.TryParse(content.Substring(2), out d))
                {
                    strWhere += " or " + fieldName + content.Substring(0, 2) + d.ToString();
                }
                else
                {
                    strWhere += GetFromText("convert(nvarchar(max)," + fieldName + ")", content);
                }
            }
            else if (content.StartsWith("<") || content.StartsWith(">") || content.StartsWith("="))
            {
                decimal d;
                if (decimal.TryParse(content.Substring(1), out d))
                {
                    strWhere += " or " + fieldName + content.Substring(0, 1) + d.ToString();
                }
                else
                {
                    strWhere += GetFromText("convert(nvarchar(max)," + fieldName + ")", content);
                }
            }
            else if (content.Contains("..."))
            {
                decimal d1, d2;
                int index = content.IndexOf("...");
                if (decimal.TryParse(content.Substring(0, index), out d1) && decimal.TryParse(content.Substring(index + 3), out d2))
                {
                    strWhere += " or " + fieldName + "between " + d1.ToString() + " and " + d2.ToString() ;
                }
                else
                {
                    strWhere += GetFromText("convert(nvarchar(max)," + fieldName + ")", content);
                }
            }
            else
            {
                strWhere += " or " + fieldName + "=" + content;
            }
            return strWhere;
        }
    }
}
