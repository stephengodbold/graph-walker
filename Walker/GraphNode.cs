using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Walker
{
    public class GraphNode<T>
    {
        public T Value { get; set; }
        public ICollection<GraphNode<T>> Neighbours { get; private set; }

        public GraphNode()
        {
            Neighbours = new Collection<GraphNode<T>>();
        }

        public void Add(GraphNode<T> relative)
        {
            Neighbours.Add(relative);
        }
    }
}