using System.Linq;

namespace Walker
{
    public static class GraphNodeExtensions
    {
        public static GraphNode<T> Find<T>(this GraphNode<T> startNode, T value)
        {
            var walker = new GraphWalker<T>(startNode);
            var path = walker.TraverseTo(value);

            return path.Last();
        }
    }
}
