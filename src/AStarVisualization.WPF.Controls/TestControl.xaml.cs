using System.Windows;
using System.Windows.Controls;

namespace AStarVisualization.WPF.Controls
{
    /// <summary>
    /// Interaction logic for TestControl.xaml
    /// </summary>
    public partial class TestControl : UserControl
    {
        public static readonly DependencyProperty CustomTextProperty =
            DependencyProperty.Register(
                "CustomText", typeof(string), typeof(AStarCanvas),
            new PropertyMetadata(string.Empty));
        public string CustomText
        {
            get => (string)GetValue(CustomTextProperty);
            set => SetValue(CustomTextProperty, value);
        }
        public TestControl()
        {
            InitializeComponent();
        }
    }
}
