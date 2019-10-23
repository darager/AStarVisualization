using System;
using System.Windows.Input;
using System.Windows.Shapes;
using AStarVisualization.Core;
using AStarVisualization.Core.Map;
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
            var rectangle = (Rectangle)args.OriginalSource;
            var canvas = (MapCanvas)rectangle.Parent;

            double rowSpacing = canvas.ActualHeight / canvas.NumRows;
            double colSpacing = canvas.ActualWidth / canvas.NumColumns;

            var mousePosition = args.GetPosition(canvas);
            int rowIdx = (int)Math.Truncate(mousePosition.Y / rowSpacing);
            int colIdx = (int)Math.Truncate(mousePosition.X / colSpacing);

            Map map = canvas.Map;
            map[rowIdx, colIdx].State = NodeState.Ground;
        }

        public event EventHandler CanExecuteChanged;
    }
}
