using System;

namespace AStarVisualization.WPF.AStarAlgorithm.AStarImplementation.Algorithmthread
{
    public class Node : IComparable
    {
        private static int Count = 0;
        private int Id;

        public double Heuristic;
        public double MovementCost;
        public double TotalCost
        {
            get { return Heuristic + MovementCost * 500; }
        }

        public int RowIndex;
        public int ColumnIndex;

        public bool IsWalkable;
        public Node Parent;

        public Node(AStarTile tile, Node parent = null)
        {
            Id = Count;
            Count++;

            RowIndex = tile.RowIndex;
            ColumnIndex = tile.ColumnIndex;
            Parent = parent;

            IsWalkable = !(tile.TileType == Tile.Wall);
        }

        public int CompareTo(object obj)
        {
            Node node = (Node)obj;

            int result = this.TotalCost.CompareTo(node.TotalCost);

            return result;
        }
    }
}
