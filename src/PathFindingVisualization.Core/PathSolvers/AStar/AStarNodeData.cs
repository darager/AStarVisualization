namespace PathFindingVisualization.Core.PathSolvers.AStar
{
    public class AStarNodeData
    {
        public double Heuristic { get; set; } = 0.0;
        public double MovementCost { get; set; } = 0.0;
        public double TotalCost => Heuristic + MovementCost; // TODO: adjust the values for efficient pathfinding
    }
}
