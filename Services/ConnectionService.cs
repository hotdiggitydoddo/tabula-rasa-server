using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TabulaRasa.Server.Domain;
using TabulaRasa.Server.Hubs;

namespace TabulaRasa.Server.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly ConcurrentDictionary<string, Connection> _connections;
        private readonly ILogger _logger;
        private readonly IHubContext<GameHub> _gameHub;
        //TODO: Add support for telnet connections here


        public ConnectionService(ILogger<ConnectionService> logger, IHubContext<GameHub> gameHub)
        {
            _logger = logger;
            _gameHub = gameHub;
            _connections = new ConcurrentDictionary<string, Connection>();
        }

        public event EventHandler<ConnectEventArgs> ClientConnected;
        public event EventHandler<ConnectEventArgs> ClientDisconnected;

        public bool Connect(string connectionId, ConnectionType type)
        {
            ClientConnected?.Invoke(this, new ConnectEventArgs(connectionId));
            return _connections.TryAdd(connectionId, new Connection(type));
        }

        public void Disconnect(string connectionId)
        {
            ClientDisconnected?.Invoke(this, new ConnectEventArgs(connectionId));
            _connections.Remove(connectionId, out var connection);
        }

        public IEnumerable<Connection> GetConnections()
        {
            return _connections.Values;
        }

        public void SendOutput(string connectionId, string output)
        {
            _gameHub.Clients.Client(connectionId).SendAsync("receiveMessage", output).GetAwaiter().GetResult();
        }
    }
}