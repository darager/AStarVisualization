using AStarVisualization.Core;
using AStarVisualization.Core.Map;
using AStarVisualization.WPF.Commands;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace AStarVisualization.WPF.ViewModels
{
    public class MapCanvasViewModel : INotifyPropertyChanged
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
        public ICommand ProcessMouseMovementCommand => _processMouseMovementCommand;
        public ICommand PlaceTileCommand => _placeTileCommand;
        public ICommand RemoveTileCommand => _removeTileCommand;

        // TODO: make sure that these properties are used correctly by the commands
        public Place Place { get; set; } = Place.Wall;
        public bool MapDesignPhaseActive { get; set; } = true;

        private Map _map;
        private List<Node> _path;

        private readonly ICommand _processMouseMovementCommand;
        private readonly ICommand _placeTileCommand;
        private readonly ICommand _removeTileCommand;

        public MapCanvasViewModel()
        {
            _map = new Map(2, 2);
            _path = new List<Node>();

            // TODO: maybe inject these with Ninject
            _processMouseMovementCommand = new ProcessMouseMovementCommand(this);
            _placeTileCommand = new PlaceTileCommand(this);
            _removeTileCommand = new RemoveTileCommand(this);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
