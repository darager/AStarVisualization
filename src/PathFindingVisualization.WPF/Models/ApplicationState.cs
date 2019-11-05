using System.ComponentModel;
using System.Windows.Input;

namespace PathFindingVisualization.WPF.Models
{
    public class ApplicationState : INotifyPropertyChanged
    {
        public AppState State
        {
            get => _state;
            set
            {
                if (value == _state)
                    return;
                _state = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("State"));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private AppState _state = AppState.MapDesignPhase;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
