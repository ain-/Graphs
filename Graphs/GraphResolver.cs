using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs
{
    public class GraphResolver
    {
        public static List<List<int>> ConnectingPaths(List<Tuple<int, int>> graph, int fromNode, int toNode)
        {
            var allPathsByLastNode = new Dictionary<int, List<GraphPath>>
                {{fromNode, new List<GraphPath>{new GraphPath(new List<int> {fromNode})}}};
            var edgesFrom = GraphAsEdgesFrom(graph);
            var frontLine = new SortedSet<int> { fromNode };
            while (frontLine.Count > 0)
            {
                var nextFront = new SortedSet<int>();

                foreach (var node in frontLine)
                {
                    foreach (var path in allPathsByLastNode[node].ToList())
                    {
                        foreach (var next in edgesFrom[node])
                        {
                            if (node != toNode && !path.HasNode(next))
                            {
                                nextFront.Add(next);
                                allPathsByLastNode[node].Remove(path);
                                if (!allPathsByLastNode.ContainsKey(next))
                                    allPathsByLastNode[next] = new List<GraphPath>();
                                allPathsByLastNode[next].Add(path.Extended(next));
                            }
                        }

                    }

                }
                frontLine = nextFront;
            }

            return allPathsByLastNode.ContainsKey(toNode) ? 
                allPathsByLastNode[toNode].Select(gp => gp.GetPath()).ToList() : new List<List<int>>();
        }

        public static Dictionary<int, SortedSet<int>> GraphAsEdgesFrom(List<Tuple<int, int>> graph)
        {
            var edgesFrom = new Dictionary<int, SortedSet<int>>();
            foreach (var edge in graph)
            {
                if (!edgesFrom.ContainsKey(edge.Item1))
                    edgesFrom.Add(edge.Item1, new SortedSet<int>());
                if (!edgesFrom.ContainsKey(edge.Item2))
                    edgesFrom.Add(edge.Item2, new SortedSet<int>());
                edgesFrom[edge.Item1].Add(edge.Item2);
            }
            return edgesFrom;
        }

        public static List<Tuple<int, int>> ParseGraph(IEnumerable<string> rawGraph,
            Action<Action<string>, string> exceptionHandler = null)
        {
            var graph = new List<Tuple<int, int>>();

            Action<string> convert = pair =>
            {
                var nodes = pair.Split(",").Select(raw => Convert.ToInt32(raw)).ToArray();
                graph.Add(new Tuple<int, int>(nodes[0], nodes[1]));
            };

            rawGraph.ToList().ForEach(rawPair =>
            {
                if (exceptionHandler != null)
                    exceptionHandler(convert, rawPair);
                else
                    convert(rawPair);
            });

            return graph;
        }
    }
}