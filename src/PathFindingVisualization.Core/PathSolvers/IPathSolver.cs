using System.Collections.Generic;
using System.Threading.Tasks;

namespace PathFindingVisualization.Core.PathSolvers
{
    public interface IPathSolver
    {
        bool AlgorithmDone { get; }
        List<Node.Node> Path { get; }

        Task PerformAlgorithmStep();
    }
}