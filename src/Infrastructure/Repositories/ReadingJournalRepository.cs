using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReadingJournalRepository : IReadingJournalRepository
    {
        private readonly RelationalDbContext _dbContext;

        public ReadingJournalRepository(RelationalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _dbContext.Books
            .Include(b => b.Authors)
            .Include(b => b.Series)
            .ToListAsync();
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();

            return book;
        }

        public async Task<Book> UpdateBookAsync(Book bookToUpdate)
        {
            _dbContext.Books.Update(bookToUpdate);
            await _dbContext.SaveChangesAsync();

            return bookToUpdate;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);

            if (book != null)
            {
                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Book> FindBookById(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);

            return book!;
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _dbContext.Authors.ToListAsync();
        }

        public async Task<Author> AddAuthorAsync(Author author)
        {
            await _dbContext.Authors.AddAsync(author);
            await _dbContext.SaveChangesAsync();

            return author;
        }

        public async Task<List<Author>> FindAuthorsById(List<int> ids)
        {
            var authors = await _dbContext.Authors
                .Where(a => ids.Contains(a.Id))
                .ToListAsync();

            return authors;
        }

        public async Task<List<Series>> GetAllSeriesAsync()
        {
            return await _dbContext.Series.ToListAsync();
        }

        public async Task<Series> AddSeriesAsync(Series series)
        {
            await _dbContext.Series.AddAsync(series);
            await _dbContext.SaveChangesAsync();

            return series;
        }

        public async Task<Series> FindSeriesById(int id)
        {
            var series = await _dbContext.Series.FindAsync(id);

            return series!;
        }
    }
}
