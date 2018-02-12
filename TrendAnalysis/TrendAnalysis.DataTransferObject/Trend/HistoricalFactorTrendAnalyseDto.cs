using TrendAnalysis.Models.Trend;


namespace TrendAnalysis.DataTransferObject.Trend
{

    /// <summary>
    ///     分析一般因子的历史传输对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HistoricalFactorTrendAnalyseDto<T> : BaseHistoricalTrendAnalyseDto<T>
    {

        public Factor<T> Factor { get; set; }

    }

}