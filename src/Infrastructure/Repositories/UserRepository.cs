using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RelationalDbContext _dbContext;

        public UserRepository(RelationalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<User>> GetAllAsync()
        {
            return _dbContext.Users.ToListAsync();
        }

        public Task AddAsync(User user)
        {
            _dbContext.Users.AddAsync(user);
            return _dbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            return _dbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(User user)
        {
            _dbContext.Users.Remove(user);
            return _dbContext.SaveChangesAsync();
        }
    }
}
