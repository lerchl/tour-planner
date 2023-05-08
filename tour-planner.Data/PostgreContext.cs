using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TourPlanner.Model;

namespace TourPlanner.Data {

    /// <summary>
    ///     <see cref="DbContext"/> for the Postgre database supplied in appsettings.json.
    /// </summary>
    public class PostgreContext : DbContext {

        private static readonly IConfiguration CONFIG = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourLog> TourLogs { get; set; }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseNpgsql(CONFIG.GetConnectionString("PostgreContext"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Tour>()
                        .Property(t => t.TransportType)
                        .HasConversion(new TransportTypeConverter()!);
        }
    }
}
