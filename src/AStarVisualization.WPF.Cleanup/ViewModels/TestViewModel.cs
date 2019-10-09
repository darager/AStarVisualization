using System.ComponentModel;

// TODO: remove this
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
                OnPropertyChanged("CustomText");
            }
        }

        public TestViewModel()
        {
            _customText = "this is so sad that I have to do this kind of stuff all the time in order to be more productive in oder";
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
