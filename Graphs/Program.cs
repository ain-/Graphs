using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Graphs
{
    public class Program
    {

        public static void Main(string[] args)
        {
            if (args == null || args.Length < 3)
            {
                Console.WriteLine("Not enough arguments, see README");
                Environment.Exit(1);
            }

            var rawGraph = args.Take(args.Length - 2);
            var graph = GraphResolver.ParseGraph(rawGraph, GetExceptionHandler());


            Tuple<int, string, int> pathRequirement = null;
            try
            {
                pathRequirement = ParsePathSearch(args[args.Length - 2], args[args.Length - 1]);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Console.WriteLine("Invalid path search command, see README");
                Environment.Exit(3);
            }


            if (pathRequirement.Item2.Contains("R"))
                FindAndShowPaths(graph, pathRequirement.Item1, pathRequirement.Item3);
            if (pathRequirement.Item2.Contains("L"))
                FindAndShowPaths(graph, pathRequirement.Item3, pathRequirement.Item1);
        }

        private static void FindAndShowPaths(List<Tuple<int, int>> graph, int from, int to)
        {
            var paths = GraphResolver.ConnectingPaths(graph, from, to);
            Console.WriteLine($"Paths from {from} to {to}:");
            PrintPaths(paths);
        }

        private static Tuple<int, string, int> ParsePathSearch(string direction, string rawNodes)
        {
            if (!(direction.Contains("L") || direction.Contains("R")))
            {
                Console.WriteLine("Invalid direction for path, see README");
                Environment.Exit(3);
            }
            var nodes = rawNodes.Split(",").Select(raw => Convert.ToInt32(raw)).ToArray();

            return new Tuple<int, string, int>(nodes[0], direction, nodes[1]);
        }

        private static Action<Action<string>, string> GetExceptionHandler()
        {
            Action<Action<string>, string> exceptionHandler = (code, pair) =>
            {
                try
                {
                    code(pair);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    Console.WriteLine($"Invalid element: {pair}");
                    Environment.Exit(2);
                }
            };
            return exceptionHandler;
        }

        private static void PrintPaths(List<List<int>> paths)
        {
            if (paths.Count == 0)
                Console.WriteLine("None");
            foreach (var path in paths)
            {
                Console.WriteLine(String.Join("->", path.ToArray()));
            }
        }
    }
}
