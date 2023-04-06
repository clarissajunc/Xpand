using Microsoft.EntityFrameworkCore;
using Xpand.PlanetsAPI.Models;

namespace Xpand.PlanetsAPI.Data
{
    public class DbInitializer
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>().HasData(
                new Image { Id = 1, Bytes = ReadFile("images/planet4.png") },
                new Image { Id = 2, Bytes = ReadFile("images/planet5.png") },
                new Image { Id = 3, Bytes = ReadFile("images/planet6.png") },
                new Image { Id = 4, Bytes = ReadFile("images/planet7.png") },
                new Image { Id = 5, Bytes = ReadFile("images/planet8.png") }
            );

            modelBuilder.Entity<Captain>().HasData(
                new Captain { Id = 1, Name = "Jonathan Smith" },
                new Captain { Id = 2, Name = "Hannah Intellis" },
                new Captain { Id = 3, Name = "Eva Brains" },
                new Captain { Id = 4, Name = "Rick Anderson" }
            );

            modelBuilder.Entity<Crew>().HasData(
                new Crew { Id = 1, Name = "Crew1" },
                new Crew { Id = 2, Name = "Crew2" },
                new Crew { Id = 3, Name = "Crew3" },
                new Crew { Id = 4, Name = "Crew4" }
            );

            modelBuilder.Entity<Planet>().HasData(
                new Planet
                {
                    Id = 1,
                    Name = "Tau 23",
                    ImageId = 1,
                    Description = "While visiting this planet, the robots have uncovered various forms of life",
                    DescriptionAuthorId = 1,
                    Status = Models.Enums.PlanetStatus.Ok,
                    CrewId = 1,
                },
                new Planet
                {
                    Id = 2,
                    Name = "Zeita 7",
                    ImageId = 2,
                    Description = "0.2% nutrients in the soil. Unfortunately that cannot sustain life.",
                    DescriptionAuthorId = 2,
                    Status = Models.Enums.PlanetStatus.NotOk,
                    CrewId = 2,
                },
                new Planet
                {
                    Id = 3,
                    Name = "Sigma 17",
                    ImageId = 3,
                    Description = null,
                    DescriptionAuthorId = null,
                    Status = Models.Enums.PlanetStatus.EnRoute,
                    CrewId = 3,
                },
                new Planet
                {
                    Id = 4,
                    Name = "Kappa 44",
                    ImageId = 4,
                    Description = "We\'ve found another sapient species and have engaged in communication",
                    DescriptionAuthorId = 3,
                    Status = Models.Enums.PlanetStatus.Ok,
                    CrewId = 4
                },
                new Planet
                {
                    Id = 5,
                    Name = "Tau 24",
                    ImageId = 5,
                    Description = "Just a huge floating rock",
                    DescriptionAuthorId = 4,
                    Status = Models.Enums.PlanetStatus.NotOk,
                    CrewId = null,
                }
            );
        }

        public static byte[] ReadFile(string sPath)
        {
            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to supply number of bytes 
            //to read from file.
            //In this case we want to read entire file. 
            //So supplying total number of bytes.
            byte[]? data = br.ReadBytes((int)numBytes);

            return data;
        }
    }

}
