using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using AStarVisualization.WPF.UIElements;
using AStarVisualization.WPF.AStarAlgorithm;
using System.Windows;
using AStarVisualization.WPF.WPF.StartupValues;

namespace AStarVisualization.WPF.Controllers
{
    public class DimensionController : IController
    {
        private TextBox TxtNumColumns;
        private TextBox TxtNumRows;

        public DimensionController(Window window)
        {
            this.TxtNumColumns = (TextBox)window.FindName(ControlNames.NumColumnsField);
            this.TxtNumRows = (TextBox)window.FindName(ControlNames.NumRowsField);
        }

        public void StartControlling()
        {
            TxtNumColumns.TextChanged += DimensionChanged;
            TxtNumRows.TextChanged += DimensionChanged;

            TxtNumColumns.LostFocus += ChangeDimensionText;
            TxtNumRows.LostFocus += ChangeDimensionText;

            AStarValues.AlgorithmStateChanged += ChangeControlState;
        }
        public void StopControlling()
        {
            TxtNumColumns.TextChanged -= DimensionChanged;
            TxtNumRows.TextChanged -= DimensionChanged;

            TxtNumColumns.LostFocus -= ChangeDimensionText;
            TxtNumRows.LostFocus -= ChangeDimensionText;

            AStarValues.AlgorithmStateChanged -= ChangeControlState;
        }

        private void ChangeControlState(object sender, EventArgs args)
        {
            if (AStarValues.AStarState == State.HasNotStarted)
                EnableControls();
            else
                DisableControls();
        }
        private void EnableControls()
        {
            TxtNumRows.IsEnabled = true;
            TxtNumColumns.IsEnabled = true;
        }
        private void DisableControls()
        {
            TxtNumRows.IsEnabled = false;
            TxtNumColumns.IsEnabled = false;
        }

        private void DimensionChanged(object sender, EventArgs args)
        {
            string numRows = TxtNumRows.Text;
            string numColumns = TxtNumColumns.Text;

            if (!IsValidDimension(numRows))
                ChangeText(TxtNumRows);
            if (!IsValidDimension(numColumns))
                ChangeText(TxtNumColumns);

            int maxDimension = StartupValues.MaxDimension;

            int RowDimension = GetDimension(numRows, maxDimension);
            int ColumnDimension = GetDimension(numColumns, maxDimension);

            AStarValues.NumGridColumns = ColumnDimension;
            AStarValues.NumGridRows = RowDimension;
        }
        private int GetDimension(string GridDimension, int MaxDimension)
        {
            var regex = GetRegex();
            int dimension = MaxDimension;

            if (regex.IsMatch(GridDimension))
            {
                Match match = regex.Match(GridDimension);

                int newDimension = Convert.ToInt32(match.ToString());

                if (newDimension <= MaxDimension && newDimension >= 2)
                    dimension = newDimension;
            }

            return dimension;
        }
        private bool IsValidDimension(string GridDimension)
        {
            if (GridDimension == String.Empty)
                return true;

            var regex = GetRegex();

            if (regex.IsMatch(GridDimension))
                return true;
            else
                return false;
        }
        private void ChangeText(TextBox textBox)
        {
            textBox.Text = StartupValues.MaxDimension.ToString();
        }
        private void ChangeDimensionText(object sender, EventArgs args)
        {
            TxtNumColumns.Text = AStarValues.NumGridColumns.ToString();
            TxtNumRows.Text = AStarValues.NumGridRows.ToString();
        }
        private Regex GetRegex()
        {
            int maxDimension = StartupValues.MaxDimension;
            char maxDimensionNumber = maxDimension.ToString()[0];

            var regex = new Regex("(^[1-9]$|^[1-" + maxDimensionNumber + "][0-9]{0,1}$)");

            return regex;
        }
    }
}
