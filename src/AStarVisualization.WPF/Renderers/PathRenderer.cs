using System;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
<<<<<<< HEAD:AStarVisualization/AStarVisualization/Renderers/PathRenderer.cs
using System.Collections.Generic;
using AStarVisualization.WPF.AStarAlgorithm;
using AStarVisualization.WPF.Renderer.RenderHelpers;
using AStarVisualization.WPF.AStarAlgorithm.AStarImplementation.Algorithmthread;
using System.Collections;
=======
>>>>>>> e92bf3931e56c010cb6668335a283a2f1a7e25a2:src/AStarVisualization.WPF/Renderers/PathRenderer.cs
using System.Windows.Media;
using AStarVisualization.WPF.Renderer.RenderHelpers;
using AStarVisualization.WPF.AStarAlgorithm;
using AStarVisualization.WPF.AStarAlgorithm.AStarImplementation.Algorithmthread;

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
            foreach(Node node in pathNodes)
            {
                double nodeX = node.ColumnIndex * columnSpacing + columnSpacing/2;
                double nodeY = node.RowIndex * rowSpacing + rowSpacing/2;

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