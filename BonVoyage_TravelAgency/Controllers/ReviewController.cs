using Microsoft.AspNetCore.Mvc;
using BonVoyage.BLL.DTOs;
using BonVoyage.BLL.Interfaces;
using BonVoyage.BLL.Infrastructure;

namespace BonVoyage_TravelAgency.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService serv)
        {
            reviewService = serv;
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

        public IActionResult Create()
        {
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