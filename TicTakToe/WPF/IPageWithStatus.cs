using System.ComponentModel;

namespace TicTakToe.WPF
{
    public interface IPageWithStatus : INotifyPropertyChanged
    {
        string StatusMessage { get; set; }
    }
}
