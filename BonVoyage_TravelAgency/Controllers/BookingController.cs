using Microsoft.AspNetCore.Mvc;
using BonVoyage.BLL.DTOs;
using BonVoyage.BLL.Interfaces;
using BonVoyage.BLL.Infrastructure;
using BonVoyage.BLL.Services;
using BonVoyage_TravelAgency.Models;
using BonVoyage.DAL.Entities;

namespace BonVoyage_TravelAgency.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService bookingService;
        private readonly ITourService _tourService;
        private readonly ITourPhotoService _tourPhotoService;
        private readonly IUserService _userService;
        public BookingController(IBookingService serv, IUserService userService, ITourService tourService, ITourPhotoService tourPhotoService)
        {
            bookingService = serv;
            _userService = userService;
            _tourService = tourService;
            _tourPhotoService = tourPhotoService;
        }

        // GET: Booking
        public async Task<IActionResult> Index()
        {
            //var bookings = await bookingService.GetAllBookingsAsync();
            return View(/*bookings*/);
        }
        
        // GET: Booking/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                BookingDTO booking = await bookingService.GetBookingByIdAsync((int)id);
                return View(booking);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }
        // GET: Booking/Create

        public async Task<IActionResult> Create(int id)
        {
            var users = await _userService.GetAllUsersAsync();
            var tour = await _tourService.GetTourByIdAsync(id);
            var tourPhoto = await _tourPhotoService.GetTourPhotoByIdAsync(tour.TourId);
            
            var viewModel = new BookingViewModel
            {
                Tour = tour,
                Users = users,
                TourPhoto = tourPhoto
            };

            return View(viewModel);
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingDTO booking)
        {
            if (ModelState.IsValid)
            {
                booking.BookingDate = DateTime.Now;
                booking.Status = "Under consideration";
                await bookingService.CreateBookingAsync(booking);
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }
        // GET: Booking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                BookingDTO booking = await bookingService.GetBookingByIdAsync((int)id);
                return View(booking);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: Booking/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookingDTO booking)
        {
            if (ModelState.IsValid)
            {
                await bookingService.UpdateBookingAsync(booking);
                return View("~/Views/Booking/Index.cshtml", await bookingService.GetAllBookingsAsync());
            }
            return View(booking);
        }
        // GET: Booking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                BookingDTO booking = await bookingService.GetBookingByIdAsync((int)id);
                return View(booking);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await bookingService.DeleteBookingAsync(id);
            return View("~/Views/Booking/Index.cshtml", await bookingService.GetAllBookingsAsync());
        }
    }
}
