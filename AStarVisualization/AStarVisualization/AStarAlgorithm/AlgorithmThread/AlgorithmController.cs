using AStarVisualization.AStarAlgorithm.AStarImplementation.Algorithmthread.Helpers;
using System;
using System.Windows.Threading;

namespace AStarVisualization.AStarAlgorithm.AStarImplementation.Algorithmthread
{
    public class AlgorithmController
    {
        private Dispatcher mainDispatcher;
        private AStarTile[,] tiles;
        private Node[,] map;
        private Node StartNode;
        private Node GoalNode;
        private AStarAlgorithm algorithm;

        public AlgorithmController(Dispatcher threadDispatcher)
        {
            this.mainDispatcher = threadDispatcher;
            BindAlgorithmToControllEvents();
            SetupMap();
            GetStartAndGoal();
            InitAlgorithm();
        }

        private void InitAlgorithm()
        {
            algorithm = new AStarAlgorithm(map, StartNode, GoalNode);
        }
        private void SetupMap()
        {
            this.tiles = AStarValues.AStarTiles;
            int numRows = tiles.GetLength(0);
            int numColumns = tiles.GetLength(1);

            this.map = new Node[numRows, numColumns];
            var nodes = new Node[numRows, numColumns];

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numColumns; col++)
                {
                    AStarTile tile;
                    if (tiles[row, col] == null)
                    {
                        tile = new AStarTile(row, col);
                    }
                    else
                    {
                        tile = tiles[row, col];
                    }

                    map[row, col] = new Node(tile);
                }
            }
        }
        private void GetStartAndGoal()
        {
            foreach (AStarTile tile in tiles)
            {
                try
                {
                    if (tile.TileType == Tile.Goal)
                    {
                        GoalNode = map[tile.RowIndex, tile.ColumnIndex];
                    }
                    else if (tile.TileType == Tile.Start)
                    {
                        StartNode = map[tile.RowIndex, tile.ColumnIndex];
                    }
                }
                catch { }
            }
        }
        private void BindAlgorithmToControllEvents()
        {
            StateObserver.PauseAlgorithm += PauseAlgorithm;
            StateObserver.ContinueAlgorithm += ContinueAlgorithm;
            AStarAlgorithm.PathFound += DrawPath;
            ClosedSet.NodeAddedToCollection += DrawTile;
            OpenSet.NodeAddedToCollection += DrawTile;
            AStarAlgorithm.AlgorithmIsDone += SetStateToFinished;
        }

        public void Reset()
        {
            PauseAlgorithm(this, null);
        }
        private void PauseAlgorithm(object sender, EventArgs args)
        {
            algorithm?.Pause();
        }
        private void ContinueAlgorithm(object sender, EventArgs args)
        {
            algorithm?.Continue();
        }
        private void DrawPath(object sender, PathFoundEventArgs args)
        {
            mainDispatcher.Invoke(() =>
            {
                AStarValues.Path = args.Path;
            }, DispatcherPriority.Normal);
        }
        private void DrawTile(object sender, NodeAddedToCollectionEventArgs args)
        {
            mainDispatcher.Invoke(() =>
            {
                Node node = args.node;
                AStarTile tile = AStarValues.AStarTiles[node.RowIndex, node.ColumnIndex];
                if (tile == null)
                {
                    tile = new AStarTile(node.RowIndex, node.ColumnIndex);
                }

                Tile tileType = tile.TileType;
                if (tileType == Tile.Empty || tileType == Tile.EmptyClosed || tileType == Tile.EmptyOpen)
                {
                    Tile newType = Tile.Empty;

                    if (sender is OpenSet)
                    {
                        newType = Tile.EmptyOpen;
                    }
                    else if (sender is ClosedSet)
                    {
                        newType = Tile.EmptyClosed;
                    }

                    tile.TileType = newType;

                    AStarValues.SetAStarTile(tile);
                }
            }, DispatcherPriority.Normal);
        }
        private void SetStateToFinished(object sender, EventArgs e)
        {
            mainDispatcher.Invoke(() =>
            {
                AStarValues.AStarState = State.Finished;
            },
            DispatcherPriority.Normal);
        }
    }
}
