using Domain.Entities;

namespace Application.Interfaces
{
    public interface IHouseholdBudgetRepository
    {
        Task<List<Income>> GetAllAsync();
        Task<Income> FindById(int id);
        Task<bool> AddAsync(Income income);
        Task<bool> UpdateAsync(Income incomeToUpdate);
        Task<bool> DeleteAsync(Income income);
    }
}
