using PathFindingVisualization.WPF.Controls;
using PathFindingVisualization.WPF.ViewModels;
using System;
using System.Windows.Input;
using System.Windows.Shapes;

namespace PathFindingVisualization.WPF.Commands
{
    public class PlaceStartCommand : ICommand
    {
        private readonly MainViewModel _mapViewModel;

        public PlaceStartCommand(MainViewModel mapCanvasViewModel)
        {
            _mapViewModel = mapCanvasViewModel;
        }

        public bool CanExecute(object parameter) => _mapViewModel.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
            var args = (MouseEventArgs)parameter;
            var shape = (Shape)args.OriginalSource;
            var mapCanvas = (MapCanvas)shape.Parent;

            _mapViewModel.PlacementMode = Place.Start;
        }

        public event EventHandler CanExecuteChanged;
    }
}
