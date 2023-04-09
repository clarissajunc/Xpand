using Microsoft.EntityFrameworkCore;
using Xpand.PlanetsAPI.Data.Models;
using Xpand.PlanetsAPI.Data.Models.Enums;

namespace Xpand.PlanetsAPI.Data
{
    public class PlanetContext : DbContext
    {
        public virtual DbSet<Captain> Captains { get; set; }

        public virtual DbSet<Image> Images { get; set; }

        public virtual DbSet<Planet> Planets { get; set; }

        public virtual DbSet<Description> Descriptions { get; set; }

        public PlanetContext(DbContextOptions<PlanetContext> options) : base(options)
        {
        }

        public PlanetContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Captain>(entity =>
            {
                entity.Property(c => c.Name)
                .IsRequired();
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(i => i.Bytes)
                    .IsRequired();
            });

            modelBuilder.Entity<Planet>(entity =>
            {
                entity.Property(p => p.Name)
                    .IsRequired();

                entity.HasOne(p => p.Image)
                      .WithOne(i => i.Planet)
                      .HasForeignKey<Planet>(p => p.ImageId)
                      .IsRequired();

                entity.HasOne(p => p.Description)
                      .WithOne()
                      .HasForeignKey<Planet>(p => p.DescriptionId)
                      .IsRequired(false);

                entity.HasOne(p => p.Crew)
                      .WithOne()
                      .HasForeignKey<Planet>(p => p.CrewId)
                      .IsRequired(false);

                entity.Property(p => p.Status)
                    .HasDefaultValue(PlanetStatus.Todo)
                    .IsRequired();
            });

            modelBuilder.Entity<Description>(entity =>
            {
                entity.Property(d => d.Text)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.HasOne(d => d.Author)
                    .WithMany()
                    .HasForeignKey(d => d.AuthorId)
                    .IsRequired();
            });

            DbInitializer.Seed(modelBuilder);
        }
    }
}
