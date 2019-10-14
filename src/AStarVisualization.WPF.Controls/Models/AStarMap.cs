using AStarVisualization.Core;
using System.Collections.Specialized;
using System.ComponentModel;

// TODO: create a wrapper for the astargrid array in ordser to be able to use mvvm properly
namespace AStarVisualization.WPF.Controls.Models
{
    public class AStarMap : INotifyCollectionChanged, INotifyPropertyChanged
    {
        public Node[][] Map
        {
            get => _map;
            set
            {
                if (value != _map)
                {
                    _map = value;
                    OnPropertyChanged("Map");
                }
            }
        }
        private Node[][] _map;
        public Node this[int i, int j]
        {
            get => _map[i][j];
            set
            {
                if (_map[i][j] != value)
                {
                    _map[i][j] = value;
                    CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace));
                }
            }
        }

        public int GetLength(int i) => _map.GetLength(i);

        private void OnItemChanged(int rowIdx, int colIdx)
        {
            CollectionChanged?.Invoke(
                this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, _map[rowIdx][colIdx]
                ));
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;
    }
}