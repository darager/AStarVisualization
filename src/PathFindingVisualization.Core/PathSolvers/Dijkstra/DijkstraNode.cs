using PathFindingVisualization.Core.Node;

namespace PathFindingVisualization.Core.PathSolvers.Dijkstra
{
    public class DijkstraNode : IAlgorithmNode
    {
        public int RowIndex => _node.RowIndex;
        public int ColIndex => _node.ColIndex;
        public NodeState State
        {
            get => _node.State;
            set => _node.State = value;
        }
        public Node.Node Parent
        {
            get => _node.Parent;
            set => _node.Parent = value;
        }

        public double MovementCost { get; set; } = 0.0;

        private Node.Node _node;

        public DijkstraNode(Node.Node node)
        {
            _node = node;
        }

        public Node.Node GetUnderlyingNode() => _node;
    }
}
