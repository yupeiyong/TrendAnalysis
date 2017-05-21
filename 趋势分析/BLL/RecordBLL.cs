using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;

namespace BLL
{
    public class RecordBLL
    {
        private IDAL.IHkRecordSQL recordSQL = DBFactory.CreateSQL.CreateRecord();
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="record">记录信息</param>
        public int AddRecord(Model.HkRecord record)
        {
            return recordSQL.AddRecord(record);
        }
        /// <summary>
        /// 添加一个记录列表
        /// </summary>
        /// <param name="lRecord"></param>
        /// <param name="addAct"></param>
        /// <returns></returns>
        public int AddRecords(List<Model.HkRecord> lRecord,Func<bool>addAct)
        {
            int count = 0;
            //声明事务
            using (TransactionScope scope = new TransactionScope())
            {
                //遍历记录列表
                foreach (var record in lRecord)
                {
                    if (addAct != null)
                    {
                        bool isStop = addAct();
                        if (isStop)
                        {

                            return 0;
                        }
                    }
                    count+=recordSQL.AddRecord(record);
                }
                //提交事务
                scope.Complete();
            }
            return count;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="nId">id号</param>
        public void DeleteRecord(long nId)
        {
            recordSQL.DeleteRecord(nId);
        }
        /// <summary>
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="record">记录信息</param>
        public void UpdateRecord(Model.HkRecord record,int oldId)
        {
            recordSQL.UpdateRecord(record,oldId);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <returns></returns>
        public List<Model.HkRecord> GetData()
        {
            return recordSQL.GetData();
        }
        /// <summary>
        /// 按查询条件获取数据
        /// </summary>
        /// <param name="lCondition"></param>
        /// <returns></returns>
        public DataTable GetDataByCondition(List<Model.Condition> lCondition)
        {
            return recordSQL.GetDataByCondition(lCondition);
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        /// <param name="nId"></param>
        /// <returns></returns>
        public Model.HkRecord GetDataById(long nId)
        {
            return recordSQL.GetDataById(nId);
        }
        /// <summary>
        /// 按期次查询单条记录
        /// </summary>
        /// <param name="times"></param>
        /// <returns></returns>
        public Model.HkRecord GetDataByTimes(string times)
        {
            return recordSQL.GetDataByTimes(times);
        }
        /// <summary>
        /// 保存数据表
        /// </summary>
        /// <param name="dttab"></param>
        /// <returns></returns>
        public bool SaveData(DataTable dttab)
        {
            //取得更改的数据
            dttab = dttab.GetChanges();
            //表不为空
            if (dttab != null)
            {
                //启动事务
                using (TransactionScope scope = new TransactionScope())                
                {                    
                    //遍历所有行
                    for (int i = 0; i < dttab.Rows.Count; i++)
                    {
                        //当前行的状态
                        DataRowState state = dttab.Rows[i].RowState;
                        //是否为新增、删除、修改
                        if (state == DataRowState.Added || state == DataRowState.Modified)
                        {
                            Model.HkRecord record = new Model.HkRecord();
                            long id;
                            object value = dttab.Rows[i]["id"];
                            if (value!=null && long.TryParse(value.ToString(), out id))
                            {
                                record.Id = id;
                            }
                            value = dttab.Rows[i]["record_date"];
                            record.RecordDate = DateTime.Parse(value.ToString());
                            value = dttab.Rows[i]["times"];
                            record.Times = value.ToString();
                            value = dttab.Rows[i]["first_num"];
                            record.FirstNum = byte.Parse(value.ToString());
                            value = dttab.Rows[i]["second_num"];
                            record.SecondNum = byte.Parse(value.ToString());
                            value = dttab.Rows[i]["third_num"];
                            record.ThirdNum = byte.Parse(value.ToString());
                            value = dttab.Rows[i]["fourth_num"];
                            record.FourthNum = byte.Parse(value.ToString());
                            value = dttab.Rows[i]["fifth_num"];
                            record.FifthNum = byte.Parse(value.ToString());
                            value = dttab.Rows[i]["sixth_num"];
                            record.SixthNum = byte.Parse(value.ToString());
                            value = dttab.Rows[i]["seventh_num"];
                            record.SeventhNum = byte.Parse(value.ToString());
                            switch (state)
                            {
                                case DataRowState.Added:
                                    recordSQL.AddRecord(record);
                                    break;
                                case DataRowState.Modified:
                                    long oldId = long.Parse(dttab.Rows[i]["id", DataRowVersion.Original].ToString());
                                    recordSQL.UpdateRecord(record,oldId);
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (dttab.Rows[i].RowState == DataRowState.Deleted)
                        {
                            long oldId;
                            if (long.TryParse(dttab.Rows[i]["id", DataRowVersion.Original].ToString(), out oldId))
                            {
                                recordSQL.DeleteRecord(oldId);
                            };
                        }
                    }
                    //提交事务
                    scope.Complete();
                }
                //提交更改
                dttab.AcceptChanges();
            }
            return true;
        }
        /// <summary>
        /// 取最大的记录
        /// </summary>
        /// <returns></returns>
        public Model.HkRecord GetMaxRecord()
        {
            return recordSQL.GetMaxRecord();
        }
                /// <summary>
        /// 取小于某期次的指定数量的记录
        /// </summary>
        /// <param name="tiems"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<Model.HkRecord> GetDataByLessThanTimes(string times, int dataCount)
        {
            return recordSQL.GetDataByLessThanTimes(times, dataCount);
        }
                /// <summary>
        /// 取某一日期范围的记录
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<Model.HkRecord> GetDataByRecordDate(DateTime start, DateTime end)
        {
            return recordSQL.GetDataByRecordDate(start, end);
        }
    }
}
