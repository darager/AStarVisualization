using System;
using System.Windows.Input;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Models;
using PathFindingVisualization.WPF.ViewModels;

namespace PathFindingVisualization.WPF.Commands.MapEditing
{
    public class ProcessMouseMovementCommand : ICommand
    {
        private MainViewModel _mainViewModel;
        private ApplicationState _appState;

        public ProcessMouseMovementCommand(MainViewModel mainViewModel, ApplicationState appState)
        {
            _mainViewModel = mainViewModel;
            _appState = appState;
        }

        public bool CanExecute(object parameter)
        {
            NodeState placementMode = _mainViewModel.PlacementMode;
            bool objectivePlacementInactive = !(placementMode == NodeState.Start || placementMode == NodeState.Goal);

            return _appState.State == AppState.MapDesignPhase && objectivePlacementInactive;
        }
        public void Execute(object parameter)
        {
            var args = (MouseEventArgs)parameter;

            if (args.LeftButton == MouseButtonState.Pressed)
            {
                if (_mainViewModel.PlaceTileCommand.CanExecute(parameter))
                {
                    _mainViewModel.PlaceTileCommand.Execute(parameter);
                }
            }

            if (args.RightButton == MouseButtonState.Pressed)
            {
                if (_mainViewModel.RemoveTileCommand.CanExecute(parameter))
                {
                    _mainViewModel.RemoveTileCommand.Execute(parameter);
                }
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
