using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendAnalysis.DataTransferObject.Trend;

namespace TrendAnalysis.Service.Trend
{
    public class PermutationFactorsTrend
    {
        ///// <summary>
        /////     分析
        ///// </summary>
        ///// <param name="dto">记录集合、比较因子、允许的最小连续次数，大于等于此数才记录......</param>
        ///// <returns></returns>
        //public List<PermutationFactorTrendConsecutiveDetails<T>> Analyse<T>(PermutationFactorTrendAnalyseDto<T> dto)
        //{
        //    //统计连续次数
        //    var factorResults = CountConsecutives(dto.Numbers, dto.PermutationFactors, dto.NumbersTailCutCount, dto.AllowMinTimes);
        //    factorResults = factorResults.Where(t => t.HistoricalConsecutiveTimes.Count > 0).ToList();
        //    foreach (var item in factorResults)
        //    {
        //        var times = 0;

        //        //因子列表数量
        //        var factorCount = item.Factors.Count;

        //        /*
        //         根据连续次数来分析可能的趋势：
                 
        //         第一种情况：
        //         如：[{left:1,2,right:3,4},{left:5,6,right:7,8}]为因子列表,排列因子列表：[{1,2},{5,6}]取第一个排列因子的反因子为{3,4}
        //         倒序的历史记录列表：6,2,5,2,6,1,5,7,8,5,6...
        //         分析结果：
        //         |6,2,|  |5,2,|  |6,1,| 5,7,8,5,6...
        //         连续次数3次
        //         可能的趋势是{3,4}

        //         第二种情况：（当前方法采用的）
        //         如：[{left:1,2,right:3,4},{left:5,6,right:7,8}]为因子列表,排列因子列表：[{1,2},{5,6}]取最后一个排列因子的反因子为{7,8}
        //         倒序的历史记录列表：1,6,2,5,2,6,1,5,7,8,5,6...
        //         分析结果：
        //         1,| |6,2,|  |5,2,|  |6,1,| 5,7,8,5,6...
        //         连续次数3次
        //         可能的趋势是{7,8}

        //         如果排列是多个，将不止有两种情况，结果应该=排列数
        //         */

        //        //号码索引位置
        //        var i = dto.Numbers.Count - 1;

        //        //从因子列表中的倒数第二个开始遍历
        //        var m = factorCount - 2;
        //        for (; m >= 0 && i - m >= 0; m--, i--)
        //        {
        //            if (!item.Factors[m].Contains(dto.Numbers[i]))
        //                break;
        //        }

        //        //所有因子都包含，则次数递增
        //        if (m < 0)
        //        {
        //            times++;
        //        }
        //        else
        //        {
        //            continue;
        //        }

        //        //记录集合倒序检查，因子是否包含当前号码
        //        for (; i >= 0; i -= factorCount)
        //        {
        //            var n = item.Factors.Count - 1;

        //            //倒序遍历因子列表
        //            //j是控制获取号码索引位置的指针
        //            for (var j = 0; n >= 0 && i - j >= 0; n--, j++)
        //            {
        //                if (!item.Factors[n].Contains(dto.Numbers[i - j]))
        //                    break;
        //            }

        //            //所有因子都包含，则次数递增
        //            if (n < 0)
        //            {
        //                times++;
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }

        //        //记录因子当前连续次数
        //        item.FactorCurrentConsecutiveTimes = times;
        //    }

        //    //1、按允许的最小因子当前连续次数和允许的最大间隔次数筛选
        //    //2、先按最大连续次数然后按最小间隔次数排序
        //    factorResults = factorResults
        //        .Where(m => m.FactorCurrentConsecutiveTimes >= dto.AllowMinFactorCurrentConsecutiveTimes && m.Interval <= dto.AllowMaxInterval)
        //        .OrderByDescending(t => t.FactorCurrentConsecutiveTimes)
        //        .ThenBy(t => t.Interval).ToList();

        //    return factorResults;
        //}

    }
}
