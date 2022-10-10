namespace Pathfinding
{
    public class PathfinderNode
    {
        public INode Node;
        public float F;
        public float H;
        public float G;
        public PathfinderNode Parent;
    }
}