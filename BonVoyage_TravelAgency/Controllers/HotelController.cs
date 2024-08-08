using Microsoft.AspNetCore.Mvc;
using BonVoyage.BLL.DTOs;
using BonVoyage.BLL.Interfaces;
using BonVoyage.BLL.Infrastructure;

namespace BonVoyage_TravelAgency.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotelService hotelService;

        public HotelController(IHotelService serv)
        {
            hotelService = serv;
        }

        // GET: Hotels
        public async Task<IActionResult> Index()
        {
            return View(await hotelService.GetAllHotelsAsync());
        }

        // GET: Hotels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                HotelDTO hotel = await hotelService.GetHotelByIdAsync((int)id);
                return View(hotel);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //
        // GET: /Hotels/Create

        public IActionResult Create()
        {
            return View();
        }

        // POST: Hotels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HotelDTO hotel)
        {
            if (ModelState.IsValid)
            {
                await hotelService.CreateHotelAsync(hotel);
                return View("~/Views/Hotels/Index.cshtml", await hotelService.GetAllHotelsAsync());
            }
            return View(hotel);
        }

        // GET: Hotels/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                HotelDTO hotel = await hotelService.GetHotelByIdAsync((int)id);
                return View(hotel);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: Hotels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HotelDTO hotel)
        {
            if (ModelState.IsValid)
            {
                await hotelService.UpdateHotelAsync(hotel);
                return View("~/Views/Hotels/Index.cshtml", await hotelService.GetAllHotelsAsync());
            }
            return View(hotel);
        }

        // GET: Hotels/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                HotelDTO hotel = await hotelService.GetHotelByIdAsync((int)id);
                return View(hotel);
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

        // POST: Hotels/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await hotelService.DeleteHotelAsync(id);
            return View("~/Views/Hotels/Index.cshtml", await hotelService.GetAllHotelsAsync());
        }

    }
}