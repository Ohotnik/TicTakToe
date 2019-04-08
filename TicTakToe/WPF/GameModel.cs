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
        private Timer _timer;

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
                _timer = new Timer((_) =>
                {
                    var winner = _game.GetGameState();

                    if (winner == GameState.XWon)
                    {
                        RequestAction?.Invoke(SetAction.Player1Won);
                    }
                    else if (winner == GameState.OWon)
                    {
                        RequestAction?.Invoke(SetAction.Player2Won);
                    }
                    else if (winner == GameState.Draw)
                    {
                        RequestAction?.Invoke(SetAction.Draw);
                    }
                    else
                    {
                        throw new ArgumentException($"State {winner} is not hanlded");
                    }
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
