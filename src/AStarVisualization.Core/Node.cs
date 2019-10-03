namespace AStarVisualization.Core
{
    public class Node
    {
        public double Heuristic { get; set; }
        public double MovementCost { get; set; }
        public NodeState State { get; set; }
        public Node Parent { get; set; }
        public bool IsWalkable => State != NodeState.Wall;
        public bool AlreadyVisited => (State != NodeState.Ground) || (State != NodeState.Goal);
        public double TotalCost => Heuristic + MovementCost; // TODO: 500 used to be here
        public int RowIndex => _rowIndex;
        public int ColIndex => _colIndex;

        private int _rowIndex;
        private int _colIndex;

        public Node(NodeState state, Node parentNode = null)
        {
            this.State = state;
        }

        public void SetIndices(int rowIndex, int colIndex)
        {
            this._rowIndex = rowIndex;
            this._colIndex = colIndex;
        }
    }
}
