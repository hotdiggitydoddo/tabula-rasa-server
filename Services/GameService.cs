using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TabulaRasa.Server.Domain;
using TabulaRasa.Server.Services.Caching;

namespace TabulaRasa.Server.Services
{
    public class GameService : IHostedService, IDisposable, IGameService
    {
        private readonly ILogger _logger;
        private readonly IConnectionService _connectionService;
        private readonly IScriptService _scriptService;
        private readonly ICacheManager _cacheManager;
        private Timer _timer;

        public GameService(ILogger<GameService> logger, IConnectionService connectionService, 
            IScriptService scriptService, ICacheManager cacheManager)
        {
            _logger = logger;
            _connectionService = connectionService;
            _connectionService.ClientConnected += async (s, e) => await OnConnectedAsync(s, e);
            _connectionService.ClientDisconnected += async (s, e) => await OnDisconnectedAsync(s, e);
            _scriptService = scriptService;
            _cacheManager = cacheManager;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(1));


            _cacheManager.Set("Commands", new List<Command>() { new Command { Id = 1, Name = "Talk"}});

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
                _logger.LogInformation(_cacheManager.Get<List<Command>>("Commands").First().Name);
        }
        
        private async Task OnConnectedAsync(object sender, ConnectEventArgs args)
        {
            var script = _scriptService.GetScript(ScriptType.Game, "OnConnected");
            //var connectScript = 

        }

        private async Task OnDisconnectedAsync(object sender, ConnectEventArgs args)
        {
            await _connectionService.SendOutputAsync(args.ConnectionId, $"Goodbye, {args.ConnectionId}, from GAME SERVICE!");
        }

        private async Task OnInputReceivedAsync(object sender, InputEventArgs args)
        {

        }
    }
}