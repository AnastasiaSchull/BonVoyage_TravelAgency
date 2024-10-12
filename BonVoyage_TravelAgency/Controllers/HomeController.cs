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
       
   
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 3)
        {
            var totalItemCount = await _tourService.GetTotalToursCount(); 
            var tours = await _tourService.GetAllToursAsync(pageNumber, pageSize); // получение туров для текущей страницы

            var tourPhotos = await _tourPhotoService.GetAllTourPhotosAsync(); 

            var viewModel = new ToursPhotosViewModel
            {
                Tours = tours,
                TourPhotos = tourPhotos,
                PageViewModel = new PageViewModel(totalItemCount, pageNumber, pageSize) // подключение модели пагинации
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
        public async Task<IActionResult> Preferences()
        {
            var tours = await _tourService.GetAllToursAsync(); // получение туров для текущей страницы

            var tourPhotos = await _tourPhotoService.GetAllTourPhotosAsync();

            var viewModel = new ToursPhotosViewModel
            {
                Tours = tours,
                TourPhotos = tourPhotos
            };
            return View(viewModel);            
        }

        public IActionResult CreatePreference(int? id)
        {

            if (id != null)
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(30); // срок хранения куки - 30 дней

                if (Request.Cookies["preference"+id] == null)
                    Response.Cookies.Append("preference"+id, id.ToString(), option); // создание куки

                    return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult DeletePreference(int? id)
        {
            if (id != null)
            {
                Response.Cookies.Delete("preference"+id); // удаление куки

                return RedirectToAction("Preferences", "Home");
            }
            return RedirectToAction("Preferences", "Home");
        }
    }
}
