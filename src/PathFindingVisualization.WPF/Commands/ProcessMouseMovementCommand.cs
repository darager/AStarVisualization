using System;
using System.Windows.Input;
using PathFindingVisualization.WPF.ViewModels;

// TODO: improve this if possible
namespace PathFindingVisualization.WPF.Commands
{
    public class ProcessMouseMovementCommand : ICommand
    {
        private MapViewModel _mapViewModel;

        public ProcessMouseMovementCommand(MapViewModel mapCanvasViewModel)
        {
            _mapViewModel = mapCanvasViewModel;
        }

        public bool CanExecute(object parameter) => _mapViewModel.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
            var args = (MouseEventArgs)parameter;

            if (args.LeftButton == MouseButtonState.Pressed && _mapViewModel.Place == Place.Wall)
            {
                ICommand placeWallCommand = _mapViewModel.PlaceTileCommand;
                if (placeWallCommand.CanExecute(parameter))
                    placeWallCommand.Execute(parameter);
            }
            if (args.RightButton == MouseButtonState.Pressed)
            {
                ICommand removeTileCommand = _mapViewModel.RemoveTileCommand;
                if (removeTileCommand.CanExecute(parameter))
                    removeTileCommand.Execute(parameter);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
