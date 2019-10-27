using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Controls;
using PathFindingVisualization.WPF.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace PathFindingVisualization.WPF.Commands
{
    public class PlaceTileCommand : ICommand
    {
        private readonly MainViewModel _mapViewModel;

        public PlaceTileCommand(MainViewModel mapCanvasViewModel)
        {
            _mapViewModel = mapCanvasViewModel;
        }

        public bool CanExecute(object parameter) => _mapViewModel.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
            Place placementMode = _mapViewModel.PlacementMode;
            if (placementMode == Place.None) return;

            var args = (MouseEventArgs)parameter;
            var shape = (Shape)args.OriginalSource;
            var mapCanvas = (MapCanvas)shape.Parent;

            Point position = args.GetPosition(mapCanvas);
            (int rowIdx, int colIdx) = mapCanvas.GetNodeIndices(position);

            Node node = mapCanvas.Map[rowIdx, colIdx];

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
