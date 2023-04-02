using Microsoft.EntityFrameworkCore;
using Xpand.CrewsAPI.Models;

namespace Xpand.CrewsAPI.Data
{
    public class CrewContext : DbContext
    {
        DbSet<Crew> Crews { get; set; }

        DbSet<Human> Humans { get; set; }

        DbSet<Robot> Robots { get; set; }

        public CrewContext(DbContextOptions<CrewContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crew>(entity =>
            {
                entity.HasOne(c => c.Captain)
                .WithOne(c => c.Crew)
                .HasForeignKey<Crew>(c => c.CaptainId)
                .IsRequired(true);
            });

            modelBuilder.Entity<Robot>(entity =>
            {
                entity.HasOne(r => r.Crew)
                .WithMany(c => c.Robots)
                .HasForeignKey(r => r.CrewId)
                .IsRequired(true);
            });

            modelBuilder.Entity<Human>();

            DbInitializer.Seed(modelBuilder);
        }
    }
}
