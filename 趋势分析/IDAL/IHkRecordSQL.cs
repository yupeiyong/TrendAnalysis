using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IDAL
{
    public interface IHkRecordSQL
    {
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="record">记录信息</param>
        int AddRecord(Model.HkRecord record);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="nId">id号</param>
        void DeleteRecord(long nId);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="nId">id号</param>
        void DeleteRecord(DateTime record_date,string times);
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="record">记录信息</param>
        int UpdateRecord(Model.HkRecord record,long oldId);
        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <returns></returns>
        List<Model.HkRecord> GetData();
        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <param name="lCondition"></param>
        /// <returns></returns>
        DataTable GetDataByCondition(List<Model.Condition> lCondition);
        /// <summary>
        /// 查询单条记录
        /// </summary>
        /// <param name="nId"></param>
        /// <returns></returns>
        Model.HkRecord GetDataById(long nId);
        /// <summary>
        /// 按期次查询单条记录
        /// </summary>
        /// <param name="times"></param>
        /// <returns></returns>
        Model.HkRecord GetDataByTimes(string times);
        /// <summary>
        /// 取最大记录
        /// </summary>
        /// <returns></returns>
        Model.HkRecord GetMaxRecord();
        /// <summary>
        /// 取小于某期次的指定数量的记录
        /// </summary>
        /// <param name="tiems"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        List<Model.HkRecord> GetDataByLessThanTimes(string times, int dataCount);
        /// <summary>
        /// 取某一日期范围的记录
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        List<Model.HkRecord> GetDataByRecordDate(DateTime start, DateTime end);
    }
}
