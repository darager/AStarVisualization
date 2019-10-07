namespace AStarVisualization.Core.PathSolvers
{
    public interface IPathSolverFactory
    {
        IPathSolver GetPathSolver(ref Node[,] map, PathSolver pathSolverType, bool diagonalsEnabled);
    }
}