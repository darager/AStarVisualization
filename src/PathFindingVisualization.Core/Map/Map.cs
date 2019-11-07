using System;
using System.Collections;

namespace PathFindingVisualization.Core.Map
{
    public class Map : IEnumerable
    {
        public Node.Node this[int i, int j]
        {
            get => _map[i][j];
            set
            {
                if (_map[i][j] == value) return;

                _map[i][j] = value;
            }
        }
        public Node.Node[][] Data
        {
            get => _map;
            set
            {
                if (_map == value) return;

                _map = value;
                UpdateNodeIndices();
            }
        }
        private Node.Node[][] _map;

        public Map(int numRows = 50, int numColumns = 50)
        {
            _map = new Node.Node[numRows][];

            for (int i = 0; i < numRows; i++)
            {
                _map[i] = new Node.Node[numColumns];

                for (int j = 0; j < numColumns; j++)
                    _map[i][j] = new Node.Node(Node.NodeState.Ground);
            }

            UpdateNodeIndices();
        }

        public int GetLength(int dimension)
        {
            if (dimension > 1 || dimension < 0) throw new ArgumentOutOfRangeException();

            int length = 0;
            if (dimension == 0) length = _map.GetLength(0);
            if (dimension == 1) length = _map[0].GetLength(0);

            return length;
        }
        public IEnumerator GetEnumerator() => _map.GetEnumerator();

        private void UpdateNodeIndices()
        {
            for (int i = 0; i < GetLength(0); i++)
                for (int j = 0; j < GetLength(1); j++)
                    _map[i][j].SetIndices(i, j);
        }
    }
}
