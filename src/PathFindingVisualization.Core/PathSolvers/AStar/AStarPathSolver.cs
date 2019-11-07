using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathFindingVisualization.Core.Exceptions;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.DataStructures;

// TODO: clean up this class
// TODO: measure performance, if necessary replace linq for more efficient methods

namespace PathFindingVisualization.Core.PathSolvers.AStar
{
    public class AStarPathSolver : IPathSolver
    {
        private Map.Map map;
        private readonly bool diagonalsAllowed;
        private Node.Node startNode;
        private Node.Node goalNode;
        private Node.Node currentNode;

        private MinPriorityQueue<double, Node.Node> openSet;
        private HashSet<Node.Node> closedSet;

        public AStarPathSolver(ref Map.Map map, bool diagonalsAllowed = false)
        {
            this.map = map;
            this.diagonalsAllowed = diagonalsAllowed;
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
        public async Task<List<Node.Node>> FindPath()
        {
            EnsureMapValidity(this.map);
            InitDataStructures(this.map);
            ComputeHeuristicCosts(this.map);
            (this.startNode, this.goalNode) = GetStartAndGoal(map);

            // 1.st step
            currentNode = startNode;
            currentNode.MovementCost = 0;
            openSet.Add(currentNode.TotalCost, currentNode);

            while (openSet.Count > 0 && currentNode != goalNode)
            {
                currentNode = openSet.Pop().Value;

                // get the neighbors
                List<Node.Node> neighbors = map.GetNeighbors(currentNode.RowIndex, currentNode.ColIndex, this.diagonalsAllowed);
                // get the successors out of the neighbors (already visited, wall, similar,...)
                List<Node.Node> successors = neighbors.Where(n => (n.State == Node.NodeState.Ground) || (n.State == Node.NodeState.Goal)).ToList<Node.Node>();
                // set the movementcost of all the successors
                successors.ForEach(n => SetSuccessorMovementCost(currentNode, n));
                // add all of the successors to the openSet
                foreach (var successor in successors)
                {
                    successor.Parent = currentNode;
                    if (successor.State != Node.NodeState.Goal)
                        successor.State = Node.NodeState.GroundToBeVisited;

                    openSet.Add(successor.TotalCost, successor);
                }

                // add the currentnode to the closedSet
                if (currentNode.State != Node.NodeState.Goal && currentNode.State != Node.NodeState.Start)
                    currentNode.State = Node.NodeState.GroundVisited;
                closedSet.Add(currentNode);

                // TODO: move this to some sort of algorithm controller class that calls the steps of the algorithm
                //await Task.Delay(1); // HACK: remove this!!!
            }

            if (currentNode.State != Node.NodeState.Goal)
                throw new NoPathFoundException();

            List<Node.Node> path = ReconstructPath(currentNode);

            return path;
        }

        private List<Node.Node> ReconstructPath(Node.Node node)
        {
            var path = new List<Node.Node>();

            while (node.Parent != null)
            {
                path.Add(node);
                node = node.Parent;
            }
            path.Add(node);
            path.Reverse();

            return path;
        }
        private void ComputeHeuristicCosts(Map.Map map, double D = 1000.0) // TODO: do something with D
        {
            (int goalRowIdx, int goalColIdx) = GetGoalIndices();

            foreach (Node.Node[] nodes in map)
                foreach (Node.Node node in nodes)
                {
                    int rowIdx = node.RowIndex;
                    int colIdx = node.ColIndex;
                    // this particular heuristic is the Manhattan distance which is used for grid layouts
                    node.Heuristic = D * (Math.Abs(rowIdx - goalRowIdx) + Math.Abs(colIdx - goalColIdx));
                }

            (int, int) GetGoalIndices()
            {
                int rowIdx = -1;
                int colIdx = -1;

                for (int i = 0; i < map.GetLength(0); i++)
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        var node = map[i, j];
                        if (node.State == Node.NodeState.Goal)
                        {
                            rowIdx = i;
                            colIdx = j;
                        }
                    }

                return (rowIdx, colIdx);
            }
        }
        private void InitDataStructures(Map.Map map)
        {
            int numNodes = map.GetLength(0) * map.GetLength(1);
            this.openSet = new MinPriorityQueue<double, Node.Node>(numNodes);
            this.closedSet = new HashSet<Node.Node>();
        }
        private void SetSuccessorMovementCost(Node.Node current, Node.Node successor)
        {
            int dx = currentNode.ColIndex - successor.ColIndex;
            int dy = currentNode.RowIndex - successor.RowIndex;

            successor.MovementCost = Math.Sqrt(dx * dx + dy * dy);
        }
        private void EnsureMapValidity(Map.Map map)
        {
            if (map is null)
                throw new ArgumentNullException();

            if (map.GetLength(0) == 1 || map.GetLength(1) == 1)
                throw new MapTooSmallException();

            if (!MapHasGoalAndPath(map))
                throw new NoWayPointsException();
        }
        private bool MapHasGoalAndPath(Map.Map map)
        {
            bool hasStart = false;
            bool hasGoal = false;

            foreach (Node.Node[] nodes in map)
                foreach (Node.Node node in nodes)
                {
                    if (node.State == Node.NodeState.Start)
                        hasStart = true;
                    else if (node.State == Node.NodeState.Goal)
                        hasGoal = true;
                }

            return (hasStart && hasGoal);
        }
        private (Node.Node, Node.Node) GetStartAndGoal(Map.Map map)
        {
            Node.Node goal = null;
            Node.Node start = null;

            foreach (Node.Node[] nodes in map)
                foreach (Node.Node node in nodes)
                {
                    if (node.State == Node.NodeState.Start)
                        start = node;
                    else if (node.State == Node.NodeState.Goal)
                        goal = node;
                }

            return (start, goal);
        }
    }
}