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
        //public ICommand PlaceTileCommand => _placeTileCommand;
        //public ICommand HandleLeftClickCommand => _handleLeftClickCommand;
        public ICommand RemoveTileCommand { get; private set; }
        public ICommand PlaceTileCommand { get; private set; }

        // TODO: make sure that these properties are used correctly
        public Place Place { get; set; } = Place.Wall;
        public bool MapDesignPhaseActive { get; set; } = true;

        private Map _map;
        private List<Node> _path;

        public MapCanvasViewModel()
        {
            _map = new Map(0, 0);
            _path = new List<Node>();

            // TODO: maybe inject these with Ninject
            RemoveTileCommand = new RemoveTileCommand(this);
            PlaceTileCommand = new PlaceTileCommand(this);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
