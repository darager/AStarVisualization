using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.Core.PathSolvers;
using PathFindingVisualization.WPF.Models;
using PathFindingVisualization.WPF.ViewModels;
using PathFindingVisualization.Core.Map;

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
        public async void Execute(object parameter)
        {
            Map map = _mainViewModel.Map;
            _mainViewModel.Path = new List<Node>();

            await _pathSolverController.ResetPathSolver(map);

            _appState.State = AppState.MapDesignPhase;
        }

        private void UpdateCanExecute(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(sender, e);
        }
        public event EventHandler CanExecuteChanged;
    }
}