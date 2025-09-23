using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ProjektZ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HouseholdBudgetController : ControllerBase
    {
        private readonly IHouseholdBudgetService _iHouseholdBudgetService;
        private readonly IHouseholdBudgetRepository _iHouseholdBudgetRepository;

        public HouseholdBudgetController(IHouseholdBudgetService iHouseholdBudgetService, IHouseholdBudgetRepository iHouseholdBudgetRepository)
        {
            _iHouseholdBudgetService = iHouseholdBudgetService;
            _iHouseholdBudgetRepository = iHouseholdBudgetRepository;

        }

        [HttpGet]
        [Route("getall-incomes")]
        public async Task<IActionResult> GetAllIncomes()
        {
            try
            {
                var result = await _iHouseholdBudgetRepository.GetAllIncomesAsync();

                if (result.Count() <= 0)
                {
                    return StatusCode(500, "An error occurred while retrieving the incomes.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("create-income")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIncome([FromBody] Income income) // ou [FromForm]
        {
            try
            {
                var model = new Income
                {
                    CompanyName = income.CompanyName,
                    GrossSalary = income.GrossSalary,
                    WorkingHoursPerMonth = income.WorkingHoursPerMonth,
                    WorkingDaysPerMonth = income.WorkingDaysPerMonth,
                    VAorVR = income.VAorVR,
                    PLR = income.PLR,
                    CreatedAt = DateOnly.FromDateTime(DateTime.Now)
                };

                var incomeCalculated = await _iHouseholdBudgetService.CalculateDiscounts(model);

                var result = await _iHouseholdBudgetRepository.AddIncomeAsync(incomeCalculated);

                if (!result)
                {
                    return StatusCode(500, "An error occurred while saving the income.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch]
        [Route("edit-income")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditIncome([FromBody] Income incomeToUpdate)
        {
            try
            {
                var model = new Income
                {
                    CompanyName = incomeToUpdate.CompanyName,
                    GrossSalary = incomeToUpdate.GrossSalary,
                    WorkingHoursPerMonth = incomeToUpdate.WorkingHoursPerMonth,
                    WorkingDaysPerMonth = incomeToUpdate.WorkingDaysPerMonth,
                    VAorVR = incomeToUpdate.VAorVR,
                    PLR = incomeToUpdate.PLR
                };

                incomeToUpdate = await _iHouseholdBudgetService.CalculateDiscounts(model);

                var result = await _iHouseholdBudgetRepository.UpdateIncomeAsync(incomeToUpdate);

                if (!result)
                {
                    return StatusCode(500, "An error occurred while editing the income.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("disable-income")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisableIncome(int id)
        {
            try
            {
                var income = await _iHouseholdBudgetRepository.FindIncomeById(id);

                var result = await _iHouseholdBudgetRepository.DisableIncomeAsync(income);

                if (!result)
                {
                    return StatusCode(500, "An error occurred while disabling the income.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("getall-expenses")]
        public async Task<IActionResult> GetAllExpenses()
        {
            try
            {
                var result = await _iHouseholdBudgetRepository.GetAllExpensesAsync();

                if (result.Count() <= 0)
                {
                    return StatusCode(500, "An error occurred while retrieving the expenses.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("create-expense")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExpense([FromBody] Expense expense)
        {
            try
            {
                var model = new Expense
                {
                    Name = expense.Name,
                    Amount = expense.Amount,
                    IsFixed = expense.IsFixed,
                    IsEssential = expense.IsEssential,
                    isCreditCard = expense.isCreditCard,
                    DueDay = expense.DueDay,
                    Renewal = expense.Renewal,
                    IsInstallment = expense.IsInstallment,
                    InstallmentCount = expense.InstallmentCount,
                    CreatedAt = DateOnly.FromDateTime(DateTime.Now)
                };

                var result = await _iHouseholdBudgetRepository.AddExpenseAsync(model);

                if (!result)
                {
                    return StatusCode(500, "An error occurred while saving the expense.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch]
        [Route("edit-expense")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditExpense([FromBody] Expense expenseToUpdate)
        {
            try
            {
                var model = new Expense
                {
                    Name = expenseToUpdate.Name,
                    Amount = expenseToUpdate.Amount,
                    IsFixed = expenseToUpdate.IsFixed,
                    IsEssential = expenseToUpdate.IsEssential,
                    isCreditCard = expenseToUpdate.isCreditCard,
                    DueDay = expenseToUpdate.DueDay,
                    Renewal = expenseToUpdate.Renewal,
                    IsInstallment = expenseToUpdate.IsInstallment,
                    InstallmentCount = expenseToUpdate.InstallmentCount,
                };

                var result = await _iHouseholdBudgetRepository.UpdateExpenseAsync(expenseToUpdate);

                if (!result)
                {
                    return StatusCode(500, "An error occurred while editing the expense.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("disable-expense")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisableExpense(int id)
        {
            try
            {
                var expense = await _iHouseholdBudgetRepository.FindExpenseById(id);

                var result = await _iHouseholdBudgetRepository.DisableExpenseAsync(expense);

                if (!result)
                {
                    return StatusCode(500, "An error occurred while disabling the expense.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
