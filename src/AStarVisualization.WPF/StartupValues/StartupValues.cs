using System;

namespace AStarVisualization.WPF.StartupValues
{
    static class StartupValues
    {
        // Griddimensions:
        public static readonly int NumGridRows = 60;
        public static readonly int NumGridColumns = 60;
        public static int MaxDimension => Math.Max(NumGridRows, NumGridColumns);

        // Delay Properties:
        public static readonly uint CurrentDelay = 0;
        public static readonly uint MinDelay = 0;
        public static readonly uint MaxDelay = 250;

        // DiagonalPaths:
        public static readonly bool DiagonalPathsAllowed = true;
    }
}