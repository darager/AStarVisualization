using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingVisualization.Core.PathSolvers.Dijkstra
{
    public class DijkstraPathSolver : IPathSolver
    {
        public List<Node.Node> Path => throw new NotImplementedException();
        public bool StopAlgorithm { get; private set; } = false;

        private Node.Node _currentNode;
        private int _step = 0;

        public DijkstraPathSolver(Map.Map map, bool diagonalsEnabled)
        {

        }

        public async Task PerformAlgorithmStep()
        {
            if (StopAlgorithm)
                return;

            if (_step == 0)
                await Task.Run(PerformFirstStep);
            else
                await Task.Run(PerformStep);

            _step++;
        }

        public void PerformFirstStep()
        {
        }
        public void PerformStep()
        {
        }

        public Task Reset()
        {
            throw new NotImplementedException();
        }
    }
}
