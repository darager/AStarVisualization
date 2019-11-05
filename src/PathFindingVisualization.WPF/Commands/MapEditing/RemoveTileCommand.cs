using System;
using System.Windows.Input;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Models;
using PathFindingVisualization.WPF.ViewModels;

namespace PathFindingVisualization.WPF.Commands.MapEditing
{
    public class RemoveTileCommand : ICommand
    {
        private MainViewModel _mainViewModel;
        private ApplicationState _appState;

        public RemoveTileCommand(MainViewModel mainViewModel, ApplicationState appState)
        {
            _mainViewModel = mainViewModel;
            _appState = appState;
        }

        public bool CanExecute(object parameter) => _appState.State == AppState.MapDesignPhase;
        public void Execute(object parameter)
        {
            ICommand placeTile = _mainViewModel.PlaceTileCommand;

            if (placeTile.CanExecute(parameter))
            {
                _mainViewModel.PlacementMode = NodeState.Ground;
                placeTile.Execute(parameter);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
