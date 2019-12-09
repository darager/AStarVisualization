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
                RemoveExistingObjective(newState);

            PlaceNode(newState, node);

            _mainViewModel.PlacementMode = NodeState.Wall; // return to the default placement mode
        }

        private bool NewObjectiveWillBePlaced(NodeState newState)
        {
            return (newState == NodeState.Goal
                || newState == NodeState.Start);
        }
        private void RemoveExistingObjective(NodeState newState)
        {
            switch (newState)
            {
                case NodeState.Start:
                    Node previousStart = _mainViewModel.Start;
                    if (previousStart != null)
                        previousStart.State = NodeState.Ground;
                    _mainViewModel.Start = null;
                    break;

                case NodeState.Goal:
                    Node previousGoal = _mainViewModel.Goal;
                    if (previousGoal != null)
                        previousGoal.State = NodeState.Ground;
                    _mainViewModel.Goal = null;
                    break;
            }
        }
        private bool WallWillOverWriteObjective(NodeState newState, NodeState oldState)
        {
            bool isObjective = oldState.Equals(NodeState.Goal) || oldState.Equals(NodeState.Start);
            bool wallWillOverrideObjective = isObjective && !newState.Equals(NodeState.Ground);

            return wallWillOverrideObjective;
        }
        private void PlaceNode(NodeState newState, Node node)
        {
            if (newState == NodeState.Start)
                _mainViewModel.Start = node;
            else if (newState == NodeState.Goal)
                _mainViewModel.Goal = node;

            node.State = newState;
        }

        private void UpdateCanExecute(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(sender, e);
        }
        public event EventHandler CanExecuteChanged;
    }
}
