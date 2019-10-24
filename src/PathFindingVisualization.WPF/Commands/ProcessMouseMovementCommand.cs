using System;
using System.Windows.Input;
using PathFindingVisualization.WPF.ViewModels;

// TODO: improve this if possible
namespace PathFindingVisualization.WPF.Commands
{
    public class ProcessMouseMovementCommand : ICommand
    {
        private MapCanvasViewModel _mapCanvasViewModel;

        public ProcessMouseMovementCommand(MapCanvasViewModel mapCanvasViewModel)
        {
            _mapCanvasViewModel = mapCanvasViewModel;
        }

        public bool CanExecute(object parameter) => _mapCanvasViewModel.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
            var args = (MouseEventArgs)parameter;

            if (args.LeftButton == MouseButtonState.Pressed)
            {
                ICommand placeTileCommand = _mapCanvasViewModel.PlaceTileCommand;
                if (placeTileCommand.CanExecute(parameter))
                    placeTileCommand.Execute(parameter);
            }
            else if (args.RightButton == MouseButtonState.Pressed)
            {
                ICommand removeTileCommand = _mapCanvasViewModel.RemoveTileCommand;
                if (removeTileCommand.CanExecute(parameter))
                    removeTileCommand.Execute(parameter);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
