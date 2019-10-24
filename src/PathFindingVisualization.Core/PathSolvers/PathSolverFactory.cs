namespace PathFindingVisualization.Core.PathSolvers
{
    public class PathSolverFactory : IPathSolverFactory
    {
        public IPathSolver GetPathSolver(ref Map.Map map, PathSolver pathSolverType, bool diagonalsEnabled)
        {
            switch (pathSolverType)
            {
                case PathSolver.AStar:
                    return new AStarPathSolver(ref map, diagonalsEnabled);
                default:
                    return null;
            }
        }
    }
}
