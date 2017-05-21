using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analysis
{
    /// <summary>
    /// 分析结果接口
    /// </summary>
    public interface IAnalysisRecord
    {
        /// <summary>
        /// 是否为右数
        /// </summary>
        /// <param name="num"></param>
        /// <param name="index">数字位置，如：千位百位十位个位</param>
        /// <returns></returns>
        bool IsInRight(string num, byte index);
        /// <summary>
        /// 是否为右数
        /// </summary>
        /// <param name="num"></param>
        /// <param name="index">数字位置，如：千位百位十位个位</param>
        /// <returns></returns>
        bool IsInRight(uint num, byte index);
        /// <summary>
        /// 指定位数是否为奇数
        /// </summary>
        /// <param name="num"></param>
        /// <param name="index">数字位置，如：千位百位十位个位</param>
        /// <returns></returns>
        bool IsOdd(string num, byte index);
        /// <summary>
        /// 指定位数是否为奇数
        /// </summary>
        /// <param name="num"></param>
        /// <param name="index">数字位置，如：千位百位十位个位</param>
        /// <returns></returns>
        bool IsOdd(uint num, byte index);
        /// <summary>
        /// 获取结果
        /// </summary>
        /// <returns></returns>
        List<uint> GetResult(Dictionary<byte, State> stateDict);
        /// <summary>
        /// 获取结果，代表数字的字符串
        /// </summary>
        /// <returns></returns>
        List<string> GetResultString(Dictionary<byte, State> stateDict);
    }
}
