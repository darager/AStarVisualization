using System;
using System.Windows.Input;
using AStarVisualization.WPF.ViewModels;

namespace AStarVisualization.WPF.Commands
{
    public class HandleRightClickCommand : ICommand
    {
        private MapCanvasViewModel mapCanvasViewModel;

        public HandleRightClickCommand(MapCanvasViewModel mapCanvasViewModel)
        {
            this.mapCanvasViewModel = mapCanvasViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return mapCanvasViewModel.MapDesignPhaseActive;
        }
        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;
    }
}
