using System;

namespace PathFindingVisualization.Core.Node
{
    public static class INodeExtensions
    {
        public static bool Equals(INode node, Type nodeType, INode otherNode)
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
    }
}
