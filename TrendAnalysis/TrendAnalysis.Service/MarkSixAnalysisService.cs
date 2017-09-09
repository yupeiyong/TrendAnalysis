using System.Collections.Generic;
using TrendAnalysis.Models;
using System.Linq;
using TrendAnalysis.Data;

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
            using(var dao=new TrendDbContext())
            {

            }
            return null;
        }

        /// <summary>
        /// 分析列表个位数
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public List<Results<string>> AnalyseByOnesDigit(List<byte> numbers,List<BinaryNode<string>>nodes, int allowMinTimes = 1)
        {
            //个位数号码列表
            var onesDigitNumbers = numbers.Select(n => n.ToString("00").Substring(1)).ToList();
            return FactorAnalysis.Consecutives(onesDigitNumbers, nodes, allowMinTimes);
        }


        /// <summary>
        /// 分析列表十位数
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public List<Results<string>> AnalyseByTensDigit(List<byte> numbers, List<BinaryNode<string>> nodes, int allowMinTimes = 1)
        {
            //十位数号码列表
            var tensDigitNumbers = numbers.Select(n => n.ToString("##").Substring(0, 1)).ToList();
            return FactorAnalysis.Consecutives(tensDigitNumbers, nodes, allowMinTimes); ;
        }


        /// <summary>
        /// 分析指定位置当前期之前的号码
        /// </summary>
        /// <param name="location">指定位置</param>
        /// <param name="times">期次</param>
        /// <param name="beforeCount">之前多少期</param>
        /// <returns></returns>
        public List<AnalysisBeforeResult> AnalyseBeforeSpecifiedLocation(int location, string times, int beforeCount)
        {
            return null;
        }
    }
}
