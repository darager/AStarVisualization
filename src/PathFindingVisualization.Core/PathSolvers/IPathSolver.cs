using System.Collections.Generic;
using System.Threading.Tasks;
using PathFindingVisualization.Core.Node;

namespace PathFindingVisualization.Core.PathSolvers
{
    public interface IPathSolver
    {
        //Task<List<INode>> FindPath();
        //void Stop();

        bool AlgorithmDone { get; }
        List<INode> Path { get; }

        Task PerformAlgorithmStep();
    }
}