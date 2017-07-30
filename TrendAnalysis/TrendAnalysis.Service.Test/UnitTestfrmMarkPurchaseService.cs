using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.Data;
using TrendAnalysis.Models;
using System.Linq;
using TrendAnalysis.Service;

namespace TrendAnalysis.Service.Test
{
    [TestClass]
    public class UnitTestfrmMarkPurchaseService
    {
        [TestMethod]
        public void TestSearch()
        {

        }

        [TestMethod]
        public void TestSave_Add()
        {
            using (new TransactionScope())
            using (var dao = new TrendDbContext())
            {
                var beforeCount = dao.Set<MarkSixSpecifiedLocationPurchase>().Count();
                var service = new MarkSixPurchaseService();
                var addDto = new MarkSixSpecifiedLocationPurchase
                {
                    Times = "2017001",
                    Odds = 40,
                    Location = 7,
                    PurchaseList = "12:50;36:100"
                };
                service.SaveSpecifiedLocation(addDto);
                var afterCount = dao.Set<MarkSixSpecifiedLocationPurchase>().Count();
                Assert.IsTrue(beforeCount + 1==afterCount);
                var purchase = dao.Set<MarkSixSpecifiedLocationPurchase>().OrderByDescending(m => m.Id).FirstOrDefault();
                Assert.IsNotNull(purchase);
                Assert.IsTrue(purchase.Times == addDto.Times);
            }
        }

        [TestMethod]
        public void TestSave_Update()
        {
            using (new TransactionScope())
            using (var dao = new TrendDbContext())
            {
                var beforeCount = dao.Set<MarkSixSpecifiedLocationPurchase>().Count();
                var service = new MarkSixPurchaseService();
                var addDto = new MarkSixSpecifiedLocationPurchase
                {
                    Times = "2017001",
                    Odds = 40,
                    Location = 7,
                    PurchaseList = "12:50;36:100"
                };
                service.SaveSpecifiedLocation(addDto);
                var afterCount = dao.Set<MarkSixSpecifiedLocationPurchase>().Count();
                Assert.IsTrue(beforeCount + 1 == afterCount);
                var purchase = dao.Set<MarkSixSpecifiedLocationPurchase>().OrderByDescending(m => m.Id).FirstOrDefault();
                Assert.IsNotNull(purchase);
                Assert.IsTrue(purchase.Times == addDto.Times);

                addDto.Id = purchase.Id;
                addDto.Location = 6;
                service.SaveSpecifiedLocation(addDto);
                purchase=dao.Set<MarkSixSpecifiedLocationPurchase>().AsNoTracking().FirstOrDefault(m=>m.Id==purchase.Id);
                Assert.IsTrue(purchase.Location==addDto.Location);
            }
        }

        [TestMethod]
        public void TestRemove()
        {

        }
    }
}
