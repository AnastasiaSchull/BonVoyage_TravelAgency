using Microsoft.AspNetCore.Mvc;
using BonVoyage.BLL.DTOs;
using BonVoyage.BLL.Interfaces;
using BonVoyage.BLL.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BonVoyage_TravelAgency.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;
        private readonly IUserService userService;
        private readonly ITourService tourService;
        private readonly IHotelService hotelService;
     

        public ReviewController(IReviewService reviewServ, 
            IUserService userServ, 
            ITourService tourServ, 
            IHotelService hotelServ)
        {
            reviewService = reviewServ;
            userService = userServ;
            tourService = tourServ;
            hotelService = hotelServ;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            return View(await reviewService.GetAllReviewsAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                ReviewDTO review = await reviewService.GetReviewByIdAsync((int)id);
                return View(review);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //
        // GET: /Reviews/Create

        public async Task<IActionResult> Create()
        {
            ViewBag.ListUsers = new SelectList(await  userService.GetAllUsersAsync(), "UserId", "Name");
            ViewBag.ListArtists = new SelectList(await tourService.GetAllToursAsync(), "TourId", "Name");
            ViewBag.ListVideos = new SelectList(await hotelService.GetAllHotelsAsync(), "HotelId", "Name");
            return View();
        }

        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewDTO review)
        {
            if (ModelState.IsValid)
            {
                await reviewService.CreateReviewAsync(review);
                return View("~/Views/Reviews/Index.cshtml", await reviewService.GetAllReviewsAsync());
            }
            ViewBag.ListUsers = new SelectList(await userService.GetAllUsersAsync(), "UserId", "Name", review.UserId);
            ViewBag.ListArtists = new SelectList(await tourService.GetAllToursAsync(), "TourId", "Name", review.TourId);
            ViewBag.ListVideos = new SelectList(await hotelService.GetAllHotelsAsync(), "HotelId", "Name", review.HotelId);
            return View(review);
        }

        // GET: Reviews/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                ReviewDTO review = await reviewService.GetReviewByIdAsync((int)id);
                ViewBag.ListUsers = new SelectList(await userService.GetAllUsersAsync(), "UserId", "Name", review.UserId);
                ViewBag.ListArtists = new SelectList(await tourService.GetAllToursAsync(), "TourId", "Name", review.TourId);
                ViewBag.ListVideos = new SelectList(await hotelService.GetAllHotelsAsync(), "HotelId", "Name", review.HotelId);
                return View(review);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ReviewDTO review)
        {
            if (ModelState.IsValid)
            {
                await reviewService.UpdateReviewAsync(review);
                ViewBag.ListUsers = new SelectList(await userService.GetAllUsersAsync(), "UserId", "Name", review.UserId);
                ViewBag.ListArtists = new SelectList(await tourService.GetAllToursAsync(), "TourId", "Name", review.TourId);
                ViewBag.ListVideos = new SelectList(await hotelService.GetAllHotelsAsync(), "HotelId", "Name", review.HotelId);
                return View("~/Views/Reviews/Index.cshtml", await reviewService.GetAllReviewsAsync());
            }
            return View(review);
        }

        // GET: Reviews/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                ReviewDTO review = await reviewService.GetReviewByIdAsync((int)id);
                return View(review);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: Reviews/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await reviewService.DeleteReviewAsync(id);
            return View("~/Views/Reviews/Index.cshtml", await reviewService.GetAllReviewsAsync());
        }

    }
}