using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Commands;
using PathFindingVisualization.WPF.Models;
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
        public ICommand PlaceStartCommand { get; private set; }
        public ICommand PlaceGoalCommand { get; private set; }
        public ICommand RemoveTileCommand { get; private set; }
        public ICommand ClearMapCommand { get; private set; }

        public Place PlacementMode { get; set; } = Place.Wall;
        public bool MapDesignPhaseActive { get; set; } = true;

        private Map _map;
        private List<Node> _path;

        public MainViewModel(MapCanvasData mapCanvasData)
        {
            Map = mapCanvasData.Map;
            Path = mapCanvasData.Path;

            // TODO: inject these if necessary
            PlaceTileCommand = new PlaceTileCommand(this);
            RemoveTileCommand = new RemoveTileCommand(this);
            ClearMapCommand = new ClearMapCommand(this);
            PlaceStartCommand = new PlaceStartCommand(this);
            PlaceGoalCommand = new PlaceGoalCommand(this);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
