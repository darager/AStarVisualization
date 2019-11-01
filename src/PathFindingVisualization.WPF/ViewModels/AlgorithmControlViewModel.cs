using System.Windows.Input;
using PathFindingVisualization.Core.PathSolvers;

namespace PathFindingVisualization.WPF.ViewModels
{
    public class AlgorithmControlViewModel
    {
        public ICommand StartAlgorithmCommand { get; private set; }
        public ICommand PauseAlgorithmCommand { get; private set; }
        public ICommand StopAlgorithmCommand { get; private set; }
        public ICommand ChooseAlgorithmCommand { get; private set; }

        private IPathSolver _pathSolver;
    }
}
