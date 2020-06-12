namespace Ookbee.Ads.Infrastructure.Models
{
    public class ConnectionStrings
    {
        public StorageSettings MongoDB { get; set; }
        public StorageSettings PostgreSQL { get; set; }
        public string Redis { get; set; }
    }
}
