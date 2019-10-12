using AStarVisualization.Core;
using AStarVisualization.WPF.Cleanup.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace AStarVisualization.WPF.Cleanup.ViewModels
{
    // TODO ICommands that are responsible for placing the tiles
    public class AStarGridViewModel : INotifyPropertyChanged
    {
        public AStarMap AStarMap
        {
            get => _aStarMap;
            set
            {
                if (_aStarMap != value)
                {
                    _aStarMap = value;
                    OnPropertyChanged("AStarMap");
                }
            }
        }
        private AStarMap _aStarMap;
        public List<Node> AStarPath
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

        public AStarGridViewModel()
        {
            _path = new List<Node>();
            _aStarMap = new AStarMap();
            _aStarMap.Map = new Node[0, 0];
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
