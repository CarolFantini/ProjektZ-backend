using Domain.Entities;

namespace Application.Interfaces
{
    public interface IHouseholdBudgetService
    {
        void CalculateINSSDiscount(Income income);
        void CalculateIRDiscount(Income income);
    }
}
