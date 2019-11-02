using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using PathFindingVisualization.Core.PathSolvers;
using Ninject;

namespace PathFindingVisualization.WPF.ViewModels
{
    public class AlgorithmControlViewModel : INotifyPropertyChanged
    {
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

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
