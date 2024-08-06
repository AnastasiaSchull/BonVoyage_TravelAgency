using Microsoft.AspNetCore.Mvc;
using BonVoyage.BLL.DTOs;
using BonVoyage.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BonVoyage_WebAPI.Controllers
{
    [ApiController]
    [Route("api/Tours")]

    public class TourController: ControllerBase
    {
        private readonly ITourService tourService;

        public TourController(ITourService serv)
        {
            tourService = serv;
        }
        // GET: api/Tours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TourDTO>>> GetAllTours()
        {
            var tours = await tourService.GetAllToursAsync();
            if (tours == null)
            {
                return NotFound();
            }
            return tours.ToList(); 
        }

        // GET: api/Tours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TourDTO>> GetTour(int id)
        {
            var tours = await tourService.GetAllToursAsync();
            if (tours == null)
            {
                return NotFound();
            }
            var tour = await tourService.GetTourByIdAsync(id); 
            if (tour == null)
            {
                return NotFound();
            }
            return tour;
        }
        // PUT: api/Tours/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTour(int id, TourDTO tour)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tour.TourId)
            {
                return BadRequest();
            }

            try
            {
                await tourService.UpdateTourAsync(tour);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TourExists(id))
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

        // POST: api/Tours
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TourDTO>> PostTour(TourDTO tour)
        {
            if (!ModelState.IsValid)
            {
               return BadRequest(ModelState);
            }

            await tourService.CreateTourAsync(tour);

            return CreatedAtAction("GetTour", new { id = tour.TourId }, tour);
        }

        // DELETE: api/Tours/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTour(int id)
        {
            var tours = await tourService.GetAllToursAsync();
            if (tours == null)
            {
                return NotFound();
            }
            var tour = await tourService.GetTourByIdAsync(id);
            if (tour == null)
            {
                return NotFound();
            }
            await tourService.DeleteTourAsync(id);

            return NoContent();
        }
        private bool TourExists(int id)
        {
            var tour = tourService.GetTourByIdAsync(id);

            if (tour == null)
                return false;

            return true;
        }       
    }
}
