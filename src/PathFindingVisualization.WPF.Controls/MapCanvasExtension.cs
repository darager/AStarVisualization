using System;
using System.Windows;
using System.Windows.Input;

namespace PathFindingVisualization.WPF.Controls
{
    public static class MapCanvasExtension
    {
        public static (int, int) GetNodeIndices(this MapCanvas mapCanvas, Point position)
        {
            double rowSpacing = mapCanvas.ActualHeight / mapCanvas.NumRows;
            double colSpacing = mapCanvas.ActualWidth / mapCanvas.NumColumns;

            int rowIdx = (int)Math.Truncate(position.Y / rowSpacing);
            int colIdx = (int)Math.Truncate(position.X / colSpacing);

            return (rowIdx, colIdx);
        }
    }
}
