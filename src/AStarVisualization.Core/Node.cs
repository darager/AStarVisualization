namespace AStarVisualization.Core
{
    public class Node
    {
        public double Heuristic { get; set; }
        public double MovementCost { get; set; }
        public NodeState State { get; set; }
        public Node Parent => _parent;
        private Node _parent;

        public bool IsWalkable => State != NodeState.Wall;
        public double TotalCost => Heuristic + MovementCost * 500; // TODO remove the 500

        public Node(NodeState state, Node parentNode = null)
        {
            this._parent = parentNode;
            this.State = state;
        }
    }
}
