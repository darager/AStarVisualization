using System.Collections.Generic;
using System.Threading.Tasks;

namespace PathFindingVisualization.Core.PathSolvers
{
    public interface IPathSolver
    {
        Task<List<Node.Node>> FindPath();
        void Stop();
    }
}