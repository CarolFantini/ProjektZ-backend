using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseContext _dbContext;

        public UserRepository(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<User>> GetAllAsync()
        {
            return _dbContext.GetAllAsync<User>();
        }

        public Task AddAsync(User user)
        {
            return _dbContext.AddAsync(user);
        }

        public Task UpdateAsync(User user)
        {
            return _dbContext.UpdateAsync(user);
        }

        public Task DeleteAsync(User user)
        {
            return _dbContext.DeleteAsync(user);
        }
    }
}
