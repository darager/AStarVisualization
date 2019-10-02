using AStarVisualization.Core.Exceptions;
using AStarVisualization.DataStructures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// TODO: clean up this class

namespace AStarVisualization.Core.PathSolvers
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

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
        public Task<List<Node>> FindPath() // TODO handle movementcost values
        {
            EnsureMapValidity(this.map);
            InitDataStructures(this.map);
            ComputeHeuristicCosts(this.map);
            SetNodeIndices(this.map);

            (startNode, goalNode) = GetStartAndGoal(map);
            Node currentNode = startNode;

            // 1.st step
            currentNode.MovementCost = 0;
            openSet.Add(currentNode.TotalCost, currentNode);
        }


        private List<Node> ReconstructPath(Node node)
        {
            var path = new List<Node>();

            while (node.Parent != null)
            {
                path.Add(node);
                node = node.Parent;
            }
            path.Add(node.Parent);

            return path;
        }
        private void ComputeHeuristicCosts(Node[,] map, double D = 1000.0) // TODO remove this
        {
            (int goalRowIdx, int goalColIdx) = GetGoalIndices();

            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    var node = map[i, j];
                    node.Heuristic = D * (Math.Abs(i - goalRowIdx) + Math.Abs(j - goalColIdx));
                }

            (int, int) GetGoalIndices()
            {
                int rowIdx = -1;
                int colIdx = -1;

                for (int i = 0; i < map.GetLength(0); i++)
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        var node = map[i, j];
                        if (node.State == NodeState.Goal)
                        {
                            rowIdx = i;
                            colIdx = j;
                        }
                    }

                return (rowIdx, colIdx);
            }
        }
        private void InitDataStructures(Node[,] map)
        {
            int numNodes = map.Length;
            this.openSet = new MinPriorityQueue<double, Node>(numNodes);
            this.closedSet = new HashSet<Node>();
        }
        private void SetNodeIndices(Node[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    map[i, j].SetIndices(i, j);
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