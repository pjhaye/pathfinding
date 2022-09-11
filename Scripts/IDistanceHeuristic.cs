namespace Pathfinding
{
    public interface IDistanceHeuristic<T> where T : INode
    {
        public float Evaluate(T nodeA, T nodeB);
    }
}