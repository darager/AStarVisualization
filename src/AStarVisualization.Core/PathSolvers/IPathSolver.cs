using System.Collections.Generic;
using System.Threading.Tasks;

namespace AStarVisualization.Core.PathSolvers
{
    public interface IPathSolver
    {
        Task<List<Node>> FindPath();
        void Stop();
    }
}