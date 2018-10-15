namespace TabulaRasa.Server.Domain
{
    public enum ConnectionType
    {
        WebSocket,
        Telnet
    }

    public class Connection
    {
        public ConnectionType Type { get; private set; }

        public Connection(ConnectionType type) => Type = type;
    }
}