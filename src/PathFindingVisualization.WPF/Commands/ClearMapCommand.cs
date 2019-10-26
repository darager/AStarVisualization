using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Controls;
using PathFindingVisualization.WPF.ViewModels;
using System;
using System.Windows.Input;

namespace PathFindingVisualization.WPF.Commands
{
    public class ClearMapCommand : ICommand
    {
        private MapViewModel _mapViewModel;

        public ClearMapCommand(MapViewModel mapViewModel)
        {
            _mapViewModel = mapViewModel;
        }

        public bool CanExecute(object parameter) => _mapViewModel.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
            Map map = _mapViewModel.Map;

            foreach (Node[] nodes in map)
                foreach (Node node in nodes)
                    node.State = NodeState.Ground;
        }

        public event EventHandler CanExecuteChanged;
    }
}
