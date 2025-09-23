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
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Book> FindBookById(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);

            return book!;
        }

        public async Task<bool> AddBookAsync(Book book)
        {
            try
            {
                await _dbContext.Books.AddAsync(book);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateBookAsync(Book bookToUpdate)
        {
            try
            {
                _dbContext.Books.Update(bookToUpdate);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
