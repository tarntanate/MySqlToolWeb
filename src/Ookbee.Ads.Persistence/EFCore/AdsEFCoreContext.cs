using Microsoft.EntityFrameworkCore;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Infrastructure;

namespace Ookbee.Ads.Persistence.EFCore
{
    public class AdsEFCoreContext : DbContext
    {
        public AdsEFCoreContext(DbContextOptions<AdsEFCoreContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(GlobalVar.AppSettings.ConnectionStrings.PostgreSQL);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdsEFCoreContext).Assembly);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name.Replace("Entity", ""));
            }
        }
    }
}
