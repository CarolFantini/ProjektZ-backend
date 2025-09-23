using Domain.Entities;

namespace Application.Interfaces
{
    public interface IHouseholdBudgetService
    {
        Task<Income> CalculateDiscounts(Income income);
    }
}
