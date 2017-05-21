using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Analysis
{
    /// <summary>
    /// 比较是否为右数
    /// </summary>
    public class CompareIsInRight
    {
        public CompareIsInRight()
        {

        }
        public CompareIsInRight(uint start, uint mid, uint end)
        {
            this.Start = start;
            this.End = end;
            this.Mid = mid;
        }
        /// <summary>
        /// 比较范围的开始数
        /// </summary>
        public uint Start { get; set; }
        /// <summary>
        /// 比较范围的尾数
        /// </summary>
        public uint End { get; set; }
        /// <summary>
        /// 比较的中间数
        /// </summary>
        public uint Mid { get; set; }

        public bool IsInRight(uint num)
        {
            return CompareIsInRight.IsInRight(num, Start, End, Mid);
        }
        /// <summary>
        /// 以一中间数，判断数字在以start开始以end结束的范围内是否为右边范围内
        /// </summary>
        /// <param name="num">待比较的数</param>
        /// <param name="start">数字范围开始</param>
        /// <param name="end">数字范围结束</param>
        /// <param name="mid">比较基数</param>
        /// <returns></returns>
        public static bool IsInRight(uint num, uint start, uint end, uint mid)
        {
            if (mid > end || mid < start)
            {
                throw new Exception("中间数不能大于数字的末尾和小于数字的开始！");
            }
            if (num > end || num < start)
            {
                throw new Exception("待比较数字不能大于数字的末尾和小于数字的开始！");
            }
            if (end <= start)
            {
                throw new Exception("数字范围的末尾数不能小于等于开始数！");
            }
            //算法：
            //1、将一个范围内数字平均分为两个部分，如数字范围1-40，(40-1+1)/2=20,平均后两个部分均为20个数字
            //2、以中间数为准，分为两部分。如1-40，分为1-20，21-40两部分
            //3、大于中间数为右边数，否则为左边数
            //4、要考虑数字越界问题:
            //   中间数+平均数<>尾数即可判断为越界
            //5、中间数+平均数>尾数，右边数范围：1、中间数+1到尾数 2、开始数到(开始数+（比较+平均数的余数），3、左边数范围，开始数到中间数
            //   如：1-40，中间数为25，25+20＝45，45大于尾数40越界。右边数为26-40，1-5。
            //6、中间数+平均数<开始数，右边数范围：中间数+平均数-1，左边数范围：1、开始数到中间数，2、（中间数+平均数除尾数的余数-1到尾数
            //   如：1-40，中间数为15，15-20＝-5，-5小于开始数1越界。右边数为16-35，左边数1-15、36-40。
            uint half = (end - start + 1) / 2;//平均数
            uint sum = mid + half;//中间数+平均数
            if (sum == end)
            {
                //直接比较是否大于中间数
                return num > mid;
            }
            else
            {
                if (sum > end)
                {
                    //除尾数的余数
                    uint mod = sum % end;
                    //大于中间数或，大于开始数小于余数
                    if (num > mid || (num >= start && num <= mod))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (num > mid && num <= sum)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
    /// <summary>
    /// 比较是否为奇数
    /// </summary>
    public class CompareIsOdd
    {
        /// <summary>
        /// 是否为奇数
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool IsOdd(string num)
        {
            uint value;
            if (uint.TryParse(num, out value))
            {
                return IsOdd(value);
            }
            else
            {
                throw new Exception("必须为非负整数！");
            }
        }
        /// <summary>
        /// 否为奇数
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool IsOdd(uint num)
        {
            return num % 2 == 1;
        }
    }
}
