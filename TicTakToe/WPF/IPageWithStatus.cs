using System;
using System.ComponentModel;

namespace TicTakToe.WPF
{
    public interface IPageWithStatus : INotifyPropertyChanged
    {
        event Action<SetAction> RequestAction;
        string StatusMessage { get; }
    }
}
