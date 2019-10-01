using AStarVisualization.WPF.AStarAlgorithm;
using AStarVisualization.WPF.Renderer.RenderHelpers;
using AStarVisualization.WPF.Renderer.TileRenderHelpers;
using System;
using System.Windows.Controls;

namespace AStarVisualization.WPF.Renderer
{
    class TileRenderer : IRenderer
    {
        private Canvas DrawingCanvas;
        private TileRenderObject[,] Tiles;

        public TileRenderer(Canvas canvas)
        {
            this.DrawingCanvas = canvas;

            int numRows = AStarValues.NumGridRows;
            int numColumns = AStarValues.NumGridColumns;

            this.Tiles = new TileRenderObject[numRows, numColumns];
            for (int i = 0; i < numRows; i++)
                for (int j = 0; j < numColumns; j++)
                {
                    var tile = new AStarTile(i, j);
                    Tiles[i, j] = new TileRenderObject(DrawingCanvas, tile);
                }
        }

        public void StartRendering()
        {
            AStarValues.TileHasChanged += Render;
            AStarValues.GridDimensionChanged += RemoveTiles;
        }
        public void StopRendering()
        {
            AStarValues.TileHasChanged -= Render;
            AStarValues.GridDimensionChanged -= RemoveTiles;
        }

        private void Render(object sender, int RowIndex, int ColumnIndex)
        {
            TileRenderObject oldTile = Tiles[RowIndex, ColumnIndex];

            AStarTile newAStarTile = AStarValues.AStarTiles[RowIndex, ColumnIndex];
            TileRenderObject newTile = new TileRenderObject(DrawingCanvas, newAStarTile);

            Tiles[RowIndex, ColumnIndex] = newTile;

            DrawingCanvas.Children.Add(newTile.Shape);
            DrawingCanvas.Children.Remove(oldTile.Shape);
        }
        private void RemoveTiles(object sender, EventArgs e)
        {
            var oldTiles = Tiles;

            foreach (var tile in oldTiles)
                DrawingCanvas.Children.Remove(tile.Shape);
        }
    }
}
