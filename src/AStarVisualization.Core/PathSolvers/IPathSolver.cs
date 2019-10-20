using System.Collections.Generic;

namespace AStarVisualization.Core.PathSolvers
{
    public interface IPathSolver
    {
        List<Node> FindPath();
        void Stop();
    }
}