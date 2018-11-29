using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TicTakToe.Properties;
using TicTakToe.TicTakToeGame;

namespace TicTakToe
{
    public class TicTacToeModel : INotifyPropertyChanged
    {
        private RelayCommand<string> _move00;
        private Game _game;

        public TicTacToeModel()
        {
            _game = new Game();
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
