using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendAnalysis.Models.Trend;

namespace TrendAnalysis.DataTransferObject.Trend
{
    public class FactorTrendAnalyseDto<T> : BaseTrendAnalyseDto<T>
    {

        public Factor<T> Factor { get; set; }

    }
}
