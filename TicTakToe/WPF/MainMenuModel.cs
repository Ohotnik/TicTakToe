using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TicTakToe.WPF
{
    public class MainMenuModel : IPageWithStatus
    {
        private RelayCommand _startNewGame;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event Action<SetAction> RequestAction;

        public string StatusMessage
        {
            get { return "Select action"; }
        }

        public RelayCommand StartGame
        {
            get
            {
                _startNewGame = _startNewGame ?? new RelayCommand(
                              () =>
                              {
                                  RequestAction?.Invoke(SetAction.NewGame);
                              });
                return _startNewGame;
            }
        }
    }
}