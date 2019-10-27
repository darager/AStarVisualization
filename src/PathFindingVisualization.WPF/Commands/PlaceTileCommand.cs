using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Controls;
using PathFindingVisualization.WPF.Models;
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

            var args = (MouseEventArgs)parameter;
            var shape = (Shape)args.OriginalSource;
            var mapCanvas = (MapCanvas)shape.Parent;

            Point position = args.GetPosition(mapCanvas);
            (int rowIdx, int colIdx) = mapCanvas.GetNodeIndices(position);

            Node node = mapCanvas.Map[rowIdx, colIdx];
            NodeState state = GetState(placementMode);

            node.State = state;

            if (placementMode == Place.Goal || placementMode == Place.Start)
                _mapViewModel.PlacementMode = Place.Wall;
        }

        private NodeState GetState(Place placementMode)
        {
            switch (placementMode)
            {
                case Place.Start:
                    return NodeState.Start;
                case Place.Goal:
                    return NodeState.Goal;
                default:
                    return NodeState.Wall;
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
