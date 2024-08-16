using Microsoft.AspNetCore.Mvc;
using BonVoyage.BLL.DTOs;
using BonVoyage.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BonVoyage_WebAPI.Controllers
{
    [ApiController]
    [Route("api/TourPhotos")]

    public class TourPhotoController: ControllerBase
    {
        private readonly ITourPhotoService tourPhotoService;

        public TourPhotoController(ITourPhotoService serv)
        {
            tourPhotoService = serv;
        }
        // GET: api/TourPhotos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TourPhotoDTO>>> GetAllTourPhotos()
        {
            var tourPhotos = await tourPhotoService.GetAllTourPhotosAsync();
            if (tourPhotos == null)
            {
                return NotFound();
            }
            return tourPhotos.ToList(); 
        }

        // GET: api/TourPhotos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TourPhotoDTO>> GetTourPhoto(int id)
        {
            var tourPhotos = await tourPhotoService.GetAllTourPhotosAsync();
            if (tourPhotos == null)
            {
                return NotFound();
            }
            var tourPhoto = await tourPhotoService.GetTourPhotoByIdAsync(id); 
            if (tourPhoto == null)
            {
                return NotFound();
            }
            return tourPhoto;
        }
        // PUT: api/TourPhotos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTourPhoto(int id, TourPhotoDTO tourPhoto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tourPhoto.TourPhotoId)
            {
                return BadRequest();
            }

            try
            {
                await tourPhotoService.UpdateTourPhotoAsync(tourPhoto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TourPhotoExists(id))
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

        // POST: api/TourPhotos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TourPhotoDTO>> PostTourPhoto(TourPhotoDTO tourPhoto)
        {
            if (!ModelState.IsValid)
            {
               return BadRequest(ModelState);
            }

            await tourPhotoService.CreateTourPhotoAsync(tourPhoto);

            return CreatedAtAction("GetTourPhoto", new { id = tourPhoto.TourPhotoId }, tourPhoto);
        }

        // DELETE: api/TourPhotos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTourPhoto(int id)
        {
            var tourPhotos = await tourPhotoService.GetAllTourPhotosAsync();
            if (tourPhotos == null)
            {
                return NotFound();
            }
            var tourPhoto = await tourPhotoService.GetTourPhotoByIdAsync(id);
            if (tourPhoto == null)
            {
                return NotFound();
            }
            await tourPhotoService.DeleteTourPhotoAsync(id);

            return NoContent();
        }
        private bool TourPhotoExists(int id)
        {
            var tourPhoto = tourPhotoService.GetTourPhotoByIdAsync(id);

            if (tourPhoto == null)
                return false;

            return true;
        }       
    }
}
