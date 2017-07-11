using System.Collections.Generic;
using TrendAnalysis.Models;

namespace TrendAnalysis.Service
{
    public class MarkSixAnalysisService
    {
        /// <summary>
        /// 分析指定位置号码
        /// </summary>
        /// <param name="location">指定的第几位</param>
        /// <param name="times"></param>
        /// <returns></returns>
        public List<byte> AnalyseSpecifiedLocation(int location, string times)
        {
            return null;
        }

        /// <summary>
        /// 分析指定位置当前期之前的号码
        /// </summary>
        /// <param name="location">指定位置</param>
        /// <param name="times">期次</param>
        /// <param name="beforeCount">之前多少期</param>
        /// <returns></returns>
        public List<AnalysisBeforeResult> AnalyseBeforeSpecifiedLocation(int location,string times,int beforeCount)
        {
            return null;
        }
    }
}
