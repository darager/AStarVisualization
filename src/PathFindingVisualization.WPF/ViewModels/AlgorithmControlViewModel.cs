using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using PathFindingVisualization.Core.PathSolvers;
using Ninject;
using PathFindingVisualization.WPF.Models;

namespace PathFindingVisualization.WPF.ViewModels
{
    public class AlgorithmControlViewModel : INotifyPropertyChanged
    {
        [Inject, Named("StartAlgorithmCommand")]
        public ICommand StartAlgorithmCommand { get; set; } // HACK: this is only or testing purposes
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
        public bool DiagonalPathsEnabled
        {
            get { return _diagonalsEnabled; }
            set { _diagonalsEnabled = value; }
        }
        private bool _diagonalsEnabled = false;
        public bool SlowDownAlgorithm
        {
            get { return _slowDownAlgorithm; }
            set { _slowDownAlgorithm = value; }
        }
        private bool _slowDownAlgorithm;
        public bool MapDesignPhaseActive => (_appState.State == AppState.MapDesignPhase);
        private ApplicationState _appState;

        public AlgorithmControlViewModel(ApplicationState appState)
        {
            _appState = appState;
            _appState.PropertyChanged += RelayAppStateChanged;
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
