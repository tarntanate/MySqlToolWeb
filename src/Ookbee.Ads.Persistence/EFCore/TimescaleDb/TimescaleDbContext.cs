using Microsoft.EntityFrameworkCore;
using Ookbee.Ads.Infrastructure;

namespace Ookbee.Ads.Persistence.EFCore.TimescaleDb
{
    public class TimescaleDbContext : DbContext
    {
        public TimescaleDbContext(DbContextOptions<TimescaleDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(GlobalVar.AppSettings.ConnectionStrings.TimescaleDb.Analytics);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfigurationsFromAssembly(typeof(TimescaleDbContext).Assembly);
            // foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            // {
            //     modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name.Replace("Entity", ""));
            // }
        }
    }
}
