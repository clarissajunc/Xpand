using Microsoft.EntityFrameworkCore;
using Xpand.PlanetsAPI.Models;

namespace Xpand.PlanetsAPI.Data
{
    public class PlanetContext : DbContext
    {
        public DbSet<Captain> Captains { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Planet> Planets { get; set; }

        public PlanetContext(DbContextOptions<PlanetContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Captain>();
            modelBuilder.Entity<Image>();
            modelBuilder.Entity<Planet>(entity =>
            {
                entity.HasOne(p => p.Image)
                      .WithOne(i => i.Planet)
                      .HasForeignKey<Planet>(p => p.ImageId)
                      .IsRequired(true);

                entity.HasOne(p => p.DescriptionAuthor)
                      .WithMany(c => c.Planets)
                      .HasForeignKey(p => p.DescriptionAuthorId)
                      .IsRequired(false);
            });

            DbInitializer.Seed(modelBuilder);
        }
    }
}
