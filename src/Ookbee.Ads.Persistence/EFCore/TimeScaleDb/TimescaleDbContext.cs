using Microsoft.EntityFrameworkCore;
using Ookbee.Ads.Infrastructure;

namespace Ookbee.Ads.Persistence.EFCore.TimeScaleDb
{
    public class TimeScaleDbContext : DbContext
    {
        public TimeScaleDbContext(DbContextOptions<TimeScaleDbContext> options) : base(options)
        {
            // context.Configuration.AutoDetectChangesEnabled = false;
            // context.Configuration.ValidateOnSaveEnabled = false;
                    }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(GlobalVar.AppSettings.ConnectionStrings.TimescaleDb.Analytics);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TimeScaleDbContext).Assembly);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name.Replace("Entity", ""));
            }
        }
    }
}
