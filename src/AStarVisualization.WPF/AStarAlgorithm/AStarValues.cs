using System;
using System.Collections;

namespace AStarVisualization.WPF.AStarAlgorithm
{
    public static class AStarValues
    {
        public static void InitAStarTiles()
        {
            AStarTiles = new AStarTile[_NumGridRows, _NumGridColumns];

            for (int i = 0; i < _NumGridRows; i++)
                for (int j = 0; j < NumGridColumns; j++)
                    AStarTiles[i, j] = new AStarTile(i, j, Tile.Empty);
        }

        #region GridDimensions
        private static readonly object lockGridValuesObject = new object();

        private static int _NumGridRows = StartupValues.StartupValues.NumGridRows;
        private static int _NumGridColumns = StartupValues.StartupValues.NumGridColumns;

        public static int NumGridRows
        {
            get { lock (lockGridValuesObject) return _NumGridRows; }
            set
            {
                lock (lockGridValuesObject)
                {
                    if (_NumGridRows == value)
                        return;

                    _NumGridRows = value;
                }
                OnGridDimensionChanged(EventArgs.Empty);
            }
        }
        public static int NumGridColumns
        {
            get { lock (lockGridValuesObject) return _NumGridColumns; }
            set
            {
                lock (lockGridValuesObject)
                {
                    if (_NumGridColumns == value)
                        return;

                    _NumGridColumns = value;
                }
                OnGridDimensionChanged(EventArgs.Empty);
            }
        }

        public static event EventHandler GridDimensionChanged;
        private static void OnGridDimensionChanged(EventArgs args)
        {
            EventHandler handler = GridDimensionChanged;

            AStarTiles = new AStarTile[NumGridRows, NumGridColumns];

            if (handler != null)
                GridDimensionChanged(null, args);
        }
        #endregion

        #region AlgorithmControls
        private static readonly object lockAstarControlsObject = new object();
        private static State _AStarState = State.HasNotStarted;
        public static State AStarState
        {
            get { lock (lockAstarControlsObject) return _AStarState; }
            set
            {
                lock (lockAstarControlsObject)
                {
                    _AStarState = value;
                    AlgorithmStateChanged(null, EventArgs.Empty);
                }
            }
        }

        public static event EventHandler AlgorithmStateChanged;
        #endregion

        #region DelayControl
        private static readonly object lockDelayControlObject = new object();
        private static uint _Delay = StartupValues.StartupValues.CurrentDelay;
        public static uint Delay
        {
            get { lock (lockDelayControlObject) return _Delay; }
            set
            {
                lock (lockDelayControlObject)
                {
                    _Delay = value;
                    OnDelayChanged();
                }
            }
        }

        private static void OnDelayChanged()
        {
            EventHandler handler = DelayChanged;
            if (handler != null)
                DelayChanged(null, null);
        }
        public static event EventHandler DelayChanged;
        #endregion

        #region DiagonalPathsEnabled
        private static object DiagonalPathsEnabledlockObject = new object();

        private static bool _DiagonalPathsEnabled = false;
        public static bool DiagonalPathsEnabled
        {
            get { lock (DiagonalPathsEnabledlockObject) return _DiagonalPathsEnabled; }
            set { lock (DiagonalPathsEnabledlockObject) _DiagonalPathsEnabled = value; }
        }
        #endregion

        #region AStarTiles
        public static readonly object lockAstarTilesObject = new object();

        private static AStarTile _StartTile = null;
        private static AStarTile _GoalTile = null;
        private static AStarTile[,] _AStarTiles = new AStarTile[NumGridRows, NumGridColumns];

        public static AStarTile StartTile
        {
            get { lock (lockAstarTilesObject) return _StartTile; }
            set { lock (lockAstarTilesObject) _StartTile = value; }
        }
        public static AStarTile GoalTile
        {
            get { lock (lockAstarTilesObject) return _GoalTile; }
            set { lock (lockAstarTilesObject) _GoalTile = value; }
        }
        public static AStarTile[,] AStarTiles
        {
            get { lock (lockAstarTilesObject) return _AStarTiles; }
            set { lock (lockAstarTilesObject) _AStarTiles = value; }
        }
        public static void SetAStarTile(AStarTile newTile)
        {
            lock (lockAstarTilesObject)
            {
                Tile tileType = newTile.TileType;
                if (tileType == Tile.Goal)
                {
                    if (GoalTile != null)
                    {
                        var previousGoalTile = GoalTile;
                        int rowIndex = previousGoalTile.RowIndex;
                        int columnIndex = previousGoalTile.ColumnIndex;

                        var tile = new AStarTile(rowIndex, columnIndex, Tile.Empty);
                        SetAStarTile(tile);
                    }

                    GoalTile = newTile;
                }
                if (tileType == Tile.Start)
                {
                    if (StartTile != null)
                    {
                        var previousStartTile = StartTile;
                        int rowIndex = previousStartTile.RowIndex;
                        int columnIndex = previousStartTile.ColumnIndex;

                        var tile = new AStarTile(rowIndex, columnIndex, Tile.Empty);
                        SetAStarTile(tile);
                    }

                    StartTile = newTile;
                }

                AStarTiles[newTile.RowIndex, newTile.ColumnIndex] = newTile;
                OnTileChanged(newTile);
            }
        }

        private static void OnTileChanged(AStarTile newTile)
        {
            TileChangedHandler handler = TileHasChanged;

            if (handler != null)
                TileHasChanged(null, newTile.RowIndex, newTile.ColumnIndex);
        }
        public delegate void TileChangedHandler(object sender, int RowIndex, int ColumnIndex);
        public static event TileChangedHandler TileHasChanged;
        #endregion

        #region SolutionPath
        public static readonly object lockPathObject = new object();

        private static ArrayList _Path = null;

        public static ArrayList Path
        {
            get
            {
                lock (lockPathObject)
                    return _Path;
            }
            set
            {
                lock (lockPathObject)
                {
                    _Path = value;
                    OnPathFound();
                }
            }
        }

        private static void OnPathFound()
        {
            EventHandler handler = PathChanged;
            if (handler != null)
                PathChanged(null, null);
        }
        public static event EventHandler PathChanged;
        #endregion
    }
}
