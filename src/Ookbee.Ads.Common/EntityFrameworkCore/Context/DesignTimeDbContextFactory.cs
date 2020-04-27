using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace Ookbee.Ads.Common.EntityFrameworkCore.Context
{
    public abstract class DesignTimeDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        private string ConnectionString { get; }

        public DesignTimeDbContextFactory(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public TContext CreateDbContext(string[] args)
        {
            return Create();
        }

        private TContext Create()
        {
            if (string.IsNullOrEmpty(ConnectionString))
                throw new ArgumentException($"Connection string '{typeof(TContext).FullName}' is null or empty.", nameof(ConnectionString));
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            return CreateNewInstance(optionsBuilder.Options);
        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);
    }
}
