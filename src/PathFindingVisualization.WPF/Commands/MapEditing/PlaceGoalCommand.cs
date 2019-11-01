using System;
using System.Windows.Input;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.ViewModels;

namespace PathFindingVisualization.WPF.Commands.MapEditing
{
    public class PlaceGoalCommand : ICommand
    {
        private MainViewModel _mainViewModel;

        public PlaceGoalCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public bool CanExecute(object parameter) => _mainViewModel.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
            if (_mainViewModel.PlacementMode == NodeState.Goal)
                _mainViewModel.PlacementMode = NodeState.Wall;
            else
                _mainViewModel.PlacementMode = NodeState.Goal;
        }

        public event EventHandler CanExecuteChanged;
    }
}
