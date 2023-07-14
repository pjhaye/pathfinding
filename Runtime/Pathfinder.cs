using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public static class Pathfinder
    {
        public static List<T> GetNodePath<T>(
            IDistanceHeuristic<T> distanceHeuristic,
            T startNode, 
            T goalNode) where T : class, INode
        {
            if (startNode == null)
            {
                Debug.LogError($"{nameof(startNode)} is null!");
                return null;
            }
            
            if (goalNode == null)
            {
                Debug.LogError($"{nameof(goalNode)} is null!");
                return null;
            }
            
            var open = new List<PathfinderNode>();
            var closedList = new List<PathfinderNode>();

            var start = new PathfinderNode()
            {
                Node = startNode,
                F = 0,
                G = 0,
                H = 0
            };
            
            open.Add(start);
            
            while (open.Count > 0)
            {
                open.Sort(delegate(PathfinderNode nodeA, PathfinderNode nodeB)
                {
                    if (nodeA.F > nodeB.F)
                    {
                        return 1;
                    }
                    else if (nodeA.F < nodeB.F)
                    {
                        return -1;
                    }

                    return 0;
                });
                var currentNode = open[0];
                open.Remove(currentNode);
                closedList.Add(currentNode);

                if (currentNode.Node.Equals(goalNode))
                {
                    var path = new List<T>();
                    while (currentNode.Parent != null)
                    {
                        path.Add((T)currentNode.Node);
                        currentNode = currentNode.Parent;
                    }

                    path.Reverse();
                    return path;
                }

                var neighbors = currentNode.Node.Neighbors;
                foreach (var neighborNode in neighbors)
                {
                    var alreadyClosed = false;
                    foreach (var closedNode in closedList)
                    {
                        if (closedNode.Node.Equals(neighborNode))
                        {
                            alreadyClosed = true;
                            break;
                        }
                    }
                    
                    if (alreadyClosed)
                    {
                        continue;
                    }

                    var g = currentNode.G + distanceHeuristic.Evaluate((T)currentNode.Node, (T)neighborNode);
                    var h = distanceHeuristic.Evaluate((T)neighborNode, goalNode);
                    var f = g + h;

                    var neighbor = new PathfinderNode()
                    {
                        Node = neighborNode,
                        G = g,
                        H = h,
                        F = f,
                        Parent = currentNode
                    };

                    var alreadyOpened = false;
                    PathfinderNode alreadyOpenNode = null;
                    foreach (var openNode in open)
                    {
                        if (openNode.Node.Equals(neighborNode))
                        {
                            alreadyOpenNode = openNode;
                            alreadyOpened = true;
                            break;
                        }
                    }

                    if (alreadyOpened)
                    {
                        if (neighbor.G > alreadyOpenNode.G)
                        {
                            continue;
                        }
                    }
                    
                    open.Add(neighbor);
                }
            }

            return null;
        }
    }
}