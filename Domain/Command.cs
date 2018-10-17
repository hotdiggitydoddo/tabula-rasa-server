namespace TabulaRasa.Server.Domain
{
    public class Command : EntityBase
    {
        public int EntityId { get; set; }
        public string Name { get; set; }
    }
}