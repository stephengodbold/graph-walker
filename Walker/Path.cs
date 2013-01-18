using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Walker
{
    public class Path<T> : IEnumerable<GraphNode<T>>
    {
        private readonly Collection<GraphNode<T>> _path;

        public Path()
        {
            _path = new Collection<GraphNode<T>>();
            IsValid = true;
        }

        public Path(IEnumerable<GraphNode<T>> path)
        {
            _path = new Collection<GraphNode<T>>();
            foreach (var step in path)
            {
                _path.Add(step);
            }

            CheckValidity();
        }

        public bool IsValid { get; private set; }

        public void Step(GraphNode<T> nextStep)
        {
            _path.Add(nextStep);
            CheckValidity();
        }

        private void CheckValidity()
        {
            IsValid = _path.Distinct().Count() == _path.Count;
        }

        public IEnumerator<GraphNode<T>> GetEnumerator()
        {
            return _path.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}