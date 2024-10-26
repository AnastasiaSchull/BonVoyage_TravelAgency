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
        private readonly IHotelService _hotelService;
        private readonly IHotelPhotoService _hotelPhotoService;

        public HomeController(ITourService tourService, ITourPhotoService tourPhotoService, IHotelService hotelService, IHotelPhotoService hotelPhotoService)
        {
            _tourService = tourService;
            _tourPhotoService = tourPhotoService;
            _hotelService = hotelService;
            _hotelPhotoService = hotelPhotoService;
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

        public async Task<IActionResult> FilterTours(string filter)
        {
            var filteredTours = await _tourService.GetFilteredToursAsync(filter);
            var tourPhotos = await _tourPhotoService.GetAllTourPhotosAsync();

            var viewModel = new ToursPhotosViewModel
            {
                Tours = filteredTours,
                TourPhotos = tourPhotos
            };

            return View("FilteredTours", viewModel);
        }
        public async Task<IActionResult> FilterHotelsByTours(string filter)
        {
            var filteredHotels = await _hotelService.GetFilteredHotelsByToursAsync(filter);
            var hotelPhotos = await _hotelPhotoService.GetAllHotelPhotosAsync();
            var tour = filter;

            var viewModel = new HotelsPhotosViewModel
            {
                Hotels = filteredHotels,
                HotelsPhotos = hotelPhotos,
                Tour = tour
            };

            return View("FilteredHotelsByTours", viewModel);
        }
    }
}
