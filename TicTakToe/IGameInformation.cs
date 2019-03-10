namespace TicTakToe
{
    public interface IGameInformationReadOnly
    {
        string Player1Name { get; }
    }

    public interface IGameInformation : IGameInformationReadOnly
    {
        void SetPlayer1Name(string newName);
    }
}
