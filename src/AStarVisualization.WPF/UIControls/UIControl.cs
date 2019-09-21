using System.Windows;
using System.Reflection;
using System.Collections.Generic;

namespace AStarVisualization.WPF.UIElements
{
    public class UIControl
    {
        public Dictionary<string, UIElement> AStarControls;

        private List<string> uiElements = new List<string>()
        {
            "DrawingCanvas",
            "TxtNumColumns",
            "TxtNumRows",
            "CmdStart",
            "CmdReset",
            "CmdPause",
            "NumDelay",
            "LblDelay",
            "DiagonalPathCheckbox",
            "CmdSetStartTile",
            "CmdSetWallTile",
            "CmdSetGoalTile",
            "CmdClearTiles",
        };

        public UIControl(Window window)
        {
            foreach (var elementName in uiElements)
            {
                var element = (UIElement)window.FindName(elementName);
                AStarControls.Add(elementName, element);
            }
        }
    }
}
