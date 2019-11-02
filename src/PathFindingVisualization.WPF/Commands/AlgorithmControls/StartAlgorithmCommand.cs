using System;
using System.Windows.Input;
using PathFindingVisualization.Core.PathSolvers;
using PathFindingVisualization.WPF.ViewModels;

namespace PathFindingVisualization.WPF.Commands.AlgorithmControls
{
    // HACK: this class is only for testing purposes for now
    public class StartAlgorithmCommand : ICommand
    {
        private IPathSolverFactory _pathSolverFactory;
        private MainViewModel _mainViewModel;

        public StartAlgorithmCommand(IPathSolverFactory pathSolverFactory, MainViewModel mainViewModel)
        {
            _pathSolverFactory = pathSolverFactory;
            _mainViewModel = mainViewModel;
        }

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            var map = _mainViewModel.Map;
            var pathSolver = _pathSolverFactory.GetPathSolver(ref map, PathSolver.AStar, true);

            var path = pathSolver.FindPath();
            _mainViewModel.Path = path;
        }

        public event EventHandler CanExecuteChanged;
    }
}
