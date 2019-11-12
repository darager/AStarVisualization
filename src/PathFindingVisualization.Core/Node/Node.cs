using System;

namespace PathFindingVisualization.Core.Node
{
    public class Node : INode
    {
        public NodeState State
        {
            get => _state;
            set
            {
                if (_state == value)
                    return;

                _state = value;
                StateChanged?.Invoke(this, (RowIndex, ColIndex));
            }
        }
        private NodeState _state;
        public bool IsWalkable => (this.State != NodeState.Wall);
        public INode Parent { get; set; }
        public int RowIndex => _rowIndex;
        private int _rowIndex;
        public int ColIndex => _colIndex;
        private int _colIndex;

        public Node(NodeState state)
        {
            this.State = state;
        }

        public void SetIndices(int rowIndex, int colIndex)
        {
            _rowIndex = rowIndex;
            _colIndex = colIndex;
        }
        public bool Equals(INode other)
        {
            if (other is null) return false;
            if (other.GetType() != typeof(Node)) return false;

            var otherNode = (Node)other;
            if (ReferenceEquals(this, otherNode)) return true;

            return (this.RowIndex == otherNode.RowIndex)
                && (this.ColIndex == otherNode.ColIndex);
        }

        public event EventHandler<(int rowIndex, int colIdndex)> StateChanged;
    }
}