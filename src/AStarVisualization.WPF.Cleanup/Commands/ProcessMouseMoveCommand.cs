using AStarVisualization.WPF.ViewModels;
using System;
using System.Windows.Input;

namespace AStarVisualization.WPF.Commands
{
    public class ProcessMouseMoveCommand : ICommand
    {
        private MapCanvasViewModel _mapCanvasViewModel;

        public ProcessMouseMoveCommand(MapCanvasViewModel mapCanvasViewModel)
        {
            _mapCanvasViewModel = mapCanvasViewModel;
        }

        public bool CanExecute(object parameter) => _mapCanvasViewModel.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
        }

        public event EventHandler CanExecuteChanged;
    }
}
