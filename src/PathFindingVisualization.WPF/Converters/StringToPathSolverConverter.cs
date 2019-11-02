using System;
using System.Globalization;
using System.Windows.Data;
using PathFindingVisualization.Core.PathSolvers;

namespace PathFindingVisualization.WPF.Converters
{
    public class StringToPathSolverConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pathsolverName = (string)value;

            switch (pathsolverName)
            {
                case "AStar":
                    return PathSolver.AStar;
                case "BreadthFirstSearch":
                    return PathSolver.BreadthFirstSearch;
                case "BestFirstSearch":
                    return PathSolver.BestFirstSearch;
                case "Dijkstra":
                    return PathSolver.Dijkstra;
                case "JumpPointSearch":
                    return PathSolver.JumpPointSearch;
                case "OrthogonalJumpPointSearch":
                    return PathSolver.OrthogonalJumpPointSearch;
                case "Trace":
                    return PathSolver.Trace;
                default:
                    return null;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pathsolver = (PathSolver)value;
            return pathsolver.ToString();
        }
    }
}
