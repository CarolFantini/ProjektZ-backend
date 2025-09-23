namespace Domain.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string? Name { get; init; }
        public List<Book>? Books { get; set; }
    }
}
