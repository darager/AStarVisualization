using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using PathFindingVisualization.Core.Map;

namespace PathFindingVisualization.Core.PathSolvers.Dijkstra
{
    public class DijkstraPathSolver : IPathSolver
    {
        public List<Node.Node> Path => throw new NotImplementedException();
        public bool StopAlgorithm { get; private set; } = false;

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

        //  1. mark all the nodes as unvisited
        //       create set of unvisited nodes (unvisited)
        //  2. calculate tentative distance for every node
        //       (0 start node and infinity for all others)
        //       set start as current
        //  3. get neighbors of current
        //       set their parents to current
        //       calc tentative score for each of them and add
        //       tentative score of current and current to neighbor
        //  4. mark the current node as visited
        //       remove it from unvisited set (will never be checked again)
        //  5. if the smallest tentative score is infinite or goal has been marked visited => stop
        //      otherwise select node with smallest tentative score => set it as current, continue at 3

        public void PerformFirstStep()
        {
            SetTentativeScore(_data.Map);

            DijkstraNode start = _data.StartNode;
            start.MovementCost = 0;

            _data.NextToBeVisited.Add(start);
        }

        public void PerformStep()
        {
            var nextToBeVisited = _data.NextToBeVisited;
            foreach (DijkstraNode node in nextToBeVisited)
            {
                _data.CurrentNode = node;

                if (nextToBeVisited.Count == 0 || node == _data.GoalNode)
                {
                    StopAlgorithm = true;
                    return;
                }

                var successors = MapExtensions
                    .GetNeighbors<DijkstraNode>(_data.Map.Data, node.RowIndex, node.ColIndex, _data.DiagonalsEnabled)
                    .Where(n => IsValidSuccessor(n.State));

                foreach (DijkstraNode successor in successors)
                {
                    successor.MovementCost = ComputeDistance(successor, node);
                    successor.State = Node.NodeState.GroundToBeVisited;
                    _data.NextToBeVisited.Add(successor);
                }

                node.State = Node.NodeState.GroundVisited;
                _data.NextToBeVisited.Remove(node);
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
