using System.Collections.Generic;
using PathFindingVisualization.DataStructures;

namespace PathFindingVisualization.Core.PathSolvers.AStar
{
    public class AStarData : IAlgorithmData
    {
        public int AlgorithmStep { get; set; } = 0;
        public bool DiagonalsEnabled { get; private set; }

        public MinPriorityQueue<double, Node.Node> OpenSet { get; private set; }
        public HashSet<Node.Node> ClosedSet { get; private set; }
        public Node.Node StartNode { get; set; }
        public Node.Node GoalNode { get; set; }
        public Node.Node CurrentNode { get; set; }

        public AStarNodeData[,] NodeData;

        public AStarData(Map.Map map, bool diagonalsEnabled)
        {
            NodeData = new AStarNodeData[map.GetLength(0), map.GetLength(1)];
            foreach (Node.Node[] nodes in map)
                foreach (Node.Node node in nodes)
                    NodeData[node.RowIndex, node.ColIndex] = new AStarNodeData();
        }
    }
}
