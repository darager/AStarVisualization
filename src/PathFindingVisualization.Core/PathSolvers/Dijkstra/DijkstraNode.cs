using System.Collections.Generic;
using PathFindingVisualization.Core.Node;

namespace PathFindingVisualization.Core.PathSolvers.Dijkstra
{
    public class DijkstraNode
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
        public List<Node.Node> ReconstructPath(IAlgorithmNode startNode)
        {
            var path = new List<Node.Node>();
            Node.Node node = this.GetUnderlyingNode();

            while (node.Parent != null)
            {
                path.Add(node);
                node = node.Parent;
            }
            path.Add(startNode.GetUnderlyingNode());

            return path;
        }
    }
}
