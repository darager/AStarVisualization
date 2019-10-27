﻿using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.ViewModels;
using System;
using System.Windows.Input;

namespace PathFindingVisualization.WPF.Commands
{
    public class ClearMapCommand : ICommand
    {
        private MainViewModel _mainViewModel;

        public ClearMapCommand(MainViewModel mapViewModel)
        {
            _mainViewModel = mapViewModel;
        }

        public bool CanExecute(object parameter) => _mainViewModel.MapDesignPhaseActive;
        public void Execute(object parameter)
        {
            _mainViewModel.Path = new System.Collections.Generic.List<Node>();

            Map map = _mainViewModel.Map;
            foreach (Node[] nodes in map)
                foreach (Node node in nodes)
                    node.State = NodeState.Ground;
        }

        public event EventHandler CanExecuteChanged;
    }
}