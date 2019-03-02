using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TicTakToe.TicTakToeGame;

namespace TicTakToe.Converters
{
    /// <summary>
    /// double non-equality to visibility converter.
    /// </summary>
    /// <author>MOP-121024</author>
    [ValueConversion(typeof(Game), typeof(string))]
    public class GameCellToStringConverter : IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var realValue = value as Game;

            if (parameter == null || realValue == null)
                return "";

            var cell = int.Parse((string)parameter);

            int i = cell / 3;
            int j = cell % 3;

            if (realValue.GameBoard.BoardState[i, j] == CellState.X)
            {
                return "X";
            }

            if (realValue.GameBoard.BoardState[i, j] == CellState.O)
            {
                return "O";
            }

            return "";
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
