using System;

namespace TabulaRasa.Server.Domain
{
    public class InputEventArgs : EventArgs
    {
        public string ConnectionId { get; }
        public string Input { get; }

        public InputEventArgs(string connectionId, string input)
        {
            ConnectionId = connectionId;
            Input = input;
        }
    }
}