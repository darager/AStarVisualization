using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.WPF.Models;
using PathFindingVisualization.WPF.ViewModels;

namespace PathFindingVisualization.WPF.Commands.MapEditing
{
    public class ClearMapCommand : ICommand
    {
        private MainViewModel _mainViewModel;
        private ApplicationState _appState;

        public ClearMapCommand(MainViewModel mainViewModel, ApplicationState appState)
        {
            _mainViewModel = mainViewModel;
            _appState = appState;
            _appState.PropertyChanged += UpdateCanExecute;
        }

        public bool CanExecute(object parameter) => _appState.State == AppState.MapDesignPhase;
        public void Execute(object parameter)
        {
            _mainViewModel.Path = new List<Node>();

            Map map = _mainViewModel.Map;
            foreach (Node[] nodes in map)
                foreach (Node node in nodes)
                    node.State = NodeState.Ground;
        }

        private void UpdateCanExecute(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(sender, e);
        }
        public event EventHandler CanExecuteChanged;
    }
}
