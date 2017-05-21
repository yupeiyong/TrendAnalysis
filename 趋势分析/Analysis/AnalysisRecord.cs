using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Analysis
{
    /// <summary>
    /// 分析记录数字（逐位分析从最高位直到个位）
    /// </summary>
    public class AnalysisRecord : IAnalysisRecord
    {
        /// <summary>
        /// 是否为右数字典
        /// </summary>
        private Dictionary<byte, CompareIsInRight> isInRightDict = new Dictionary<byte, CompareIsInRight>();
        /// <summary>
        /// 数字范围的开始数
        /// </summary>
        private uint startNum;
        /// <summary>
        /// 数字范围的结尾数
        /// </summary>
        private uint endNum;
        /// <summary>
        /// 总位数
        /// </summary>
        private int totalDigit;
        public AnalysisRecord(string numRange)
        {
            if (Regex.IsMatch(numRange, @"^\d{1,8}-\d{1,8}$") == false)
            {
                throw new Exception("设置数字范围格式错误！");
            }
            string[] numRangeArr = numRange.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            //开始数
            startNum = uint.Parse(numRangeArr[0]);
            ///结尾数
            endNum = uint.Parse(numRangeArr[1]);
            if (startNum >= endNum)
            {
                throw new Exception("设置数字范围格式错误！开始数字必须小于结束数字！");
            }
            //总位数，如百位数总位数为3
            totalDigit = endNum.ToString().Length;
        }
        /// <summary>
        /// 添加指定位数的比较规则(比较左右、奇偶)
        /// </summary>
        /// <param name="digitIndex">比较的位数，如个位为1，十位为2，百位为3，千位为4，依次类推,先添加个位规则</param>
        /// <param name="analysis"></param>
        /// <returns></returns>
        public void AddCompare(byte digitIndex,CompareIsInRight compIsInRight)
        {
            if (digitIndex > totalDigit)
            {
                throw new Exception("分析数组长度不能大于设置的位数！");
            }
            if (digitIndex != isInRightDict.Count + 1)
            {
                throw new Exception("指定的位置错误，必须是当前比较字典的位置递加！\r\n首先添加个位规则，然后是十位、百位、千位等。");
            }
            if (isInRightDict.ContainsKey(digitIndex))
            {
                throw new Exception("指定的位置重复！");
            }
            else
            {
                if (compIsInRight.Start > 9 || compIsInRight.End > 9)
                {
                    throw new Exception("按位比较的开始数与结尾数不能大于9！");
                }
                isInRightDict.Add(digitIndex, compIsInRight);
            }
        }
        /// <summary>
        /// 是否为右数
        /// </summary>
        /// <param name="num">字符串</param>
        /// <returns></returns>
        public bool IsInRight(string num, byte digitIndex)
        {
            if (Regex.IsMatch(num, @"^\d+$") == false)
            {
                throw new Exception("字符串必须为数字！");
            }
            if (num.Length < digitIndex)
            {
                throw new Exception("字符串长度小于当前分析的位置！");
            }
            int length = num.Length;
            char[] arr = num.ToCharArray();
            //将字符反转,因为按位数比较时，是从个位开始到最大位，如：125，
            //个位为5，个位的索引位置1，而程序中的字符串个位是最高位，所以必须反转才正确
            for (int i = 0; i < length / 2; i++)
            {
                char temp = num[i];
                arr[i] = num[length - i - 1];
                arr[length - i - 1] = temp;
            }
            //取指定位数的数字
            uint digit = uint.Parse(arr[digitIndex - 1].ToString());
            //是否为右边数
            if (isInRightDict.ContainsKey(digitIndex))
            {
                return isInRightDict[digitIndex].IsInRight(digit);
            }
            else
            {
                throw new Exception("不存在指定的位数比较规则！");
            }
        }
        /// <summary>
        /// 是否为右数
        /// </summary>
        /// <param name="num"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsInRight(uint num, byte digitIndex)
        {
            string style = string.Empty;
            for (int i = 1; i <= digitIndex; i++)
            {
                style += "0";
            }
            return IsInRight(num.ToString(style), digitIndex);
        }

        /// <summary>
        /// 指定位数是否为奇数
        /// </summary>
        /// <param name="num"></param>
        /// <param name="index">从个位开始的位数</param>
        /// <returns></returns>
        public bool IsOdd(string num, byte digitIndex)
        {
            if (Regex.IsMatch(num, @"^\d+$") == false)
            {
                throw new Exception("字符串必须为数字！");
            }
            if (num.Length < digitIndex)
            {
                throw new Exception("字符串长度小于当前分析的位置！");
            }
            int length=num.Length;
            char[] arr = num.ToCharArray();
            //将字符反转,因为按位数比较时，是从个位开始到最大位，如：125，
            //个位为5，个位的索引位置1，而程序中的字符串个位是最高位，所以必须反转才正确
            for (int i = 0; i < length / 2; i++)
            {
                char temp = num[i];
                arr[i] = num[length - i - 1];
                arr[length - i - 1] = temp;
            }
            //取指定位数的数字
            uint digit = uint.Parse(arr[digitIndex - 1].ToString());
            //返回是否为奇数
            return CompareIsOdd.IsOdd(digit);
        }
        public bool IsOdd(uint num, byte digitIndex)
        {
            //数字格式
            string style = string.Empty;
            //数字显示的位置，如不够补0，如比较34百位数是否为奇数，补0后为034
            for (int i = 0; i < this.totalDigit; i++)
            {
                style += "0";
            }
            return IsOdd(num.ToString(style), digitIndex);
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        /// <returns></returns>
        public List<uint> GetResult(Dictionary<byte, State> stateDict)
        {
            if (stateDict.Count >255)
            {
                throw new Exception("状态字典数量不能大于255位数！");
            }
            //如字典数量小于总位数
            if (stateDict.Count < totalDigit)
            {
                //当前状态有多少位数
                byte curIndex = (byte)(stateDict.Count+1);
                for (byte i = curIndex ; i <= totalDigit; i++)
                {
                    //添加空的规则
                    stateDict.Add(i, new State());
                }
            }
            for (int i = 1; i <= totalDigit; i++)
            {
                if (stateDict.ContainsKey((byte)i) == false)
                {
                    throw new Exception("状态字典的位置索引必须与分析字典的位置索引一致！");
                }
            }
            return GetResult(stateDict, startNum, endNum);
        }
        private List<uint> GetResult(Dictionary<byte, State> stateDict, uint start, uint end)
        {
            //显示数字的字符格式
            string style = string.Empty;
            for (int i = 1; i <= totalDigit; i++)
            {
                style += "0";
            }

            List<uint> lNum = new List<uint>();
            for (uint i = start; i <= end; i++)
            {
                //当前数字字符
                string num = i.ToString(style);
                //当前与状态一致
                bool isSameState = true;
                //从个位开始比较
                for (byte index = 1; index <= isInRightDict.Count; index++)
                {
                    //当前位置是否为奇数
                    bool _isOdd = IsOdd(num, index);
                    //当前位置是否为右数
                    bool _isInRight = IsInRight(num, index);
                    State state = stateDict[index];
                    if (state.IsOdd.HasValue)
                    {
                        if (state.IsOdd.Value != _isOdd)
                        {
                            isSameState = false;
                        }
                    }
                    if (state.IsInRight.HasValue)
                    {
                        if (state.IsInRight.Value != _isInRight)
                        {
                            isSameState = false;
                        }
                    }
                }
                if (isSameState)
                {
                    //添加符合条件的数字
                    lNum.Add(i);
                }
            }
            return lNum;
        }
        /// <summary>
        /// 获取结果，代表数字的字符串
        /// </summary>
        /// <returns></returns>
        public List<string> GetResultString(Dictionary<byte, State> stateDict)
        {
            //获取结果列表
            List<uint> lNum = GetResult(stateDict);
            //显示数字的字符格式
            string style = string.Empty;
            //按总位数设置，不够补0
            for (int i = 0; i < totalDigit; i++)
            {
                style += "0";
            }
            List<string> lNumStr = new List<string>(lNum.Count);
            for (int i = 0; i < lNum.Count; i++)
            {
                //将数字转化为字符
                lNumStr.Add(lNum[i].ToString(style));
            }
            return lNumStr;
        }
    }
    /// <summary>
    /// 分析记录数字（逐位分析）
    /// </summary>
    public class AnalysisRecordNum : IAnalysisRecord
    {
        /// <summary>
        /// 是否为右数字典
        /// </summary>
        private Dictionary<byte, CompareIsInRight> isInRightDict = new Dictionary<byte, CompareIsInRight>();
        /// <summary>
        /// 最高位比较对象
        /// </summary>
        private CompareIsInRight highDigitCompare = null;
        /// <summary>
        /// 数字范围的开始数
        /// </summary>
        private uint startNum;
        /// <summary>
        /// 数字范围的结尾数
        /// </summary>
        private uint endNum;
        /// <summary>
        /// 总位数
        /// </summary>
        private byte totalDigit;
        public AnalysisRecordNum(string numRange)
        {
            if (Regex.IsMatch(numRange, @"^\d{1,8}-\d{1,8}$") == false)
            {
                throw new Exception("设置数字范围格式错误！");
            }
            string[] numRangeArr = numRange.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            //开始数
            startNum = uint.Parse(numRangeArr[0]);
            ///结尾数
            endNum = uint.Parse(numRangeArr[1]);
            if (startNum >= endNum)
            {
                throw new Exception("设置数字范围格式错误！开始数字必须小于结束数字！");
            }
            //总位数，如百位数总位数为3
            totalDigit =(byte)endNum.ToString().Length;
        }
        /// <summary>
        /// 添加指定位数的比较规则(比较左右、奇偶)
        /// </summary>
        /// <param name="digitIndex">比较的位数，如个位为1，十位为2，百位为3，千位为4，依次类推,必须先添加个位规则</param>
        /// <param name="analysis"></param>
        /// <returns></returns>
        public void AddCompare(byte digitIndex, CompareIsInRight compIsInRight)
        {
            if (digitIndex > totalDigit-1)
            {
                throw new Exception("按数字分析的最高位必须单独指定分析对象！");
            }
            if (digitIndex != isInRightDict.Count + 1)
            {
                throw new Exception("指定的位置错误，必须是当前比较字典的位置递加！\r\n首先添加个位规则，然后是十位、百位、千位等。");
            }
            if (isInRightDict.ContainsKey(digitIndex))
            {
                throw new Exception("指定的位置重复！");
            }
            else
            {
                if (compIsInRight.Start > 9 || compIsInRight.End > 9)
                {
                    throw new Exception("按位比较的开始数与结尾数不能大于9！");
                }
                isInRightDict.Add(digitIndex, compIsInRight);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="compIsInRight"></param>
        public void AddHighDigitCompare(CompareIsInRight compIsInRight)
        {
            if (isInRightDict.Count != totalDigit - 1)
            {
                throw new Exception("最高位比较规则必须在最后添加！");
            }
            if (compIsInRight.Start != this.startNum || compIsInRight.End != this.endNum)
            {
                throw new Exception("最高位比较规则的开始数与结尾数不等于设置范围的开始与结尾数！");
            }
            //添加项到字典
            this.isInRightDict.Add(totalDigit, compIsInRight);
            ////设置最高位比较规则
            //highDigitCompare = compIsInRight;
        }
        /// <summary>
        /// 是否为右数
        /// </summary>
        /// <param name="num">字符串</param>
        /// <returns></returns>
        public bool IsInRight(string num, byte digitIndex)
        {
            if (Regex.IsMatch(num, @"^\d+$") == false)
            {
                throw new Exception("字符串必须为数字！");
            }
            if (num.Length < digitIndex)
            {
                throw new Exception("字符串长度小于当前分析的位置！");
            }
            uint digit = 0;
            //比较的位数是否为最高位
            if (digitIndex == totalDigit)
            {
                digit = uint.Parse(num);
            }
            else
            {
                int length = num.Length;
                char[] arr = num.ToCharArray();
                //将字符反转,因为按位数比较时，是从个位开始到最大位，如：125，
                //个位为5，个位的索引位置1，而程序中的字符串个位是最高位，所以必须反转才正确
                for (int i = 0; i < length / 2; i++)
                {
                    char temp = num[i];
                    arr[i] = num[length - i - 1];
                    arr[length - i - 1] = temp;
                }
                //取指定位数的数字
                digit = uint.Parse(arr[digitIndex - 1].ToString());
            }
            //是否为右边数
            if (isInRightDict.ContainsKey(digitIndex))
            {
                return isInRightDict[digitIndex].IsInRight(digit);
            }
            else
            {
                throw new Exception("不存在指定的位数比较规则！");
            }
        }
        /// <summary>
        /// 是否为右数
        /// </summary>
        /// <param name="num"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsInRight(uint num, byte digitIndex)
        {
            string style = string.Empty;
            for (int i = 1; i <= digitIndex; i++)
            {
                style += "0";
            }
            return IsInRight(num.ToString(style), digitIndex);
        }

        /// <summary>
        /// 指定位数是否为奇数
        /// </summary>
        /// <param name="num"></param>
        /// <param name="index">从1开始的位数</param>
        /// <returns></returns>
        public bool IsOdd(string num, byte digitIndex)
        {
            if (Regex.IsMatch(num, @"^\d+$") == false)
            {
                throw new Exception("字符串必须为数字！");
            }
            if (num.Length < digitIndex)
            {
                throw new Exception("字符串长度小于当前分析的位置！");
            }
            int length = num.Length;
            char[] arr = num.ToCharArray();
            //将字符反转,因为按位数比较时，是从个位开始到最大位，如：125，
            //个位为5，个位的索引位置1，而程序中的字符串个位是最高位，所以必须反转才正确
            for (int i = 0; i < length / 2; i++)
            {
                char temp = num[i];
                arr[i] = num[length - i - 1];
                arr[length - i - 1] = temp;
            }
            //取指定位数的数字
            uint digit = uint.Parse(arr[digitIndex - 1].ToString());
            //返回是否为奇数
            return CompareIsOdd.IsOdd(digit);
        }
        public bool IsOdd(uint num, byte digitIndex)
        {
            //数字格式
            string style = string.Empty;
            //数字显示的位置，如不够补0，如比较34百位数是否为奇数，补0后为034
            for (int i = 0; i < this.totalDigit; i++)
            {
                style += "0";
            }
            return IsOdd(num.ToString(style), digitIndex);
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        /// <returns></returns>
        public List<uint> GetResult(Dictionary<byte, State> stateDict)
        {
            if (stateDict.Count != totalDigit)
            {
                throw new Exception("状态字典数量必须与分析字典数量一致！");
            }
            //if (highDigitCompare == null)
            //{
            //    throw new Exception("必须设置最高位的比较规则！");
            //}
            for (int i = 1; i < totalDigit; i++)
            {
                if (stateDict.ContainsKey((byte)i) == false)
                {
                    throw new Exception("状态字典的位置索引必须与分析字典的位置索引一致！");
                }
            }
            return GetResult(stateDict, startNum, endNum);
        }
        private List<uint> GetResult(Dictionary<byte, State> stateDict, uint start, uint end)
        {
            //显示数字的字符格式
            string style = string.Empty;
            for (int i = 1; i <= totalDigit; i++)
            {
                style += "0";
            }

            List<uint> lNum = new List<uint>();
            //先取最高位的奇偶大小等状态
            State highState = stateDict[(byte)totalDigit];
            //是否设置了右数
            if (highState.IsInRight.HasValue)
            {
                //是否为右数
                bool stateIsInright = highState.IsInRight.Value;
                for (uint i = start; i <= end; i++)
                {
                    bool isInright =isInRightDict[totalDigit].IsInRight(i);
                    if (stateIsInright == isInright)
                    {
                        //添加到列表
                        lNum.Add(i);
                    }
                }
            }
            else
            {
                for (uint i = start; i <= end; i++)
                {
                    //将整个范围内的数字全部添加
                    lNum.Add(i);
                }
            }
            if (highState.IsOdd.HasValue)
            {
                //是否为奇数
                bool stateIsOdd = highState.IsOdd.Value;
                for (int i = lNum.Count - 1; i >= 0; i--)
                {
                    //当前位数是否为奇数
                    bool isOdd = this.IsOdd(lNum[i], (byte)totalDigit);
                    //与状态设置的是否为奇数不一致
                    if (isOdd != stateIsOdd)
                    {
                        //从列表中删除
                        lNum.RemoveAt(i);
                    }
                }
            }
            byte curDight = (byte)(totalDigit - 1);
            //从第二位开始分析，如是3位数，从第2位十位开始
            for (byte digitIndex = curDight; digitIndex >= 1; digitIndex--)
            {
                //当前位数是否设置了奇数状态
                if (stateDict[digitIndex].IsOdd.HasValue)
                {
                    //取得当前位置的奇数状态
                    bool stateIsOdd = stateDict[digitIndex].IsOdd.Value;
                    for (int i = lNum.Count - 1; i >= 0; i--)
                    {
                        //如果数字长度小于当前比较的位置，不用分析，如当前比较百位，数字12不用参与分析
                        if (lNum[i].ToString().Length < digitIndex) { continue; }
                        //当前位数是否为奇数
                        bool isOdd = this.IsOdd(lNum[i], digitIndex);
                        //与状态设置的是否为奇数不一致
                        if (isOdd!=stateIsOdd)
                        {
                            //从列表中删除
                            lNum.RemoveAt(i);
                        }
                    }
                }
                //当前位数是否设置了右数状态
                if (stateDict[digitIndex].IsInRight.HasValue)
                {
                    //取得当前位置的右数状态
                    bool stateIsInRight = stateDict[digitIndex].IsInRight.Value;
                    for (int i = lNum.Count - 1; i >= 0; i--)
                    {
                        //如果数字长度小于当前比较的位置，不用分析，如当前比较百位，数字12不用参与分析
                        if (lNum[i].ToString().Length < digitIndex) { continue; }
                        //当前位数是否为右数
                        bool isInRight = this.IsInRight(lNum[i], digitIndex);
                        //与状态设置的是否为右数不一致
                        if (isInRight != stateIsInRight)
                        {
                            //从列表中删除
                            lNum.RemoveAt(i);
                        }
                    }
                }
            }
            return lNum;
        }
        /// <summary>
        /// 获取结果，代表数字的字符串
        /// </summary>
        /// <returns></returns>
        public List<string> GetResultString(Dictionary<byte, State> stateDict)
        {
            //获取结果列表
            List<uint> lNum = GetResult(stateDict);
            //显示数字的字符格式
            string style = string.Empty;
            //按总位数设置，不够补0
            for (int i = 0; i < totalDigit; i++)
            {
                style += "0";
            }
            List<string> lNumStr = new List<string>(lNum.Count);
            for (int i = 0; i < lNum.Count; i++)
            {
                //将数字转化为字符
                lNumStr.Add(lNum[i].ToString(style));
            }
            return lNumStr;
        }
    }
}
