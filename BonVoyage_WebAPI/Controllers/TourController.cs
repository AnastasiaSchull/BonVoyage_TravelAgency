using Microsoft.AspNetCore.Mvc;
using BonVoyage.BLL.DTOs;
using BonVoyage.BLL.Interfaces;
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

                string? photoUrl = null;

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

                        photoUrl = newPhotoPath; // обновляем значение photoUrl
                    }
                    else
                    {                       
                        photoUrl = await SaveFileAsync(request.Photo);
                        var tourPhoto = new TourPhotoDTO
                        {
                            TourId = id,                           
                            PhotoUrl = photoUrl
                        };
                        await tourPhotoService.CreateTourPhotoAsync(tourPhoto);

                    }
                }
                else
                {
                    // если новое фото не загружено, сохраняем существующее
                    var existingPhoto = await tourPhotoService.GetTourPhotoByTourIdAsync(id);
                    if (existingPhoto != null)
                    {
                        photoUrl = existingPhoto.PhotoUrl;
                    }
                }
                //return Ok(tour); 
                return Ok(new
                {
                    tour.TourId,
                    tour.Title,
                    tour.Description,
                    tour.Duration,
                    tour.Price,
                    tour.Country,
                    tour.Route,
                    tour.StartDate,
                    tour.EndDate,
                    PhotoUrl = photoUrl // возвращаем либо новый, либо существующий URL фото
                });

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
            Console.WriteLine($"File received: {request.Photo?.FileName}");

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

            string? photoPath = null;

            if (request.Photo != null)
            {
                photoPath = await SaveFileAsync(request.Photo);

                // создаем объект для связи фотографии с туром
                var tourPhoto = new TourPhotoDTO
                {
                    TourId = createdTour.TourId,
                    PhotoUrl = photoPath
                };

                await tourPhotoService.CreateTourPhotoAsync(tourPhoto);  
            }

            // добавляем URL фотографии в объект тура, чтобы он был в ответе
            var tourResponse = new
            {
                tour.TourId,
                tour.Title,
                tour.Description,
                tour.Duration,
                tour.Price,
                tour.Country,
                tour.Route,
                tour.StartDate,
                tour.EndDate,
                PhotoUrl = photoPath // добавляем URL фотографии
            };

            // возвращаем ответ с созданным туром и фото URL
            return CreatedAtAction("GetTour", new { id = createdTour.TourId }, tourResponse);
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
