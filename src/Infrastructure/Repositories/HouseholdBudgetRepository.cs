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

        public async Task<List<Income>> GetAllIncomesAsync()
        {
            return await _dbContext.Incomes.ToListAsync();
        }

        public async Task<Income> FindIncomeById(int id)
        {
            var income = await _dbContext.Incomes.FindAsync(id);

            return income!;
        }

        public async Task<bool> AddIncomeAsync(Income income)
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

        public async Task<bool> UpdateIncomeAsync(Income incomeToUpdate)
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

        public async Task<bool> DisableIncomeAsync(Income income)
        {
            try
            {
                income.IsActive = false;
                _dbContext.Incomes.Remove(income);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Expense>> GetAllExpensesAsync()
        {
            return await _dbContext.Expenses.ToListAsync();
        }

        public async Task<Expense> FindExpenseById(int id)
        {
            var expense = await _dbContext.Expenses.FindAsync(id);

            return expense!;
        }

        public async Task<bool> AddExpenseAsync(Expense expense)
        {
            try
            {
                await _dbContext.Expenses.AddAsync(expense);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateExpenseAsync(Expense expenseToUpdate)
        {
            try
            {
                _dbContext.Expenses.Update(expenseToUpdate);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DisableExpenseAsync(Expense expense)
        {
            try
            {
                expense.IsActive = false;
                _dbContext.Expenses.Update(expense);

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
