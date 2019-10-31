using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Controls;
using PathFindingVisualization.WPF.Models;

namespace PathFindingVisualization.WPF.Commands
{
    public class RemoveTileCommand : ICommand
    {
        private MapEditor _mapEditor;
        private MapCanvasData _data;

        public RemoveTileCommand(MapEditor mapEditor, MapCanvasData data)
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

            node.State = NodeState.Ground;
        }

        public event EventHandler CanExecuteChanged;
    }
}
