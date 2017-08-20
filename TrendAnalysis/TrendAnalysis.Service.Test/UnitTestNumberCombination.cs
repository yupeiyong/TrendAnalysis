using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TrendAnalysis.Service.Test
{
    [TestClass]
    public class UnitTestNumberCombination
    {
        [TestMethod]
        public void TestCreateBinaryCombinations()
        {
            var combination = new NumberCombination();
            var arr = new List<int>(){1,2,3,4};
            var nodes = combination.CreateBinaryCombinations<int>(arr);
            Assert.IsNotNull(nodes);
            Assert.IsTrue(nodes.Count ==3);

        }

        [TestMethod]
        public void TestCreateBinaryCombinations_Length1()
        {
            var combination = new NumberCombination();
            var arr = new List<int>() { 1};
            var nodes = combination.CreateBinaryCombinations<int>(arr);
            Assert.IsNotNull(nodes);
            Assert.IsTrue(nodes.Count==1);
            Assert.IsTrue(nodes[0].Left != null && nodes[0].Left.Count == 1 && nodes[0].Left[0] == 1);
            Assert.IsTrue(nodes[0].Right != null && nodes[0].Right.Count ==0);
        }

        [TestMethod]
        public void TestCreateBinaryCombinations_Length2()
        {
            var combination = new NumberCombination();
            var arr = new List<int>() { 1, 2 };
            var nodes = combination.CreateBinaryCombinations<int>(arr);
            Assert.IsNotNull(nodes);
            Assert.IsTrue(nodes.Count == 1);
            Assert.IsTrue(nodes[0].Left != null && nodes[0].Left.Count == 1 && nodes[0].Left[0] == 1);
            Assert.IsTrue(nodes[0].Right != null && nodes[0].Right.Count ==1 && nodes[0].Right[0] == 2);
        }

    }
}
