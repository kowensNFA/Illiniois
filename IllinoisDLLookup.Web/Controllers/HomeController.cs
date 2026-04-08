using Microsoft.AspNetCore.Mvc;
using IllinoisDLLookup.Web.Models;

namespace IllinoisDLLookup.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home/Index
        public IActionResult Index()
        {
            return View(new LicenseLookupViewModel());
        }

        // POST: Home/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LicenseLookupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Result = LicenseLookupResult.Validate(model.LicenseNumber);
            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
