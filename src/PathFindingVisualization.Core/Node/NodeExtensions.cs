using System;
using System.Collections.Generic;

namespace PathFindingVisualization.Core.Node
{
    public static class NodeExtensions
    {
        public static bool Equals(Node node, Type nodeType, Node otherNode)
        {
            if (otherNode is null)
                return false;
            if (otherNode.GetType() != nodeType)
                return false;
            if (ReferenceEquals(node, otherNode))
                return true;

            return (node.RowIndex == otherNode.RowIndex)
                && (node.ColIndex == otherNode.ColIndex);
        }
        public static List<Node> ReconstructPath(Node startNode, Node currentNode)
        {
            var path = new List<Node>();
            Node node = currentNode;

            while (node.Parent != null)
            {
                path.Add(node);
                node = node.Parent;
            }
            path.Add(startNode);

            return path;
        }
    }
}
