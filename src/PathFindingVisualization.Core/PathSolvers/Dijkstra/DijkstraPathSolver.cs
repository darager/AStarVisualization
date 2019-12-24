using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;

namespace PathFindingVisualization.Core.PathSolvers.Dijkstra
{
    public class DijkstraPathSolver : IPathSolver
    {
        public bool StopAlgorithm { get; private set; } = false;
        public List<Node.Node> Path
        {
            get => NodeExtensions.ReconstructPath(_data.StartNode, _data.CurrentNode);
        }

        private DijkstraData _data;

        public DijkstraPathSolver(Map.Map map, bool diagonalsEnabled)
        {
            _data = new DijkstraData(map, diagonalsEnabled);
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

        public void PerformFirstStep()
        {
            SetTentativeScore(_data.Map);

            DijkstraNode start = _data.StartNode;
            start.MovementCost = 0;

            _data.NextToBeVisited.Add(start);
        }
        public void PerformStep()
        {
            int count = _data.NextToBeVisited.Count;
            if (count == 0)
            {
                StopAlgorithm = true;
                return;
            }

            var currentlyVisited = new DijkstraNode[count];
            _data.NextToBeVisited.CopyTo(currentlyVisited);

            foreach (DijkstraNode currentNode in currentlyVisited)
            {
                _data.CurrentNode = currentNode;
                if (currentNode == _data.GoalNode)
                {
                    StopAlgorithm = true;
                    return;
                }

                var successors = MapExtensions
                    .GetNeighbors<DijkstraNode>(_data.Map.Data, currentNode.RowIndex, currentNode.ColIndex, _data.DiagonalsEnabled)
                    .Where(n => IsValidSuccessor(n.State));

                foreach (DijkstraNode successor in successors)
                {
                    successor.Parent = currentNode.GetUnderlyingNode();
                    successor.MovementCost = ComputeDistance(successor, currentNode);
                    if (successor.State != NodeState.Goal)
                        successor.State = Node.NodeState.GroundToBeVisited;
                    _data.NextToBeVisited.Add(successor);
                }

                if (currentNode.State != NodeState.Goal && currentNode.State != NodeState.Start)
                    currentNode.State = Node.NodeState.GroundVisited;
                _data.NextToBeVisited.Remove(currentNode);
            }
        }

        public async Task Stop()
        {
            StopAlgorithm = true;
        }

        private void SetTentativeScore(DijkstraMap map)
        {
            foreach (DijkstraNode node in map)
            {
                node.MovementCost = double.PositiveInfinity;
            }
        }
        private bool IsValidSuccessor(Node.NodeState state)
        {
            return state == Node.NodeState.Ground
                || state == Node.NodeState.Goal;
        }
        private double ComputeDistance(DijkstraNode node1, DijkstraNode node2)
        {
            int dx = Math.Abs(node1.ColIndex - node2.ColIndex);
            int dy = Math.Abs(node1.RowIndex - node2.RowIndex);

            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
