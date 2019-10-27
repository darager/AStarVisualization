using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Commands;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace PathFindingVisualization.WPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public Map Map
        {
            get => _map;
            set
            {
                if (_map != value)
                {
                    _map = value;
                    _map.UpdateNodeIndices();
                    OnPropertyChanged("Map");
                }
            }
        }
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

        public ICommand PlaceTileCommand { get; private set; }
        public ICommand RemoveTileCommand { get; private set; }
        public ICommand ProcessMouseMovementCommand { get; private set; }
        public ICommand ClearMapCommand { get; private set; }

        public Place PlacementMode { get; set; } = Place.Wall;
        public bool MapDesignPhaseActive { get; set; } = true;

        private Map _map = new Map(2, 2);
        private List<Node> _path = new List<Node>();

        public MainViewModel()
        {
            // TODO: inject these if necessary
            PlaceTileCommand = new PlaceTileCommand(this);
            RemoveTileCommand = new RemoveTileCommand(this);
            ProcessMouseMovementCommand = new ProcessMouseMovementCommand(this);
            ClearMapCommand = new ClearMapCommand(this);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
