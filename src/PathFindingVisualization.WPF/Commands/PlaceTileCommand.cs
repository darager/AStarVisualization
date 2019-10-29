using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using System;
using System.Windows.Input;

namespace PathFindingVisualization.WPF.Commands
{
    public class PlaceTileCommand : ICommand
    {
        private readonly MapEditor _mapEditor;

        public PlaceTileCommand(MapEditor mapEditor)
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

            NodeState state = _mapEditor.PlacementMode;

            node.State = state;

            if (state == NodeState.Goal || state == NodeState.Start)
                _mapEditor.PlacementMode = NodeState.Wall;
        }

        public event EventHandler CanExecuteChanged;
    }
}
