using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Controls;
using PathFindingVisualization.WPF.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace PathFindingVisualization.WPF.Commands
{
    public class PlaceWallCommand : ICommand
    {
        private readonly MapViewModel _mapViewModel;

        public PlaceWallCommand(MapViewModel mapCanvasViewModel)
        {
            _mapViewModel = mapCanvasViewModel;
        }

        public bool CanExecute(object parameter) => _mapViewModel.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
            var args = (MouseEventArgs)parameter;
            var shape = (Shape)args.OriginalSource;
            var mapCanvas = (MapCanvas)shape.Parent;

            Point position = args.GetPosition(mapCanvas);
            (int rowIdx, int colIdx) = mapCanvas.GetNodeIndices(position);

            Node node = mapCanvas.Map[rowIdx, colIdx];
            node.State = NodeState.Wall;
        }

        public event EventHandler CanExecuteChanged;
    }
}
