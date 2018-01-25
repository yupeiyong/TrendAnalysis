using TrendAnalysis.Models.Trend;


namespace TrendAnalysis.DataTransferObject.Trend
{

    /// <summary>
    ///     分析因子的历史传输对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AnalyseFactorHistoricalTrendDto<T> : BaseAnalyseHistoricalTrendDto<T>
    {

        public Factor<T> Factor { get; set; }

    }

}