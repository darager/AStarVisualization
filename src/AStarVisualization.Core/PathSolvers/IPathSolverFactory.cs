namespace AStarVisualization.Core.PathSolvers
{
    public interface IPathSolverFactory
    {
        IPathSolver GetPathSolver(ref Map.Map map, PathSolver pathSolverType, bool diagonalsEnabled);
    }
}