namespace Xpand.PlanetsAPI.Data.Models
{
    public class Description
    {
        public int Id { get; set; }

        public string Text { get; set; } = null!;

        public int AuthorId { get; set; }

        public Captain Author { get; set; } = null!;
    }
}
