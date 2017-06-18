using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendAnalysis.Data;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.Models;

namespace TrendAnalysis.Service
{
    /// <summary>
    /// MarkSix记录操作类
    /// </summary>
    public class MarkSixReportService
    {
        public List<MarkSixReport> Search(MarkSixReportSearchDto dto)
        {
            using(var dao=new TrendDbContext())
            {
                var source = dao.Set<MarkSixReport>().AsQueryable();
                return source.OrderBy(m=>m.Times).Skip(dto.StartIndex).Take(dto.TakeCount).ToList();
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public List<MarkSixReport> Export()
        {
            return null;
        }

        /// <summary>
        /// 导入
        /// </summary>
        public void Import()
        {

        }

        /// <summary>
        /// 通过网络抓取
        /// </summary>
        public void NetworkCapture()
        {

        }
    }
}
