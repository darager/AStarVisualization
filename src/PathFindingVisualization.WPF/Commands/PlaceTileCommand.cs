using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Controls;
using PathFindingVisualization.WPF.ViewModels;
using System;
using System.Windows.Input;
using System.Windows.Shapes;

namespace PathFindingVisualization.WPF.Commands
{
    public class PlaceTileCommand : ICommand
    {
        private readonly MapCanvasViewModel _mapCanvasViewModel;

        public PlaceTileCommand(MapCanvasViewModel mapCanvasViewModel)
        {
            _mapCanvasViewModel = mapCanvasViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return _mapCanvasViewModel.MapDesignPhaseActive && (_mapCanvasViewModel.Place != Place.None);
        }
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
            Place placementMode = _mapCanvasViewModel.Place;

            node.State = GetState(placementMode);
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
