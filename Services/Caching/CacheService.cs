using StackExchange.Redis;
using TabulaRasa.Server.Domain;

namespace TabulaRasa.Server.Services.Caching
{
    public class CacheService : ICacheService
    {
        private readonly ConnectionMultiplexer _client;

        public CacheService()
        {
            _client = ConnectionMultiplexer.Connect("localhost");
            var db = _client.GetDatabase();
            db.SetAdd("Commands", new RedisValue)
        }
    }
}