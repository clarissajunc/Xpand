namespace Xpand.PlanetsAPI.Models
{
    public class Image
    {
        public int Id { get; set; }

        public byte[] Bytes { get; set; }

        public int? PlanetId { get; set; }  

        public Planet? Planet { get; set; }
    }
}
