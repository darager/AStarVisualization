using System;
using System.Collections.Generic;
using System.Text;

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

        public Node(Node parent = null)
        {
            this._parent = parent;
        }
    }
}
