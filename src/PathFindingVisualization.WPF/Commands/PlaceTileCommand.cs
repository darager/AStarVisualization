using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Controls;
using PathFindingVisualization.WPF.Models;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace PathFindingVisualization.WPF.Commands
{
    public class PlaceTileCommand : ICommand
    {
        private readonly MapEditor _mapEditor;
        private readonly MapCanvasData _data;

        public PlaceTileCommand(MapEditor mapEditor, MapCanvasData data)
        {
            _mapEditor = mapEditor;
            _data = data;
        }

        public bool CanExecute(object parameter) => _mapEditor.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
            var args = (MouseEventArgs)parameter;
            var shape = (Shape)args.OriginalSource;
            var mapCanvas = (MapCanvas)shape.Parent;

            Point position = args.GetPosition(mapCanvas);
            (int rowIdx, int colIdx) = mapCanvas.GetNodeIndices(position);

            Map map = _data.Map;
            Node node = map[rowIdx, colIdx];

            NodeState state = _mapEditor.PlacementMode;

            node.State = state;

            if (state == NodeState.Goal || state == NodeState.Start)
                _mapEditor.PlacementMode = NodeState.Wall;
        }

        public event EventHandler CanExecuteChanged;
    }
}
