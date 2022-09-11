using System.Collections.Generic;

namespace Pathfinding
{
    public interface INode
    {
        public List<INode> Neighbors { get; }
    }
}