using Microsoft.AspNetCore.Mvc;
using BonVoyage.BLL.DTOs;
using BonVoyage.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using BonVoyage_WebAPI.Models;

namespace BonVoyage_WebAPI.Controllers
{
    [ApiController]
    [Route("api/Hotels")]

    public class HotelController : ControllerBase
    {
        private readonly IHotelService hotelService;
        private readonly IHotelPhotoService hotelPhotoService;

        public HotelController(IHotelService serv, IHotelPhotoService photoServ)
        {
            hotelService = serv;
            hotelPhotoService = photoServ;
        }
        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<List<HotelViewModel>>> GetAllHotels()
        {
            var hotels = await hotelService.GetAllHotelsAsync();
            if (hotels == null)
            {
                return NotFound();
            }
            var hotelsPhotos = await hotelPhotoService.GetAllHotelPhotosAsync();

            List<HotelViewModel> hotelsList = new List<HotelViewModel>();
            foreach (var hotel in hotels)
            {
                HotelViewModel hotelViewModel = new HotelViewModel();
                hotelViewModel.HotelId = hotel.HotelId;
                hotelViewModel.Name = hotel.Name;
                hotelViewModel.Location = hotel.Location;
                hotelViewModel.Country = hotel.Country;
                hotelViewModel.City = hotel.City;
                hotelViewModel.PricePerNight = hotel.PricePerNight;
                hotelViewModel.StarRating = hotel.StarRating;
                hotelViewModel.HasSwimmingPool = hotel.HasSwimmingPool;
                hotelViewModel.Description = hotel.Description;
                hotelViewModel.TourId = hotel.TourId;
                hotelViewModel.Tour = hotel.Tour;


                foreach (var hotelPhoto in hotelsPhotos)
                {
                    if (hotelPhoto.HotelId == hotel.HotelId)
                        hotelViewModel.PhotoUrl = hotelPhoto.PhotoUrl;
                }
                hotelsList.Add(hotelViewModel);
            }
            return hotelsList;
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
            var hotels = await hotelService.GetAllHotelsAsync();
            if (hotels == null)
            {
                return NotFound();
            }
            var hotel = await hotelService.GetHotelByIdAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return hotel;
        }
        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, HotelDTO hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hotel.HotelId)
            {
                return BadRequest();
            }

            try
            {
                await hotelService.UpdateHotelAsync(hotel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelDTO>> PostHotel(HotelDTO hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await hotelService.CreateHotelAsync(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.HotelId }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotels = await hotelService.GetAllHotelsAsync();
            if (hotels == null)
            {
                return NotFound();
            }
            var hotel = await hotelService.GetHotelByIdAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            await hotelService.DeleteHotelAsync(id);

            return NoContent();
        }
        private bool HotelExists(int id)
        {
            var hotel = hotelService.GetHotelByIdAsync(id);

            if (hotel == null)
                return false;

            return true;
        }
    }
}
