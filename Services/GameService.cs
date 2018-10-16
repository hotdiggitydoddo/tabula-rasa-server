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
            _connectionService.ClientConnected += OnConnected;
            _connectionService.ClientDisconnected += OnDisconnected;
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
            _connectionService.ClientConnected -= OnConnected;
            _connectionService.ClientDisconnected -= OnDisconnected;
        }


        private void DoWork(object state)
        {
            _logger.LogInformation("Timed Background Service is working.");
        }
        private void OnConnected(object sender, ConnectEventArgs eventArgs)
        {
            _connectionService.SendOutput(eventArgs.ConnectionId, $"Hello, {eventArgs.ConnectionId}, from GAME SERVICE!");
        }

        private void OnDisconnected(object sender, ConnectEventArgs eventArgs)
        {
            _connectionService.SendOutput(eventArgs.ConnectionId, $"Goodbye, {eventArgs.ConnectionId}, from GAME SERVICE!");
        }
    }
}