using BonVoyage.BLL.Interfaces;
using BonVoyage_TravelAgency.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace BonVoyage_TravelAgency.Controllers
{
	public class HomeController : BaseController
	{
        private readonly ITourService _tourService;
        private readonly ITourPhotoService _tourPhotoService;

        public HomeController(ITourService tourService, ITourPhotoService tourPhotoService)
        {
            _tourService = tourService;
            _tourPhotoService = tourPhotoService;
        }
     
        public async Task<IActionResult> Index()
        {
            var tours = await _tourService.GetAllToursAsync();
            var tourPhotos = await _tourPhotoService.GetAllTourPhotosAsync();

            var viewModel = new ToursPhotosViewModel
            {
                Tours = tours,
                TourPhotos = tourPhotos
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
		{
			return View();
		}

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult AboutUs()
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
