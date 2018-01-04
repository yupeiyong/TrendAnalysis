using System;
using System.Collections.Generic;
using System.Linq;
using TrendAnalysis.Data;
using TrendAnalysis.Models.Trend;


namespace TrendAnalysis.Service.Trend
{

    public class HistoricalTrendService
    {

        public static void AddRange(List<HistoricalTrend> historicalTrends)
        {
            using (var dao = new TrendDbContext())
            {
                foreach (var historical in historicalTrends)
                {
                    //删除相同的记录
                    var trends = dao.Set<HistoricalTrend>().Where(ht => ht.StartTimes == historical.StartTimes
                            && ht.Location == historical.Location
                            && ht.AllowConsecutiveTimes == historical.AllowConsecutiveTimes
                            && ht.AllowInterval == historical.AllowInterval).ToList();
                    foreach (var item in trends)
                    {
                        //删除子项
                        dao.Set<HistoricalTrendItem>().RemoveRange(item.Items);
                    }
                    dao.Set<HistoricalTrend>().RemoveRange(trends);

                    //创建时间和修改时间
                    historical.CreatorTime = DateTime.Now;
                    historical.LastModifyTime = DateTime.Now;
                }
                dao.Set<HistoricalTrend>().AddRange(historicalTrends);
                dao.SaveChanges();
            }
        }

    }

}