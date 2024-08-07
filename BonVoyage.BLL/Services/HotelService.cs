using BonVoyage.BLL.Interfaces;
using BonVoyage.DAL.Interfaces;
using BonVoyage.DAL.Entities;
using BonVoyage.BLL.Infrastructure;
using BonVoyage.BLL.DTOs;
using AutoMapper;


namespace BonVoyage.BLL.Services
{
    public class HotelService: IHotelService
    {
        IUnitOfWork Database { get; set; }

        public HotelService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task CreateHotelAsync(HotelDTO hotelDTO)
        {
            var hotel = new Hotel
            {
               HotelId = hotelDTO.HotelId,
               Name = hotelDTO.Name,
               Location = hotelDTO.Location,
               Country = hotelDTO.Country,
               City = hotelDTO.City,
               PricePerNight = hotelDTO.PricePerNight,
               StarRating = hotelDTO.StarRating,
               HasSwimmingPool = hotelDTO.HasSwimmingPool,
               TourId = hotelDTO.TourId
               };
           await Database.Hotels.Create(hotel);
           await Database.Save();
        }
        public async Task UpdateHotelAsync(HotelDTO hotelDTO)
        {
            var hotel = new Hotel
            {
                HotelId = hotelDTO.HotelId,
                Name = hotelDTO.Name,
                Location = hotelDTO.Location,
                Country = hotelDTO.Country,
                City = hotelDTO.City,
                PricePerNight = hotelDTO.PricePerNight,
                StarRating = hotelDTO.StarRating,
                HasSwimmingPool = hotelDTO.HasSwimmingPool,
                TourId = hotelDTO.TourId
            };
            Database.Hotels.Update(hotel);
            await Database.Save();
        }
        public async Task DeleteHotelAsync(int id)
        {
            await Database.Hotels.Delete(id);
            await Database.Save();
        }

        public async Task<HotelDTO> GetHotelByIdAsync(int id)
        {
            var hotel = await Database.Hotels.Get(id);
            if (hotel  == null)
                throw new ValidationException("Wrong hotel!", "");
            return new HotelDTO
            {
                HotelId = hotel.HotelId,
                Name = hotel.Name,
                Location = hotel.Location,
                Country = hotel.Country,
                City = hotel.City,
                PricePerNight = hotel.PricePerNight,
                StarRating = hotel.StarRating,
                HasSwimmingPool = hotel.HasSwimmingPool,
                TourId = hotel.TourId,
                Tour = hotel.Tour?.Title
            };
        }
        // Automapper 
        public async Task<IEnumerable<HotelDTO>> GetAllHotelsAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Hotel, HotelDTO>()
            .ForMember("Tour", opt => opt.MapFrom(c => c.Tour.Title)));
            var mapper = new Mapper(config);
            return mapper.Map<IQueryable<Hotel>, IEnumerable<HotelDTO>>(await Database.Hotels.GetAll());
        }
    }
}
