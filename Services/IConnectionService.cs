using System;
using System.Collections.Generic;
using TabulaRasa.Server.Domain;

namespace TabulaRasa.Server.Services
{
    public interface IConnectionService
    {
        bool Connect(string connectionId, ConnectionType type);
        void Disconnect(string connectionId);
        IEnumerable<Connection> GetConnections();
        void SendOutput(string connectionId, string output);

        event EventHandler<ConnectEventArgs> ClientConnected;
        event EventHandler<ConnectEventArgs> ClientDisconnected;
    }
}