using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.DataStructures;

// TODO: make sure that the algorithm is properly stopped and continued
// TODO: make sure that the path is drawn

namespace PathFindingVisualization.Core.PathSolvers.AStar
{
    public class AStarPathSolver : IPathSolver
    {
        public bool AlgorithmDone => throw new NotImplementedException();
        public List<INode> Path => throw new NotImplementedException();
        public IAlgorithmData AlgorithmData { get; private set; }

        public AStarPathSolver(Map.Map map, bool diagonalsEnabled)
        {
            AlgorithmData = new AStarData(map, diagonalsEnabled);
        }

        public async Task PerformAlgorithmStep()
        {
            if (AlgorithmData.AlgorithmStep == 0)
                await Task.Run(PerformFirstStep);
            else
                await Task.Run(PerformStep);

            AlgorithmData.AlgorithmStep++;
        }

        private void PerformFirstStep()
        {
            SetUpDataStructures(_map);
            ComputeHeuristicCosts(_map);

            (_startNode, _goalNode) = _map.GetStartAndGoal();

            _currentNode = (AStarNode)_startNode;
            _currentNode.MovementCost = 0;
            _openSet.Add(_currentNode.TotalCost, _currentNode);
        }
        private void PerformStep()
        {
            if (_openSet.Count == 0 || _currentNode == _goalNode)
            {
                StopAlgorithm();
                return;
            }

            _currentNode = _openSet.Pop().Value;

            // get the neighbors
            IEnumerable<INode> neighbors = _map.GetNeighbors(_currentNode.RowIndex, _currentNode.ColIndex, _diagonalsEnabled);
            // get the successors out of the neighbors (already visited, wall, similar,...)
            List<AStarNode> successors = neighbors
                .Select(n => (AStarNode)n)
                .Where(n => (n.State == NodeState.Ground) || (n.State == NodeState.Goal))
                .ToList<AStarNode>();
            // set the movementcost of all the successors
            successors.ForEach(n => SetSuccessorMovementCost(_currentNode, n));
            // add all of the successors to the _openSet
            foreach (var successor in successors)
            {
                successor.Parent = _currentNode;
                if (successor.State != NodeState.Goal)
                    successor.State = NodeState.GroundToBeVisited;

                _openSet.Add(successor.TotalCost, successor);
            }

            // add the currentnode to the closedSet
            if (_currentNode.State != NodeState.Goal && _currentNode.State != NodeState.Start)
                _currentNode.State = NodeState.GroundVisited;
            _closedSet.Add(_currentNode);
        }

        private void SetUpDataStructures(AStarMap map)
        {
            int numNodes = map.GetLength(0) * map.GetLength(1);
            _openSet = new MinPriorityQueue<double, AStarNode>(numNodes);
            _closedSet = new HashSet<AStarNode>();
        }
        private void ComputeHeuristicCosts(AStarMap map, double D = 1000.0) // TODO: do something with D
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
                        var node = (AStarNode)map[i, j];
                        if (node.State == NodeState.Goal)
                        {
                            rowIdx = i;
                            colIdx = j;
                        }
                    }

                return (rowIdx, colIdx);
            }
        }
        private void SetSuccessorMovementCost(AStarNode current, AStarNode successor)
        {
            int dx = _currentNode.ColIndex - successor.ColIndex;
            int dy = _currentNode.RowIndex - successor.RowIndex;

            successor.MovementCost = Math.Sqrt(dx * dx + dy * dy);
        }
        private void StopAlgorithm()
        {
            Path = INodeExtensions.ReconstructPath(_startNode, _currentNode);
            AlgorithmDone = true;
        }
    }
}