using System;

namespace AStarVisualization.WPF.WPF.StartupValues
{
    static class StartupValues
    {
        // Griddimensions:
        public static readonly int NumGridRows = 40;
        public static readonly int NumGridColumns = 40;
        public static int MaxDimension => Math.Max(NumGridRows, NumGridColumns);

        // Delay Properties:
        public static readonly uint CurrentDelay = 0;
        public static readonly uint MinDelay = 0;
        public static readonly uint MaxDelay = 250;

        // DiagonalPaths:
        public static readonly bool DiagonalPathsEnabled = true;
    }
}