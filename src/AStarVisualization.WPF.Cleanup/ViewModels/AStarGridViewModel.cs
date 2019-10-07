using AStarVisualization.Core;
using System.Collections.Generic;
using System.ComponentModel;

namespace AStarVisualization.WPF.Cleanup.ViewModels
{
    // TODO ICommands that are responsible for placing the tiles
    public class AStarGridViewModel : INotifyPropertyChanged
    {
        public List<Node> Path
        {
            get => _path;
            set
            {
                if (_path != value)
                {
                    _path = value;
                    OnPropertyChanged("Path");

                }
            }
        }
        private List<Node> _path;
        public Node[,] Map
        {
            get => _map;
            set
            {
                if (_map != value)
                {
                    _map = value;
                    OnPropertyChanged("Map");
                }
            }
        }
        private Node[,] _map;


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
