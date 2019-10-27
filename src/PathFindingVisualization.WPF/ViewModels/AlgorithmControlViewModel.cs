using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.PathSolvers;
using PathFindingVisualization.WPF.Models;
using System.Windows.Input;

namespace PathFindingVisualization.WPF.ViewModels
{
    public class AlgorithmControlViewModel
    {
        public ICommand StartAlgorithmCommand { get; private set; }
        public ICommand PauseAlgorithmCommand { get; private set; }
        public ICommand StopAlgorithmCommand { get; private set; }
        public ICommand ChooseAlgorithmCommand { get; private set; }

        private MapCanvasData _mapCanvasData;
        private IPathSolver _pathSolver;

        public AlgorithmControlViewModel(MapCanvasData mapCanvasData)
        {
            //StartAlgorithmCommand =
            //PauseAlgorithmCommand =
            //StopAlgorithmCommand =
            //ChooseAlgorithmCommand =
            _mapCanvasData = mapCanvasData;
        }
    }
}
