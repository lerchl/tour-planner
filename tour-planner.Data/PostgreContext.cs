using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TourPlanner.Model;

namespace TourPlanner.Data {
    
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
            Console.WriteLine(CONFIG.GetConnectionString("PostgreContext"));
            optionsBuilder.UseNpgsql(CONFIG.GetConnectionString("PostgreContext"), x => x.UseNetTopologySuite());
        }
    }
}
