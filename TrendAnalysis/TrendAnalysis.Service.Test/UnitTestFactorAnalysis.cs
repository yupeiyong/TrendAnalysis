﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TrendAnalysis.Models;

namespace TrendAnalysis.Service.Test
{
    [TestClass]
    public class UnitTestFactorAnalysis
    {
        [TestMethod]
        public void TestMethod1()
        {
            var ls = new List<BinaryNode<string>>() { new BinaryNode<string> { Left = new List<string> { "1", "2", "3" }, Right = new List<string> { } } };
            var numbers = new List<string> { "3", "2", "1", "2", "0", "0", "1", "2", "3", "3", "4", "4", "2", "3", "3","0", "2", "3", "3" };
            var rows= FactorAnalysis.Consecutives(numbers, ls);
            Assert.IsTrue(rows.Count > 0);
            Assert.IsTrue(rows.Count == 1);
            Assert.IsTrue(rows[0].ConsecutiveTimes.Count == 2);

            var keys = rows[0].ConsecutiveTimes.Keys.ToList();
            keys.Sort();
            var dict=rows[0].ConsecutiveTimes;
            Assert.IsTrue(keys.Count == 2);
            Assert.IsTrue(keys[0] == 3 && dict[keys[0]] == 2);
            Assert.IsTrue(keys[1] == 4 && dict[keys[0]] == 2);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var ls = new List<BinaryNode<int>>() { new BinaryNode<int> { Left = new List<int> { 1,2,3 }, Right = new List<int> { } } };
            var numbers = new List<int> { 3, 2, 1, 2, 0, 0, 1, 2, 3, 3, 4, 4, 2, 3, 3, 0, 2, 3, 3 };
            var rows = FactorAnalysis.Consecutives(numbers, ls);

            Assert.IsTrue(rows.Count > 0);
            Assert.IsTrue(rows.Count == 1);
            Assert.IsTrue(rows[0].ConsecutiveTimes.Count == 2);

            var keys = rows[0].ConsecutiveTimes.Keys.ToList();
            keys.Sort();
            var dict = rows[0].ConsecutiveTimes;
            Assert.IsTrue(keys.Count == 2);
            Assert.IsTrue(keys[0] == 3 && dict[keys[0]] == 2);
            Assert.IsTrue(keys[1] == 4 && dict[keys[0]] == 2);

        }

    }
}
