using System;
using System.Collections;
using System.Collections.Specialized;

// TODO: has to be tested
namespace AStarVisualization.Core.Map
{
    public class Map : INotifyCollectionChanged, IEnumerable
    {
        public Node this[int i, int j]
        {
            get => _map[i][j];
            set
            {
                if (_map[i][j] != value)
                {
                    _map[i][j] = value;
                    CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, _map[i][j]);
                }
            }
        }
        public Node[][] Data
        {
            get => _map;
            set
            {
                if (_map != value)
                {
                    _map = value;
                    OnCollection(NotifyCollectionChangedAction.Reset);
                }
            }
        }
        private Node[][] _map;

        public Map(int numRows, int numColumns)
        {
            _map = new Node[numRows][];

            for (int i = 0; i < numRows; i++)
                _map[i] = new Node[numColumns];
        }

        public int GetLength(int dimension)
        {
            if (dimension > 1) throw new ArgumentOutOfRangeException();

            int length = 0;
            if (dimension == 0) length = _map.GetLength(0);
            if (dimension == 1) length = _map[0].GetLength(0);

            return length;
        }
        public IEnumerator GetEnumerator() => _map.GetEnumerator();

        private void OnCollection(NotifyCollectionChangedAction action)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action));
        }
        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
