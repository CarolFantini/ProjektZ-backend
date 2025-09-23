using Domain.Entities;

namespace Application.Interfaces
{
    public interface IHouseholdBudgetRepository
    {
        Task<List<Income>> GetAllIncomesAsync();
        Task<Income> FindIncomeById(int id);
        Task<bool> AddIncomeAsync(Income income);
        Task<bool> UpdateIncomeAsync(Income incomeToUpdate);
        Task<bool> DisableIncomeAsync(Income income);
        Task<List<Expense>> GetAllExpensesAsync();
        Task<Expense> FindExpenseById(int id);
        Task<bool> AddExpenseAsync(Expense expense);
        Task<bool> UpdateExpenseAsync(Expense expenseToUpdate);
        Task<bool> DisableExpenseAsync(Expense expense);
    }
}
