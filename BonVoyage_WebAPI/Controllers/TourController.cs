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

        private readonly IWebHostEnvironment _environment;

        public TourController(IWebHostEnvironment environment, ITourService serv, ITourPhotoService photoServ)
        {
            _environment = environment;
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
        [HttpPut("{id}"), DisableRequestSizeLimit]
        public async Task<IActionResult> PutTour(int id, [FromForm] UpdateTourRequest request)
        {
            try
            {
                Console.WriteLine($"Updating Tour with ID: {id}");
                Console.WriteLine($"Title: {request.Title}, StartDate: {request.StartDate}, EndDate: {request.EndDate}");
                if (request.Photo != null)
                {
                    Console.WriteLine($"Photo received: {request.Photo.FileName}");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // проверка, что id в запросе соответствует id в объекте тура
                if (id != request.TourId)
                {
                    return BadRequest();
                }

                // объект TourDTO для обновления тура
                var tour = new TourDTO
                {
                    TourId = request.TourId,
                    Title = request.Title,
                    Description = request.Description,
                    Duration = request.Duration,
                    Price = request.Price,
                    Country = request.Country,
                    Route = request.Route,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate
                };

                // обновляем основные данные
                await tourService.UpdateTourAsync(tour);

                // используем путь к директории, как в SaveFileAsync                
                var baseDirectory = Path.Combine(_environment.ContentRootPath, @"..\BonVoyage_TravelAgency\wwwroot");
                
                if (request.Photo != null)
                {
                    var existingPhoto = await tourPhotoService.GetTourPhotoByTourIdAsync(id);

                    if (existingPhoto != null)
                    {
                         // строим путь к старому фото
                        var oldPhotoPath = Path.Combine(baseDirectory, existingPhoto.PhotoUrl.TrimStart('/'));

                        if (System.IO.File.Exists(oldPhotoPath))
                        {
                            System.IO.File.Delete(oldPhotoPath);
                            Console.WriteLine($"Deleted old photo: {oldPhotoPath}");
                        }

                        var newPhotoPath = await SaveFileAsync(request.Photo);
                        existingPhoto.PhotoUrl = newPhotoPath;
                        await tourPhotoService.UpdateTourPhotoAsync(existingPhoto);
                    }
                    else
                    {
                        var newPhotoPath = await SaveFileAsync(request.Photo);
                        var tourPhoto = new TourPhotoDTO
                        {
                            TourId = id,
                            PhotoUrl = newPhotoPath
                        };
                        await tourPhotoService.CreateTourPhotoAsync(tourPhoto);
                    }
                }

                return Ok(tour); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
                return StatusCode(500, "An error occurred while updating");
            }
        }


        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> PostTour([FromForm] CreateTourRequest request)
        {
            Console.WriteLine($"File received: {request.Photo.FileName}");

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
            // путь к директории wwwroot другого проекта
            var baseDirectory = Path.GetFullPath(Path.Combine(_environment.ContentRootPath, @"..\BonVoyage_TravelAgency\wwwroot\images\tours"));

            var fileName = Path.GetFileNameWithoutExtension(photo.FileName) + "_" + DateTime.Now.Ticks + Path.GetExtension(photo.FileName);
            var filePath = Path.Combine(baseDirectory, fileName);

            Console.WriteLine($"Original format: {Path.GetExtension(photo.FileName)}");
            Console.WriteLine($"Saving file to: {filePath}");

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(fileStream);
            }

            //  путь для сохранения в базу данных
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
