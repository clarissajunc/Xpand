namespace Xpand.API.Domain.Models
{
    public class Description
    {
        public int Id { get; set; }

        public string Text { get; set; } = null!;

        public int AuthorId { get; set; }

        public Human Author { get; set; } = null!;
    }
}
