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
        [Route("getall")]
        public IActionResult GetAll()
        {
            try
            {
                var result = _iHouseholdBudgetRepository.GetAllAsync().Result;

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
        [Route("create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromBody] Income income) // ou [FromForm]
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
                    CreatedAt = DateTime.Now
                };

                var incomeCalculated = _iHouseholdBudgetService.CalculateDiscounts(model).Result;

                var result = _iHouseholdBudgetRepository.AddAsync(incomeCalculated).Result;

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
        [Route("edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [FromBody] Income incomeToUpdate)
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

                var result = _iHouseholdBudgetRepository.UpdateAsync(incomeToUpdate).Result;

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
        [Route("delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var income = _iHouseholdBudgetRepository.FindById(id).Result;

                var result = _iHouseholdBudgetRepository.DeleteAsync(income).Result;

                if (!result)
                {
                    return StatusCode(500, "An error occurred while deleting the income.");
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
