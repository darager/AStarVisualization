using System.Windows;

namespace AStarVisualization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeAStarVisualization();
        }

        private void InitializeAStarVisualization()
        {
            var AStarVisualizer = new AStarVisualizer.AStarVisualizer(this);
        }
    }
}