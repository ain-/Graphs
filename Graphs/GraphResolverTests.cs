using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Graphs
{
    public class GraphResolverTests
    {
        [Test]
        [TestCase(
            "Simple tree graph",
            "1,2 1,3 3,4 3,5", 1, 4, 1, "1->3->4")]
        [TestCase(
            "Simple tree graph with nodes in illogical order",
            "3,5 1,3 1,2 3,4", 1, 4, 1, "1->3->4")]
        [TestCase(
            "Two separate paths that both start at A and end at B but do not intersect",
            "1,2 1,3 3,4 2,5 4,5", 1, 5, 2, "1->2->5", "1->3->4->5")]
        [TestCase(
            "No overrun if the answer is in-between a longer path",
            "1,2 2,3 3,4", 2, 3, 1, "2->3")]
        [TestCase(
            "Two paths that start and end at the same node chains but spread apart in the middle",
            "1,2 2,3 3,4 4,5 5,6 3,7 7,4", 1, 6, 2, "1->2->3->4->5->6", "1->2->3->7->4->5->6")]
        [TestCase(
            "Path from one disconnected component to another",
            "1,2 3,4", 2, 3, 0)]
        [TestCase(
            "Finding a path in a smaller connected component while the whole graph is disconnected",
            "1,2 2,3 4,5 5,6", 4, 6, 1, "4->5->6")]
        [TestCase(
            "Simple no path",
            "1,2 2,3", 3, 1, 0)]
        [TestCase(
            "Simple cyclic",
            "1,2 2,3 3,1", 3, 2, 1, "3->1->2")]
        [TestCase(
            "Cycle in the middle",
            "1,2 2,3 3,4 4,5 5,6 3,7 7,5 5,3", 1, 6, 2, "1->2->3->4->5->6", "1->2->3->7->5->6")]
        public void TestConnectingPaths(string description, string rawGraph, int from, 
            int to, int answerCount, params string[] answers)
        {
            var paths = GraphResolver.ConnectingPaths(ParseGraph(rawGraph), from, to);
            Assert.AreEqual(answerCount, paths.Count, description);
            var testPairs = answers.Zip(paths, 
                (answer, path) => new { Answer = answer, Path = path}).ToList();
            testPairs.ForEach(testPair => 
                Assert.AreEqual(testPair.Answer, PathToStringForTest(testPair.Path), description));
        }

        private List<Tuple<int, int>> ParseGraph(string rawGraph)
        {
            return GraphResolver.ParseGraph(rawGraph.Split(" "));
        }

        private string PathToStringForTest(List<int> path)
        {
            return String.Join("->", path.ToArray());
        }
    }
}
