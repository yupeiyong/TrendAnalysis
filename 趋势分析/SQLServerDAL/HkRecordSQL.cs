using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace SQLServerDAL
{
    public class HkRecordSQL:IDAL.IHkRecordSQL 
    {
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="record">记录信息</param>
        public int AddRecord(Model.HkRecord record)
        {
            //先删除相同的记录
            DeleteRecord(record.RecordDate, record.Times);
            string strInsert = "insert into [LuHeRecord]" +
                "([record_date]" +
              ",[times]" +
              ",[first_num]" +
              ",[second_num]" +
              ",[third_num]" +
              ",[fourth_num]" +
              ",[fifth_num]" +
              ",[sixth_num]" +
              ",[seventh_num])" +
              "values" +
              "(@record_date" +
              ",@times" +
              ",@first_num" +
              ",@second_num" +
              ",@third_num" +
              ",@fourth_num" +
              ",@fifth_num" +
              ",@sixth_num" +
              ",@seventh_num)";
            SqlParameter[] pars = new SqlParameter[9];
            pars[0] = new SqlParameter("@record_date", record.RecordDate);
            pars[1] = new SqlParameter("@times", record.Times);
            pars[2] = new SqlParameter("@first_num", record.FirstNum);
            pars[3] = new SqlParameter("@second_num", record.SecondNum);
            pars[4] = new SqlParameter("@third_num", record.ThirdNum);
            pars[5] = new SqlParameter("@fourth_num", record.FourthNum);
            pars[6] = new SqlParameter("@fifth_num", record.FifthNum);
            pars[7] = new SqlParameter("@sixth_num", record.SixthNum);
            pars[8] = new SqlParameter("@seventh_num", record.SeventhNum);
            DBUtility.SQLServerHelper helper = new DBUtility.SQLServerHelper();
            return helper.RunSQL(strInsert, pars);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="nId">id号</param>
        public void DeleteRecord(long nId)
        {
            string strDelete = "delete from [LuHeRecord] where [id]=@id";
            SqlParameter[] pars = new SqlParameter[1];
            pars[0] = new SqlParameter("@id", nId);
            DBUtility.SQLServerHelper helper = new DBUtility.SQLServerHelper();
            helper.RunSQL(strDelete, pars);
        }
        /// 删除
        /// </summary>
        /// <param name="nId">id号</param>
        public void DeleteRecord(DateTime record_date, string times)
        {
            string strDelete = "delete from [LuHeRecord] where [record_date]=@record_date and [times]=@times";
            SqlParameter[] pars = new SqlParameter[2];
            pars[0] = new SqlParameter("@record_date", record_date);
            pars[1] = new SqlParameter("@times", times);
            DBUtility.SQLServerHelper helper = new DBUtility.SQLServerHelper();
            helper.RunSQL(strDelete, pars);
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="record">记录信息</param>
        public int UpdateRecord(Model.HkRecord record,long oldId)
        {
            string strUpdate = "update [LuHeRecord] " +
              "set [id]=@id "+
              "[record_date]=@record_date" +
              ",[times]=@times" +
              ",[first_num]=@first_num" +
              ",[second_num]=@second_num" +
              ",[third_num]=@third_num" +
              ",[fourth_num]=@fourth_num" +
              ",[fifth_num]=@fifth_num" +
              ",[sixth_num]=@sixth_num" +
              ",[seventh_num]=@seventh_num "+
              "where [id]=@oldId";
            SqlParameter[] pars = new SqlParameter[11];
            pars[0] = new SqlParameter("@record_date", record.RecordDate);
            pars[1] = new SqlParameter("@times", record.Times);
            pars[2] = new SqlParameter("@first_num", record.FirstNum);
            pars[3] = new SqlParameter("@second_num", record.SecondNum);
            pars[4] = new SqlParameter("@third_num", record.ThirdNum);
            pars[5] = new SqlParameter("@fourth_num", record.FourthNum);
            pars[6] = new SqlParameter("@fifth_num", record.FifthNum);
            pars[7] = new SqlParameter("@sixth_num", record.SixthNum);
            pars[8] = new SqlParameter("@seventh_num", record.SeventhNum);
            pars[9] = new SqlParameter("@id", record.Id);
            pars[10] = new SqlParameter("@oldId", oldId);
            DBUtility.SQLServerHelper helper = new DBUtility.SQLServerHelper();
            return helper.RunSQL(strUpdate, pars);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <returns></returns>
        public List<Model.HkRecord> GetData()
        {
            //查询语句
            string strSelect =
                @"SELECT [id]
                        ,[record_date]
                        ,[times]
                        ,[first_num]
                        ,[second_num]
                        ,[third_num]
                        ,[fourth_num]
                        ,[fifth_num]
                        ,[sixth_num]
                        ,[seventh_num]
                    FROM [LuHeRecord]";
            //记录列表
            List<Model.HkRecord> lRecord = new List<Model.HkRecord>();
            //数据读取对象
            SqlDataReader read;
            DBUtility.SQLServerHelper helper = new DBUtility.SQLServerHelper();
            helper.RunSQL(strSelect, out read);
            while (read.Read())
            {
                Model.HkRecord record = new Model.HkRecord();
                record.Id= read.GetInt64(0);
                record.RecordDate = read.GetDateTime(1);
                record.Times = read.GetString(2);
                record.FirstNum = read.GetByte(3);
                record.SecondNum = read.GetByte(4);
                record.ThirdNum = read.GetByte(5);
                record.FourthNum = read.GetByte(6);
                record.FifthNum = read.GetByte(7);
                record.SixthNum = read.GetByte(8);
                record.SeventhNum = read.GetByte(9);
                lRecord.Add(record);
            }
            return lRecord;
        }
        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <param name="lCondition"></param>
        /// <returns></returns>
        public DataTable GetDataByCondition(List<Model.Condition> lCondition)
        {
            if (lCondition == null)
            {
                string strSelect = "select top 0 * from [LuHeRecord]";
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
                string strSelect = "select * from [LuHeRecord] " + strWhere;
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
        /// <summary>
        /// 查询单条记录
        /// </summary>
        /// <param name="nId"></param>
        /// <returns></returns>
        public Model.HkRecord GetDataById(long nId)
        {
            //查询语句
            string strSelect = "select * from [LuHeRecord] where [id]=@id";
            SqlParameter[] pars = new SqlParameter[1];
            pars[0] = new SqlParameter("@id", nId);
            //记录对象
            Model.HkRecord record = new Model.HkRecord();
            //数据读取对象
            SqlDataReader read;
            DBUtility.SQLServerHelper helper = new DBUtility.SQLServerHelper();
            //执行查询
            helper.RunSQL(strSelect,pars, out read);
            while (read.Read())
            {
                record.Id = read.GetInt64(0);
                record.RecordDate = read.GetDateTime(1);
                record.Times = read.GetString(2);
                record.FirstNum = read.GetByte(3);
                record.SecondNum = read.GetByte(4);
                record.ThirdNum = read.GetByte(5);
                record.FourthNum = read.GetByte(6);
                record.FifthNum = read.GetByte(7);
                record.SixthNum = read.GetByte(8);
                record.SeventhNum = read.GetByte(9);
            }
            //返回记录对象
            return record;
        }
        /// <summary>
        /// 按期次查询单条记录
        /// </summary>
        /// <param name="times"></param>
        /// <returns></returns>
        public Model.HkRecord GetDataByTimes(string times)
        {
            //查询语句
            string strSelect = "select * from [LuHeRecord] where [times]=@times";
            SqlParameter[] pars = new SqlParameter[1];
            pars[0] = new SqlParameter("@times", times);
            //记录对象
            Model.HkRecord record = new Model.HkRecord();
            //数据读取对象
            SqlDataReader read;
            DBUtility.SQLServerHelper helper = new DBUtility.SQLServerHelper();
            //执行查询
            helper.RunSQL(strSelect,pars, out read);
            while (read.Read())
            {
                record.Id = read.GetInt64(0);
                record.RecordDate = read.GetDateTime(1);
                record.Times = read.GetString(2);
                record.FirstNum = read.GetByte(3);
                record.SecondNum = read.GetByte(4);
                record.ThirdNum = read.GetByte(5);
                record.FourthNum = read.GetByte(6);
                record.FifthNum = read.GetByte(7);
                record.SixthNum = read.GetByte(8);
                record.SeventhNum = read.GetByte(9);
            }
            //返回记录对象
            return record;
        }
        /// 取最大记录日期记录
        /// </summary>
        /// <returns></returns>
        public Model.HkRecord GetMaxRecord()
        {
            //查询语句
            string strSelect = 
                @"SELECT TOP 1 [id]
                        ,[record_date]
                        ,[times]
                        ,[first_num]
                        ,[second_num]
                        ,[third_num]
                        ,[fourth_num]
                        ,[fifth_num]
                        ,[sixth_num]
                        ,[seventh_num]
                    FROM [LuHeRecord]
                    ORDER BY [record_date] DESC";
            //记录列表
            Model.HkRecord record = new Model.HkRecord();
            //数据读取对象
            SqlDataReader read;
            DBUtility.SQLServerHelper helper = new DBUtility.SQLServerHelper();
            helper.RunSQL(strSelect, out read);
            if (read.Read())
            {
                record.Id = read.GetInt64(0);
                record.RecordDate = read.GetDateTime(1);
                record.Times = read.GetString(2);
                record.FirstNum = read.GetByte(3);
                record.SecondNum = read.GetByte(4);
                record.ThirdNum = read.GetByte(5);
                record.FourthNum = read.GetByte(6);
                record.FifthNum = read.GetByte(7);
                record.SixthNum = read.GetByte(8);
                record.SeventhNum = read.GetByte(9);
            }
            return record;
        }
        /// <summary>
        /// 取小于某期次的指定数量的记录
        /// </summary>
        /// <param name="tiems"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<Model.HkRecord> GetDataByLessThanTimes(string times, int dataCount)
        {
            string strSelect = 
                @"SELECT * FROM
                    (
                        SELECT TOP "+dataCount+@" * FROM [LuHeRecord] 
                            WHERE [record_date]<=
                                (
                                    SELECT [record_date] FROM [LuHeRecord] WHERE [times]=@times
                                )
                            ORDER BY [record_date] DESC
                    ) T
                ORDER BY [record_date] ASC";
            SqlDataReader dr;
            SqlParameter[] pars=new SqlParameter[1];
            pars[0]=new SqlParameter("@times",times);
            DBUtility.SQLServerHelper helper = new DBUtility.SQLServerHelper();
            helper.RunSQL(strSelect, pars, out dr);
            List<Model.HkRecord> lRecord = new List<Model.HkRecord>();
            while (dr.Read())
            {
                Model.HkRecord record = new Model.HkRecord();
                record.Id = dr.GetInt64(0);
                record.RecordDate = dr.GetDateTime(1);
                record.Times = dr.GetString(2);
                record.FirstNum = dr.GetByte(3);
                record.SecondNum = dr.GetByte(4);
                record.ThirdNum = dr.GetByte(5);
                record.FourthNum = dr.GetByte(6);
                record.FifthNum = dr.GetByte(7);
                record.SixthNum = dr.GetByte(8);
                record.SeventhNum = dr.GetByte(9);
                lRecord.Add(record);
            }
            return lRecord;
        }
        /// <summary>
        /// 取某一日期范围的记录
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<Model.HkRecord> GetDataByRecordDate(DateTime start,DateTime end)
        {
            string strSelect =
                @"SELECT [id]
                    ,[record_date]
                    ,[times]
                    ,[first_num]
                    ,[second_num]
                    ,[third_num]
                    ,[fourth_num]
                    ,[fifth_num]
                    ,[sixth_num]
                    ,[seventh_num]
                FROM [db_record].[dbo].[LuHeRecord]
                WHERE [record_date] <=@start AND [record_date]>=@end
                ORDER BY [record_date] ASC";
            SqlDataReader dr;
            SqlParameter[] pars = new SqlParameter[2];
            pars[0] = new SqlParameter("@start", start);
            pars[1] = new SqlParameter("@end", end);
            DBUtility.SQLServerHelper helper = new DBUtility.SQLServerHelper();
            helper.RunSQL(strSelect, pars, out dr);
            List<Model.HkRecord> lRecord = new List<Model.HkRecord>();
            while (dr.Read())
            {
                Model.HkRecord record = new Model.HkRecord();
                record.Id = dr.GetInt64(0);
                record.RecordDate = dr.GetDateTime(1);
                record.Times = dr.GetString(2);
                record.FirstNum = dr.GetByte(3);
                record.SecondNum = dr.GetByte(4);
                record.ThirdNum = dr.GetByte(5);
                record.FourthNum = dr.GetByte(6);
                record.FifthNum = dr.GetByte(7);
                record.SixthNum = dr.GetByte(8);
                record.SeventhNum = dr.GetByte(9);
                lRecord.Add(record);
            }
            return lRecord;
        }
    }
}
