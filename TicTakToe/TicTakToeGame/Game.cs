using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using TicTakToe.Properties;

namespace TicTakToe.TicTakToeGame
{
    public class Game : INotifyPropertyChanged
    {

        private Board _gameBoard;
        private string _message;

        public Board GameBoard
        {
            get => _gameBoard;
            set
            {
                _gameBoard = value;
                RaisePropertyChanged(nameof(GameBoard));
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                RaisePropertyChanged(nameof(Message));
            }
        }

        public bool GameOver => _gameOver;

        private bool _gameOver;
        private object timer1;

        public const int No = 0;

        public Game()
        {           
            _gameOver = false;
            GameBoard = new Board();
            Message = "Game is started!";
        }

        public void MakeTurn(int i, int j)
        {
            if (GameOver)
                return;

            if (GameBoard.BoardState[i, j] != CellState.Free)
            {
                //Todo: Write some message to the user that move is incorrect

                var randomGenerator = new Random();

                var index = randomGenerator.Next(7);

                string[] MessengNo = new string[10];
                MessengNo[0] = "No";
                MessengNo[1] = "Sorry bat no";
                MessengNo[2] = "You can't";
                MessengNo[3] = "So, you Seriously?";
                MessengNo[4] = "You can see. Tnis not free";
                MessengNo[5] = "later";
                MessengNo[6] = "Sorry";
                MessengNo[7] = "NO NO NO NO";
                MessengNo[8] = "I think it bad idea";
                MessengNo[9] = "...";

                Message = MessengNo[index];

                var timer = new Timer((_) =>
                {
                    Message = "Next turn";
                }, null, 2000, Timeout.Infinite);
                return;

            }

            var newState = GetNextState();
            GameBoard.BoardState[i, j] = newState;
                
            if (GetIsGameOver() != GameState.InProgress)
            {
                Message = "So Game Over";

                _gameOver = true;

                return;
            }

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

        public GameState GetIsGameOver()

        {
            var xCount = 0;
            var oCount = 0;

            for (var i = 0; i <= 2; i++)
            {
                xCount = 0;
                oCount = 0;
                for (var j = 0; j <= 2; j++)
                {
                    if (GameBoard.BoardState[i, j] == CellState.X)
                        xCount++;
                    if (GameBoard.BoardState[i, j] == CellState.O)
                        oCount++;
                }
                if (xCount == 3)
                    return GameState.XWon;
                if (oCount == 3)
                    return GameState.OWon;

            }
            for (var j = 0; j <= 2; j++)
            {
                xCount = 0;
                oCount = 0;
                for (var i = 0; i <= 2; i++)
                {
                    if (GameBoard.BoardState[i, j] == CellState.X)
                        xCount++;
                    if (GameBoard.BoardState[i, j] == CellState.O)
                        oCount++;
                }
                if (xCount == 3)
                    return GameState.XWon;
                if (oCount == 3)
                    return GameState.OWon;

            }

            xCount = 0;
            oCount = 0;
            for (var j = 0; j <= 2; j++)
            {
                if (GameBoard.BoardState[j, j] == CellState.X)
                    xCount++;
                if (GameBoard.BoardState[j, j] == CellState.O)
                    oCount++;
                if (xCount == 3)
                    return GameState.XWon;
                if (oCount == 3)
                    return GameState.OWon;
            }

            xCount = 0;
            oCount = 0;

            for (var j = 0; j <= 2; j++)
            {
                if (GameBoard.BoardState[2 - j, j] == CellState.X)
                    xCount++;
                if (GameBoard.BoardState[2 - j, j] == CellState.O)
                    oCount++;
                if (xCount == 3)
                    return GameState.XWon;
                if (oCount == 3)
                    return GameState.OWon;
            }

            for (var i = 0; i <= 2; i++)
                for (var j = 0; j <= 2; j++)
                {
                    {
                        if (GameBoard.BoardState[i, j] == CellState.Free)
                           return GameState.InProgress;
                    }

                }
            return GameState.Draw;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
