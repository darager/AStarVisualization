using PathFindingVisualization.Core.Map;

namespace PathFindingVisualization.Core.PathSolvers
{
    public interface IPathSolverFactory
    {
        IPathSolver GetPathSolver(ref IMap map, PathSolver pathSolverType, bool diagonalsEnabled);
    }
}