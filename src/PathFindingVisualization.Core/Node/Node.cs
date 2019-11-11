using System;

// TODO: write wrappers for this class (not ever pathsolver requires the same values!!
// TODO: write tests for this class
namespace PathFindingVisualization.Core.Node
{
    public class Node : IEquatable<Node>
    {
        public double Heuristic { get; set; }
        public double MovementCost { get; set; }

        public NodeState State
        {
            get => _state;
            set
            {
                if (value == _state)
                    return;

                NodeState oldState = _state;
                _state = value;
                StateChanged?.Invoke(this, new StateChangedEventArgs(this, _state, oldState));
            }
        }
        private NodeState _state;

        public int RowIndex => _rowIndex;
        public int ColIndex => _colIndex;
        private int _rowIndex;
        private int _colIndex;

        public bool IsWalkable => State != NodeState.Wall;
        public double TotalCost => Heuristic + MovementCost; // TODO: adjust the values for efficient pathfinding
        public bool AlreadyVisited => (State != NodeState.Ground) || (State != NodeState.Goal);
        public Node Parent { get; set; }

        public Node(NodeState state)
        {
            this.State = state;
        }

        public void SetIndices(int rowIndex, int colIndex)
        {
            _rowIndex = rowIndex;
            _colIndex = colIndex;
        }

        public override bool Equals(object other)
        {
            if (other.GetType() != this.GetType()) return false;
            var node = (Node)other;

            return Equals(node);
        }
        public bool Equals(Node other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return (this.RowIndex == other.RowIndex)
                && (this.ColIndex == other.ColIndex);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return (RowIndex.ToString() + ColIndex.ToString()).GetHashCode();
            }
        }

        public delegate void StateChangedEventHandler(object sender, StateChangedEventArgs e);
        public event StateChangedEventHandler StateChanged;
    }
}
