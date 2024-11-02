using Microsoft.AspNetCore.Mvc;
using BonVoyage.BLL.DTOs;
using BonVoyage.BLL.Interfaces;
using BonVoyage.BLL.Infrastructure;
using BonVoyage_TravelAgency.Models;
using System.Net.Mail;
using System.Net;
using System.Text;
using BonVoyage.DAL.Entities;


namespace BonVoyage_TravelAgency.Controllers
{
    public class BookingHotelController : BaseController
    {
        private readonly IBookingHotelService bookingHotelService;
        private readonly IHotelService _hotelService;
        private readonly IHotelPhotoService _hotelPhotoService;
        private readonly IUserService _userService;
        IWebHostEnvironment _appEnvironment;
        public BookingHotelController(IBookingHotelService serv, IUserService userService, IHotelService hotelService, IHotelPhotoService hotelPhotoService, IWebHostEnvironment appEnvironment)
        {
            bookingHotelService = serv;
            _userService = userService;
            _hotelService = hotelService;
            _hotelPhotoService = hotelPhotoService;
            _appEnvironment = appEnvironment;
        }

        // GET: BookingHotel
        public async Task<IActionResult> Index(string? user, string? status)
        {
            var bookingsHotels = await bookingHotelService.GetAllBookingsHotelsAsync();
            if (!string.IsNullOrEmpty(user))
            {
                bookingsHotels = bookingsHotels.Where(p => p.User == user);
            }
            if (!string.IsNullOrEmpty(status))
            {
                bookingsHotels = bookingsHotels.Where(p => p.Status == status);
            }
            
            BookingHotelFilterModel filterModel = new BookingHotelFilterModel(bookingsHotels, status, user);

            return View(filterModel);
        }
        
        // GET: BookingHotel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                BookingHotelDTO bookingHotel = await bookingHotelService.GetBookingHotelByIdAsync((int)id);
                return View(bookingHotel);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }
        // GET: BookingHotel/Create

        public async Task<IActionResult> Create(int id)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Login", "Account");

            var users = await _userService.GetAllUsersAsync();
            var hotel = await _hotelService.GetHotelByIdAsync(id);
            var hotelPhoto = await _hotelPhotoService.GetHotelPhotoByHotelIdAsync(hotel.HotelId);
            
            var viewModel = new BookingHotelViewModel
            {
                Hotel = hotel,
                Users = users,
                HotelPhoto = hotelPhoto
            };

            return View(viewModel);
        }

        // POST: BookingHotel/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingHotelDTO bookingHotel)
        {
           if (ModelState.IsValid)
            {
                bookingHotel.BookingDate = DateTime.Today.ToLocalTime();
                bookingHotel.Status = "Under consideration";
                await bookingHotelService.CreateBookingHotelAsync(bookingHotel);

                var user = await _userService.GetUserByIdAsync(bookingHotel.UserId);
                var hotel = await _hotelService.GetHotelByIdAsync(bookingHotel.HotelId);
                var hotelPhoto = await _hotelPhotoService.GetHotelPhotoByHotelIdAsync(bookingHotel.HotelId);
                string path = hotelPhoto.PhotoUrl;
                var fullPath = _appEnvironment.WebRootPath + path;  

                //MailMessage - представляет сообщение электронной почты, которое может быть отправлено с помощью класса SmtpClient.
                MailMessage message = new MailMessage();
                message.To.Add(new MailAddress(user.Email)); // электронный адрес получателя (login@itstep.academy)   
                message.From = new MailAddress("bon.voyage.step@gmail.com"); // электронный адрес отправителя (login@gmail.com)
                message.Subject = "Hotel booking"; // тема письма
                message.Body = "Dear, " + user.UserName + " your booking request for hotel " + hotel.Name + " will be processed as soon as possible!"; // содержимое письма
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

                return Json(new { success = true, message = "Booking request successfully sent!" });
            }
            return View(bookingHotel);
        }
        // GET: BookingHotel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                BookingHotelDTO bookingHotel = await bookingHotelService.GetBookingHotelByIdAsync((int)id);
                return View(bookingHotel);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: BookingHotel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookingHotelDTO bookingHotel)
        {
            if (ModelState.IsValid)
            {
                await bookingHotelService.UpdateBookingHotelAsync(bookingHotel);

                var user = await _userService.GetUserByIdAsync(bookingHotel.UserId);
                var hotel = await _hotelService.GetHotelByIdAsync(bookingHotel.HotelId);
                var hotelPhoto = await _hotelPhotoService.GetHotelPhotoByHotelIdAsync(bookingHotel.HotelId);
                string path = hotelPhoto.PhotoUrl;
                var fullPath = _appEnvironment.WebRootPath + path;

                //MailMessage - представляет сообщение электронной почты, которое может быть отправлено с помощью класса SmtpClient.
                MailMessage message = new MailMessage();
                message.To.Add(new MailAddress(user.Email)); // электронный адрес получателя (login@itstep.academy)   
                message.From = new MailAddress("bon.voyage.step@gmail.com"); // электронный адрес отправителя (login@gmail.com)
                message.Subject = "Tour booking"; // тема письма
                bookingHotel.Status.Trim();
                if (bookingHotel.Status == "Confirmed")
                    message.Body = "Dear, " + user.UserName + " your booking request for hotel " + hotel.Name + " has been confirmed! Our agent will contact you shortly."; // содержимое письма
                if (bookingHotel.Status == "Cancelled")
                    message.Body = "Dear, " + user.UserName + " your booking request for hotel " + hotel.Name + " has been cancelled.";
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
                return RedirectToAction("Index", "BookingHotel");
            }
            return View(bookingHotel);
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faq = await bookingHotelService.GetBookingHotelByIdAsync(id);
            if (faq == null)
            {
                return Json(new { success = false, message = "Booking not found!" });
            }

            await bookingHotelService.DeleteBookingHotelAsync(id);
            return Json(new { success = true, message = "Booking deleted successfully!" });
        }
    }
}
