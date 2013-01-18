using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Walker
{
    public class GraphWalker<T>
    {
        private readonly WalkingPath<T> _defaultPath;

        public GraphWalker(GraphNode<T> startNode)
        {
            _defaultPath = new WalkingPath<T>();
            _defaultPath.Step(startNode);
        }

        public WalkingPath<T> Traverse(T value)
        {
            var paths = new Collection<WalkingPath<T>> {_defaultPath};
            var startNode = _defaultPath.Path[0];

            if (startNode.Neighbours.Count == 0) return _defaultPath;

            var currentPaths = new Collection<WalkingPath<T>>(paths.ToArray());

            while (currentPaths.Count(walk => walk.Path.Count(path => path.Neighbours != null) > 0) > 0)
            {
                paths = new Collection<WalkingPath<T>>();

                foreach (var walkingPath in currentPaths)
                {
                    var currentNode = walkingPath.Path.Last();

                    foreach (var step in currentNode.Neighbours)
                    {
                        var branch = new WalkingPath<T>(walkingPath);
                        branch.Step(step);

                        if (step.Value.Equals(value)) return branch;
                        if (branch.IsValid)
                        {
                            paths.Add(branch);
                        }
                    }
                }

                currentPaths = new Collection<WalkingPath<T>>(paths.ToArray());
            }

            throw new ArgumentOutOfRangeException("value", value, "The value was not found in the graph");
        }       
    }

    public class WalkingPath<T>
    {
        private readonly Collection<GraphNode<T>> _path;

        public WalkingPath()
        {
            _path = new Collection<GraphNode<T>>();
            IsValid = true;
        }

        public WalkingPath(WalkingPath<T> walkingPath)
        {
            _path = new Collection<GraphNode<T>>();
            foreach (var step in walkingPath.Path)
            {
                _path.Add(step);
            }

            CheckValidity();
        }

        public Collection<GraphNode<T>> Path
        {
            get { return _path; }
        }

        public bool IsValid { get; private set; }

        public void Step(GraphNode<T> nextStep)
        {
            _path.Add(nextStep);
            CheckValidity();
        }

        private void CheckValidity()
        {
            IsValid = _path.Distinct().Count() == Path.Count;
        }
    }
}