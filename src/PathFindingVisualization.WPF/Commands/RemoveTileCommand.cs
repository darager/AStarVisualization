using System;
using System.Windows.Input;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;

namespace PathFindingVisualization.WPF.Commands
{
    public class RemoveTileCommand : ICommand
    {
        private MapEditor _mapEditor;

        public RemoveTileCommand(MapEditor mapEditor)
        {
            _mapEditor = mapEditor;
        }

        public bool CanExecute(object parameter) => _mapEditor.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
            var indices = (ValueTuple<int, int>)parameter;
            int rowIdx = indices.Item1;
            int colIdx = indices.Item2;

            Map map = _mapEditor.Map;
            Node node = map[rowIdx, colIdx];

            node.State = NodeState.Ground;
        }

        public event EventHandler CanExecuteChanged;
    }
}
