using AStarVisualization.Core;
using System.Windows.Controls;

namespace AStarVisualization.WPF.Controls
{
    /// <summary>
    /// Interaction logic for AStarCanvas.xaml
    /// </summary>
    public partial class AStarCanvas : UserControl
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public Node[,] Nodes { get; set; }

        public AStarCanvas()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
