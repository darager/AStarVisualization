namespace PathFindingVisualization.Core.Node
{
    public class Node
    {
        public Node Parent { get; set; }
        public int RowIndex { get; private set; }
        public int ColIndex { get; private set; }
        public bool IsWalkable => (this.State != NodeState.Wall);
        public NodeState State
        {
            get => _state;
            set
            {
                if (_state == value)
                    return;

                NodeState oldState = _state;
                _state = value;

                StateChanged?.Invoke(this, new StateChangedEventArgs(this, _state, oldState));
            }
        }
        private NodeState _state;

        public Node(NodeState state, int rowIndex, int colIndex)
        {
            this.State = state;
            this.RowIndex = rowIndex;
            this.ColIndex = colIndex;
        }

        public bool Equals(Node other) => NodeExtensions.Equals(this, typeof(Node), other);

        public delegate void StateChangedEventHandler(object sender, StateChangedEventArgs e);
        public event StateChangedEventHandler StateChanged;
    }
}