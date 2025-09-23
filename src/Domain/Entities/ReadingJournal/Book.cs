using Domain.Enums.ReadingJournal;

namespace Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string? Name { get; init; }
        public Author? Author { get; init; }
        public int? Pages { get; init; }
        public int? CurrentPage { get; set; }
        public DateOnly? StartDate { get; init; }
        public DateOnly? EndDate { get; set; }
        public List<Genres>? Genre { get; set; }
        public string? Language { get; init; }
        public string? Publisher { get; init; }
        public Formats Format { get; init; }
        public string? Description { get; set; }
        public int? Rating { get; set; }
        public Status Status { get; set; }
        public decimal Price { get; init; }
        public Series? Series { get; set; }
        public decimal ProgressPercentage => (Pages.HasValue && Pages > 0 && CurrentPage.HasValue) ? Math.Round((decimal)CurrentPage.Value / Pages.Value * 100, 2) : 0;
        public int DaysSpentReading => (EndDate.HasValue && StartDate.HasValue) ? (EndDate.Value.DayNumber - StartDate.Value.DayNumber) : 0;
    }
}
