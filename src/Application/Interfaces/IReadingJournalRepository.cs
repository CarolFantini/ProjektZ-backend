using Domain.Entities;

namespace Application.Interfaces
{
    public interface IReadingJournalRepository
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book> FindBookById(int id);
        Task<bool> AddBookAsync(Book book);
        Task<bool> UpdateBookAsync(Book bookToUpdate);
    }
}
