using System;
using System.Collections.Generic;
using PathFindingVisualization.DataStructures;
using PathFindingVisualization.Core.Map;

namespace PathFindingVisualization.Core.PathSolvers.AStar
{
    public class AStarData
    {
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

            ComputeHeuristicCosts();
        }

        private (AStarNode StartNode, AStarNode GoalNode) GetStartAndGoal(Map.Map map)
        {
            (var originalStart, var originalGoal) = MapExtensions.GetStartAndGoal(map);

            AStarNode start = Map[originalStart.RowIndex, originalStart.ColIndex];
            AStarNode goal = Map[originalGoal.RowIndex, originalGoal.ColIndex];

            return (start, goal);
        }

        private void ComputeHeuristicCosts(double D = 1000.0) // TODO: do something with D
        {
            int goalRowIdx = GoalNode.RowIndex;
            int goalColIdx = GoalNode.ColIndex;

            foreach (AStarNode astarNode in Map)
            {
                int rowIdx = astarNode.RowIndex;
                int colIdx = astarNode.ColIndex;
                // this particular heuristic is the Manhattan distance which is used for grid layouts
                astarNode.Heuristic = D * (Math.Abs(rowIdx - goalRowIdx) + Math.Abs(colIdx - goalColIdx));
            }
        }
    }
}
