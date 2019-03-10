using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TicTakToe.WPF
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        private IPageWithStatus _currentControl;
        private SetStatus _setStatus;

        private Dictionary<SetAction, Action> _setActions = new Dictionary<SetAction, Action>();

        public MainWindowModel()
        {
            _setStatus = new SetStatus();
            _setActions[SetAction.Reset] = () => _setStatus.Reset();
            _setActions[SetAction.MainMenu] = () => {CurrentControl = new MainMenuModel();};
            _setActions[SetAction.NewGame] = () => { CurrentControl = new GameModel(); };
            CurrentControl = new MainMenuModel();
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
                if (_currentControl != null)
                {
                    _currentControl.PropertyChanged -= HandleControlPropertyChanged;
                    _currentControl.RequestAction -= HandleRequestedAction;
                }

                _currentControl = value;
                _currentControl.PropertyChanged += HandleControlPropertyChanged;
                _currentControl.RequestAction += HandleRequestedAction;

                HandleControlPropertyChanged(this, null);
                NotifyPropertyChanged(nameof(CurrentControl));
            }
        }

        private void HandleRequestedAction(SetAction requestedAction)
        {
           if (!_setActions.ContainsKey(requestedAction))
               throw new NotImplementedException($"Action {requestedAction} is not supported");
           _setActions[requestedAction].Invoke();
        }

        public string StatusMessage => _currentControl.StatusMessage;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
