using System;
using System.Windows.Input;
using AStarVisualization.WPF.ViewModels;

namespace AStarVisualization.WPF.Commands
{
    public class HandleLeftClickCommand : ICommand
    {
        private MapCanvasViewModel mapCanvasViewModel;

        public HandleLeftClickCommand(MapCanvasViewModel mapCanvasViewModel)
        {
            this.mapCanvasViewModel = mapCanvasViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return mapCanvasViewModel.MapDesignPhaseActive && (mapCanvasViewModel.Place != Place.None);
        }
        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;
    }
}
