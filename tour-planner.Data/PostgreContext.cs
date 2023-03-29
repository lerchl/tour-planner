using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TourPlanner.Model;

namespace TourPlanner.Data {
    
    internal class PostgreContext : DbContext {

        private readonly IConfiguration _configuration;

        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourLog> TourLogs { get; set; }

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public PostgreContext(IConfiguration configuration) {
            _configuration = configuration;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreConnection") /*, sopt => sopt.UseNetTopologySuite() */);
        }
    }
}
