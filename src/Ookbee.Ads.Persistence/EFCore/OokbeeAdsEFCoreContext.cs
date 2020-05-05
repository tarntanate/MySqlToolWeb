using Microsoft.EntityFrameworkCore;
using Ookbee.Ads.Domain.EFCore;
using Ookbee.Ads.Infrastructure;

namespace Ookbee.Ads.Persistence.EFCore
{
    public class OokbeeAdsEFCoreContext : DbContext
    {
        public OokbeeAdsEFCoreContext(DbContextOptions<OokbeeAdsEFCoreContext> options) : base(options)
        {

        }

        public DbSet<CampaignEntity> Campaign { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(GlobalVar.AppSettings.ConnectionStrings.PostgreSQL);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OokbeeAdsEFCoreContext).Assembly);
        }
    }
}
