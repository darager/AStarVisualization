using System;
using PathFindingVisualization.Core.PathSolvers.AStar;
using PathFindingVisualization.Core.PathSolvers.Dijkstra;

namespace PathFindingVisualization.Core.PathSolvers
{
    public class PathSolverFactory
    {
        public IPathSolver GetPathSolver(Map.Map map, PathSolver pathSolverType, bool diagonalsEnabled)
        {
            return pathSolverType switch
            {
                PathSolver.AStar => new AStarPathSolver(map, diagonalsEnabled),
                PathSolver.Dijkstra => new DijkstraPathSolver(map, diagonalsEnabled),
                PathSolver.BestFirstSearch => throw new NotImplementedException(),
                PathSolver.BreadthFirstSearch => throw new NotImplementedException(),
                _ => throw new Exception($"The requested Pathsolving algorithm ({pathSolverType}) is not implemented")
            };
        }
    }
}
