using AStarVisualization.Core;
using AStarVisualization.Core.Map;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace AStarVisualization.WPF.ViewModels
{
    // TODO: ICommands that are responsible for placing the tiles
    public class MapCanvasViewModel : INotifyPropertyChanged
    {
        public Map Map
        {
            get => _pathfindingMap;
            set
            {
                if (_pathfindingMap != value)
                {
                    _pathfindingMap = value;
                    _pathfindingMap.UpdateNodeIndices();
                    OnPropertyChanged("Map");
                }
            }
        }
        public List<Node> Path
        {
            get => _discoveredPath;
            set
            {
                if (_discoveredPath != value)
                {
                    _discoveredPath = value;
                    OnPropertyChanged("Path");
                }
            }
        }
        public ICommand PlaceTileCommand => _placeTileCommand;

        private Map _pathfindingMap;
        private List<Node> _discoveredPath;
        private ICommand _placeTileCommand;

        public MapCanvasViewModel()
        {
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
