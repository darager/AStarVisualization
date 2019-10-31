using Ninject;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
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
            get => _mapCanvasData.Map;
            set
            {
                if (_mapCanvasData.Map != value)
                {
                    _mapCanvasData.Map = value;
                    _mapCanvasData.Map.UpdateNodeIndices();
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

        public ICommand PlaceTileCommand => _mapEditor.PlaceTile;
        public ICommand RemoveTileCommand => _mapEditor.RemoveTile;
        public ICommand ProcessMouseMovementCommand => _mapEditor.ProcessMouseMovement;
        public ICommand ClearMapCommand => _mapEditor.ClearMap;

        //public ICommand PlaceStartCommand { get; private set; }
        //public ICommand PlaceGoalCommand { get; private set; }

        private MapCanvasData _mapCanvasData;
        private MapEditor _mapEditor;

        public MainViewModel(MapCanvasData mapCanvasData, MapEditor mapEditor)
        {
            _mapCanvasData = mapCanvasData;
            _mapEditor = mapEditor;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
