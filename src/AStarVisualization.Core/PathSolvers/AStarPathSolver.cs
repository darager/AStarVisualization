using AStarVisualization.Core.Exceptions;
using System.Linq;
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

        public AStarPathSolver(ref Node[,] map, bool diagonalsAllowed = false)
        {
            this.map = map;
            this.diagonalsAllowed = diagonalsAllowed;
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
        public async Task<List<Node>> FindPath() // TODO handle movementcost values
        {
            EnsureMapValidity(this.map);
            InitDataStructures(this.map);
            ComputeHeuristicCosts(this.map);
            SetNodeIndices(this.map);
            (this.startNode, this.goalNode) = GetStartAndGoal(map);

            // 1.st step
            currentNode = startNode;
            currentNode.MovementCost = 0;
            openSet.Add(currentNode.TotalCost, currentNode);

            while (openSet.Count > 0 && currentNode != goalNode)
            {
                currentNode = openSet.Pop().Value;

                // get the neighbors
                List<Node> neighbors = map.GetNeighbors(currentNode.RowIndex, currentNode.ColIndex, this.diagonalsAllowed);
                // get the successors out of the neighbors (already visited, wall, similar,...)
                List<Node> successors = neighbors.Where(n => (n.State == NodeState.Ground) || (n.State == NodeState.Goal)).ToList<Node>();
                // set the movementcost of all the successors
                successors.ForEach(n => SetSuccessorMovementCost(currentNode, n));
                // add all of the successors to the openSet
                foreach (var successor in successors)
                {
                    successor.Parent = currentNode;
                    if (successor.State != NodeState.Goal)
                        successor.State = NodeState.GroundToBeVisited;

                    openSet.Add(successor.TotalCost, successor);
                }
                // add the currentnode to the closedSet
                currentNode.State = NodeState.GroundVisited;
                closedSet.Add(currentNode);
            }

            List<Node> path = await ReconstructPath(currentNode);

            return path;
        }


        private async Task<List<Node>> ReconstructPath(Node node)
        {
            var path = new List<Node>();

            while (node.Parent != null)
            {
                path.Add(node);
                node = node.Parent;
            }
            path.Add(node.Parent);
            path.Reverse();

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
        private void SetSuccessorMovementCost(Node current, Node successor)
        {
            int dx = currentNode.ColIndex - successor.ColIndex;
            int dy = currentNode.RowIndex - successor.RowIndex;

            successor.MovementCost = Math.Sqrt(dx * dx + dy * dy);
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