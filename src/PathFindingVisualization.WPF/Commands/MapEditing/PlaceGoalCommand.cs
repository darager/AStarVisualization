using System;
using System.ComponentModel;
using System.Windows.Input;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Models;
using PathFindingVisualization.WPF.ViewModels;

namespace PathFindingVisualization.WPF.Commands.MapEditing
{
    public class PlaceGoalCommand : ICommand
    {
        private MainViewModel _mainViewModel;
        private ApplicationState _appState;

        public PlaceGoalCommand(MainViewModel mainViewModel, ApplicationState appState)
        {
            _mainViewModel = mainViewModel;
            _appState = appState;
            _appState.PropertyChanged += UpdateCanExecute;
        }

        public bool CanExecute(object parameter) => _appState.State == AppState.MapDesignPhase;
        public void Execute(object parameter)
        {
            if (_mainViewModel.PlacementMode == NodeState.Goal)
                _mainViewModel.PlacementMode = NodeState.Wall;
            else
                _mainViewModel.PlacementMode = NodeState.Goal;
        }

        private void UpdateCanExecute(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(sender, e);
        }
        public event EventHandler CanExecuteChanged;
    }
}
