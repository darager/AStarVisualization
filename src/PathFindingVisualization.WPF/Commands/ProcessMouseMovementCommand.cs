using System;
using System.Windows.Input;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.ViewModels;

namespace PathFindingVisualization.WPF.Commands
{
    public class ProcessMouseMovementCommand : ICommand
    {
        private MainViewModel _mainViewModel;

        public ProcessMouseMovementCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public bool CanExecute(object parameter)
        {
            NodeState placementMode = _mainViewModel.PlacementMode;
            bool objectivePlacementInactive = !(placementMode == NodeState.Start || placementMode == NodeState.Goal);

            return _mainViewModel.MapDesignPhaseActive && objectivePlacementInactive;
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
