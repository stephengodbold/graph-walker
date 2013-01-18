using System.Linq;

namespace Walker
{
    public static class GraphNodeExtensions
    {
        public static GraphNode<T> FindRelative<T>(this GraphNode<T> startNode, T value)
        {
            var walker = new GraphWalker<T>(startNode);
            var walkingPath = walker.WalkToNode(value);

            return walkingPath.Path.Last();
        }
    }
}
