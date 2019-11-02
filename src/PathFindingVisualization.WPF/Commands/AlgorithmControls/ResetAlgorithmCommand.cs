using System;
using System.Collections.Generic;
using System.Windows.Input;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.ViewModels;

namespace PathFindingVisualization.WPF.Commands.AlgorithmControls
{
    public class ResetAlgorithmCommand : ICommand
    {
        private MainViewModel _mainViewModel;

        public ResetAlgorithmCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public bool CanExecute(object parameter) => !_mainViewModel.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
            _mainViewModel.Path = new List<Node>();

            foreach (Node node in _mainViewModel.Map)
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

            _mainViewModel.MapDesignPhaseActive = true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
