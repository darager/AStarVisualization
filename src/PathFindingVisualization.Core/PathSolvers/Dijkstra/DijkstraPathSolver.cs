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
            var nextNodes = _data.NextToBeVisited;


            nextNodes.ForEach(node => MapExtensions
                    .GetNeighbors<DijkstraNode>(_data.Map.Data, node.RowIndex, node.ColIndex, _data.DiagonalsEnabled)
                    .Where(n => n.State == Node.NodeState.Ground)
                    .ToList<DijkstraNode>()
                    .ForEach(n =>
                    {
                        n.State = Node.NodeState.GroundToBeVisited;
                    }));
        }

        public Task Stop()
        {
            throw new NotImplementedException();
        }

        private void SetTentativeScore(DijkstraMap map)
        {
            foreach (DijkstraNode node in map)
            {
                node.MovementCost = double.PositiveInfinity;
            }
        }
    }
}
