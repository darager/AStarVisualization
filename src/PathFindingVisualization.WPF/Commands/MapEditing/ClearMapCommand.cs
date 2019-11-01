using System;
using System.Windows.Input;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Models;
using PathFindingVisualization.WPF.ViewModels;

namespace PathFindingVisualization.WPF.Commands.MapEditing
{
    public class ClearMapCommand : ICommand
    {
        private MapCanvasData _data;
        private MainViewModel _mainViewModel;

        public ClearMapCommand(MapCanvasData data, MainViewModel mainViewModel)
        {
            _data = data;
            _mainViewModel = mainViewModel;
        }

        public bool CanExecute(object parameter) => _mainViewModel.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
            _data.Path = new System.Collections.Generic.List<Node>();

            Map map = _data.Map;
            foreach (Node[] nodes in map)
                foreach (Node node in nodes)
                    node.State = NodeState.Ground;
        }

        public event EventHandler CanExecuteChanged;
    }
}
