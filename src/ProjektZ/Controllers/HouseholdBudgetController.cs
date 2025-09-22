using Microsoft.AspNetCore.Mvc;

namespace ProjektZ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HouseholdBudgetController : ControllerBase
    {
        [HttpGet]
        [Route("index")]
        public ActionResult Index()
        {
            return Ok();
        }

        [HttpGet]
        [Route("details")]
        public IActionResult Details(int id)
        {
            return Ok("Details");
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Ok();
            }
        }

        [HttpPost]
        [Route("edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Ok();
            }
        }

        [HttpPost]
        [Route("delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Ok();
            }
        }
    }
}
