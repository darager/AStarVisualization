using AStarVisualization.Core.Exceptions;
using System;
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
            if (map == null)
                throw new ArgumentNullException();

            if (map.GetLength(0) == 1 || map.GetLength(1) == 1)
                throw new MapTooSmallException();

            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}