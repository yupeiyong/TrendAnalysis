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
                var nodes=NumberCombination.CreateBinaryCombinations(new List<byte>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Select(n=>n.ToString()).ToList());
                var service = new MarkSixAnalysisService();
                var result=service.AnalyseByOnesDigit(numbers, nodes,9);
                result = result.Where(m => m.ConsecutiveTimes.Count > 0).ToList();
            }

        }

        [TestMethod]
        public void TestMethod_AnalyseByTensDigit()
        {
            using (var dao = new TrendDbContext())
            {
                var numbers = dao.Set<MarkSixRecord>().OrderBy(n => n.Times).Take(20).Select(n => n.SeventhNum).ToList();
                var nodes = NumberCombination.CreateBinaryCombinations(new List<byte>() { 0, 1, 2, 3, 4}.Select(n => n.ToString()).ToList());
                var service = new MarkSixAnalysisService();
                var result = service.AnalyseByTensDigit(numbers, nodes, 4);
                result = result.Where(m => m.ConsecutiveTimes.Count > 0).ToList();
            }

        }
    }
}
