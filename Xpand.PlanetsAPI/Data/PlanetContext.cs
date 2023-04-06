using Microsoft.EntityFrameworkCore;
using Xpand.PlanetsAPI.Data.Models;

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
                      .WithMany()
                      .HasForeignKey(p => p.DescriptionAuthorId)
                      .IsRequired(false);

                entity.HasOne(p => p.Crew)
                      .WithOne()
                      .HasForeignKey<Planet>(p => p.CrewId);
            });

            DbInitializer.Seed(modelBuilder);
        }
    }
}
