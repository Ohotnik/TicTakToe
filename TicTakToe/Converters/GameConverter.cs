using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using TicTakToe.TicTakToeGame;
using WpfAnimatedGif;

namespace TicTakToe.Converters
{
    /// <summary>
    /// double non-equality to visibility converter.
    /// </summary>
    /// <author>MOP-121024</author>
    [ValueConversion(typeof(Game), typeof(Image))]
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
                return SignsImages.X();
            }

            if (realValue.GameBoard.BoardState[i, j] == CellState.O)
            {
                //ToDo: Зробити так, щоб тут повертався нолик :)
                return SignsImages.O();
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

    public static class SignsImages
    {

        public static Image X()
        {
            var signX = new Image();
            var img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri("/Images/SignX.gif", UriKind.Relative);
            img.EndInit();
            ImageBehavior.SetAnimatedSource(signX, img);
            return signX;
        }
        public static Image O()
        {
            var signO = new Image();
            var img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri("/Images/SignO.gif", UriKind.Relative);
            img.EndInit();
            ImageBehavior.SetAnimatedSource(signO, img);
            return signO;
        }
    }
}
