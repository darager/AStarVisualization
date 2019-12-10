using System.Collections.Generic;
using System.Threading.Tasks;

namespace PathFindingVisualization.Core.PathSolvers
{
    public interface IPathSolver
    {
        bool StopAlgorithm { get; }
        List<Node.Node> Path { get; }

        Task PerformAlgorithmStep();
        Task Stop();
    }
}