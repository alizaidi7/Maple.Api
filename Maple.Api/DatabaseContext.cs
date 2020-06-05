using Maple.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Maple.Api
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
          : base(options)
        {

        }

        public virtual DbSet<CoveragePlanModel> CoveragePlan { get; set; }
        public virtual DbSet<RateChartModel> RateChart { get; set; }
        public virtual DbSet<ContractsModel> Contracts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CoveragePlanModel>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<ContractsModel>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<RateChartModel>(entity =>
            {
                entity.HasNoKey();
            });
        }
    }
}
