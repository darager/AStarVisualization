using System.Collections.Generic;
using System.Threading.Tasks;

namespace AStarVisualization.Core.PathSolvers
{
    public interface IPathSolver
    {
        List<Node> FindPath();
        void Stop();
    }
}