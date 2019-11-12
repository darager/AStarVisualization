using System;
using System.Collections.Generic;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.Core.PathSolvers;
using PathFindingVisualization.Core.PathSolvers.AStar;

namespace PathFindingVisualization.Core.Map
{
    public static class MapExtensions
    {
        public static int GetLength(this IMap map, int dimension)
        {
            if (dimension > 1 || dimension < 0) throw new ArgumentOutOfRangeException();

            INode[][] data = map.Data;
            int length = 0;
            if (dimension == 0) length = data.GetLength(0);
            if (dimension == 1) length = data[0].GetLength(0);

            return length;
        }
        public static IEnumerable<INode> GetNeighbors(this IMap map, int rowIdx, int colIdx, bool diagonalsEnabled)
        {
            static bool IsDiagonalNeighbor(int rowIdx, int colIdx, int nRowidx, int nColIdx)
            {
                return ((Math.Abs(rowIdx - nRowidx)) == 1)
                    && ((Math.Abs(colIdx - nColIdx) == 1));
            }

            var neighbors = new List<INode>();

            int rowCount = map.GetLength(0);
            int colCount = map.GetLength(1);
            var rowIndices = new int[] { rowIdx - 1, rowIdx, rowIdx + 1 };
            var colIndices = new int[] { colIdx - 1, colIdx, colIdx + 1 };

            foreach (int i in rowIndices)
                foreach (int j in colIndices)
                {
                    if (i >= rowCount || i < 0)
                        continue;
                    if (j >= colCount || j < 0)
                        continue;
                    if (!diagonalsEnabled && IsDiagonalNeighbor(rowIdx, colIdx, i, j))
                        continue;
                    if (i == rowIdx && j == colIdx)
                        continue;

                    neighbors.Add(map[i, j]);
                }

            return neighbors;
        }
        public static IMap GetAlgorithmSpecificMap(this IMap map, PathSolver pathsolverType)
        {
            // TODO: implement more of the algorithms
            return pathsolverType switch
            {
                PathSolver.AStar => new AStarMap(map),
                //PathSolver.BreadthFirstSearch => new BreadthFirstSearchMap(map),
                //PathSolver.BestFirstSearch => new BestFirstSearchMap(map),
                //PathSolver.Dijkstra => new DijkstraMap(map),
                //PathSolver.JumpPointSearch => new JumpPointSearchMap(map),
                //PathSolver.OrthogonalJumpPointSearch => new OrthogonalJumpPointSearchMap(map),
                //PathSolver.Trace => new TraceMap(map),
                _ => throw new Exception($"The Map for the {pathsolverType} algorithm has not yet been implemented!")
            };
        }
    }
}
