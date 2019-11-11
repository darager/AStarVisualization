using System;
using System.ComponentModel;
using System.Windows.Input;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.PathSolvers;
using PathFindingVisualization.WPF.Models;
using PathFindingVisualization.WPF.ViewModels;

// TODO: clean up this command and create an object that handles most of the logic
namespace PathFindingVisualization.WPF.Commands.AlgorithmControls
{
    // HACK: this class is only for testing purposes for now
    public class StartAlgorithmCommand : ICommand
    {
        private IPathSolverFactory _pathSolverFactory;
        private ApplicationState _appState;
        private MainViewModel _mainViewModel;
        private AlgorithmControlViewModel _algorithmControlViewModel;

        public StartAlgorithmCommand(IPathSolverFactory pathSolverFactory, AlgorithmControlViewModel algorithmControlViewModel, ApplicationState appState, MainViewModel mainViewModel)
        {
            _pathSolverFactory = pathSolverFactory;
            _algorithmControlViewModel = algorithmControlViewModel;
            _appState = appState;
            _mainViewModel = mainViewModel;
            _appState.PropertyChanged += UpdateCanExecute;
        }

        public bool CanExecute(object parameter) => _appState.State == AppState.MapDesignPhase;
        public async void Execute(object parameter)
        {
            _appState.State = AppState.AlgorithmActive;

            Map map = _mainViewModel.Map;
            bool diagonalsEnabled = _algorithmControlViewModel.DiagonalPathsEnabled;
            PathSolver pathsolverType = _algorithmControlViewModel.PathSolverType;

            var pathSolver = _pathSolverFactory.GetPathSolver(ref map, pathsolverType, diagonalsEnabled);
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
