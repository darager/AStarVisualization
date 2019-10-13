using AStarVisualization.Core;
using AStarVisualization.WPF.Controls.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace AStarVisualization.WPF.Cleanup.ViewModels
{
    // TODO: ICommands that are responsible for placing the tiles
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
            // TODO: remove mock data
            var map = new Node[,] {
                { new Node(NodeState.Start), new Node(NodeState.Ground), new Node(NodeState.Wall) },
                { new Node(NodeState.Ground), new Node(NodeState.Wall), new Node(NodeState.Wall) },
                { new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Goal) },
            };
            map.UpdateNodeIndices();
            _aStarMap = new AStarMap();
            _aStarMap.Map = map;
            _path = new List<Node> { map[0, 0], map[1, 0], map[2, 0], map[2, 1], map[2, 2] };
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
