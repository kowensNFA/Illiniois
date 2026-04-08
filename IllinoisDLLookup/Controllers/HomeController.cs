using System.Web.Mvc;
using IllinoisDLLookup.Models;

namespace IllinoisDLLookup.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home/Index
        public ActionResult Index()
        {
            return View(new LicenseLookupViewModel());
        }

        // POST: Home/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LicenseLookupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Result = LicenseLookupResult.Validate(model.LicenseNumber);
            return View(model);
        }
    }
}
