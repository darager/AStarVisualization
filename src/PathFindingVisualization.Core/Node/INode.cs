using System;

namespace PathFindingVisualization.Core.Node
{
    public interface INode : IEquatable<INode>
    {
        INode Parent { get; set; }
        NodeState State { get; set; }
        bool IsWalkable { get; }
        int ColIndex { get; }
        int RowIndex { get; }

        Node GetStandardNodeImplementationEquivalent();
    }
}
