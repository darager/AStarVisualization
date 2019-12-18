using System.Collections.Generic;
using PathFindingVisualization.Core.Map;

namespace PathFindingVisualization.Core.PathSolvers.Dijkstra
{
    public class DijkstraData
    {
        public int Step { get; set; } = 0;
        public bool DiagonalsEnabled { get; private set; }

        public DijkstraMap Map { get; private set; }
        public DijkstraNode StartNode { get; set; }
        public DijkstraNode GoalNode { get; set; }

        public DijkstraNode CurrentNode { get; set; }
        public List<DijkstraNode> NextToBeVisited;

        public DijkstraData(Map.Map map, bool diagonalsEnabled)
        {
            this.DiagonalsEnabled = diagonalsEnabled;
            this.NextToBeVisited = new List<DijkstraNode>();
            this.Map = new DijkstraMap(map);

            (StartNode, GoalNode) = GetStartAndGoal(map);
        }

        private (DijkstraNode StartNode, DijkstraNode GoalNode) GetStartAndGoal(Map.Map map)
        {
            (var originalStart, var originalGoal) = MapExtensions.GetStartAndGoal(map);

            DijkstraNode start = Map[originalStart.RowIndex, originalStart.ColIndex];
            DijkstraNode goal = Map[originalGoal.RowIndex, originalGoal.ColIndex];

            return (start, goal);
        }
    }
}
