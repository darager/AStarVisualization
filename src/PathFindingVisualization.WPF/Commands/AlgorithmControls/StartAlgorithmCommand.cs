using System;
using System.ComponentModel;
using System.Windows.Input;
using PathFindingVisualization.Core.PathSolvers;
using PathFindingVisualization.WPF.Models;
using PathFindingVisualization.WPF.ViewModels;

namespace PathFindingVisualization.WPF.Commands.AlgorithmControls
{
    // HACK: this class is only for testing purposes for now
    public class StartAlgorithmCommand : ICommand
    {
        private IPathSolverFactory _pathSolverFactory;
        private ApplicationState _appState;
        private MainViewModel _mainViewModel;

        public StartAlgorithmCommand(IPathSolverFactory pathSolverFactory, ApplicationState appState, MainViewModel mainViewModel)
        {
            _pathSolverFactory = pathSolverFactory;
            _appState = appState;
            _mainViewModel = mainViewModel;
            _appState.PropertyChanged += UpdateCanExecute;
        }

        public bool CanExecute(object parameter) => _appState.State == AppState.MapDesignPhase;
        public async void Execute(object parameter)
        {
            _appState.State = AppState.AlgorithmActive;

            var map = _mainViewModel.Map;
            var pathSolver = _pathSolverFactory.GetPathSolver(ref map, PathSolver.AStar, true);

            _mainViewModel.Path = await pathSolver.FindPath();

            _appState.State = AppState.AlgorithmDone;
        }

        private void UpdateCanExecute(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(sender, e);
        }
        public event EventHandler CanExecuteChanged;
    }
}
