using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TabulaRasa.Server.Domain;

namespace TabulaRasa.Server.Services
{
    public interface IConnectionService
    {
        bool Connect(string connectionId, ConnectionType type);
        void Disconnect(string connectionId);
        IEnumerable<Connection> GetConnections();
        Task SendOutputAsync(string connectionId, string output);
        Task SendOutputAsync(IEnumerable<string> connectionIds, string output);
        Task BroadcastAsync(string output);

        event EventHandler<ConnectEventArgs> ClientConnected;
        event EventHandler<ConnectEventArgs> ClientDisconnected;
    }
}