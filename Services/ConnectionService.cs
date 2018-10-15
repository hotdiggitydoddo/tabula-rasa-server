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
        private readonly ILogger _logger;
        private readonly IHubContext<GameHub> _gameHub;
        private readonly ConcurrentDictionary<string, Connection> _connections;


        public ConnectionService(ILogger<ConnectionService> logger, IHubContext<GameHub> gameHub)
        {
            _logger = logger;
            _gameHub = gameHub;
            _connections = new ConcurrentDictionary<string, Connection>();
        }

        public bool CreateConnection(string connectionId, ConnectionType type)
        {
            return _connections.TryAdd(connectionId, new Connection(type));
        }

        public void DeleteConnection(string connectionId)
        {
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