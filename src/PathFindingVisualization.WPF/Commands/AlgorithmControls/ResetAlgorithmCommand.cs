using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.Core.PathSolvers;
using PathFindingVisualization.WPF.Models;
using PathFindingVisualization.WPF.ViewModels;

namespace PathFindingVisualization.WPF.Commands.AlgorithmControls
{
    public class ResetAlgorithmCommand : ICommand
    {
        private ApplicationState _appState;
        private PathSolverController _pathSolverController;
        private AlgorithmControlViewModel _algorithmControlViewModel;
        private MainViewModel _mainViewModel;

        public ResetAlgorithmCommand(ApplicationState appState, PathSolverController pathSolverController, AlgorithmControlViewModel algorithmControlViewModel, MainViewModel mainViewModel)
        {
            _appState = appState;
            _mainViewModel = mainViewModel;
            _pathSolverController = pathSolverController;
            _algorithmControlViewModel = algorithmControlViewModel;
            _appState.PropertyChanged += UpdateCanExecute;
        }

        public bool CanExecute(object parameter) => _appState.State == AppState.AlgorithmActive || _appState.State == AppState.AlgorithmDone;
        public void Execute(object parameter)
        {
            _pathSolverController.ResetPathSolver();

            Task.Delay(300);

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

        private void UpdateCanExecute(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(sender, e);
        }
        public event EventHandler CanExecuteChanged;
    }
}