﻿namespace Ookbee.Ads.Infrastructure.Settings
{
    public class ConnectionStrings
    {
        public StorageSettings MongoDB { get; set; }
        public StorageSettings PostgreSQL { get; set; }
        public StorageSettings Redis { get; set; }
    }
}