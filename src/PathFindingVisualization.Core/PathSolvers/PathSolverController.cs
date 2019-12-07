using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PathFindingVisualization.Core.Map;

namespace PathFindingVisualization.Core.PathSolvers
{
    public class PathSolverController
    {
        private PathSolverFactory _pathSolverFactory;
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public PathSolverController(PathSolverFactory pathSolverFactory)
        {
            _pathSolverFactory = pathSolverFactory;
        }

        public async void StartPathSolver(Map.Map map, PathSolver pathsolverType, bool diagonalsEnabled)
        {
            if (!map.IsValid())
                return;

            await Task.Factory.StartNew(() =>
            {
                IPathSolver pathSolver = _pathSolverFactory.GetPathSolver(map, pathsolverType, diagonalsEnabled);

                PerformPathsolvingAlgorithm(pathSolver);
            }, _tokenSource.Token);
        }
        public void ResetPathSolver()
        {
            _tokenSource.Cancel();
        }

        private async Task PerformPathsolvingAlgorithm(IPathSolver pathSolver)
        {
            while (!pathSolver.AlgorithmDone)
                await pathSolver.PerformAlgorithmStep();

            PathChanged?.Invoke(this, pathSolver.Path);
        }

        public event EventHandler<List<Node.Node>> PathChanged;
    }
}
