using System;
using System.Collections.Generic;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.Core.PathSolvers;

namespace PathFindingVisualization.Core.Map
{
    public static class MapExtensions
    {
        public static bool IsValid(this Map map)
        {
            if (map is null)
                return false;

            if (map.GetLength(0) == 1 || map.GetLength(1) == 1)
                return false;

            if (!map.HasStartAndGoal())
                return false;

            return true;
        }
        private static bool HasStartAndGoal(this Map map)
        {
            (Node.Node start, Node.Node goal) = GetStartAndGoal(map);

            return ((start != null)
                && (goal != null));
        }
        public static (Node.Node, Node.Node) GetStartAndGoal(this Map map)
        {
            Node.Node goal = null;
            Node.Node start = null;

            foreach (Node.Node[] nodes in map)
                foreach (Node.Node node in nodes)
                {
                    if (node.State == NodeState.Start)
                        start = node;
                    else if (node.State == NodeState.Goal)
                        goal = node;
                }

            return (start, goal);
        }
        public static int GetLength(this Map map, int dimension)
        {
            if (dimension > 1 || dimension < 0)
                throw new ArgumentOutOfRangeException();

            Node.Node[][] data = map.Data;
            int length = 0;

            if (dimension == 0) length = data.GetLength(0);
            if (dimension == 1) length = data[0].GetLength(0);

            return length;
        }
        public static List<T> GetNeighbors<T>(T[,] map, int rowIdx, int colIdx, bool diagonalsEnabled)
        {
            static bool IsDiagonalNeighbor(int rowIdx, int colIdx, int nRowidx, int nColIdx)
            {
                return ((Math.Abs(rowIdx - nRowidx)) == 1)
                    && ((Math.Abs(colIdx - nColIdx) == 1));
            }

            var neighbors = new List<T>();

            int rowCount = map.GetLength(0);
            int colCount = map.GetLength(1);
            int[] rowIndices = { rowIdx - 1, rowIdx, rowIdx + 1 };
            int[] colIndices = { colIdx - 1, colIdx, colIdx + 1 };

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
    }
}
