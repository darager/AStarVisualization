using System;
using System.Windows.Input;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.ViewModels;

namespace PathFindingVisualization.WPF.Commands.MapEditing
{
    public class RemoveTileCommand : ICommand
    {
        private MainViewModel _mainViewModel;

        public RemoveTileCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public bool CanExecute(object parameter) => _mainViewModel.MapDesignPhaseActive;
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
