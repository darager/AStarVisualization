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
            InitializeAStarVisualization();
        }

        private void InitializeAStarVisualization()
        {
            var uiElements = new UIElements.UIControl(AStarcontrols, DrawingCanvas);
            var AStarVisualizer = new AStarVisualizer.AStarVisualizer(uiElements);
        }
        /*
                    _
                .__(.)<  (MEOW)
                 \___)
        */
    }
}
