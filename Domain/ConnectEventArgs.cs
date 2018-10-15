using System;

namespace TabulaRasa.Server.Domain
{
    public class ConnectEventArgs : EventArgs
    {
        public string ConnectionId { get; }

        public ConnectEventArgs(string connectionId)
        {
            ConnectionId = connectionId;
        }
    }
}