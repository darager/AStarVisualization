using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Controls;
using PathFindingVisualization.WPF.ViewModels;

namespace PathFindingVisualization.WPF.Commands
{
    public class RemoveTileCommand : ICommand
    {
        private MainViewModel _mapViewModel;

        public RemoveTileCommand(MainViewModel mapCanvasViewModel)
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
            node.State = NodeState.Ground;
        }

        public event EventHandler CanExecuteChanged;
    }
}
