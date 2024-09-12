using Microsoft.AspNetCore.Mvc;

namespace BonVoyage_TravelAgency.Controllers
{
    public class GuideController : Controller
    {
        public IActionResult WhereToGo()
        {
            return View();
        }

        public IActionResult TravelInsurance()
        {
            return View();
        }

        public IActionResult TravelNews()
        {
            return View();
        }

        public IActionResult TipsForTourists()
        {
            return View();
        }

        public IActionResult CoronavirusTests()
        {
            return View();
        }
    }
}
