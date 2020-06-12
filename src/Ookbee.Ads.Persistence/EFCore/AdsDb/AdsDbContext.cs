using Microsoft.EntityFrameworkCore;
using Ookbee.Ads.Infrastructure;

namespace Ookbee.Ads.Persistence.EFCore.AdsDb
{
    public class AdsDbContext : DbContext
    {
        public AdsDbContext(DbContextOptions<AdsDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(GlobalVar.AppSettings.ConnectionStrings.PostgreSQL);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdsDbContext).Assembly);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name.Replace("Entity", ""));
            }
        }
    }
}
