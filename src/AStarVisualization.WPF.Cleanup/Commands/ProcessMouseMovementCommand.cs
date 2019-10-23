using System;
using System.Windows.Input;
using System.Windows.Shapes;
using AStarVisualization.WPF.Controls;
using AStarVisualization.WPF.ViewModels;

namespace AStarVisualization.WPF.Commands
{
    public class ProcessMouseMovementCommand : ICommand
    {
        private MapCanvasViewModel mapCanvasViewModel;

        public ProcessMouseMovementCommand(MapCanvasViewModel mapCanvasViewModel)
        {
            this.mapCanvasViewModel = mapCanvasViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return false;
            //return mapCanvasViewModel.MapDesignPhaseActive && (mapCanvasViewModel.Place != Place.None);
        }
        public void Execute(object parameter)
        {
            //var args = (MouseEventArgs)parameter;
            //var shape = (Shape)args.OriginalSource;
            //var mapCanvas = (MapCanvas)shape.Parent;

            ////Point position = args.GetPosition(mapCanvas);
            //if (args.LeftButton == MouseButtonState.Pressed)
            //{
            //    ICommand placeTileCommand = mapCanvasViewModel.PlaceTileCommand;
            //    if (placeTileCommand.CanExecute(parameter))
            //        placeTileCommand.Execute(parameter);
            //}
            //else if (args.RightButton == MouseButtonState.Pressed)
            //{
            //    ICommand removeTileCommand = mapCanvasViewModel.RemoveTileCommand;
            //    if (removeTileCommand.CanExecute(parameter))
            //        removeTileCommand.Execute(parameter);
            //}
        }

        public event EventHandler CanExecuteChanged;
    }
}
