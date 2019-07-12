using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace AStarVisualization.WPF.UIElements
{
    public class UIControl
    {
        public Dictionary<string, UIElement> AStarControls;

        public UIControl(Panel parent, Canvas drawingCanvas)
        {
            AStarControls = new Dictionary<string, UIElement>();

            AStarControls.Add(drawingCanvas.Name, drawingCanvas);
            List<Control> allControls = GetAllControls(parent);
            List<Control> controls = RemoveNoNameControls(allControls);

            foreach (Control ctrl in controls)
                AStarControls.Add(ctrl.Name, ctrl);
        }

        private List<Control> GetAllControls(Panel parent)
        {
            var result = new List<Control>();

            //// get all the children of a border
            //var borderChildren = parent

            // get all of the Controls from the Children that are also Panels
            var childPanels = parent.Children.OfType<Panel>();
            foreach (Panel p in childPanels)
                result.AddRange(GetAllControls(p));

            // add all of the children that are controls
            var childControls = parent.Children.OfType<Control>();
            foreach (Control child in childControls)
                result.Add(child);

            return result;
        }
        private List<Control> RemoveNoNameControls(List<Control> controls)
        {
            var result = new List<Control>();

            foreach (Control c in controls)
                if (c.Name.Length > 0) // if a control is not given a name the default name will be ""
                    result.Add(c);

            return result;
        }
    }
}
