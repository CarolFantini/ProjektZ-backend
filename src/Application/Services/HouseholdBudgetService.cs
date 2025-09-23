using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class HouseholdBudgetService : IHouseholdBudgetService
    {
        public Task<Income> CalculateDiscounts(Income income)
        {
            CalculateINSSDiscount(income);
            CalculateIRDiscount(income);

            return Task.FromResult(income);
        }

        private void CalculateINSSDiscount(Income income)
        {
            decimal grossSalary = income.GrossSalary;
            decimal inssDiscount = 0m;

            if (grossSalary <= 1412.00m)
            {
                inssDiscount = grossSalary * 0.075m;
            }
            else if (grossSalary <= 2666.68m)
            {
                inssDiscount = (1412.00m * 0.075m) +
                               ((grossSalary - 1412.00m) * 0.09m);
            }
            else if (grossSalary <= 4000.03m)
            {
                inssDiscount = (1412.00m * 0.075m) +
                               ((2666.68m - 1412.00m) * 0.09m) +
                               ((grossSalary - 2666.68m) * 0.12m);
            }
            else if (grossSalary <= 7786.02m)
            {
                inssDiscount = (1412.00m * 0.075m) +
                               ((2666.68m - 1412.00m) * 0.09m) +
                               ((4000.03m - 2666.68m) * 0.12m) +
                               ((grossSalary - 4000.03m) * 0.14m);
            }
            else
            {
                // Teto do INSS
                inssDiscount = (1412.00m * 0.075m) +
                               ((2666.68m - 1412.00m) * 0.09m) +
                               ((4000.03m - 2666.68m) * 0.12m) +
                               ((7786.02m - 4000.03m) * 0.14m);
            }

            income.UpdateINSSDiscount(decimal.Round(inssDiscount, 2));
        }

        private void CalculateIRDiscount(Income income)
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
