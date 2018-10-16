using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TabulaRasa.Server.Domain;

namespace TabulaRasa.Server.Services
{
    public class GameService : IHostedService, IDisposable, IGameService
    {
        private readonly ILogger _logger;
        private readonly IConnectionService _connectionService;
        private Timer _timer;


        public GameService(ILogger<GameService> logger, IConnectionService connectionService)
        {
            _logger = logger;
            _connectionService = connectionService;
            _connectionService.ClientConnected += async (s, e) => await OnConnectedAsync(s, e);
            _connectionService.ClientDisconnected += async (s, e) => await OnDisconnectedAsync(s, e);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
            _connectionService.ClientConnected -= async (s, e) => await OnConnectedAsync(s, e);
            _connectionService.ClientDisconnected -= async (s, e) => await OnDisconnectedAsync(s, e);
        }


        private void DoWork(object state)
        {
            _logger.LogInformation("Timed Background Service is working.");
        }
        
        private async Task OnConnectedAsync(object sender, ConnectEventArgs eventArgs)
        {
            await _connectionService.SendOutputAsync(eventArgs.ConnectionId, $"Hello, {eventArgs.ConnectionId}, from GAME SERVICE!");
        }

        private async Task OnDisconnectedAsync(object sender, ConnectEventArgs eventArgs)
        {
            await _connectionService.SendOutputAsync(eventArgs.ConnectionId, $"Goodbye, {eventArgs.ConnectionId}, from GAME SERVICE!");
        }
    }
}