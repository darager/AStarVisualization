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
            EnsureMapValidity(this.map);

            return Task.Factory.StartNew(() => new List<Node>()); // TODO remove this and implement the algorithm
        }
        private void EnsureMapValidity(Node[,] map)
        {
            if (map == null)
                throw new ArgumentNullException();

            if (map.GetLength(0) == 1 || map.GetLength(1) == 1)
                throw new MapTooSmallException();

            if (!MapHasGoalAndPath(map))
                throw new NoWayPointsException();
        }
        private bool MapHasGoalAndPath(Node[,] map)
        {
            bool hasStart = false;
            bool hasGoal = false;

            foreach (Node node in map)
            {
                if (node.State == NodeState.Start)
                    hasStart = true;
                if (node.State == NodeState.Goal)
                    hasGoal = true;
            }

            return (hasStart && hasGoal);
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}