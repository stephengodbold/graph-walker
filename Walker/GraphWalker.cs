using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Walker
{
    public class GraphWalker<T>
    {
        private readonly Path<T> _defaultPath;

        public GraphWalker(GraphNode<T> startNode)
        {
            _defaultPath = new Path<T>();
            _defaultPath.Step(startNode);
        }

        public Path<T> TraverseTo(T value)
        {
            var paths = new Collection<Path<T>> {_defaultPath};
            var startNode = _defaultPath.First();

            if (startNode.Neighbours.Count == 0) return _defaultPath;

            var currentPaths = new Collection<Path<T>>(paths.ToArray());

            while (currentPaths.Count(path => path.Count(thisPath => thisPath.Neighbours != null) > 0) > 0)
            {
                paths = new Collection<Path<T>>();

                foreach (var path in currentPaths)
                {
                    var currentNode = path.Last();

                    foreach (var step in currentNode.Neighbours)
                    {
                        var branch = new Path<T>(path);
                        branch.Step(step);

                        if (step.Value.Equals(value)) return branch;
                        if (branch.IsValid)
                        {
                            paths.Add(branch);
                        }
                    }
                }

                currentPaths = new Collection<Path<T>>(paths.ToArray());
            }

            throw new ArgumentOutOfRangeException("value", value, "The value was not found in the graph");
        }       
    }
}