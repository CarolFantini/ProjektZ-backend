using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class HouseholdBudgetRepository : IHouseholdBudgetRepository
    {
        private readonly RelationalDbContext _dbContext;

        public HouseholdBudgetRepository(RelationalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Income>> GetAllAsync()
        {
            return await _dbContext.Incomes.ToListAsync();
        }

        public async Task<Income> FindById(int id)
        {
            var income = await _dbContext.Incomes.FindAsync(id);

            return income!;
        }

        public async Task<bool> AddAsync(Income income)
        {
            try
            {
                await _dbContext.Incomes.AddAsync(income);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Income incomeToUpdate)
        {
            try
            {
                _dbContext.Incomes.Update(incomeToUpdate);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Income income)
        {
            try
            {
                _dbContext.Incomes.Remove(income);

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
