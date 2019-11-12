using System;
using System.Collections.Generic;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.Core.PathSolvers;
using PathFindingVisualization.Core.PathSolvers.AStar;

namespace PathFindingVisualization.Core.Map
{
    public static class MapExtensions
    {
        public static IEnumerable<INode> GetNeighbors(this Map map, int rowIdx, int colIdx, bool diagonalsEnabled)
        {
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
        private static bool IsDiagonalNeighbor(int rowIdx, int colIdx, int nRowidx, int nColIdx)
        {
            return ((Math.Abs(rowIdx - nRowidx)) == 1)
                && ((Math.Abs(colIdx - nColIdx) == 1));
        }

        public static INode[][] GetAlgorithmSpecificMap(this Map map, PathSolver pathsolverType)
        {
            int numRows = map.GetLength(0);
            int numColumns = map.GetLength(1);

            var algorithmMap = new INode[numRows][];
            for (int i = 0; i < numRows; i++)
            {
                algorithmMap[i] = new INode[numColumns];

                for (int j = 0; j < numColumns; j++)
                {
                    var node = (Node.Node)map[i, j];
                    algorithmMap[i][j] = GetAlgorithmSpecificNode(pathsolverType, node);
                }
            }

            return algorithmMap;
        }
        private static INode GetAlgorithmSpecificNode(PathSolver pathsolverType, Node.Node node)
        {
            return pathsolverType switch
            {
                PathSolver.AStar => new AStarNode(node),
                //PathSolver.BreadthFirstSearch => typeof(BreadthFirstSearchNode),
                //PathSolver.BestFirstSearch => typeof(BestFirstSearchNode),
                //PathSolver.Dijkstra => typeof(DijkstraNode),
                //PathSolver.JumpPointSearch => typeof(JumpPointSearchNode),
                //PathSolver.OrthogonalJumpPointSearch => typeof(OrthogonalJumpPointSearchNode),
                //PathSolver.Trace => typeof(TraceNode),
                _ => throw new Exception("The Node for the algorithm of type '{pathsolverType}' does not exist!")
            };
        }
    }
}
