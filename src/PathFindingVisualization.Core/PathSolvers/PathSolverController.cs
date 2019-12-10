using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PathFindingVisualization.Core.Map;

namespace PathFindingVisualization.Core.PathSolvers
{
    public class PathSolverController
    {
        private PathSolverFactory _pathSolverFactory;
        private IPathSolver _pathSolver;

        public PathSolverController(PathSolverFactory pathSolverFactory)
        {
            _pathSolverFactory = pathSolverFactory;
        }

        public async Task StartPathSolver(Map.Map map, PathSolver pathsolverType, bool diagonalsEnabled)
        {
            if (!map.IsValid())
                throw new Exception("The map is not valid.");

            _pathSolver = _pathSolverFactory.GetPathSolver(map, pathsolverType, diagonalsEnabled);

            await PerformPathsolvingAlgorithm(_pathSolver);
        }
        public async Task ResetPathSolver(Map.Map map)
        {
            await _pathSolver.Stop();
        }

        private async Task PerformPathsolvingAlgorithm(IPathSolver pathSolver)
        {
            while (!pathSolver.StopAlgorithm)
                await pathSolver.PerformAlgorithmStep();

            PathChanged?.Invoke(this, pathSolver.Path);
        }

        public event EventHandler<List<Node.Node>> PathChanged;
    }
}
