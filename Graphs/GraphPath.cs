using System.Collections.Generic;
using System.Linq;

namespace Graphs
{
    public class GraphPath
    {
        private readonly HashSet<int> _nodes;
        private readonly List<int> _path;

        public GraphPath(List<int> path)
        {
            _path = path;
            _nodes = new HashSet<int>();
            _path.ForEach(node => _nodes.Add(node));
        }

        public GraphPath Extended(int node)
        {
            var newPath = _path.Concat(new[] {node}).ToList();
            return new GraphPath(newPath);
        }

        public List<int> GetPath()
        {
            return _path;
        }

        public bool HasNode(int node)
        {
            return _nodes.Contains(node);
        }

    }
}
