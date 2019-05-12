using System;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Collections.Generic;
using AStarVisualization.AStarAlgorithm;
using AStarVisualization.Renderer.RenderHelpers;
using AStarVisualization.AStarAlgorithm.AStarImplementation.Algorithmthread;
using System.Collections;

namespace AStarVisualization.Renderer
{
    class PathRenderer : IRenderer
    {
        Canvas canvas;

        List<Line> path = new List<Line>();

        public PathRenderer(Canvas canvas)
        {
            this.canvas = canvas;
        }


        public void StartRendering()
        {
            AStarValues.GridDimensionChanged += RemovePath;
            StateObserver.ResetAlgorithm += RemovePath;
            AStarValues.PathChanged += Render;
        }
        public void StopRendering()
        {
            AStarValues.GridDimensionChanged -= RemovePath;
            StateObserver.ResetAlgorithm -= RemovePath;
            AStarValues.PathChanged -= Render;
        }
        public void Render(object sender, EventArgs args)
        {
            ArrayList path = AStarValues.Path;

            var pathNodes = new List<Node>();
            foreach (Node node in path)
                pathNodes.Add(node);

            int numRows = AStarValues.NumGridRows;
            int numColumns = AStarValues.NumGridColumns;

            double width = canvas.ActualWidth;
            double height = canvas.ActualHeight;

            double rowSpacing = height / numRows;
            double columnSpacing = width / numColumns;

            Point lastPoint = new Point(pathNodes[0].ColumnIndex * columnSpacing + columnSpacing / 2, pathNodes[0].RowIndex * rowSpacing + rowSpacing / 2);
            foreach (Node node in pathNodes)
            {
                Point currentPoint = new Point(node.ColumnIndex * columnSpacing + columnSpacing / 2, node.RowIndex * rowSpacing + rowSpacing / 2);

                Line line = new Line();
                line.X1 = lastPoint.X;
                line.Y1 = lastPoint.Y;
                line.X2 = currentPoint.X;
                line.Y2 = currentPoint.Y;
                line.StrokeThickness = 3;
                line.SnapsToDevicePixels = true;
                line.Stroke = RenderColors.Path;

                this.path.Add(line);
                canvas.Children.Add(line);

                lastPoint = currentPoint;
            }
        }

        private void RemovePath(object sender, EventArgs e)
        {
            foreach (Line line in path)
                canvas.Children.Remove(line);
        }
    }
}
