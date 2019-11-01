using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Controls;
using PathFindingVisualization.WPF.Models;
using PathFindingVisualization.WPF.ViewModels;

// TODO: clean up this class
namespace PathFindingVisualization.WPF.Commands.MapEditing
{
    public class PlaceTileCommand : ICommand
    {
        private MapCanvasData _data;
        private MainViewModel _mainViewModel;

        public PlaceTileCommand(MapCanvasData data, MainViewModel mainViewModel)
        {
            _data = data;
            _mainViewModel = mainViewModel;
        }

        public bool CanExecute(object parameter) => _mainViewModel.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
            var args = (MouseEventArgs)parameter;
            var shape = (Shape)args.OriginalSource;
            var mapCanvas = (MapCanvas)shape.Parent;

            Point position = args.GetPosition(mapCanvas);
            (int rowIdx, int colIdx) = mapCanvas.GetNodeIndices(position);

            Map map = _data.Map;
            Node node = map[rowIdx, colIdx];
            NodeState oldState = node.State;
            NodeState newState = _mainViewModel.PlacementMode;

            // Do not place walls on the Start or Goal!
            bool nodeIsObjective = (oldState == NodeState.Goal || oldState == NodeState.Start);
            if (nodeIsObjective && newState != NodeState.Ground)
                return;

            switch (oldState)
            {
                case NodeState.Start:
                    _data.Start = null;
                    break;
                case NodeState.Goal:
                    _data.Goal = null;
                    break;
                default:
                    break;
            }

            switch (newState)
            {
                case NodeState.Start:
                    Node start = _data.Start;
                    if (start != null)
                    {
                        start.State = NodeState.Ground;
                    }
                    _data.Start = node;
                    node.State = NodeState.Start;
                    break;

                case NodeState.Goal:
                    Node goal = _data.Goal;
                    if (goal != null)
                    {
                        goal.State = NodeState.Ground;
                    }
                    _data.Goal = node;
                    node.State = NodeState.Goal;
                    break;

                default:
                    node.State = newState;
                    break;
            }

            // set the default placement mode
            _mainViewModel.PlacementMode = NodeState.Wall;
        }

        public event EventHandler CanExecuteChanged;
    }
}
