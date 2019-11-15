using PathFindingVisualization.Core.Node;

namespace PathFindingVisualization.Core.PathSolvers.AStar
{
    public class AStarNode : INode
    {
        public NodeState State
        {
            get => _node.State;
            set => _node.State = value;
        }
        public bool IsWalkable => _node.IsWalkable;
        public INode Parent
        {
            get => _node.Parent;
            set => _node.Parent = value;
        }
        public int RowIndex => _node.RowIndex;
        public int ColIndex => _node.ColIndex;
        public double Heuristic { get; set; }
        public double MovementCost { get; set; }
        public double TotalCost => Heuristic + MovementCost; // TODO: adjust the values for efficient pathfinding
        // not required ?
        //public bool AlreadyVisited => (this.State != NodeState.Ground) && (this.State != NodeState.Wall);

        private INode _node;

        public AStarNode(INode node)
        {
            _node = node;
        }

        public bool Equals(INode other) => INodeExtensions.Equals(this, typeof(AStarNode), other);
        public Node.Node GetStandardNodeImplementationEquivalent()
        {
            return new Node.Node(this.State, this.RowIndex, this.ColIndex);
        }
    }
}
