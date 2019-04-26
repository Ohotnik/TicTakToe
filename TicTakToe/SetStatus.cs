using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TicTakToe.TicTakToeGame;

namespace TicTakToe
{
    public class TurnirStatus : IGameInformation, INotifyPropertyChanged
    {
        private Dictionary<GameState,int> _currentStatus = new Dictionary<GameState,int>();
        private string _player1Name;
        public string Player1Name => _player1Name;

        public void SetPlayer1Name(string newName)
        {
            _player1Name = newName;
            NotifyPropertyChanged(nameof(Player1Name));
        }

        public void Reset()
        {
            _currentStatus = new Dictionary<GameState, int>();
        }

        public void RegisterGameResult(GameState result)
        {
            if (_currentStatus.ContainsKey(result))
            {
                _currentStatus[result] = _currentStatus[result] + 1;
            }
            else
            {
                _currentStatus[result] = 1;
            }    
        }

        public GameState GetState()
        {
             
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
