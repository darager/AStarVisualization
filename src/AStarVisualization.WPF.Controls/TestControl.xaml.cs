using System.Windows;
using System.Windows.Controls;

// TODO: remove this
namespace AStarVisualization.WPF.Controls
{
    public partial class TestControl : UserControl
    {
        public static readonly DependencyProperty CustomTextProperty =
            DependencyProperty.Register(
                "CustomText", typeof(string), typeof(TestControl),
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
