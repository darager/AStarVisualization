using AStarVisualization.Core;
using AStarVisualization.Core.Map;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace AStarVisualization.WPF.ViewModels
{
    // TODO: ICommands that are responsible for placing the tiles
    public class AStarGridViewModel : INotifyPropertyChanged
    {
        public Map AStarMap
        {
            get => _aStarMap;
            set
            {
                if (_aStarMap != value)
                {
                    _aStarMap = value;
                    _aStarMap.UpdateNodeIndices();
                    OnPropertyChanged("AStarMap");
                }
            }
        }
        private Map _aStarMap;

        public List<Node> AStarPath
        {
            get => _path;
            set
            {
                if (_path != value)
                {
                    _path = value;
                    OnPropertyChanged("AStarPath");
                }
            }
        }
        private List<Node> _path;

        //TODO: remove mock data
        public AStarGridViewModel()
        {
            #region mockdata
            var map = new Map(3, 3)
            {
                Data = new Node[][]
                {
                    new Node[]{ new Node(NodeState.Start), new Node(NodeState.Ground), new Node(NodeState.Wall) },
                    new Node[]{ new Node(NodeState.Ground), new Node(NodeState.Wall), new Node(NodeState.Wall) },
                    new Node[]{ new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Goal) },
                }
            };
            AStarMap = map;
            AStarPath = new List<Node> { map[0, 0], map[1, 1], map[2, 0], map[2, 1], map[2, 2] };
            #endregion
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
