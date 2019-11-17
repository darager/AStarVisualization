using System.Windows;
using PathFindingVisualization.WPF.ViewModels;

namespace PathFindingVisualization.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel mainViewModel, AlgorithmControlViewModel algorithmControlViewModel)
        {
            InitializeComponent();

            this.DataContext = mainViewModel;
            AlgorithmControls.DataContext = algorithmControlViewModel;
        }
    }
}
