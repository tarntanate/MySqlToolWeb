namespace Ookbee.Ads.Infrastructure.Models
{
    public class ConnectionStrings
    {
        public StorageSettings MongoDB { get; set; }
        public StorageSettings PostgreSQL { get; set; }
        public StorageSettings TimescaleDb { get; set; }
        public StorageSettings Redis { get; set; }
    }
}
