using System.Collections.Generic;
using TrendAnalysis.DataTransferObject.Trend;
using TrendAnalysis.Models.Trend;


namespace TrendAnalysis.Service.Trend
{

    /// <summary>
    ///     因子列表的历史趋势
    /// </summary>
    public class FactorsTrend
    {

        public List<Factor<T>> Analyse<T>(FactorsTrendAnalyseDto<T> dto)
        {
            //预测的可能因子
            var predictiveFactors = new List<Factor<T>>();

            var factorTrend = new FactorTrend();

            //分析每个因子
            foreach (var factor in dto.Factors)
            {
                var analyseDto = new FactorTrendAnalyseDto<T>
                {
                    Factor = factor,
                    Numbers = dto.Numbers,
                    AllowMinTimes = dto.AllowMinTimes,
                    AnalyseHistoricalTrendEndIndex = dto.AnalyseHistoricalTrendEndIndex,
                    AddConsecutiveTimes = dto.AddConsecutiveTimes,
                    AddInterval = dto.AddInterval
                };
                var predictiveFactor = factorTrend.Analyse(analyseDto);
                if (predictiveFactor != null)
                    predictiveFactors.Add(factor);
            }
            return predictiveFactors;
        }

    }

}