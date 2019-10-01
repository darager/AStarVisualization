using System.Windows;

namespace AStarVisualization.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var astarVisualization = new AStarVisualizer(this);
        }
    }
}