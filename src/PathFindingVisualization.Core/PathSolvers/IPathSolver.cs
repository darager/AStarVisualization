using System.Collections.Generic;

namespace PathFindingVisualization.Core.PathSolvers
{
    public interface IPathSolver
    {
        List<Node.Node> FindPath();
        void Stop();
    }
}