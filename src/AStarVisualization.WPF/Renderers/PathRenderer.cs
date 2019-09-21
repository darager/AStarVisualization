using AStarVisualization.WPF.AStarAlgorithm;
using AStarVisualization.WPF.AStarAlgorithm.AStarImplementation.Algorithmthread;
using AStarVisualization.WPF.Renderer.RenderHelpers;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AStarVisualization.WPF.Renderer
{
    class PathRenderer : IRenderer
    {
        Canvas canvas;

        Polyline pathline;

        public PathRenderer(Canvas canvas)
        {
            this.canvas = canvas;

            InitPathLine();
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
            Node[] pathNodes = AStarValues.Path.ToArray(typeof(Node)) as Node[];

            int numRows = AStarValues.NumGridRows;
            int numColumns = AStarValues.NumGridColumns;
            double width = canvas.ActualWidth;
            double height = canvas.ActualHeight;
            double rowSpacing = height / numRows;
            double columnSpacing = width / numColumns;

            var points = new PointCollection();
            foreach (Node node in pathNodes)
            {
                double nodeX = node.ColumnIndex * columnSpacing + columnSpacing / 2;
                double nodeY = node.RowIndex * rowSpacing + rowSpacing / 2;

                points.Add(new Point(nodeX, nodeY));
            }

            pathline.Points = points;

            canvas.Children.Remove(pathline);
            canvas.Children.Add(pathline);
        }
        private void RemovePath(object sender, EventArgs e)
        {
            canvas.Children.Remove(pathline);
        }
        private void InitPathLine()
        {
            pathline = new Polyline();
            pathline.Stroke = RenderColors.Path;
            pathline.StrokeThickness = RenderColors.PathThickness;
        }
    }
}