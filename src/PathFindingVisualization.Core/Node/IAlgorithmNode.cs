using System.Collections.Generic;

namespace PathFindingVisualization.Core.Node
{
    public interface IAlgorithmNode
    {
        public int RowIndex { get; }
        public int ColIndex { get; }
        public NodeState State { get; set; }

        public Node GetUnderlyingNode();
    }
}
