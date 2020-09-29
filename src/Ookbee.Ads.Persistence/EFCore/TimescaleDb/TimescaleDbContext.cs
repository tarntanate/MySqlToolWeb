using Microsoft.EntityFrameworkCore;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Domain.Entities.ReportEntities;
// using Ookbee.Ads.Application.Business.Report.AdGroupReport;

namespace Ookbee.Ads.Persistence.EFCore.TimescaleDb
{
    public class TimescaleDbContext : DbContext
    {
        public DbSet<AdGroupReportEntity> AdGroupReports { get; set; }
        public DbSet<AdImpressionReportEntity> AdImpressionReports { get; set; }
        public DbSet<PlatformReportEntity> PlatformReports { get; set; }

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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TimescaleDbContext).Assembly);

            modelBuilder.Entity<AdGroupReportEntity>().HasNoKey();
            modelBuilder.Entity<AdImpressionReportEntity>().HasNoKey();
            modelBuilder.Entity<PlatformReportEntity>().HasNoKey();

            // foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            // {
            //     modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name.Replace("Entity", ""));
            // }
        }
    }
}
