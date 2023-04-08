namespace Xpand.PlanetsAPI.Data.Models
{
    public class Image
    {
        public int Id { get; set; }

        public byte[] Bytes { get; set; } = null!; 

        public Planet? Planet { get; set; }
    }
}
