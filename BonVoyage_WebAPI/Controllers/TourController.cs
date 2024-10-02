using Microsoft.AspNetCore.Mvc;
using BonVoyage.BLL.DTOs;
using BonVoyage.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using BonVoyage_WebAPI.Models;


namespace BonVoyage_WebAPI.Controllers
{
    [ApiController]
    [Route("api/Tours")]

    public class TourController: ControllerBase
    {
        private readonly ITourService tourService;
        private readonly ITourPhotoService tourPhotoService;

        public TourController(ITourService serv, ITourPhotoService photoServ)
        {
            tourService = serv;
            tourPhotoService = photoServ;
        }
        // GET: api/Tours
        [HttpGet]
        public async Task<ActionResult<List<TourViewModel>>> GetAllTours()
        {
            var tours = await tourService.GetAllToursAsync();
            if (tours == null)
            {
                return NotFound();
            }
            var toursPhotos = await tourPhotoService.GetAllTourPhotosAsync();

            List<TourViewModel> toursList = new List<TourViewModel>();
            foreach(var tour in tours)
            {
                TourViewModel tourViewModel = new TourViewModel();
                tourViewModel.TourId = tour.TourId;
                tourViewModel.Title = tour.Title;
                tourViewModel.Description = tour.Description;
                tourViewModel.Duration = tour.Duration;
                tourViewModel.Price = tour.Price;
                tourViewModel.Country = tour.Country;
                tourViewModel.Route = tour.Route;
                tourViewModel.StartDate = tour.StartDate;
                tourViewModel.EndDate = tour.EndDate;
                
                foreach(var tourPhoto in toursPhotos)
                {
                    if(tourPhoto.TourId == tour.TourId)
                        tourViewModel.PhotoUrl = tourPhoto.PhotoUrl;
                }
                toursList.Add(tourViewModel);
            }
            return toursList; 
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
     

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> PostTour([FromForm] CreateTourRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tour = new TourDTO
            {
                Title = request.Title,
                Description = request.Description,
                Duration = request.Duration,
                Price = request.Price,
                Country = request.Country,
                Route = request.Route,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            var createdTour = await tourService.CreateTourAsync(tour);  
            if (request.Photo != null)
            {
                var photoPath = await SaveFileAsync(request.Photo);  
                var tourPhoto = new TourPhotoDTO
                {
                    TourId = createdTour.TourId,
                    PhotoUrl = photoPath
                };

                await tourPhotoService.CreateTourPhotoAsync(tourPhoto);  
            }

            return CreatedAtAction("GetTour", new { id = createdTour.TourId }, createdTour);
        }

        private async Task<string> SaveFileAsync(IFormFile photo)
        {
            //путь к wwwroot проекта BonVoyage_TravelAgency
            var uploadPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "BonVoyage_TravelAgency/BonVoyage_TravelAgency/wwwroot/images/tours");                  

            // генерация уникального имени файла
            var fileName = Path.GetFileNameWithoutExtension(photo.FileName) + "_" + DateTime.Now.Ticks + Path.GetExtension(photo.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                try
                {
                    await photo.CopyToAsync(fileStream);
                    Console.WriteLine($"File saved: {filePath}"); 
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving file: {ex.Message}");
                }
            }

            // путь для сохранения в базу данных
            return $"/images/tours/{fileName}";
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
