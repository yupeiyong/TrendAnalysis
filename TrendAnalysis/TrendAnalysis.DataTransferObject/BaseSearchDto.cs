using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendAnalysis.DataTransferObject
{
    public class BaseSearchDto
    {
        public int StartIndex { get; set; }

        public int TakeCount { get; set; }
    }
}
