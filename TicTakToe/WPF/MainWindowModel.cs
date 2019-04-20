using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Media;
using System.Runtime.CompilerServices;
using System.Windows;
using TicTakToe.TicTakToeGame;

namespace TicTakToe.WPF
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        private IPageWithStatus _currentControl;
        private TurnirStatus _turnirStatus;

        private Dictionary<SetAction, Action> _setActions = new Dictionary<SetAction, Action>();
        private SoundPlayer _player;

        public MainWindowModel()
        {
            _turnirStatus = new TurnirStatus();
            _setActions[SetAction.Reset] = () => _turnirStatus.Reset();
            _setActions[SetAction.MainMenu] = () => { CurrentControl = new MainMenuModel(); };
            _setActions[SetAction.NewGame] = () => { CurrentControl = new GameModel(); };
            _setActions[SetAction.Draw] = () => HandleGameOver(GameState.Draw);
            _setActions[SetAction.Player1Won] = () => HandleGameOver(GameState.XWon);
            _setActions[SetAction.Player2Won] = () => HandleGameOver(GameState.OWon);
            CurrentControl = new MainMenuModel();

            var sri = Application.GetResourceStream(new Uri(@"pack://application:,,,/Sound/Music.wav"));

            if ((sri != null))
            {
                using (var s = sri.Stream)
                {
                    _player = new System.Media.SoundPlayer(s);
                    _player.Load();
                    _player.PlayLooping();
                }
            }
        }

        private void HandleGameOver (GameState gameResult)
        {
            _turnirStatus.RegisterGameResult(gameResult);
            //ToDo: start new game here if we already won 3 times :) 
            //you can get it from TurnirStatus (_turnirStatus).
            //Він сам по собі не з'виться - треба зробити проерті (property) який буде повертати внутрішній стан (_currentStatus) 
            //назовні, абе нехай (краще) сам тернір можна спитати чин е завершений він. і якщо не завершений то треба створити нову гру, 
            // а не виставити в головлне меню. Дивися як працює NewGame :)
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
