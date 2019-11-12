using System;
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
        private Node.Node _node;

        public double Heuristic { get; set; }
        public double MovementCost { get; set; }
        public double TotalCost => Heuristic + MovementCost; // TODO: adjust the values for efficient pathfinding
        //public bool AlreadyVisited => (this.State != NodeState.Ground) && (this.State != NodeState.Wall); // not required ?

        public AStarNode(Node.Node node)
        {
            _node = node;
        }

        public void SetIndices(int rowIndex, int colIndex) => _node.SetIndices(rowIndex, colIndex);
        public bool Equals(INode other)
        {
            if (other is null) return false;
            if (other.GetType() != typeof(AStarNode)) return false;

            var otherNode = (AStarNode)other;
            if (ReferenceEquals(this, otherNode)) return true;

            return (this.RowIndex == otherNode.RowIndex)
                && (this.ColIndex == otherNode.ColIndex);
        }

        public event EventHandler<(int rowIndex, int colIdndex)> StateChanged;
    }
}
