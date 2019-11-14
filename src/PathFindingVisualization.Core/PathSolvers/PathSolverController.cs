using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PathFindingVisualization.Core.Map;

namespace PathFindingVisualization.Core.PathSolvers
{
    public class PathSolverController
    {
        private IPathSolverFactory _pathSolverFactory;
        private IPathSolver _pathSolver;
        private bool _algorithmActive;

        public PathSolverController(IPathSolverFactory pathSolverFactory)
        {
            _pathSolverFactory = pathSolverFactory;
        }

        public async Task Start(Map.Map map, PathSolver pathSolverType, bool diagonalsEnabled)
        {
            IMap algorithmSpecificMap = map.GetAlgorithmSpecificMap(pathSolverType);
            _pathSolver = _pathSolverFactory.GetPathSolver(ref algorithmSpecificMap, pathSolverType, diagonalsEnabled);
            await _pathSolver.PerformAlgorithmStep();
        }
        public async Task Pause()
        {
        }
        public async Task Continue()
        {
        }
        public async Task Reset(Map.Map map, PathSolver pathSolverType, bool diagonalsEnabled)
        {
            await Pause();
            await Start(map, pathSolverType, diagonalsEnabled);
        }

        private async Task PerformAlgorithm()
        {
            while (_algorithmActive && !_pathSolver.PathFound)
            {
                await _pathSolver.PerformAlgorithmStep();
            }
        }
    }
}
