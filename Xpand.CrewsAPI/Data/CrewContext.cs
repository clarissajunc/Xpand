using Microsoft.EntityFrameworkCore;
using Xpand.CrewsAPI.Data.Models;

namespace Xpand.CrewsAPI.Data
{
    public class CrewContext : DbContext
    {
        public DbSet<Crew> Crews { get; set; }

        public DbSet<Human> Humans { get; set; }

        public DbSet<Robot> Robots { get; set; }

        public CrewContext(DbContextOptions<CrewContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crew>(entity =>
            {
                entity.Property(c => c.Name)
                    .IsRequired();

                entity.HasOne(c => c.Captain)
                      .WithOne(c => c.Crew)
                      .HasForeignKey<Crew>(c => c.CaptainId)
                      .IsRequired(true);
            });

            modelBuilder.Entity<Robot>(entity =>
            {
                entity.Property(r => r.Name)
                    .IsRequired();

                entity.HasOne(r => r.Crew)
                      .WithMany(c => c.Robots)
                      .HasForeignKey(r => r.CrewId)
                      .IsRequired(true);
            });

            modelBuilder.Entity<Human>(entity =>
            {
                entity.Property(h => h.Name)
                    .IsRequired();
            });

            DbInitializer.Seed(modelBuilder);
        }
    }
}
