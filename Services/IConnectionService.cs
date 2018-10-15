using System.Collections.Generic;
using TabulaRasa.Server.Domain;

namespace TabulaRasa.Server.Services
{
    public interface IConnectionService
    {
        bool CreateConnection(string connectionId, ConnectionType type);
        void DeleteConnection(string connectionId);
        IEnumerable<Connection> GetConnections();
        void SendOutput(string connectionId, string output);
    }
}