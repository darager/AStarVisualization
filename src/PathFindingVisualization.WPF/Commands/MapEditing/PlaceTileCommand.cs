using System;
using System.Linq;
using System.ComponentModel;
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
        private MainViewModel _mainViewModel;
        private ApplicationState _appState;

        public PlaceTileCommand(MainViewModel mainViewModel, ApplicationState appState)
        {
            _mainViewModel = mainViewModel;
            _appState = appState;
            _appState.PropertyChanged += UpdateCanExecute;
        }

        public bool CanExecute(object parameter) => _appState.State == AppState.MapDesignPhase;
        public void Execute(object parameter)
        {
            var mouseArgs = (MouseEventArgs)parameter;
            var shape = (Shape)mouseArgs.OriginalSource;
            var mapCanvas = (MapCanvas)shape.Parent;

            Point position = mouseArgs.GetPosition(mapCanvas);
            (int rowIdx, int colIdx) = mapCanvas.GetNodeIndices(position);

            Map map = _mainViewModel.Map;
            var node = (Node)map[rowIdx, colIdx];

            NodeState oldState = node.State;
            NodeState newState = _mainViewModel.PlacementMode;

            if (WallWillOverWriteObjective(newState, oldState))
                return;

            if (NewObjectiveWillBePlaced(newState))
                RemoveExistingObjective(oldState);

            PlaceNode(newState, node);

            _mainViewModel.PlacementMode = NodeState.Wall; // return to the default placement mode
        }

        private bool NewObjectiveWillBePlaced(NodeState newState)
        {
            return (newState == NodeState.Goal
                || newState == NodeState.Start);
        }
        private void RemoveExistingObjective(NodeState oldState)
        {
            switch (oldState)
            {
                case NodeState.Start:
                    _mainViewModel.Start = null;
                    break;
                case NodeState.Goal:
                    _mainViewModel.Goal = null;
                    break;
            }
        }
        private bool WallWillOverWriteObjective(NodeState newState, NodeState oldState)
        {
            bool nodeIsObjective = oldState.Equals(NodeState.Goal) || oldState.Equals(NodeState.Start);
            bool wallWillOverrideObjective = nodeIsObjective && !newState.Equals(NodeState.Ground);

            return wallWillOverrideObjective;
        }
        private void PlaceNode(NodeState newState, Node node)
        {
            switch (newState)
            {
                case NodeState.Start:
                    Node start = _mainViewModel.Start;
                    if (start != null)
                    {
                        start.State = NodeState.Ground;
                    }
                    _mainViewModel.Start = node;
                    node.State = NodeState.Start;
                    break;

                case NodeState.Goal:
                    Node goal = _mainViewModel.Goal;
                    if (goal != null)
                    {
                        goal.State = NodeState.Ground;
                    }
                    _mainViewModel.Goal = node;
                    node.State = NodeState.Goal;
                    break;

                default:
                    node.State = newState;
                    break;
            }
        }

        private void UpdateCanExecute(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(sender, e);
        }
        public event EventHandler CanExecuteChanged;
    }
}
