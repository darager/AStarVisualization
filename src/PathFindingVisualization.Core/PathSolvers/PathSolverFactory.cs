using System;
using PathFindingVisualization.Core.PathSolvers.AStar;

namespace PathFindingVisualization.Core.PathSolvers
{
    public class PathSolverFactory
    {
        public IPathSolver GetPathSolver(Map.Map map, PathSolver pathSolverType, bool diagonalsEnabled)
        {
            return pathSolverType switch
            {
                PathSolver.AStar => new AStarPathSolver(map, diagonalsEnabled),
                PathSolver.Dijkstra => throw new NotImplementedException(),
                PathSolver.BestFirstSearch => throw new NotImplementedException(),
                PathSolver.BreadthFirstSearch => throw new NotImplementedException(),
                PathSolver.JumpPointSearch => throw new NotImplementedException(),
                PathSolver.OrthogonalJumpPointSearch => throw new NotImplementedException(),
                _ => throw new Exception($"The requested Pathsolving algorithm ({pathSolverType}) is not implemented")
            };
        }
    }
}
