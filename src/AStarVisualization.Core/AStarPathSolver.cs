using AStarVisualization.Core.Exceptions;
using AStarVisualization.DataStructures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AStarVisualization.Core
{
    public class AStarPathSolver : IPathSolver
    {
        private Node[,] map;
        private bool diagonalsAllowed;
        private Node startNode;
        private Node goalNode;
        private Node currentNode;

        private MinPriorityQueue<double, Node> openSet;
        private HashSet<Node> closedSet;

        public AStarPathSolver(ref Node[,] map)
        {
            this.map = map;
        }

        public Task<List<Node>> FindPath()
        {
            EnsureMapValidity(this.map);
            InitDataStructures();






            return Task.Factory.StartNew(() => new List<Node>()); // TODO remove this
        }

        private void InitDataStructures()
        {
            this.openSet = new MinPriorityQueue<double, Node>(102341234); // TODO: implement this
            this.closedSet = new HashSet<Node>();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
        private void EnsureMapValidity(Node[,] map)
        {
            if (map is null)
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
                else if (node.State == NodeState.Goal)
                    hasGoal = true;
            }

            return (hasStart && hasGoal);
        }
        private (Node, Node) GetStartAndGoal(Node[,] map)
        {
            Node goal = null;
            Node start = null;

            foreach (var node in map)
            {
                if (node.State == NodeState.Start)
                    start = node;
                else if (node.State == NodeState.Goal)
                    goal = node;
            }

            return (start, goal);
        }
    }
}