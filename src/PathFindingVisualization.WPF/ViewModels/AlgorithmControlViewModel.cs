using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Ninject;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.Core.PathSolvers;
using PathFindingVisualization.WPF.Models;

namespace PathFindingVisualization.WPF.ViewModels
{
    public class AlgorithmControlViewModel : INotifyPropertyChanged
    {
        [Inject, Named("StartAlgorithmCommand")]
        public ICommand StartAlgorithmCommand { get; set; }
        [Inject, Named("ResetAlgorithmCommand")]
        public ICommand ResetAlgorithmCommand { get; set; }

        public List<PathSolver> PathSolverTypes =>
            Enum.GetValues(typeof(PathSolver))
            .Cast<PathSolver>()
            .ToList<PathSolver>();
        public PathSolver PathSolverType
        {
            get => _pathSolverType;
            set
            {
                if (_pathSolverType == value)
                    return;

                _pathSolverType = value;
                OnPropertyChanged("PathSolver");
            }
        }
        private PathSolver _pathSolverType = PathSolver.AStar;

        public bool DiagonalPathsEnabled { get; set; } = false;
        public bool MapDesignPhaseActive => (_appState.State == AppState.MapDesignPhase);

        private ApplicationState _appState;
        private PathSolverController _pathSolverController;
        private MainViewModel _mainViewModel;

        public AlgorithmControlViewModel(ApplicationState appState, PathSolverController pathSolverController, MainViewModel mainViewModel)
        {
            _appState = appState;
            _appState.PropertyChanged += RelayAppStateChanged;

            _pathSolverController = pathSolverController;
            _pathSolverController.PathChanged += RelayPathChanged;

            _mainViewModel = mainViewModel;
        }

        private void RelayPathChanged(object sender, List<Node> newPath)
        {
            _mainViewModel.Path = newPath;
        }
        private void RelayAppStateChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("MapDesignPhaseActive");
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
