using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendAnalysis.Models
{
    public interface IEntity<T>where T:struct
    {
        /// <summary>
        /// 主键
        /// </summary>
        T Id { get; set; }

        byte[] RowVersion { get; set; }
    }
}
