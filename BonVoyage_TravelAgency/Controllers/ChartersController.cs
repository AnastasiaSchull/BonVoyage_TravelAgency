using Microsoft.AspNetCore.Mvc;

namespace BonVoyage_TravelAgency.Controllers
{
    public class ChartersController : BaseController
    {
        //для отображения страницы с чартерами в Грецию
        public IActionResult Greece()
        {
            return View("CharterGreece");
        }
    }
}
