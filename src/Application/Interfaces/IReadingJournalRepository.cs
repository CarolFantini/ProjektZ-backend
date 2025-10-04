using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IReadingJournalRepository
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book> AddBookAsync(Book book);
        Task<Book> UpdateBookAsync(Book bookToUpdate);
        Task<bool> DeleteBookAsync(int id);
        Task<Book> FindBookById(int id);
        Task<List<Author>> GetAllAuthorsAsync();
        Task<Author> AddAuthorAsync(Author author);
        Task<List<Author>> FindAuthorsById(List<int> ids);
        Task<List<Series>> GetAllSeriesAsync();
        Task<Series> AddSeriesAsync(Series series);
        Task<Series> FindSeriesById(int id);
    }
}
