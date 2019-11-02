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
                    throw new System.Exception($"The requested Pathsolving algorithm ({pathSolverType}) is not implemented");
            }
        }
    }
}
