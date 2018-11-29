namespace TicTakToe.TicTakToeGame
{
    public class Board
    {
        public CellState[,] BoardState;

        public Board()
        {
            BoardState = new CellState[3, 3];
            for (var i = 0; i <= 2; i++)
            for (var j = 0; j <= 2; j++)
            {
                BoardState[i, j] = CellState.Free;
            }
        }
    }
}
