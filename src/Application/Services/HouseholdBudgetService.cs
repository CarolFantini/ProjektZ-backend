using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class HouseholdBudgetService : IHouseholdBudgetService
    {
        private static readonly (decimal Limit, decimal Rate)[] InssBrackets = new[]
        {
            (1412.00m, 0.075m),
            (2666.68m, 0.09m),
            (4000.03m, 0.12m),
            (7786.02m, 0.14m)
        };

        private decimal CalculateINSS(decimal grossSalary)
        {
            decimal discount = 0m;
            decimal previousLimit = 0m;

            foreach (var (limit, rate) in InssBrackets)
            {
                if (grossSalary > previousLimit)
                {
                    var taxable = Math.Min(grossSalary, limit) - previousLimit;
                    discount += taxable * rate;
                    previousLimit = limit;
                }
            }

            return decimal.Round(discount, 2);
        }

        public void CalculateINSSDiscount(Income income)
        {
            decimal inssDiscount = CalculateINSS(income.GrossSalary);

            income.UpdateINSSDiscount(decimal.Round(inssDiscount, 2));
        }

        public void CalculateIRDiscount(Income income)
        {
            decimal baseIR = income.GrossSalary - income.INSSDiscount;
            decimal irDiscount = 0m;

            if (baseIR <= 2259.20m)
            {
                irDiscount = 0m;
            }
            else if (baseIR <= 2826.65m)
            {
                irDiscount = (baseIR * 0.075m) - 169.44m;
            }
            else if (baseIR <= 3751.05m)
            {
                irDiscount = (baseIR * 0.15m) - 381.44m;
            }
            else if (baseIR <= 4664.68m)
            {
                irDiscount = (baseIR * 0.225m) - 662.77m;
            }
            else
            {
                irDiscount = (baseIR * 0.275m) - 896.00m;
            }

            irDiscount = irDiscount < 0 ? 0 : irDiscount;

            income.UpdateIRDiscount(decimal.Round(irDiscount, 2));
        }
    }
}
