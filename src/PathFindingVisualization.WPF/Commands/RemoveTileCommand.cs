using System;
using System.Windows.Input;
using System.Windows.Shapes;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Controls;
using PathFindingVisualization.WPF.ViewModels;

namespace PathFindingVisualization.WPF.Commands
{
    public class RemoveTileCommand : ICommand
    {
        private MapCanvasViewModel _mapCanvasViewModel;

        public RemoveTileCommand(MapCanvasViewModel mapCanvasViewModel)
        {
            _mapCanvasViewModel = mapCanvasViewModel;
        }

        public bool CanExecute(object parameter) => _mapCanvasViewModel.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
            var args = (MouseEventArgs)parameter;
            var shape = (Shape)args.OriginalSource;
            var mapCanvas = (MapCanvas)shape.Parent;

            double rowSpacing = mapCanvas.ActualHeight / mapCanvas.NumRows;
            double colSpacing = mapCanvas.ActualWidth / mapCanvas.NumColumns;

            var mousePosition = args.GetPosition(mapCanvas);
            int rowIdx = (int)Math.Truncate(mousePosition.Y / rowSpacing);
            int colIdx = (int)Math.Truncate(mousePosition.X / colSpacing);

            Node node = mapCanvas.Map[rowIdx, colIdx];
            node.State = NodeState.Ground;
        }

        public event EventHandler CanExecuteChanged;
    }
}
