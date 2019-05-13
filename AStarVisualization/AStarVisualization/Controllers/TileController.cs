using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using AStarVisualization.UIElements;
using AStarVisualization.AStarAlgorithm;

namespace AStarVisualization.Controllers
{
    class TileController : IController
    {
        private readonly SolidColorBrush ButtonHighlightColor = new SolidColorBrush(Colors.LightBlue);
        private readonly SolidColorBrush EnabledButtonColor;
        private readonly SolidColorBrush DisabledButtonColor = new SolidColorBrush(Colors.DarkGray);

        private Canvas canvas;
        private Button SetStartButton;
        private Button SetGoalButton;
        private Button SetWallButton;
        private Button ClearTilesButton;

        private bool SetStartActive = false;
        private bool SetGoalActive = false;
        private bool SetWallActive = false;
        private bool SetTileActive => (SetStartActive || SetGoalActive || SetWallActive);

        public TileController(UIElements.UIControl uiElements)
        {
            canvas = (Canvas)uiElements.AStarControls[ControlNames.DrawingCanvas];
            SetStartButton = (Button)uiElements.AStarControls[ControlNames.SetStartTileButton];
            SetGoalButton = (Button)uiElements.AStarControls[ControlNames.SetGoalTileButton];
            SetWallButton = (Button)uiElements.AStarControls[ControlNames.SetWallTileButton];
            ClearTilesButton = (Button)uiElements.AStarControls[ControlNames.ClearTilesButton];

            EnabledButtonColor = (SolidColorBrush)SetStartButton.Background; // get the default backgroundcolor of the buttons
        }

        public void StartControlling()
        {
            SetStartButton.Click += SetStartTileValueChanged;
            SetGoalButton.Click += SetGoalTileValueChanged;
            SetWallButton.Click += SetWallTileValueChanged;
            ClearTilesButton.Click += ClearTiles_Pressed;

            canvas.MouseLeftButtonDown += PlaceTile;
            canvas.MouseRightButtonDown += RemoveTile;
            canvas.MouseMove += MouseMoved;

            AStarValues.AlgorithmStateChanged += AlgorithmStateChanged;
            AStarValues.GridDimensionChanged += ResetTiles;

            StateObserver.StartAlgorithm += SetRandomStartGoal;
            StateObserver.ResetAlgorithm += ResetPathTiles;
        }
        public void StopControlling()
        {
            SetStartButton.Click -= SetStartTileValueChanged;
            SetGoalButton.Click -= SetGoalTileValueChanged;
            SetWallButton.Click -= SetWallTileValueChanged;
            ClearTilesButton.Click -= ClearTiles_Pressed;

            canvas.MouseLeftButtonDown -= PlaceTile;
            canvas.MouseRightButtonDown -= RemoveTile;
            canvas.MouseMove -= MouseMoved;

            AStarValues.AlgorithmStateChanged -= AlgorithmStateChanged;
            AStarValues.GridDimensionChanged -= ResetTiles;

            StateObserver.StartAlgorithm -= SetRandomStartGoal;
            StateObserver.ResetAlgorithm -= ResetPathTiles;
        }

        private void EnableAllButtons()
        {
            SetStartButton.IsEnabled = true;
            SetGoalButton.IsEnabled = true;
            SetWallButton.IsEnabled = true;

            ClearTilesButton.IsEnabled = true;
        }
        private void DisableAllButtons()
        {
            if (SetStartActive)
                SetStartTileValueChanged(this, EventArgs.Empty);
            else if (SetWallActive)
                SetWallTileValueChanged(this, EventArgs.Empty);
            else if (SetGoalActive)
                SetGoalTileValueChanged(this, EventArgs.Empty);

            SetStartButton.IsEnabled = false;
            SetGoalButton.IsEnabled = false;
            SetWallButton.IsEnabled = false;

            ClearTilesButton.IsEnabled = false;
        }
        private void AlgorithmStateChanged(object sender, EventArgs args)
        {
            if (AStarValues.AStarState == State.HasNotStarted)
                EnableAllButtons();
            else
                DisableAllButtons();
        }

        private void DisableButton(Button button)
        {
            button.IsEnabled = false;
            button.Background = DisabledButtonColor;
        }
        private void EnableButton(Button button)
        {
            button.IsEnabled = true;
            button.Background = EnabledButtonColor;
        }
        private void AddButtonHighlight(Button button)
        {
            button.Background = ButtonHighlightColor;
        }
        private void RemoveButtonHighlight(Button button)
        {
            button.Background = EnabledButtonColor;
        }

        private void SetStartTileValueChanged(object sender, EventArgs args)
        {
            if (!SetStartActive)
            {
                SetStartActive = true;

                AddButtonHighlight(SetStartButton);
                DisableButton(SetGoalButton);
                DisableButton(SetWallButton);
            }
            else
            {
                SetStartActive = false;

                RemoveButtonHighlight(SetStartButton);
                EnableButton(SetGoalButton);
                EnableButton(SetWallButton);
            }
        }
        private void SetGoalTileValueChanged(object sender, EventArgs args)
        {
            if (!SetGoalActive)
            {
                SetGoalActive = true;

                AddButtonHighlight(SetGoalButton);
                DisableButton(SetStartButton);
                DisableButton(SetWallButton);
            }
            else
            {
                SetGoalActive = false;

                RemoveButtonHighlight(SetGoalButton);
                EnableButton(SetStartButton);
                EnableButton(SetWallButton);
            }
        }
        private void SetWallTileValueChanged(object sender, EventArgs args)
        {
            if (!SetWallActive)
            {
                SetWallActive = true;

                AddButtonHighlight(SetWallButton);
                DisableButton(SetStartButton);
                DisableButton(SetGoalButton);
            }
            else
            {
                SetWallActive = false;

                RemoveButtonHighlight(SetWallButton);
                EnableButton(SetStartButton);
                EnableButton(SetGoalButton);
            }
        }

