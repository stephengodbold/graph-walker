using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Walker
{
    public class GraphNode<T>
    {
        public GraphNode()
        {
            Relatives = new Collection<GraphNode<T>>();
        }

        public T Value { get; set; }
        public ICollection<GraphNode<T>> Relatives { get; private set; }

        public void AddRelative(GraphNode<T> relative)
        {
            Relatives.Add(relative);
        }
    }
}