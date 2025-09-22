using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class RelationalDbContext : DbContext, IDatabaseContext
    {
        public RelationalDbContext(DbContextOptions<RelationalDbContext> options)
            : base(options) { }

        public DbSet<T> Set<T>() where T : class => base.Set<T>();

        public async Task<List<T>> GetAllAsync<T>(string? collectionName = null) where T : class
        {
            return await Set<T>().ToListAsync();
        }

        public async Task AddAsync<T>(T entity, string? collectionName = null) where T : class
        {
            await Set<T>().AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync<T>(T entity, string? collectionName = null) where T : class
        {
            Set<T>().Update(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(T entity, string? collectionName = null) where T : class
        {
            Set<T>().Remove(entity);
            await SaveChangesAsync();
        }
    }
}
