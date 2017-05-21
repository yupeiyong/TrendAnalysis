using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BLL
{
    public class AnalysisBLL
    {
        /// <summary>
        /// 创建分析数据层实例
        /// </summary>
        private IDAL.IHkAnalysisSQL analysisSQL = DBFactory.CreateSQL.CreateAnalysis();
        /// <summary>
        /// 新增分析记录
        /// </summary>
        /// <param name="analysis"></param>
        /// <returns></returns>
        public int Add(Model.HkRecordAnalysis analysis)
        {
            return analysisSQL.Add(analysis);
        }
        /// <summary>
        /// 删除分析记录
        /// </summary>
        /// <param name="analysis"></param>
        public void Delete(Model.HkRecordAnalysis analysis)
        {
            analysisSQL.Delete(analysis);
        }
        /// <summary>
        /// 删除分析记录
        /// </summary>
        /// <param name="times"></param>
        public void Delete(string times)
        {
            analysisSQL.Delete(times);
        }
        /// <summary>
        /// 更新分析记录
        /// </summary>
        /// <param name="analysis"></param>
        /// <returns></returns>
        public int Update(Model.HkRecordAnalysis analysis)
        {
            return analysisSQL.Update(analysis);
        }
        /// <summary>
        /// 保存分析列表
        /// </summary>
        /// <param name="lAnalysis"></param>
        public void Update(List<Model.HkRecordAnalysis> lAnalysis)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                for (int i = 0; i < lAnalysis.Count; i++)
                {
                    //更新
                    Update(lAnalysis[i]);
                }
                //提交事务
                scope.Complete();
            }
        }
        /// <summary>
        /// 获取所有分析记录
        /// </summary>
        /// <returns></returns>
        public List<Model.HkRecordAnalysis> GetData()
        {
            return analysisSQL.GetData();
        }
        /// <summary>
        /// 按期次获取分析记录
        /// </summary>
        /// <param name="times"></param>
        /// <returns></returns>
        public Model.HkRecordAnalysis GetDataByTimes(string times)
        {
            return analysisSQL.GetDataByTimes(times);
        }
        /// <summary>
        /// 按条件获取分析记录
        /// </summary>
        /// <param name="lCondition"></param>
        /// <returns></returns>
        public DataTable GetDataByCondition(List<Model.Condition> lCondition)
        {
            return analysisSQL.GetDataByCondition(lCondition);
        }
        /// <summary>
        /// 保存数据表
        /// </summary>
        /// <param name="dttab"></param>
        /// <returns></returns>
        public bool SaveData(DataTable dttab)
        {
            if (dttab == null) { return false; }
            using (TransactionScope scope = new TransactionScope())
            {
                //遍历所有行
                for (int i = 0; i < dttab.Rows.Count; i++)
                {
                    //当前行的状态
                    DataRowState state = dttab.Rows[i].RowState;
                    if (state == DataRowState.Added || state == DataRowState.Modified)
                    {
                        DataRow row = dttab.Rows[i];
                        Model.HkRecordAnalysis analysis = new Model.HkRecordAnalysis();
                        analysis.Times = row["times"].ToString();
                        object value = row["what_number"];
                        if (value != DBNull.Value)
                        {
                            analysis.WhatNumber = byte.Parse(value.ToString());
                        }
                        value = row["multiple"];
                        if (value != DBNull.Value)
                        {
                            analysis.Multiple = int.Parse(value.ToString());
                        }
                        value = row["num_money"];
                        if (value != DBNull.Value)
                        {
                            analysis.NumMoney = value.ToString();
                        }
                        value = row["outlay"];
                        if (value != DBNull.Value)
                        {
                            analysis.Outlay = double.Parse(value.ToString());
                        }
                        value = row["expect_income"];
                        if (value != DBNull.Value)
                        {
                            analysis.ExpectIncome = value.ToString();
                        }
                        value = row["num"];
                        if (value != DBNull.Value)
                        {
                            analysis.RecordNum = byte.Parse(value.ToString());
                        }
                        value = row["relity_income"];
                        if (value != DBNull.Value)
                        {
                            analysis.RelityIncome = double.Parse(value.ToString());
                        }
                        value = row["balance"];
                        if (value != DBNull.Value)
                        {
                            analysis.Balance = double.Parse(value.ToString());
                        }
                        value = row["writer"];
                        if (value != DBNull.Value)
                        {
                            analysis.Writer = value.ToString();
                        }
                        value = row["write_date"];
                        if (value != DBNull.Value)
                        {
                            DateTime dt;
                            dt = DateTime.Parse(value.ToString());
                            analysis.WriteDate = dt;
                        }
                        switch (state)
                        {
                            case DataRowState.Added:
                                //新增
                                analysisSQL.Add(analysis);
                                break;
                            case DataRowState.Modified:
                                //更新保存
                                analysisSQL.Update(analysis);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        if (state == DataRowState.Deleted)
                        {
                            string times = dttab.Rows[i]["times", DataRowVersion.Original].ToString();
                            //按期交执行删除
                            analysisSQL.Delete(times);
                        }
                    }
                }
                //提交事务
                scope.Complete();
            }
            return true;
        }
    }
}
