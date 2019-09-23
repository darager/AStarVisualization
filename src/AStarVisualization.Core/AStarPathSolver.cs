using System.Collections.Generic;
using System.Threading.Tasks;

namespace AStarVisualization.Core
{
    public class AStarPathSolver : IPathSolver
    {
        private Node[,] map;

        public AStarPathSolver(ref Node[,] map)
        {
            this.map = map;
        }

        public Task<List<Node>> FindPath()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}