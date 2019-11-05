using System;
using System.Collections.Generic;
using System.Windows.Input;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Models;
using PathFindingVisualization.WPF.ViewModels;

namespace PathFindingVisualization.WPF.Commands.AlgorithmControls
{
    public class ResetAlgorithmCommand : ICommand
    {
        private MainViewModel _mainViewModel;
        private ApplicationState _appState;

        public ResetAlgorithmCommand(MainViewModel mainViewModel, ApplicationState appState)
        {
            _mainViewModel = mainViewModel;
            _appState = appState;
        }

        public bool CanExecute(object parameter) => _appState.State == AppState.AlgorithmDone;
        public void Execute(object parameter)
        {
            _mainViewModel.Path = new List<Node>();

            var map = _mainViewModel.Map;
            foreach (Node[] nodes in map)
                foreach (Node node in nodes)
                {
                    switch (node.State)
                    {
                        case NodeState.GroundVisited:
                        case NodeState.GroundToBeVisited:
                            node.State = NodeState.Ground;
                            break;
                        default:
                            break;
                    }
                }

            _appState.State = AppState.MapDesignPhase;
        }

        public event EventHandler CanExecuteChanged;
    }
}
