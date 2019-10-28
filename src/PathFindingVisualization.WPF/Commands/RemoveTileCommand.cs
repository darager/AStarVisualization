using System;
using System.Windows.Input;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.ViewModels;

namespace PathFindingVisualization.WPF.Commands
{
    public class RemoveTileCommand : ICommand
    {
        private MainViewModel _mapViewModel;

        public RemoveTileCommand(MainViewModel mapCanvasViewModel)
        {
            _mapViewModel = mapCanvasViewModel;
        }

        public bool CanExecute(object parameter) => _mapViewModel.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
            var indices = (ValueTuple<int, int>)parameter;
            int rowIdx = indices.Item1;
            int colIdx = indices.Item2;

            Map map = _mapViewModel.Map;
            Node node = map[rowIdx, colIdx];

            node.State = NodeState.Ground;
        }

        public event EventHandler CanExecuteChanged;
    }
}
