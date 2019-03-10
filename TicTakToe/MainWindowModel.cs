using System.ComponentModel;
using System.Runtime.CompilerServices;
using TicTakToe.Properties;
using TicTakToe.WPF;

namespace TicTakToe
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        private IPageWithStatus _currentControl;

        public MainWindowModel()
        {
            CurrentControl = new GameModel();
            CurrentControl.PropertyChanged += HandleControlPropertyChanged;
        }

        private void HandleControlPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e == null || e.PropertyName.ToLowerInvariant().Contains(nameof(StatusMessage).ToLowerInvariant()))
            {
                NotifyPropertyChanged(nameof(StatusMessage));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public IPageWithStatus CurrentControl
        {
            get => _currentControl;
            set
            {
                _currentControl = value;
                NotifyPropertyChanged(nameof(CurrentControl));
            }
        }

        public string StatusMessage => _currentControl.StatusMessage;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
