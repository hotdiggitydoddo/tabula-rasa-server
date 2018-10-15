namespace TabulaRasa.Server.Services
{
    public interface IGameService
    {
        void ReceiveInput(string connectionId, string input);
    }
}