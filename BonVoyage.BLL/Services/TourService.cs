using BonVoyage.BLL.Interfaces;
using BonVoyage.DAL.Interfaces;
using BonVoyage.DAL.Entities;
using BonVoyage.BLL.Infrastructure;
using BonVoyage.BLL.DTOs;
using AutoMapper;
using System.Collections.Generic;


namespace BonVoyage.BLL.Services
{
    public class TourService: ITourService
    {
        IUnitOfWork Database { get; set; }

        public TourService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task CreateTourAsync(TourDTO tourDTO)
        {
            var tour = new Tour
            {
                TourId = tourDTO.TourId,
                Title = tourDTO.Title,
                Description = tourDTO.Description,
                Duration = tourDTO.Duration,
                Price = tourDTO.Price,
                Country = tourDTO.Country,
                Route = tourDTO.Route,
                StartDate = tourDTO.StartDate,
                EndDate = tourDTO.EndDate
            };
            await Database.Tours.Create(tour);
            await Database.Save();
        }
        public async Task UpdateTourAsync(TourDTO tourDTO)
        {
            var tour = new Tour
            {
                TourId = tourDTO.TourId,
                Title = tourDTO.Title,
                Description = tourDTO.Description,
                Duration = tourDTO.Duration,
                Price = tourDTO.Price,
                Country = tourDTO.Country,
                Route = tourDTO.Route,
                StartDate = tourDTO.StartDate,
                EndDate = tourDTO.EndDate
            };
            Database.Tours.Update(tour);
            await Database.Save();
        }
        public async Task DeleteTourAsync(int id)
        {
            await Database.Tours.Delete(id);
            await Database.Save();
        }
        public async Task<TourDTO> GetTourByIdAsync(int id)
        {
            var tour = await Database.Tours.Get(id);
            if (tour  == null)
                throw new ValidationException("Wrong tour!", "");
            return new TourDTO
            {
                TourId = tour.TourId,
                Title = tour.Title,
                Description = tour.Description,
                Duration = tour.Duration,
                Price = tour.Price,
                Country = tour.Country,
                Route = tour.Route,
                StartDate = tour.StartDate,
                EndDate = tour.EndDate
            };
        }
        // Automapper 

        //public async Task<IQueryable<TourDTO>> GetAllToursAsync()
        //{
        //    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Tour, TourDTO>()).CreateMapper();
        //    return mapper.Map<IQueryable<Tour>, IQueryable<TourDTO>>(await Database.Tours.GetAll());
        //}

        public async Task<IEnumerable<TourDTO>> GetAllToursAsync()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Tour, TourDTO>()).CreateMapper();
            return mapper.Map<IQueryable<Tour>, IEnumerable<TourDTO>>(await Database.Tours.GetAll());
        }

        public async Task<IEnumerable<TourDTO>> GetAllToursAsync(int pageNumber = 1, int pageSize = 10)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Tour, TourDTO>()).CreateMapper();

            var toursQuery = await Database.Tours.GetAll();

            // применяем пагинацию
            var tours = toursQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return mapper.Map<IEnumerable<TourDTO>>(tours);
        }

        public async Task<int> GetTotalToursCount()
        {
            return await Database.Tours.CountAsync();
        }
    }
}
