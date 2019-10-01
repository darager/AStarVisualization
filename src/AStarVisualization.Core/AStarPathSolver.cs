using AStarVisualization.Core.Exceptions;
using AStarVisualization.DataStructures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// TODO: clean up this class

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

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
        public Task<List<Node>> FindPath()
        {
            EnsureMapValidity(this.map);
            ComputeHeuristicCosts(this.map);
            InitDataStructures(this.map);

            (startNode, goalNode) = GetStartAndGoal(map);

            // 1. step
            currentNode = startNode;
            currentNode.MovementCost = 0;

            if (currentNode == goalNode)
                return Task.Factory.StartNew(() => new List<Node>());

            openSet.Add(currentNode.TotalCost, currentNode);

            // x. Step
            while (true)
            {
                if (openSet.Count == 0)
                    throw new NoPathFoundException();

                currentNode = openSet.GetMinPriorityPair().Value;

                if (currentNode == goalNode)
                    return Task.Factory.StartNew(() => ReconstructPath(currentNode));

                List<Node> neighbors = map.GetNeighbors();
                List<Node> successors = // neighbors without the visited nodes and without the walls

               foreach (Node successor in successors)
                {
                    successor.MovementCost = currentNode.MovementCost + MovementCost(successor, currentNode);
                    successor.Parent = currentNode;

                    openSet.Add(successor);
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);
            }

            return Task.Factory.StartNew(() => new List<Node>()); // TODO remove this
        }

        private List<Node> ReconstructPath(Node currentNode)
        {
            // TODO: implement this method
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