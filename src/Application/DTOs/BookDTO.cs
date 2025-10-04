using Domain.Entities;
using Domain.Enums.ReadingJournal;

namespace Application.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public required string Name { get; init; }
        public required ICollection<AuthorDTO> Authors { get; init; } = new List<AuthorDTO>();
        public required int Pages { get; init; }
        public int? CurrentPage { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public required List<Genres> Genres { get; set; }
        public required string Publisher { get; init; }
        public required Formats Format { get; init; }
        public string? Description { get; set; }
        public string? Review { get; set; }
        public required Status Status { get; set; }
        public decimal? Price { get; set; }
        public Series? Series { get; set; }
        public decimal ProgressPercentage => (Pages > 0 && CurrentPage.HasValue) ? Math.Round((decimal)CurrentPage.Value / Pages * 100, 2) : 0;
        public int DaysSpentReading => StartDate.HasValue ? (int)((EndDate ?? DateTime.Today).Date - StartDate.Value.Date).TotalDays : 0;
    }
}
