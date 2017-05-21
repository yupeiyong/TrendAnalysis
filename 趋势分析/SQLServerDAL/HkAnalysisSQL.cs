using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SQLServerDAL
{
    public class HkAnalysisSQL:IDAL.IHkAnalysisSQL
    {
        /// <summary>
        /// 新增分析记录
        /// </summary>
        /// <param name="analysis"></param>
        /// <returns></returns>
        public int Add(Model.HkRecordAnalysis analysis)
        {
            string strInsert =
                @"INSERT INTO [t_record_analysis]
                           ([times]
                           ,[what_number]
                           ,[multiple]
                           ,[num_money]
                           ,[outlay]
                           ,[expect_income]
                           ,[num]
                           ,[relity_income]
                           ,[balance]
                           ,[writer]
                           ,[write_date]
                           ,[modify_date])
                     VALUES
                           (@times
                           ,@what_number
                           ,@multiple
                           ,@num_money
                           ,@outlay
                           ,@expect_income
                           ,@num
                           ,@relity_income
                           ,@balance
                           ,@writer
                           ,getdate()
                           ,getdate()
                            )";
            DBUtility.SQLServerHelper helper = new DBUtility.SQLServerHelper();
            //先删除记录
            Delete(analysis.Times);
            SqlParameter[] pars = new SqlParameter[10];
            pars[0] = new SqlParameter("@times", analysis.Times);
            pars[1] = new SqlParameter("@what_number", analysis.WhatNumber);
            pars[2] = new SqlParameter("@multiple", analysis.Multiple);
            pars[3] = new SqlParameter("@num_money", analysis.NumMoney);
            pars[4] = new SqlParameter("@outlay", analysis.Outlay);
            pars[5] = new SqlParameter("@expect_income", analysis.ExpectIncome);
            pars[6] = new SqlParameter("@num", analysis.RecordNum);
            pars[7] = new SqlParameter("@relity_income", analysis.RelityIncome);
            pars[8] = new SqlParameter("@balance", analysis.Balance);
            if (analysis.Writer != null)
            {
                pars[9] = new SqlParameter("@writer", analysis.Writer);
            }
            else
            {
                pars[9] = new SqlParameter("@writer", DBNull.Value);
            }
            //执行新增
            return helper.RunSQL(strInsert, pars);
        }
        /// <summary>
        /// 删除分析记录
        /// </summary>
        /// <param name="analysis"></param>
        public void Delete(Model.HkRecordAnalysis analysis)
        {
            //执行删除
            Delete(analysis.Times);
        }
        /// <summary>
        /// 删除分析记录
        /// </summary>
        /// <param name="times"></param>
        public void Delete(string times)
        {
            string strDel = "DELETE FROM [t_record_analysis] " +
                "WHERE [times]=@times";
            SqlParameter[] pars = new SqlParameter[1];
            pars[0] = new SqlParameter("@times", times);
            DBUtility.SQLServerHelper helper = new DBUtility.SQLServerHelper();
            //执行删除
            helper.RunSQL(strDel, pars);
        }
        /// <summary>
        /// 更新分析记录
        /// </summary>
        /// <param name="analysis"></param>
        /// <returns></returns>
        public int Update(Model.HkRecordAnalysis analysis)
        {
            string strUpdate =
                @"UPDATE [t_record_analysis]
                   SET [what_number] = @what_number
                      ,[multiple] = @multiple
                      ,[num_money] = @num_money
                      ,[outlay] = @outlay
                      ,[expect_income] = @expect_income
                      ,[num] = @num
                      ,[relity_income] = @relity_income
                      ,[balance] = @balance
                      ,[writer] = @writer
                      ,[write_date] = @write_date
                      ,[modify_date] = getdate()
                 WHERE [times] = @times";
            SqlParameter[] pars = new SqlParameter[11];
            pars[0] = new SqlParameter("@times", analysis.Times);
            pars[1] = new SqlParameter("@what_number", analysis.WhatNumber);
            pars[2] = new SqlParameter("@multiple", analysis.Multiple);
            pars[3] = new SqlParameter("@num_money", analysis.NumMoney);
            pars[4] = new SqlParameter("@outlay", analysis.Outlay);
            pars[5] = new SqlParameter("@expect_income", analysis.ExpectIncome);
            pars[6] = new SqlParameter("@num", analysis.RecordNum);
            pars[7] = new SqlParameter("@relity_income", analysis.RelityIncome);
            pars[8] = new SqlParameter("@balance", analysis.Balance);
            if (analysis.Writer != null)
            {
                pars[9] = new SqlParameter("@writer", analysis.Writer);
            }
            else
            {
                pars[9] = new SqlParameter("@writer", DBNull.Value);
            }
            pars[10] = new SqlParameter("@write_date", analysis.WriteDate);
            DBUtility.SQLServerHelper helper = new DBUtility.SQLServerHelper();
            //执行更新
            return helper.RunSQL(strUpdate, pars);
        }
        /// <summary>
        /// 获取所有分析记录
        /// </summary>
        /// <returns></returns>
        public List<Model.HkRecordAnalysis> GetData() 
        {
            //按升序查找所有记录
            string strSql = "SELECT *  FROM [t_record_analysis] ORDER BY [times] ASC";
            SqlDataReader read;
            //分析列表
            List<Model.HkRecordAnalysis> lAnalysis = new List<Model.HkRecordAnalysis>();
            DBUtility.SQLServerHelper helper = new DBUtility.SQLServerHelper();
            //执行更新
            helper.RunSQL(strSql,out read);
            while (read.Read())
            {
                Model.HkRecordAnalysis analysis = new Model.HkRecordAnalysis();
                analysis.Times = read.GetString(0);
                analysis.WhatNumber = read.GetByte(1);
                analysis.Multiple = read.GetInt16(2);
                analysis.NumMoney = read.GetString(3);
                analysis.Outlay = read.GetDouble(4);
                analysis.ExpectIncome = read.GetString(5);
                analysis.RecordNum = read.GetInt16(6);
                analysis.RelityIncome = read.GetDouble(7);
                analysis.Balance = read.GetDouble(8);
                object value = read[9];
                if (value != null)
                {
                    analysis.Writer = value.ToString();
                }
                analysis.WriteDate = read.GetDateTime(10);
                analysis.ModifyDate = read.GetDateTime(11);
                lAnalysis.Add(analysis);
            }
            return lAnalysis;
        }
        /// <summary>
        /// 按期次获取分析记录
        /// </summary>
        /// <param name="times"></param>
        /// <returns></returns>
        public Model.HkRecordAnalysis GetDataByTimes(string times)
        {
            string strSql =
                @"SELECT [times]
                      ,[what_number]
                      ,[multiple]
                      ,[num_money]
                      ,[outlay]
                      ,[expect_income]
                      ,[num]
                      ,[relity_income]
                      ,[balance]
                      ,[writer]
                      ,[write_date]
                      ,[modify_date]
                  FROM [t_record_analysis]";
            SqlDataReader read;
            //分析列表
            Model.HkRecordAnalysis analysis = new Model.HkRecordAnalysis();
            DBUtility.SQLServerHelper helper = new DBUtility.SQLServerHelper();
            //执行更新
            helper.RunSQL(strSql, out read);
            if (read.Read())
            {
                analysis.Times = read.GetString(0);
                analysis.WhatNumber = read.GetByte(1);
                analysis.Multiple = read.GetInt16(2);
                analysis.NumMoney = read.GetString(3);
                analysis.Outlay = read.GetDouble(4);
                analysis.ExpectIncome = read.GetString(5);
                analysis.RecordNum = read.GetByte(6);
                analysis.RelityIncome = read.GetDouble(7);
                analysis.Balance = read.GetDouble(8);
                analysis.Writer = read.GetString(9);
                analysis.WriteDate = read.GetDateTime(10);
                analysis.ModifyDate = read.GetDateTime(11);
            }
            return analysis;
        }
        /// <summary>
        /// 按条件获取分析记录
        /// </summary>
        /// <param name="lCondition"></param>
        /// <returns></returns>
        public DataTable GetDataByCondition(List<Model.Condition> lCondition)
        {
            if (lCondition == null)
            {
                string strSelect = "select top 0 * from [t_record_analysis]";
                DataSet ds = new DataSet();
                DBUtility.SQLServerHelper helper = new DBUtility.SQLServerHelper();
                helper.RunSQL(strSelect, ref ds);
                if (ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                string strWhere = string.Empty;
                foreach (Model.Condition condition in lCondition)
                {
                    if (IDAL.CheckParamValues.CheckKeywords(condition.paramValue))
                    {
                        throw new Exception("包含有非法查询字符！");
                    }
                    string str = IDAL.CheckParamValues.GetConditions(condition);
                    if (str.Length > 0)
                    {
                        strWhere += " and " + str;
                    }
                }
                if (strWhere.Length > 5)
                {
                    strWhere = " where " + strWhere.Substring(5);
                }
                string strSelect = "select * from [t_record_analysis] " + strWhere;
                DataSet ds = new DataSet();
                DBUtility.SQLServerHelper helper = new DBUtility.SQLServerHelper();
                helper.RunSQL(strSelect, ref ds);
                if (ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
