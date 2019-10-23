using System;
using System.Windows.Input;
using AStarVisualization.WPF.Controls;
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

        public bool CanExecute(object parameter) => mapCanvasViewModel.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
            var args = parameter as MouseEventArgs;
            var canvas = (MapCanvas)args.Source;

            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;
    }
}
