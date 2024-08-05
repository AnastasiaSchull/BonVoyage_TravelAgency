using Microsoft.AspNetCore.Mvc;
using BonVoyage.BLL.DTOs;
using BonVoyage.BLL.Interfaces;
using BonVoyage.BLL.Infrastructure;

namespace BonVoyage_TravelAgency.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService bookingService;

        public BookingController(IBookingService serv)
        {
            bookingService = serv;
        }

        // GET: Booking
        public async Task<IActionResult> Index()
        {
            return View(await bookingService.GetAllBookingsAsync());
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

        public IActionResult Create()
        {
            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingDTO booking)
        {
            if (ModelState.IsValid)
            {
                await bookingService.CreateBookingAsync(booking);
                TempData["msg"] = "successful booking";
                return View("~/Views/Home/Index.cshtml");
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
