using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TabulaRasa.Server.Domain;
using TabulaRasa.Server.Services;

namespace TabulaRasa.Server.Hubs
{
     public class GameHub : Hub
    {
        private readonly IConnectionService _connectionService;
        public GameHub(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }
        public async Task NewMessage(string username, string message)
        {
            await Clients.All.SendAsync("receiveMessage", username, "_connectionService.Count");
        }

        public override Task OnConnectedAsync()
        {
            _connectionService.Connect(Context.ConnectionId, ConnectionType.WebSocket);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _connectionService.Disconnect(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}