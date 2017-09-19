using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrendAnalysis.Data;
using TrendAnalysis.Models;
using System.Linq;
using System.Collections.Generic;

namespace TrendAnalysis.Service.Test
{
    [TestClass]
    public class UnitTestMarkSixAnalysisService
    {
        [TestMethod]
        public void TestMethod_AnalyseByOnesDigit()
        {
            using (var dao = new TrendDbContext())
            {
                var numbers = dao.Set<MarkSixRecord>().OrderBy(n=>n.Times).Take(20).Select(n=>n.SeventhNum).ToList();
                var service = new MarkSixAnalysisService();
                var result=service.AnalyseByOnesDigit(numbers, 9);
                result = result.Where(m => m.ConsecutiveTimes.Count > 0).ToList();
            }

        }

        [TestMethod]
        public void TestMethod_AnalyseByTensDigit()
        {
            using (var dao = new TrendDbContext())
            {
                var numbers = dao.Set<MarkSixRecord>().OrderBy(n => n.Times).Take(20).Select(n => n.SeventhNum).ToList();
                var service = new MarkSixAnalysisService();
                var result = service.AnalyseByTensDigit(numbers,  4);
                result = result.Where(m => m.ConsecutiveTimes.Count > 0).ToList();
            }
        }

        [TestMethod]
        public void TestMethod_AnalyseSpecifiedLocation()
        {
            using (var dao = new TrendDbContext())
            {
                var service = new MarkSixAnalysisService();

                var records=dao.Set<MarkSixRecord>().OrderByDescending(m=>m.Times).Take(20).ToList();
                var result = service.AnalyseSpecifiedLocation(7, records[19].Times);
                
            }
        }

    }
}
