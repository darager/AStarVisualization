using System.Collections.Generic;

namespace PathFindingVisualization.Core.PathSolvers
{
    public interface IAlgorithmData
    {
        int AlgorithmStep { get; set; }
        bool DiagonalsEnabled { get; }
    }
}
