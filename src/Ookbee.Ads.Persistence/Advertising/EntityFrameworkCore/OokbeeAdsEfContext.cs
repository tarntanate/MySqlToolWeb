using Microsoft.EntityFrameworkCore;
using Ookbee.Ads.Domain.Advertising.Entity;
using Ookbee.Ads.Infrastructure;

namespace Ookbee.Ads.Persistence.Advertising.EntityFrameworkCore
{
    public class OokbeeAdsEfContext : DbContext
    {
        public OokbeeAdsEfContext(DbContextOptions<OokbeeAdsEfContext> options) : base(options)
        {

        }

        public DbSet<Campaign> Campaign { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(GlobalVar.AppSettings.ConnectionStrings.PostgreSQL);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OokbeeAdsEfContext).Assembly);
        }
    }
}
