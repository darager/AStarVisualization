using System.Collections.Generic;
using PathFindingVisualization.DataStructures;
using PathFindingVisualization.Core.Map;

namespace PathFindingVisualization.Core.PathSolvers.AStar
{
    public class AStarData
    {
        public int Step { get; set; } = 0;
        public bool DiagonalsEnabled { get; private set; }

        public AStarMap Map { get; }
        public AStarNode StartNode { get; set; }
        public AStarNode GoalNode { get; set; }

        public AStarNode CurrentNode { get; set; }
        public MinPriorityQueue<double, AStarNode> OpenSet { get; private set; }
        public HashSet<AStarNode> ClosedSet { get; private set; }

        public AStarData(Map.Map map, bool diagonalsEnabled)
        {
            this.DiagonalsEnabled = diagonalsEnabled;
            this.Map = new AStarMap(map);
            (StartNode, GoalNode) = GetStartAndGoal(map);

            int capacity = Map.GetLength(0) * Map.GetLength(1);
            this.OpenSet = new MinPriorityQueue<double, AStarNode>(capacity);
            this.ClosedSet = new HashSet<AStarNode>();
        }

        private (AStarNode StartNode, AStarNode GoalNode) GetStartAndGoal(Map.Map map)
        {
            (var originalStart, var originalGoal) = MapExtensions.GetStartAndGoal(map);

            AStarNode start = Map[originalStart.RowIndex, originalStart.ColIndex];
            AStarNode goal = Map[originalGoal.RowIndex, originalGoal.ColIndex];

            return (start, goal);
        }
    }
}
