using AStarVisualization.Core;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AStarVisualization.WPF.Controls
{
    /// <summary>
    /// Interaction logic for AStarCanvas.xaml
    /// </summary>
    public partial class AStarCanvas : UserControl
    {
        public static readonly DependencyProperty Map =
            DependencyProperty.Register(
                "Map", typeof(Node[,]), typeof(AStarCanvas),
            new FrameworkPropertyMetadata(
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty Path =
            DependencyProperty.Register(
                "Path", typeof(List<Node>), typeof(AStarCanvas),
            new FrameworkPropertyMetadata(
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public AStarCanvas()
        {
            InitializeComponent();
        }
    }
}
