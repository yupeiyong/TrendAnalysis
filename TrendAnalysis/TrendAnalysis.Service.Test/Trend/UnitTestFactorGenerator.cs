using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrendAnalysis.Service.Trend;


namespace TrendAnalysis.Service.Test.Trend
{

    [TestClass]
    public class UnitTestFactorGenerator
    {

        [TestMethod]
        public void TestCreate()
        {
            var combination = new FactorGenerator();
            var arr = new List<int> {1, 2, 3, 4};
            var nodes = FactorGenerator.Create(arr);
            Assert.IsNotNull(nodes);
            Assert.IsTrue(nodes.Count == 6);
            Assert.IsTrue(nodes[0].Left != null && string.Join(",", nodes[0].Left) == "1,2");
            Assert.IsTrue(nodes[0].Right != null && string.Join(",", nodes[0].Right) == "3,4");

            Assert.IsTrue(nodes[1].Left != null && string.Join(",", nodes[1].Left) == "3,4");
            Assert.IsTrue(nodes[1].Right != null && string.Join(",", nodes[1].Right) == "1,2");

            Assert.IsTrue(nodes[2].Left != null && string.Join(",", nodes[2].Left) == "1,3");
            Assert.IsTrue(nodes[2].Right != null && string.Join(",", nodes[2].Right) == "2,4");

            Assert.IsTrue(nodes[3].Left != null && string.Join(",", nodes[3].Left) == "2,4");
            Assert.IsTrue(nodes[3].Right != null && string.Join(",", nodes[3].Right) == "1,3");

            Assert.IsTrue(nodes[4].Left != null && string.Join(",", nodes[4].Left) == "1,4");
            Assert.IsTrue(nodes[4].Right != null && string.Join(",", nodes[4].Right) == "2,3");

            Assert.IsTrue(nodes[5].Left != null && string.Join(",", nodes[5].Left) == "2,3");
            Assert.IsTrue(nodes[5].Right != null && string.Join(",", nodes[5].Right) == "1,4");
        }


        [TestMethod]
        public void TestCreate_Length_Three()
        {
            var combination = new FactorGenerator();
            var arr = new List<int> {1, 2, 3};
            var nodes = FactorGenerator.Create(arr,1);
            Assert.IsNotNull(nodes);
            Assert.IsTrue(nodes.Count == 4);
            Assert.IsTrue(nodes[0].Left != null && string.Join(",", nodes[0].Left) == "1,2");
            Assert.IsTrue(nodes[0].Right != null && string.Join(",", nodes[0].Right) == "3");

            Assert.IsTrue(nodes[1].Left != null && string.Join(",", nodes[1].Left) == "3");
            Assert.IsTrue(nodes[1].Right != null && string.Join(",", nodes[1].Right) == "1,2");

            Assert.IsTrue(nodes[2].Left != null && string.Join(",", nodes[2].Left) == "1,3");
            Assert.IsTrue(nodes[2].Right != null && string.Join(",", nodes[2].Right) == "2");

            Assert.IsTrue(nodes[3].Left != null && string.Join(",", nodes[3].Left) == "2");
            Assert.IsTrue(nodes[3].Right != null && string.Join(",", nodes[3].Right) == "1,3");
        }

    }

}