using System.Collections.Generic;
using System.Linq;
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
            var arr = new List<int>() { 1, 2, 3, 4 };
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
            var arr = new List<int>() { 1, 2, 3 };
            var nodes = FactorGenerator.Create(arr,1,false);
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

        //[TestMethod]
        //public void TestCreateBinaryCombinations_Length_Three2()
        //{
        //    var combination = new FactorGenerator();
        //    var arr = new List<int>() { 1, 2, 3};
        //    var nodes = FactorGenerator.Create<int>(arr,1);
        //    Assert.IsNotNull(nodes);
        //    Assert.IsTrue(nodes.Count == 3);
        //    Assert.IsTrue(nodes[0].Left != null && nodes[0].Left.Count == 2 && nodes[0].Left[0] == 1 && nodes[0].Left[1] == 2);
        //    Assert.IsTrue(nodes[0].Right != null && nodes[0].Right.Count == 1 && nodes[0].Right[0] == 3);

        //    Assert.IsTrue(nodes[1].Left != null && nodes[1].Left.Count == 2 && nodes[1].Left[0] == 1 && nodes[1].Left[1] == 3);
        //    Assert.IsTrue(nodes[1].Right != null && nodes[1].Right.Count == 1 && nodes[1].Right[0] == 2);

        //    Assert.IsTrue(nodes[2].Left != null && nodes[2].Left.Count == 2 && nodes[2].Left[0] == 2 && nodes[2].Left[1] == 3);
        //    Assert.IsTrue(nodes[2].Right != null && nodes[2].Right.Count == 1 && nodes[2].Right[0] == 1);
        //}

        //[TestMethod]
        //public void TestCreateBinaryCombinations_Length1()
        //{
        //    var arr = new List<int>() { 1 };
        //    var nodes = FactorGenerator.Create<int>(arr);
        //    Assert.IsNotNull(nodes);
        //    Assert.IsTrue(nodes.Count == 1);
        //    Assert.IsTrue(nodes[0].Left != null && nodes[0].Left.Count == 1 && nodes[0].Left[0] == 1);
        //    Assert.IsTrue(nodes[0].Right != null && nodes[0].Right.Count == 0);
        //}

        //[TestMethod]
        //public void TestCreateBinaryCombinations_Length2()
        //{
        //    var arr = new List<int>() { 1, 2 };
        //    var nodes = FactorGenerator.Create<int>(arr);
        //    Assert.IsNotNull(nodes);
        //    Assert.IsTrue(nodes.Count == 1);
        //    Assert.IsTrue(nodes[0].Left != null && nodes[0].Left.Count == 1 && nodes[0].Left[0] == 1);
        //    Assert.IsTrue(nodes[0].Right != null && nodes[0].Right.Count == 1 && nodes[0].Right[0] == 2);
        //}

        //[TestMethod]
        //public void TestCreateBinaryCombinations_Many_Odd()
        //{
        //    var arr = new List<int>();
        //    for (var i = 1; i <= 7; i++)
        //    {
        //        arr.Add(i);
        //    }
        //    var nodes = FactorGenerator.Create<int>(arr);
        //    Assert.IsNotNull(nodes);
        //}

        //[TestMethod]
        //public void TestCreateBinaryCombinations_49()
        //{
        //    var arr = new List<int>();
        //    for (var i = 1; i <= 49; i++)
        //    {
        //        arr.Add(i);
        //    }
        //    var nodes = FactorGenerator.Create<int>(arr, 3, true);
        //    var node = nodes.FirstOrDefault(n => string.Join(",", n.Left) == "2,14,26,38");
        //    Assert.IsNotNull(nodes);
        //}
    }
}
