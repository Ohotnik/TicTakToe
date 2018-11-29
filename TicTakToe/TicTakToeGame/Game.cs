using System.ComponentModel;
using System.Runtime.CompilerServices;
using TicTakToe.Properties;

namespace TicTakToe.TicTakToeGame
{
    public class Game : INotifyPropertyChanged
    {
        private Board _gameBoard;

        public Board GameBoard
        {
            get => _gameBoard;
            set
            {
                _gameBoard = value;
                RaisePropertyChanged(nameof(GameBoard));
            }
        }

        public Game()
        {
            GameBoard = new Board();
        }

        public void MakeTurn(int i, int j)
        {
            if (GameBoard.BoardState[i, j] != CellState.Free )
            {
                throw new InvalidEnumArgumentException();
            }

            var newState = GetNextState();
            GameBoard.BoardState[i, j] = newState;
        }

        private CellState GetNextState()
        {
            var xCount = 0;
            var oCount = 0;
            for (var i = 0; i <= 2; i++)
            for (var j = 0; j <= 2; j++)
            {
                if (GameBoard.BoardState[i, j] == CellState.X)
                    xCount++;
                if (GameBoard.BoardState[i, j] == CellState.O)
                    oCount++;
            }

            return (xCount == oCount) ? CellState.X : CellState.O;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
