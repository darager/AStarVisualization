using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PathFindingVisualization.Core.Map;

namespace PathFindingVisualization.Core.PathSolvers
{
    public class PathSolverController
    {
        private Map.Map _map;
        private PathSolverFactory _pathSolverFactory;
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public PathSolverController(PathSolverFactory pathSolverFactory)
        {
            _pathSolverFactory = pathSolverFactory;
        }

        public async Task StartPathSolver(Map.Map map, PathSolver pathsolverType, bool diagonalsEnabled)
        {
            if (!map.IsValid())
                return;

            _map = map;

            IPathSolver pathSolver = _pathSolverFactory.GetPathSolver(map, pathsolverType, diagonalsEnabled);

            await PerformPathsolvingAlgorithm(pathSolver);
        }
        public async Task ResetPathSolver(Map.Map map)
        {
            foreach (Node.Node[] nodes in map)
                foreach (Node.Node node in nodes)
                    if (node.State == Node.NodeState.GroundVisited || node.State == Node.NodeState.GroundToBeVisited)
                        node.State = Node.NodeState.Ground;

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
