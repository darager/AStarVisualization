using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;

namespace PathFindingVisualization.Core.PathSolvers
{
    public class PathSolverController
    {
        public List<Node.Node> Path { get; private set; }

        private bool _pauseAlgorithm = true;

        private IPathSolver _pathSolver;
        private PathSolverFactory _pathSolverFactory;

        public PathSolverController(PathSolverFactory pathSolverFactory)
        {
            _pathSolverFactory = pathSolverFactory;
        }

        public async void StartPathSolver(Map.Map map, PathSolver pathsolverType, bool diagonalsEnabled)
        {
            if (!map.IsValid())
                return;

            IMap algorithmSpecificMap = map.GetAlgorithmSpecificMap(pathsolverType);
            _pathSolver = _pathSolverFactory.GetPathSolver(algorithmSpecificMap, pathsolverType, diagonalsEnabled);

            _pauseAlgorithm = false;
            await PerformPathfindingAlgorithm();
        }
        public void PausePathSolver()
        {
            if (_pathSolver is null)
                return;
            if (_pauseAlgorithm == false)
                return;

            _pauseAlgorithm = true;
        }
        public async void ContinuePathSolver()
        {
            if (_pathSolver is null)
                return;
            if (_pauseAlgorithm == true)
                return;

            _pauseAlgorithm = false;
            await PerformPathfindingAlgorithm();
        }
        public void ResetPathSolver()
        {
            // TODO: make sure that no tiles are changed when then algorithm is already paused
            _pauseAlgorithm = true;
            //_pathSolver = null;
        }

        private async Task PerformPathfindingAlgorithm()
        {
            while (!_pathSolver.AlgorithmDone)
            {
                if (_pauseAlgorithm)
                    return;

                await _pathSolver.PerformAlgorithmStep();

                if (_pauseAlgorithm)
                    return;
            }

            List<INode> algorithmSpecificPath = _pathSolver.Path;
            if (algorithmSpecificPath != null)
                SetPath(algorithmSpecificPath);
        }
        private void SetPath(List<INode> algorithmSpecificPath)
        {
            if (algorithmSpecificPath is null)
                return;

            Path = new List<Node.Node>();
            foreach (INode specificNode in algorithmSpecificPath)
            {
                Node.Node node = specificNode.GetStandardNodeImplementationEquivalent();
                Path.Add(node);
            }

            PathChanged?.Invoke(this, Path);
        }

        public event EventHandler<List<Node.Node>> PathChanged;
    }
}
