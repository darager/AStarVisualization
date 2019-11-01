using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Ninject;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Models;

namespace PathFindingVisualization.WPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        [Inject, Named("PlaceTileCommand")]
        public ICommand PlaceTileCommand { get; set; }
        [Inject, Named("RemoveTileCommand")]
        public ICommand RemoveTileCommand { get; set; }
        [Inject, Named("ProcessMouseMovementCommand")]
        public ICommand ProcessMouseMovementCommand { get; set; }
        [Inject, Named("ClearMapCommand")]
        public ICommand ClearMapCommand { get; set; }
        [Inject, Named("PlaceStartCommand")]
        public ICommand PlaceStartCommand { get; set; }
        [Inject, Named("PlaceGoalCommand")]
        public ICommand PlaceGoalCommand { get; set; }

        public ICommand StartAlgorigthmCommand { get; set; }

        public Map Map
        {
            get => _mapCanvasData.Map;
            set
            {
                if (_mapCanvasData.Map != value)
                {
                    _mapCanvasData.Map = value;
                    OnPropertyChanged("Map");
                }
            }
        }
        public List<Node> Path
        {
            get => _mapCanvasData.Path;
            set
            {
                if (_mapCanvasData.Path != value)
                {
                    _mapCanvasData.Path = value;
                    OnPropertyChanged("Path");
                }
            }
        }

        public bool StartPlacementActive => (PlacementMode == NodeState.Start);
        public bool GoalPlacementActive => (PlacementMode == NodeState.Goal);

        // TODO: handle these pyoperties differently
        public bool MapDesignPhaseActive { get; set; } = true;
        public NodeState PlacementMode
        {
            get => _placementMode;
            set
            {
                if (_placementMode != value)
                {
                    _placementMode = value;
                    OnPropertyChanged("StartPlacementActive");
                    OnPropertyChanged("GoalPlacementActive");
                }
            }
        }
        private NodeState _placementMode = NodeState.Wall;

        private readonly MapCanvasData _mapCanvasData;

        public MainViewModel(MapCanvasData mapCanvasData)
        {
            _mapCanvasData = mapCanvasData;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
