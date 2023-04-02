using Microsoft.EntityFrameworkCore;
using Xpand.CrewsAPI.Models;

namespace Xpand.CrewsAPI.Data
{
    public static class DbInitializer
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Human>().HasData(
               new Human { Id = 1, Name = "Jonathan Smith" },
               new Human { Id = 2, Name = "Hannah Intellis" },
               new Human { Id = 3, Name = "Eva Brains" },
               new Human { Id = 4, Name = "Rick Anderson" }
           );

            modelBuilder.Entity<Crew>().HasData(
                new Crew { Id = 1, Name = "Crew1", CaptainId = 1 },
                new Crew { Id = 2, Name = "Crew1", CaptainId = 2 },
                new Crew { Id = 3, Name = "Crew1", CaptainId = 3 },
                new Crew { Id = 4, Name = "Crew1", CaptainId = 4 }
            );

            modelBuilder.Entity<Robot>().HasData(
               new Robot { Id = 1, Name = "T2011", CrewId = 1 },
               new Robot { Id = 2, Name = "T2020", CrewId = 1 },
               new Robot { Id = 3, Name = "T2031", CrewId = 1 },
               new Robot { Id = 4, Name = "T21", CrewId = 2 },
               new Robot { Id = 5, Name = "T28", CrewId = 2 },
               new Robot { Id = 6, Name = "T29", CrewId = 2 },
               new Robot { Id = 7, Name = "T201", CrewId = 3 },
               new Robot { Id = 8, Name = "T18", CrewId = 4 },
               new Robot { Id = 9, Name = "T19", CrewId = 4 },
               new Robot { Id = 10, Name = "T31", CrewId = 4 }
            );
        }
    }
}
