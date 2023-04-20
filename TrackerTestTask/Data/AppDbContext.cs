using Microsoft.EntityFrameworkCore;
using TrackerTestTask.Models;

namespace TrackerTestTask.Data
{
    public class AppDbContext : DbContext
    {        

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TrackLocations> TrackLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrackLocations>().Property(x => x.Latitude).HasColumnType("decimal(12,9)");
            modelBuilder.Entity<TrackLocations>().Property(x => x.Longitude).HasColumnType("decimal(12,9)");
        }
    }
}
