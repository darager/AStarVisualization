using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using AStarVisualization.AStarAlgorithm;
using AStarVisualization.Renderer.RenderHelpers;
using System.Collections.Generic;

namespace AStarVisualization.Renderer
{
    public class GridRenderer : IRenderer
    {
        private Canvas canvas;

        private SolidColorBrush Brush = RenderHelpers.RenderColors.Grid;

        private List<UIElement> GridLines = new List<UIElement>();

        public GridRenderer(Canvas canvas)
        {
            this.canvas = canvas;
            this.Brush = new SolidColorBrush(Colors.Black);
        }

        public void StartRendering()
        {
            AStarValues.GridDimensionChanged += Render;
            canvas.SizeChanged += Render;
        }
        public void StopRendering()
        {
            AStarValues.GridDimensionChanged -= Render;
            canvas.SizeChanged -= Render;
        }
        public void Render(object sender, EventArgs args)
        {
            RemoveGridLines();
            GridLines = GetGridLines(AStarValues.NumGridRows, AStarValues.NumGridColumns, canvas.ActualHeight, canvas.ActualWidth, Brush);
            AddGridLines();
        }

        private void AddGridLines()
        {
            foreach (UIElement line in GridLines)
                canvas.Children.Add(line);
        }
        private void RemoveGridLines()
        {
            foreach(UIElement line in GridLines)
                canvas.Children.Remove(line);
        }

        private List<UIElement> GetGridLines(int numRows, int numColumns, double CanvasHeight, double CanvasWidth, Brush brush)
        {
            var result = new List<UIElement>();

            // add the RowLines
            int numRowLines = numRows - 1;
            double RowLineSpacing = CanvasHeight / numRows;
            double RowLineX1 = 0;
            double RowLineX2 = CanvasWidth;
            for(int i = 0; i < numRowLines + 1; i++)
            {
                Line line = new Line();

                line.X1 = RowLineX1;
                line.X2 = RowLineX2;
                line.Y1 = (i+1) * RowLineSpacing;
                line.Y2 = line.Y1;

                line.Stroke = brush;
                line.SnapsToDevicePixels = true;

                result.Add(line);
            }

            // add the ColumnLines
            int numColumnLines = numColumns - 1;
            double ColumnLineSpacing = CanvasWidth / numColumns;
            double ColumnLineY1 = 0;
            double ColumnLineY2 = CanvasHeight;
            for (int i = 0; i < numColumnLines; i++)
            {
                Line line = new Line();

                line.X1 = (i+1) * ColumnLineSpacing;
                line.X2 = line.X1;
                line.Y1 = ColumnLineY1;
                line.Y2 = ColumnLineY2;

                line.Stroke = brush;
                line.SnapsToDevicePixels = true;

                result.Add(line);
            }

            return result;
        }
    }
}