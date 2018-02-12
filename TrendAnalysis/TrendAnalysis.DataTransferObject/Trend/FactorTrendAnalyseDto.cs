using TrendAnalysis.Models.Trend;


namespace TrendAnalysis.DataTransferObject.Trend
{

    public class FactorTrendAnalyseDto<T> : BaseTrendAnalyseDto<T>
    {

        public Factor<T> Factor { get; set; }

    }

}