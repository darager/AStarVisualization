using System.Collections.Generic;
using System;

namespace AStarVisualization.Core.Map
{
    public static class MapExtensions
    {
        public static List<Node> GetNeighbors(this Map map, int rowIdx, int colIdx, bool diagonalsEnabled)
        {
            var neighbors = new List<Node>();

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
        public static void UpdateNodeIndices(this Map map) // TODO: write tests for this extension method
        {
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    map[i, j].SetIndices(i, j);
        }
        public static int GetNeighborCount(this Map map, int rowIdx, int colIdx, bool diagonalsEnabled)
        {
            return map.GetNeighbors(rowIdx, colIdx, diagonalsEnabled).Count;
        }
        private static bool IsDiagonalNeighbor(int rowIdx, int colIdx, int nRowidx, int nColIdx)
        {
            return ((Math.Abs(rowIdx - nRowidx)) == 1) && ((Math.Abs(colIdx - nColIdx) == 1));
        }
    }
}
