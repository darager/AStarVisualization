using System;
using System.Windows.Input;
using System.Windows.Shapes;
using AStarVisualization.Core;
using AStarVisualization.WPF.Controls;
using AStarVisualization.WPF.ViewModels;

namespace AStarVisualization.WPF.Commands
{
    public class RemoveTileCommand : ICommand
    {
        private MapCanvasViewModel mapCanvasViewModel;

        public RemoveTileCommand(MapCanvasViewModel mapCanvasViewModel)
        {
            this.mapCanvasViewModel = mapCanvasViewModel;
        }

        public bool CanExecute(object parameter) => mapCanvasViewModel.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
            var args = parameter as MouseEventArgs;
            var rectangle = (Shape)args.OriginalSource;
            var mapCanvas = (MapCanvas)rectangle.Parent;

            double rowSpacing = mapCanvas.ActualHeight / mapCanvas.NumRows;
            double colSpacing = mapCanvas.ActualWidth / mapCanvas.NumColumns;

            var mousePosition = args.GetPosition(mapCanvas);
            int rowIdx = (int)Math.Truncate(mousePosition.Y / rowSpacing);
            int colIdx = (int)Math.Truncate(mousePosition.X / colSpacing);

            // TODO: nothing is bound to the StateChangedEvent this results in the View not updating
            Node node = mapCanvas.Map[rowIdx, colIdx];
            node.State = NodeState.Ground;
        }

        public event EventHandler CanExecuteChanged;
    }
}
