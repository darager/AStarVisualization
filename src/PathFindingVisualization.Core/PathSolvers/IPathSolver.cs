using System.Collections.Generic;
using System.Threading.Tasks;
using PathFindingVisualization.Core.Node;

namespace PathFindingVisualization.Core.PathSolvers
{
    public interface IPathSolver
    {
        bool AlgorithmDone { get; }
        List<INode> Path { get; }
        IAlgorithmData AlgorithmData { get; }

        Task PerformAlgorithmStep();
    }
}