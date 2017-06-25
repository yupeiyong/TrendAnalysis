using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using TrendAnalysis.DataTransferObject;

namespace TrendAnalysis.Service.Test
{
    [TestClass]
    public class UnitTestMarkSixReportService
    {
        [TestMethod]
        public void TestSearch()
        {
            using(new TransactionScope())
            {
                var service = new MarkSixRecordService();
                var rows=service.Search(new MarkSixRecordSearchDto { StartIndex=0,TakeCount=20});
                Assert.IsTrue(rows.Count > 0);
            }
        }
    }
}