        private void ClearTiles_Pressed(object sender, EventArgs args)
        {
            int numRows = AStarValues.NumGridRows;
            int numColumns = AStarValues.NumGridColumns;

            AStarValues.StartTile = null;
            AStarValues.GoalTile = null;

            for (int i = 0; i < numRows; i++)
                for (int j = 0; j < numColumns; j++)
                {
                    var newTile = new AStarTile(i, j, Tile.Empty);
                    AStarValues.SetAStarTile(newTile);
                }
        }

        private int GetRowIndex(double MousePosY)
        {
            double canvasHeight = canvas.ActualHeight;
            double GridRowSpacing = canvasHeight / AStarValues.NumGridRows;

            int RowIndex = (int)(MousePosY / GridRowSpacing); // this truncates the decimal number

            return RowIndex;
        }
        private int GetColumnIndex(double MousePosX)
        {
            double canvasWidth = canvas.ActualWidth;
            double GridColumnIndex = canvasWidth / AStarValues.NumGridColumns;

            int ColumnIndex = (int)(MousePosX / GridColumnIndex); // this truncates the decimal number

            return ColumnIndex;
        }

        private void MouseMoved(object sender, MouseEventArgs e)
        {
            if (SetWallActive)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    PlaceTile(this, e);
            }

            if (e.RightButton == MouseButtonState.Pressed)
                RemoveTile(this, e);
        }

        private void PlaceTile(object sender, MouseEventArgs e)
        {
            Point mousePosition = e.GetPosition(canvas);
            double MousePosX = mousePosition.X;
            double MousePosY = mousePosition.Y;

            int RowIndex = GetRowIndex(MousePosY);
            int ColumnIndex = GetColumnIndex(MousePosX);

            AStarTile previousTile;
            if(AStarValues.AStarTiles[RowIndex, ColumnIndex] != null)
            {
                previousTile = AStarValues.AStarTiles[RowIndex, ColumnIndex];

                switch (previousTile.TileType)
                {
                    case Tile.Goal:
                        AStarValues.GoalTile = null;
                        break;
                    case Tile.Start:
                        AStarValues.StartTile = null;
                        break;
                    default:
                        break;
                }
            }

            if (SetWallActive)
            {
                SetWallTile(RowIndex, ColumnIndex);
                return;
            }
            if (SetGoalActive)
            {
                SetGoalTile(RowIndex, ColumnIndex);
                return;
            }
            if (SetStartActive)
            {
                SetStartTile(RowIndex, ColumnIndex);
                return;
            }
        }
        private void SetStartTile(int RowIndex, int ColumnIndex)
        {
            var startTile = new AStarTile(RowIndex, ColumnIndex, Tile.Start);
            AStarValues.SetAStarTile(startTile);
            AStarValues.StartTile = startTile;

            SetStartTileValueChanged(this, EventArgs.Empty);
        }
        private void SetGoalTile(int RowIndex, int ColumnIndex)
        {
            var goalTile = new AStarTile(RowIndex, ColumnIndex, Tile.Goal);
            AStarValues.SetAStarTile(goalTile);
            AStarValues.GoalTile = goalTile;

            SetGoalTileValueChanged(this, EventArgs.Empty);
        }

        private void SetWallTile(int RowIndex, int ColumnIndex)
        {
            var wallTile = new AStarTile(RowIndex, ColumnIndex, Tile.Wall);

            AStarValues.SetAStarTile(wallTile);
        }
        private void RemoveTile(object sender, MouseEventArgs e)
        {
            if (AStarValues.AStarState == State.HasNotStarted)
            {
                Point mousePosition = e.GetPosition(canvas);
                double MousePosX = mousePosition.X;
                double MousePosY = mousePosition.Y;

                int RowIndex = GetRowIndex(MousePosY);
                int ColumnIndex = GetColumnIndex(MousePosX);

                AStarTile oldTile = AStarValues.AStarTiles[RowIndex, ColumnIndex];

                if (oldTile == null)
                    return;

                if (oldTile.TileType == Tile.Start)
                    AStarValues.StartTile = null;
                else if (oldTile.TileType == Tile.Goal)
                    AStarValues.GoalTile = null;

                var emptyTile = new AStarTile(RowIndex, ColumnIndex, Tile.Empty);
                AStarValues.SetAStarTile(emptyTile);
            }
        }
        private void SetRandomStartGoal(object sender, EventArgs args)
        {
            if (AStarValues.StartTile == null)
                SetStartTile(0, 0);

            if (AStarValues.GoalTile == null)
            {
                int numRows = AStarValues.NumGridRows;
                int numColumns = AStarValues.NumGridColumns;

                var rnd = new Random();
                int rndRowIndex = rnd.Next(1, numRows);
                int rndColumnIndex = rnd.Next(1, numColumns);

                SetGoalTile(rndRowIndex, rndColumnIndex);
            }
        }

        private void ResetTiles(object sender, EventArgs e)
        {
            AStarValues.GoalTile = null;
            AStarValues.StartTile = null;
            AStarValues.AStarTiles = new AStarTile[AStarValues.NumGridRows, AStarValues.NumGridColumns];
        }
        private void ResetPathTiles(object sender, EventArgs args)
        {
            AStarTile[,] tiles = AStarValues.AStarTiles;

            foreach(AStarTile tile in tiles)
            {
                if (tile == null)
                    continue;

                if (tile.TileType == Tile.EmptyClosed || tile.TileType == Tile.EmptyOpen)
                {
                    tile.TileType = Tile.Empty;
                    AStarValues.SetAStarTile(tile);
                }
            }
        }
    }
}