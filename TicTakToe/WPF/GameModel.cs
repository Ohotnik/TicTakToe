using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using TicTakToe.TicTakToeGame;

namespace TicTakToe.WPF
{
    public class GameModel : IPageWithStatus
    {
        private RelayCommand<string> _move00;
        private Game _game;
        private string _statusMessage;

        public Game Game => _game;

        public GameModel()
        {
            _game = new Game();
            _game.PropertyChanged += HandleGamePropertyChanged;
            HandleGamePropertyChanged(this, null);
        }

        private void HandleGamePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            StatusMessage = Game.Message;
        }

        public RelayCommand<string> Move
        {
            get
            {
                _move00 = _move00 ?? new RelayCommand<string>(
                              (s) =>
                              {
                                  var parsed = int.Parse(s);
                                  MoveOn(parsed / 3, parsed % 3);
                              });
                return _move00;
            }
        }

        private void MoveOn(int i, int j)
        {
            _game.MakeTurn(i, j);

            NotifyPropertyChanged(nameof(TicTakToeGame.Game));

            if (_game.GameOver)
            {
                var timer = new Timer((_) =>
                {
                    RequestAction?.Invoke(SetAction.GameOver);
                }, null, 5000, Timeout.Infinite);
            }
        }

        public event Action<SetAction> RequestAction;

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                if (_statusMessage != value)
                {
                    _statusMessage = value;
                    NotifyPropertyChanged(nameof(StatusMessage));
                }
            }
        }

        #region PropertyChangeImplementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
