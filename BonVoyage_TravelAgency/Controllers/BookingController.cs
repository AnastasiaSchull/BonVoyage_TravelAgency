using Microsoft.AspNetCore.Mvc;
using BonVoyage.BLL.DTOs;
using BonVoyage.BLL.Interfaces;
using BonVoyage.BLL.Infrastructure;
using BonVoyage_TravelAgency.Models;
using System.Net.Mail;
using System.Net;
using System.Text;


namespace BonVoyage_TravelAgency.Controllers
{
    public class BookingController : BaseController
    {
        private readonly IBookingService bookingService;
        private readonly ITourService _tourService;
        private readonly ITourPhotoService _tourPhotoService;
        private readonly IUserService _userService;
        IWebHostEnvironment _appEnvironment;
        public BookingController(IBookingService serv, IUserService userService, ITourService tourService, ITourPhotoService tourPhotoService, IWebHostEnvironment appEnvironment)
        {
            bookingService = serv;
            _userService = userService;
            _tourService = tourService;
            _tourPhotoService = tourPhotoService;
            _appEnvironment = appEnvironment;
        }

        // GET: Booking
        public async Task<IActionResult> Index()
        {
            var bookings = await bookingService.GetAllBookingsAsync();                                   
                return View(bookings);
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
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Login", "Account");

            var users = await _userService.GetAllUsersAsync();
            var tour = await _tourService.GetTourByIdAsync(id);
            var tourPhoto = await _tourPhotoService.GetTourPhotoByTourIdAsync(tour.TourId);
            
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

                var user = await _userService.GetUserByIdAsync(booking.UserId);
                var tour = await _tourService.GetTourByIdAsync(booking.TourId);
                var tourPhoto = await _tourPhotoService.GetTourPhotoByTourIdAsync(booking.TourId);
                string path = tourPhoto.PhotoUrl;
                var fullPath = _appEnvironment.WebRootPath + path;  

                //MailMessage - представляет сообщение электронной почты, которое может быть отправлено с помощью класса SmtpClient.
                MailMessage message = new MailMessage();
                message.To.Add(new MailAddress(user.Email)); // электронный адрес получателя (login@itstep.academy)   
                message.From = new MailAddress("bon.voyage.step@gmail.com"); // электронный адрес отправителя (login@gmail.com)
                message.Subject = "Tour booking"; // тема письма
                message.Body = "Dear, " + user.UserName + " your booking request for tour " + tour.Title + " will be processed as soon as possible!"; // содержимое письма
                                             // кодировка, используемая для темы данного сообщения электронной почты
                message.SubjectEncoding = Encoding.UTF8;
                // кодировка, используемая для кодирования текста письма
                message.BodyEncoding = Encoding.UTF8;
                message.Attachments.Add(new Attachment(fullPath)); // путь к прикрепленному файлу
                                                                       // SmtpClient позволяет приложениям отправлять электронную почту с помощью протокола SMTP (Simple Mail Transfer Protocol)
                int port = Convert.ToInt32(587);
                SmtpClient smtp = new SmtpClient("smtp.gmail.com" /* сервер SMTP */, port /* порт */); // например, smtp.gmail.com   порт 587

                // Credentials - учетные данные, используемые для проверки подлинности отправителя
                smtp.Credentials = new NetworkCredential("bon.voyage.step@gmail.com" /* логин */, "tuvuozlyjplgcqae" /* пароль */);
                smtp.EnableSsl = true; // Указывает, использует ли SmtpClient протокол SSL для шифрования подключения.
                                       // Send отправляет указанное сообщение на сервер SMTP для доставки
                smtp.Send(message);               

                return RedirectToAction("Index", "Home");
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
        
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faq = await bookingService.GetBookingByIdAsync(id);
            if (faq == null)
            {
                return Json(new { success = false, message = "Booking not found!" });
            }

            await bookingService.DeleteBookingAsync(id);
            return Json(new { success = true, message = "Booking deleted successfully!" });
        }
    }
}
