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
        public bool AlgorithmDone { get; private set; } = false;
        public List<Node.Node> Path => _data.CurrentNode.ReconstructPath(_data.StartNode);

        private AStarData _data;

        public AStarPathSolver(Map.Map map, bool diagonalsEnabled)
        {
            _data = new AStarData(map, diagonalsEnabled);
            ComputeHeuristicCosts(_data.Map, _data.GoalNode, 1000); //TODO: adjust the D value
            PerformFirstStep();
        }

        private void PerformFirstStep()
        {
            _data.CurrentNode = _data.StartNode;
            _data.CurrentNode.MovementCost = 0;
            _data.OpenSet.Add(_data.CurrentNode.TotalCost, _data.CurrentNode);
        }
        public Task PerformAlgorithmStep() => Task.Run(PerformStep);
        public void PerformStep()
        {
            if (AlgorithmDone)
                return;

            if (_data.OpenSet.Count == 0 || _data.CurrentNode == _data.GoalNode)
                return;

            _data.CurrentNode = _data.OpenSet.Pop().Value;

            // get the successors (already visited, wall, similar,...)
            int rowIndex = _data.CurrentNode.RowIndex;
            int colIndex = _data.CurrentNode.ColIndex;
            List<AStarNode> successors =
                 MapExtensions.GetNeighbors<AStarNode>(_data.Map.Data, rowIndex, colIndex, _data.DiagonalsEnabled)
                .AsEnumerable<AStarNode>()
                .Where(n => (n.State == NodeState.Ground) || (n.State == NodeState.Goal))
                .ToList<AStarNode>();

            // set the movementcost of all the successors
            successors.ForEach(n => SetSuccessorMovementCost(_data.CurrentNode, n));

            // add all of the successors to the _data.openSet
            foreach (var successor in successors)
            {
                successor.Parent = _data.CurrentNode.GetUnderlyingNode();
                if (successor.State != NodeState.Goal)
                    successor.State = NodeState.GroundToBeVisited;

                _data.OpenSet.Add(successor.TotalCost, successor);
            }

            // add the currentnode to the closedSet
            if (_data.CurrentNode.State != NodeState.Goal && _data.CurrentNode.State != NodeState.Start)
                _data.CurrentNode.State = NodeState.GroundVisited;
            _data.ClosedSet.Add(_data.CurrentNode);
        }

        private void ComputeHeuristicCosts(AStarMap map, AStarNode goal, float D = 1000)
        {
            int goalRowIdx = goal.RowIndex;
            int goalColIdx = goal.ColIndex;

            foreach (AStarNode node in map)
            {
                int rowIdx = node.RowIndex;
                int colIdx = node.ColIndex;
                // this particular heuristic is the Manhattan distance which is used for grid layouts
                node.Heuristic = D * (Math.Abs(rowIdx - goalRowIdx) + Math.Abs(colIdx - goalColIdx));
            }
        }
        private void SetSuccessorMovementCost(AStarNode current, AStarNode successor)
        {
            int dx = _data.CurrentNode.ColIndex - successor.ColIndex;
            int dy = _data.CurrentNode.RowIndex - successor.RowIndex;

            successor.MovementCost = Math.Sqrt(dx * dx + dy * dy);
        }
    }
}