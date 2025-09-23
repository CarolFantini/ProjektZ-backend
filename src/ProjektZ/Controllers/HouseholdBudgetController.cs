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
        public IActionResult GetAllIncomes()
        {
            try
            {
                var result = _iHouseholdBudgetRepository.GetAllIncomesAsync().Result;

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
        public ActionResult CreateIncome([FromBody] Income income) // ou [FromForm]
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

                var incomeCalculated = _iHouseholdBudgetService.CalculateDiscounts(model).Result;

                var result = _iHouseholdBudgetRepository.AddIncomeAsync(incomeCalculated).Result;

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

        [HttpPost]
        [Route("edit-income")]
        [ValidateAntiForgeryToken]
        public ActionResult EditIncome([FromBody] Income incomeToUpdate)
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

                incomeToUpdate = _iHouseholdBudgetService.CalculateDiscounts(model).Result;

                var result = _iHouseholdBudgetRepository.UpdateIncomeAsync(incomeToUpdate).Result;

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
        public ActionResult DisableIncome(int id)
        {
            try
            {
                var income = _iHouseholdBudgetRepository.FindIncomeById(id).Result;

                var result = _iHouseholdBudgetRepository.DisableIncomeAsync(income).Result;

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
        public IActionResult GetAllExpenses()
        {
            try
            {
                var result = _iHouseholdBudgetRepository.GetAllExpensesAsync().Result;

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
        public ActionResult CreateExpense([FromBody] Expense expense) // ou [FromForm]
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

                var result = _iHouseholdBudgetRepository.AddExpenseAsync(model).Result;

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

        [HttpPost]
        [Route("edit-expense")]
        [ValidateAntiForgeryToken]
        public ActionResult EditExpense([FromBody] Expense expenseToUpdate)
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

                var result = _iHouseholdBudgetRepository.UpdateExpenseAsync(expenseToUpdate).Result;

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
        public ActionResult DisableExpense(int id)
        {
            try
            {
                var expense = _iHouseholdBudgetRepository.FindExpenseById(id).Result;

                var result = _iHouseholdBudgetRepository.DisableExpenseAsync(expense).Result;

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
