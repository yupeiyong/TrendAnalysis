using System;
using System.Collections.Generic;
using System.Linq;
using TrendAnalysis.Data;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.Models.Trend;
using Z.EntityFramework.Plus;


namespace TrendAnalysis.Service.Trend
{

    public class HistoricalTrendService
    {

        public static void AddRange(List<HistoricalTrend> historicalTrends)
        {
            using (var dao = new TrendDbContext())
            {
                //foreach (var historical in historicalTrends)
                //{
                //    //删除相同的记录
                //    var trends = dao.Set<HistoricalTrend>().Where(ht => ht.StartTimes == historical.StartTimes
                //                                                        && ht.Location == historical.Location
                //                                                        && ht.AllowConsecutiveTimes == historical.AllowConsecutiveTimes
                //                                                        && ht.AllowInterval == historical.AllowInterval
                //                                                        && ht.HistoricalTrendType == historical.HistoricalTrendType
                //                                                        && ht.TypeDescription == historical.TypeDescription).ToList();

                //    foreach (var item in trends)
                //    {
                //        //删除子项
                //        dao.Set<HistoricalTrendItem>().RemoveRange(item.Items);
                //    }
                //    dao.Set<HistoricalTrend>().RemoveRange(trends);

                //    //创建时间和修改时间
                //    historical.CreatorTime = DateTime.Now;
                //    historical.LastModifyTime = DateTime.Now;
                //}
                foreach (var trend in historicalTrends)
                {
                    trend.CreatorTime = DateTime.Now;
                    trend.LastModifyTime = DateTime.Now;
                    dao.Set<HistoricalTrend>().Add(trend);
                    dao.SaveChanges();
                }
                //historicalTrends.ForEach(trend =>
                //{
                //    trend.CreatorTime = DateTime.Now;
                //    trend.LastModifyTime = DateTime.Now;
                //});

                //dao.Set<HistoricalTrend>().AddRange(historicalTrends);

                //dao.SaveChanges();
            }
        }


        public static void Remove(HistoricalTrendServiceRemoveDto dto)
        {
            using (var dao = new TrendDbContext())
            {
                ////删除相同的记录
                //var trends = dao.Set<HistoricalTrend>().Where(ht => ht.Location == dto.Location
                //    && ht.StartTimes >= dto.StartTimesValue && ht.StartTimes <= dto.EndTimesValue
                //    && ht.AllowConsecutiveTimes >= dto.StartAllowConsecutiveTimes && ht.AllowConsecutiveTimes <= dto.EndAllowConsecutiveTimes
                //    && ht.AllowInterval >= dto.StartAllowMaxInterval && ht.AllowInterval <= dto.EndAllowMaxInterval
                //    && ht.HistoricalTrendType == dto.HistoricalTrendType
                //    && ht.HistoricalTrendItemType == dto.HistoricalTrendItemType)
                //    .ToList();

                //foreach (var item in trends)
                //{
                //    //删除子项
                //    dao.Set<HistoricalTrendItem>().RemoveRange(item.Items);
                //}
                //dao.Set<HistoricalTrend>().RemoveRange(trends);
                //dao.SaveChanges();

                //删除相同的记录
                var trendsSource = dao.Set<HistoricalTrend>().Where(ht => ht.Location == dto.Location
                    && ht.StartTimes >= dto.StartTimesValue && ht.StartTimes <= dto.EndTimesValue
                    && ht.AllowConsecutiveTimes >= dto.StartAllowConsecutiveTimes && ht.AllowConsecutiveTimes <= dto.EndAllowConsecutiveTimes
                    && ht.AllowInterval >= dto.StartAllowMaxInterval && ht.AllowInterval <= dto.EndAllowMaxInterval
                    && ht.HistoricalTrendType == dto.HistoricalTrendType
                    && ht.HistoricalTrendItemType == dto.HistoricalTrendItemType);

                var trendItemIds = trendsSource.SelectMany(t => t.Items).Select(hti => hti.Id);
                //删除子项
                dao.Set<HistoricalTrendItem>().Where(hti => trendItemIds.Any(id => id == hti.Id)).Delete();

                trendsSource.Delete();
            }
        }

    }

}