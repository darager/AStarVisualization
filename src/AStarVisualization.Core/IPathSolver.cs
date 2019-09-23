using System.Collections.Generic;
using System.Threading.Tasks;

namespace AStarVisualization.Core
{
    public interface IPathSolver
    {
        Task<List<Node>> FindPath();
        void Stop();
    }
}