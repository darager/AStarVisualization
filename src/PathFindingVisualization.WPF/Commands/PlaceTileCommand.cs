using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Models;
using PathFindingVisualization.WPF.ViewModels;
using System;
using System.Windows.Input;

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
            var indices = (ValueTuple<int, int>)parameter;
            int rowIdx = indices.Item1;
            int colIdx = indices.Item2;

            Map map = _mapViewModel.Map;
            Node node = map[rowIdx, colIdx];

            Place placementMode = _mapViewModel.PlacementMode;
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
                case Place.Wall:
                    return NodeState.Wall;
                default:
                    return NodeState.Wall;
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
