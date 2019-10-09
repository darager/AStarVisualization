using System.ComponentModel;

namespace AStarVisualization.WPF.Cleanup.ViewModels
{
    public class TestViewModel : INotifyPropertyChanged
    {
        private string _customText;
        public string CustomText
        {
            get { return _customText; }
            set
            {
                _customText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CustomText"));
            }
        }

        public TestViewModel()
        {
            _customText = "this is so sad that I have to do this kind of stuff all the time in order to be more productive in oder";
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
