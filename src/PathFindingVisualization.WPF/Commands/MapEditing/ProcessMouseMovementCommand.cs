using System;
using System.ComponentModel;
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
            _appState.PropertyChanged += UpdateCanExecute;
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
                ICommand placeTileCommand = _mainViewModel.PlaceTileCommand;

                if (placeTileCommand.CanExecute(parameter))
                    placeTileCommand.Execute(parameter);
            }

            if (args.RightButton == MouseButtonState.Pressed)
            {
                ICommand removeTileCommand = _mainViewModel.RemoveTileCommand;

                if (removeTileCommand.CanExecute(parameter))
                    removeTileCommand.Execute(parameter);
            }
        }

        private void UpdateCanExecute(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(sender, e);
        }
        public event EventHandler CanExecuteChanged;
    }
}
