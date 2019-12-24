using PathFindingVisualization.Core.Node;

namespace PathFindingVisualization.Core.PathSolvers.AStar
{
    public class AStarNode : IAlgorithmNode
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

        // AStar specific properties
        public double Heuristic { get; set; } = 0.0;
        public double MovementCost { get; set; } = 0.0;
        public double TotalCost => Heuristic + MovementCost; // TODO: adjust the values for efficient pathfinding

        private Node.Node _node;

        public AStarNode(Node.Node node)
        {
            _node = node;
        }

        public Node.Node GetUnderlyingNode() => _node;
    }
}
