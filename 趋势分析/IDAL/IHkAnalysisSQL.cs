using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IDAL
{
    public interface IHkAnalysisSQL
    {
        /// <summary>
        /// 新增分析记录
        /// </summary>
        /// <param name="analysis"></param>
        /// <returns></returns>
        int Add(Model.HkRecordAnalysis analysis);
        /// <summary>
        /// 删除分析记录
        /// </summary>
        /// <param name="analysis"></param>
        void Delete(Model.HkRecordAnalysis analysis);
        /// <summary>
        /// 删除分析记录
        /// </summary>
        /// <param name="times"></param>
        void Delete(string times);
        /// <summary>
        /// 更新分析记录
        /// </summary>
        /// <param name="analysis"></param>
        /// <returns></returns>
        int Update(Model.HkRecordAnalysis analysis);
        /// <summary>
        /// 获取所有分析记录
        /// </summary>
        /// <returns></returns>
        List<Model.HkRecordAnalysis> GetData();
        /// <summary>
        /// 按期次获取分析记录
        /// </summary>
        /// <param name="times"></param>
        /// <returns></returns>
        Model.HkRecordAnalysis GetDataByTimes(string times);
        /// <summary>
        /// 按条件获取分析记录
        /// </summary>
        /// <param name="lCondition"></param>
        /// <returns></returns>
        DataTable GetDataByCondition(List<Model.Condition> lCondition);
    }
}
