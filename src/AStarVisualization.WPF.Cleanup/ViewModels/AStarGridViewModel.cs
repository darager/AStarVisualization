using AStarVisualization.Core;
using AStarVisualization.WPF.Cleanup.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace AStarVisualization.WPF.Cleanup.ViewModels
{
    // TODO ICommands that are responsible for placing the tiles
    public class AStarGridViewModel : INotifyPropertyChanged
    {
        public AStarMap AStarMap;
        public List<Node> AStarPath
        {
            get => _path;
            set
            {
                if(_path != value)
                {
                    _path = value;
                    OnPropertyChanged("Path");
                }
            }
        }
        private List<Node> _path;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
