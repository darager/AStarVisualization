using System;
using System.Collections;
using System.Collections.Specialized;

namespace PathFindingVisualization.Core.Map
{
    public class Map : INotifyCollectionChanged, IEnumerable
    {
        public Node.Node this[int i, int j]
        {
            get => _map[i][j];
            set
            {
                if (_map[i][j] != value)
                {
                    _map[i][j] = value;
                    CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, _map[i][j]));
                }
            }
        }
        public Node.Node[][] Data
        {
            get => _map;
            set
            {
                if (_map != value)
                {
                    _map = value;
                    CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }
            }
        }

        private Node.Node[][] _map;

        public Map(int numRows, int numColumns)
        {
            _map = new Node.Node[numRows][];

            for (int i = 0; i < numRows; i++)
                _map[i] = new Node.Node[numColumns];

            for (int i = 0; i < numRows; i++)
                for (int j = 0; j < numColumns; j++)
                    _map[i][j] = new Node.Node(Node.NodeState.Ground);

            this.UpdateNodeIndices();
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

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
