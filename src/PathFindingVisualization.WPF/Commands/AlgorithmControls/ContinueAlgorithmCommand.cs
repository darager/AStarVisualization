using System;
using System.ComponentModel;
using System.Windows.Input;
using PathFindingVisualization.Core.PathSolvers;
using PathFindingVisualization.WPF.Models;

namespace PathFindingVisualization.WPF.Commands.AlgorithmControls
{
    public class ContinueAlgorithmCommand : ICommand
    {
        private ApplicationState _appState;
        private PathSolverController _pathSolverController;

        public ContinueAlgorithmCommand(ApplicationState appState, PathSolverController pathSolverController)
        {
            _appState = appState;
            _pathSolverController = pathSolverController;
            _appState.PropertyChanged += UpdateCanExecute;
        }

        public bool CanExecute(object parameter) => _appState.State == AppState.MapDesignPhase;
        public void Execute(object parameter)
        {
            _appState.State = AppState.AlgorithmActive;

            _pathSolverController.ContinuePathSolver();
        }

        private void UpdateCanExecute(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(sender, e);
        }
        public event EventHandler CanExecuteChanged;
    }
}
