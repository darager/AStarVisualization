using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathFindingVisualization.Core.Exceptions;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.DataStructures;

// TODO: clean up this class
// TODO: measure performance, if necessary replace linq for more efficient methods

namespace PathFindingVisualization.Core.PathSolvers.AStar
{
    public class AStarPathSolver : IPathSolver
    {
        private AStarNode[][] _algorithmSpecificMap;
        private readonly bool _diagonalsAllowed;
        private AStarNode _startNode;
        private AStarNode _goalNode;
        private AStarNode _currentNode;

        private MinPriorityQueue<double, AStarNode> openSet;
        private HashSet<AStarNode> closedSet;

        public AStarPathSolver(ref Map.Map map, bool diagonalsAllowed = false)
        {
            _algorithmSpecificMap = (AStarNode[][])map.GetAlgorithmSpecificMap(PathSolver.AStar);
            _diagonalsAllowed = diagonalsAllowed;
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
        public async Task<List<INode>> FindPath()
        {
            EnsureMapValidity(_algorithmSpecificMap);
            InitDataStructures(_algorithmSpecificMap);
            ComputeHeuristicCosts(_algorithmSpecificMap);
            (INode start, INode goal) = GetStartAndGoal(_algorithmSpecificMap);
            _startNode = (AStarNode)start;
            _goalNode = (AStarNode)goal;

            // 1.st step
            _currentNode = _startNode;
            _currentNode.MovementCost = 0;
            openSet.Add(_currentNode.TotalCost, _currentNode);

            while (openSet.Count > 0 && _currentNode != _goalNode)
            {
                _currentNode = openSet.Pop().Value;

                // get the neighbors
                IEnumerable<INode> neighbors = _algorithmSpecificMap.GetNeighbors(_currentNode.RowIndex, _currentNode.ColIndex, _diagonalsAllowed);
                // get the successors out of the neighbors (already visited, wall, similar,...)
                List<AStarNode> successors = neighbors
                    .Select(n => (AStarNode)n)
                    .Where(n => (n.State == NodeState.Ground) || (n.State == NodeState.Goal))
                    .ToList<AStarNode>();
                // set the movementcost of all the successors
                successors.ForEach(n => SetSuccessorMovementCost(_currentNode, n));
                // add all of the successors to the openSet
                foreach (var successor in successors)
                {
                    successor.Parent = _currentNode;
                    if (successor.State != NodeState.Goal)
                        successor.State = NodeState.GroundToBeVisited;

                    openSet.Add(successor.TotalCost, successor);
                }

                // add the currentnode to the closedSet
                if (_currentNode.State != NodeState.Goal && _currentNode.State != NodeState.Start)
                    _currentNode.State = NodeState.GroundVisited;
                closedSet.Add(_currentNode);

                // TODO: move this to some sort of algorithm controller class that calls the steps of the algorithm
                //await Task.Delay(1); // HACK: remove this!!!
            }

            if (_currentNode.State != NodeState.Goal)
                throw new NoPathFoundException();

            List<INode> path = ReconstructPath(_currentNode);

            return path;
        }

        private List<INode> ReconstructPath(INode node)
        {
            var path = new List<INode>();

            while (node.Parent != null)
            {
                path.Add(node);
                node = node.Parent;
            }
            path.Add(node);
            path.Reverse();

            return path;
        }
        private void ComputeHeuristicCosts(AStarNode[][] map, double D = 1000.0) // TODO: do something with D
        {
            (int goalRowIdx, int goalColIdx) = GetGoalIndices();

            foreach (AStarNode[] nodes in map)
                foreach (AStarNode node in nodes)
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
                        AStarNode node = map[i][j];
                        if (node.State == NodeState.Goal)
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
            this.openSet = new MinPriorityQueue<double, AStarNode>(numNodes);
            this.closedSet = new HashSet<AStarNode>();
        }
        private void SetSuccessorMovementCost(AStarNode current, AStarNode successor)
        {
            int dx = _currentNode.ColIndex - successor.ColIndex;
            int dy = _currentNode.RowIndex - successor.RowIndex;

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

            foreach (INode[] nodes in map)
                foreach (INode node in nodes)
                {
                    if (node.State == NodeState.Start)
                        hasStart = true;
                    else if (node.State == NodeState.Goal)
                        hasGoal = true;
                }

            return (hasStart && hasGoal);
        }
        private (INode, INode) GetStartAndGoal(Map.Map map)
        {
            INode goal = null;
            INode start = null;

            foreach (INode[] nodes in map)
                foreach (INode node in nodes)
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