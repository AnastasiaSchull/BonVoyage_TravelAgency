using BonVoyage.BLL.Interfaces;
using BonVoyage_TravelAgency.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BonVoyage_TravelAgency.Controllers
{
	public class HomeController : Controller
	{
		private readonly ITourService _tourService;

		public HomeController(ITourService tourService)
		{
			_tourService = tourService;
		}

		public async Task<IActionResult> Index()
		{
			var tours = await _tourService.GetAllToursAsync();
            return View(tours);
        }

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
