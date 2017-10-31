using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendAnalysis.Models
{
    public class BaseEntity:IEntity<long>
    {
        public long Id { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
