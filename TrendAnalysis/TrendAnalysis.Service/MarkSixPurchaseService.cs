using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendAnalysis.Models;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.Data;

namespace TrendAnalysis.Service
{
    public class MarkSixPurchaseService
    {
        /// <summary>
        /// 获取指定位置的购买记录
        /// </summary>
        /// <returns></returns>
        public List<MarkSixSpecifiedLocationPurchase> SearchSpecifiedLocation(MarkSixSpecifiedLocationPurchaseSearchDto dto)
        {
            using (var dao = new TrendDbContext())
            {
                var source = dao.Set<MarkSixSpecifiedLocationPurchase>().AsQueryable();

                if (dto.StartDateTime.HasValue || dto.EndDateTime.HasValue)
                {
                    if (!dto.StartDateTime.HasValue)
                    {
                        dto.StartDateTime = DateTime.MinValue.AddDays(1);
                    }
                    if (!dto.EndDateTime.HasValue)
                    {
                        dto.EndDateTime = DateTime.MaxValue.AddDays(-1);
                    }
                    dto.StartDateTime = dto.StartDateTime.Value.Date.AddDays(-1);
                    dto.EndDateTime = dto.EndDateTime.Value.Date.AddDays(1);
                    source = source.Where(m => m.OnModified > dto.StartDateTime.Value && m.OnModified < dto.EndDateTime);
                }

                if (!string.IsNullOrWhiteSpace(dto.Times))
                {
                    source = source.Where(m => m.Times.Contains(dto.Times));
                }
                if (dto.IsGetTotalCount)
                {
                    dto.TotalCount = source.Count();
                }
                return source.OrderBy(m => m.Times).Skip(dto.StartIndex).Take(dto.PageSize).ToList();
            }
        }

        public void SeveSpecifiedLocation(MarkSixSpecifiedLocationPurchaseSaveDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Times))
            {
                throw new Exception(string.Format("错误，期次不能为空"));
            }
            if (string.IsNullOrWhiteSpace(dto.PurchaseList))
            {
                throw new Exception(string.Format("错误，购买清单不能为空"));
            }
            if (dto.Location > 7 || dto.Location < 1)
            {
                throw new Exception(string.Format("错误，购买的指定位置必须为1-7！"));
            }
            if (dto.Odds <= 0)
            {
                throw new Exception(string.Format("错误，赔率不能小于等于0！"));
            }
            using (var dao = new TrendDbContext())
            {
                if (dto.Id > 0)
                {
                    var purchase = dao.Set<MarkSixSpecifiedLocationPurchase>().FirstOrDefault(m => m.Id == dto.Id);
                    if (purchase == null)
                    {
                        throw new Exception(string.Format("错误，购买记录不存在！（Id:{0}）", dto.Id));
                    }
                    purchase.Times = dto.Times;
                    purchase.PurchaseList = dto.PurchaseList;
                    purchase.Odds = dto.Odds;
                    purchase.Location = dto.Location;
                    purchase.PurchaseAmount = dto.PurchaseAmount;
                    purchase.OnModified = DateTime.Now;
                }
                else
                {
                    var purchase = new MarkSixSpecifiedLocationPurchase
                    {
                        Times = dto.Times,
                        PurchaseList = dto.PurchaseList,
                        Odds = dto.Odds,
                        Location = dto.Location,
                        PurchaseAmount = dto.PurchaseAmount,
                        OnCreated = DateTime.Now,
                        OnModified = DateTime.Now
                    };
                    dao.Set<MarkSixSpecifiedLocationPurchase>().Add(purchase);
                }
                dao.SaveChanges();
            }

        }

        public void RemoveSpecifiedLocation(params long[] ids)
        {

        }
    }
}
