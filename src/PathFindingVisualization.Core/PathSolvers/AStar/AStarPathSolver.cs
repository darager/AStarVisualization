using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;

namespace PathFindingVisualization.Core.PathSolvers.AStar
{
    public class AStarPathSolver : IPathSolver
    {
        public bool StopAlgorithm { get; private set; } = false;
        public List<Node.Node> Path
        {
            get
            {
                var path = new List<Node.Node>();
                AStarNode node = _data.CurrentNode;

                if (node.State == NodeState.Goal)
                    path = node.ReconstructPath(_data.StartNode);

                return path;
            }
        }

        private AStarData _data;

        public AStarPathSolver(Map.Map map, bool diagonalsEnabled)
        {
            _data = new AStarData(map, diagonalsEnabled);
        }

        public async Task PerformAlgorithmStep()
        {
            if (StopAlgorithm)
                return;

            if (_data.Step == 0)
                await Task.Run(PerformFirstStep);
            else
                await Task.Run(PerformStep);

            _data.Step++;
        }
        private void PerformFirstStep()
        {
            _data.CurrentNode = _data.StartNode;
            _data.CurrentNode.MovementCost = 0;
            _data.OpenSet.Add(_data.CurrentNode.TotalCost, _data.CurrentNode);

            ComputeHeuristicCosts(_data, 80000); // TODO: adjust this value
        }
        public void PerformStep()
        {
            if (_data.OpenSet.Count == 0 || _data.CurrentNode == _data.GoalNode)
            {
                StopAlgorithm = true;
                return;
            }

            _data.CurrentNode = _data.OpenSet.Pop().Value;

            // get the successors (already visited, wall, similar,...)
            int rowIndex = _data.CurrentNode.RowIndex;
            int colIndex = _data.CurrentNode.ColIndex;
            List<AStarNode> successors =
                 MapExtensions.GetNeighbors<AStarNode>(_data.Map.Data, rowIndex, colIndex, _data.DiagonalsEnabled)
                .Where(n => (n.State == NodeState.Ground) || (n.State == NodeState.Goal))
                .ToList<AStarNode>();

            // set the movementcost of all the successors
            successors.ForEach(n => SetSuccessorMovementCost(_data.CurrentNode, n));

            // add all of the successors to the _data.openSet
            foreach (AStarNode successor in successors)
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

        private void SetSuccessorMovementCost(AStarNode current, AStarNode successor)
        {
            int dx = _data.CurrentNode.ColIndex - successor.ColIndex;
            int dy = _data.CurrentNode.RowIndex - successor.RowIndex;

            successor.MovementCost = Math.Sqrt(dx * dx + dy * dy);
        }

        public async Task Stop()
        {
            StopAlgorithm = true;
            // TODO: refactor
            //await Task.Run(() =>
            //{
            //    foreach (AStarNode node in _data.Map)
            //        if (node.State == Node.NodeState.GroundVisited || node.State == Node.NodeState.GroundToBeVisited)
            //            node.State = Node.NodeState.Ground;
            //});
        }

        private void ComputeHeuristicCosts(AStarData data, double D = 1000.0)
        {
            int goalRowIdx = data.GoalNode.RowIndex;
            int goalColIdx = data.GoalNode.ColIndex;

            foreach (AStarNode astarNode in data.Map)
            {
                int rowIdx = astarNode.RowIndex;
                int colIdx = astarNode.ColIndex;
                // this particular heuristic is the Manhattan distance which is used for grid layouts
                astarNode.Heuristic = D * (Math.Abs(rowIdx - goalRowIdx) + Math.Abs(colIdx - goalColIdx));
            }
        }
    }
}