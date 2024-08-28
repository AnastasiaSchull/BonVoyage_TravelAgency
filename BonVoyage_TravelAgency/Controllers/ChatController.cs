using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BonVoyage_TravelAgency.Controllers
{
    public class ChatController : Controller
    {      
        public IActionResult Index()
        {
            return View();
        }
    }
}
