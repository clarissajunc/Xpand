namespace Xpand.PlanetsAPI.Models
{
    public class Image
    {
        public int Id { get; set; }

        public byte[] Bytes { get; set; }

        public Planet? Planet { get; set; }
    }
}
