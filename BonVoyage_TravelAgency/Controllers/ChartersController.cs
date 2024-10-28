using BonVoyage.BLL.Interfaces;
using BonVoyage_TravelAgency.Models;
using Microsoft.AspNetCore.Mvc;

namespace BonVoyage_TravelAgency.Controllers
{
    public class ChartersController : BaseController
    {
        private readonly IUserService _userService;
        public ChartersController(IUserService userService)
        {
            _userService = userService;
        }

        //для отображения страницы с чартерами в Грецию
        public IActionResult Greece()
        {
            return View("CharterGreece");
        }


        [HttpPost]
        public ActionResult SearchFlights(FlightSearchRequest request)
        {
            //симулируем поиск рейсов на основе данных запроса
            List<FlightViewModel> results = new List<FlightViewModel>();
           
            results.Add(new FlightViewModel
            {
                From = request.CityOfDeparture,
                To = request.DestinationCity,
                Departure = request.DepartureDate,
                Arrival = request.DepartureDate.AddHours(3), 
                Price = 200, 
                Airline = "Airline A" 
            });

          
            results.Add(new FlightViewModel
            {
                From = request.CityOfDeparture,
                To = request.DestinationCity,
                Departure = request.DepartureDate,
                Arrival = request.DepartureDate.AddHours(2), 
                Price = 250,
                Airline = "Airline B"
            });

            return View("FlightResults", results);
        }


        [HttpPost]
        public async Task<IActionResult> BookFlight(FlightViewModel flight)
        {
            //залогинен ли пользователь
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            int userId = HttpContext.Session.GetInt32("UserId").Value;
            var user = await _userService.GetUserByIdAsync(userId);


            var bookingViewModel = new BookingFlightViewModel
            {
                Airline = flight.Airline,
                From = flight.From,
                To = flight.To,
                Departure = flight.Departure,
                Arrival = flight.Arrival,
                Price = flight.Price,
                PassengerName = user.UserName, 
                PassengerSurname = user.UserSurname,
                Email = user.Email
            };
            return View(bookingViewModel);
        }

        [HttpPost]
        public IActionResult ConfirmBooking(BookingFlightViewModel model)
        {        
            return View("BookingConfirmed", model); 
        }


    }
}
