using Application.DTOs;

namespace Application.Interfaces
{
    public interface IReadingJournalService
    {
        Task<List<BookDTO>> GetAllBooksAsync();
        Task<bool> CreateBookAsync(BookDTO dto);
        Task<bool> EditBookAsync(int id, BookDTO dto);
        Task<bool> DeleteBookAsync(int id);
        Task<List<AuthorDTO>> GetAllAuthorsAsync();
        Task<bool> CreateAuthorAsync(AuthorDTO dto);
        Task<bool> CreateSeriesAsync(string name);
    }
}
