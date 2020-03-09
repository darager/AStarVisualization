using System;
using System.Collections.Generic;

namespace PathFindingVisualization.Core.Node
{
    public static class NodeExtensions
    {
        public static bool Equals(Node node, Type nodeType, Node other)
        {
            if (other is null)
                return false;
            if (other.GetType() != nodeType)
                return false;
            if (ReferenceEquals(node, other))
                return true;

            return (node.RowIndex, node.ColIndex)
                == (other.RowIndex, other.ColIndex);
        }
        public static List<Node> ReconstructPath(Node startNode, Node currentNode)
        {
            var path = new List<Node>();

            if (currentNode.State != NodeState.Goal)
                return path;

            Node node = currentNode;

            while (node.Parent != null)
            {
                path.Add(node);
                node = node.Parent;
            }
            path.Add(startNode);

            return path;
        }
        public static List<Node> ReconstructPath(IAlgorithmNode startNode, IAlgorithmNode currentNode)
        {
            return ReconstructPath(startNode.GetUnderlyingNode(), currentNode.GetUnderlyingNode());
        }
    }
}
