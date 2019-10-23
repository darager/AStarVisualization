using AStarVisualization.Core;
using AStarVisualization.WPF.Controls;
using AStarVisualization.WPF.ViewModels;
using System;
using System.Windows.Input;
using System.Windows.Shapes;

namespace AStarVisualization.WPF.Commands
{
    public class PlaceTileCommand : ICommand
    {
        private MapCanvasViewModel mapCanvasViewModel;

        public PlaceTileCommand(MapCanvasViewModel mapCanvasViewModel)
        {
            this.mapCanvasViewModel = mapCanvasViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return mapCanvasViewModel.MapDesignPhaseActive && (mapCanvasViewModel.Place != Place.None);
        }
        public void Execute(object parameter)
        {
            var args = (MouseEventArgs)parameter;
            var rectangle = (Shape)args.OriginalSource;
            var mapCanvas = (MapCanvas)rectangle.Parent;

            double rowSpacing = mapCanvas.ActualHeight / mapCanvas.NumRows;
            double colSpacing = mapCanvas.ActualWidth / mapCanvas.NumColumns;

            var mousePosition = args.GetPosition(mapCanvas);
            int rowIdx = (int)Math.Truncate(mousePosition.Y / rowSpacing);
            int colIdx = (int)Math.Truncate(mousePosition.X / colSpacing);

            Node node = mapCanvas.Map[rowIdx, colIdx];
            Place placementMode = mapCanvasViewModel.Place;
            NodeState newState = GetState(placementMode);

            node.State = newState;
        }

        private NodeState GetState(Place placementMode)
        {
            switch (placementMode)
            {
                case Place.Wall:
                    return NodeState.Wall;
                case Place.Start:
                    return NodeState.Start;
                case Place.Goal:
                    return NodeState.Goal;
                default:
                    return NodeState.Ground;
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
