namespace Domain.Entities
{
    public class Series
    {
        public int Id { get; set; }
        public string? Name { get; init; }
        public Author? Author { get; set; }
        public List<Book>? Books { get; set; }
    }
}
