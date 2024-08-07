using BonVoyage.BLL.Interfaces;
using BonVoyage.DAL.Interfaces;
using BonVoyage.DAL.Entities;
using BonVoyage.BLL.Infrastructure;
using BonVoyage.BLL.DTOs;
using AutoMapper;

namespace BonVoyage.BLL.Services
{
    public class HotelPhotoService: IHotelPhotoService
    {
        IUnitOfWork Database { get; set; }

        public HotelPhotoService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task CreateHotelPhotoAsync(HotelPhotoDTO hotelPhotoDTO )
        {
            var hotelPhoto = new HotelPhoto
            {
                HotelPhotoId  = hotelPhotoDTO.HotelPhotoId,
                HotelId = hotelPhotoDTO.HotelId,
                PhotoUrl = hotelPhotoDTO.PhotoUrl
            };
            await Database.HotelPhotos.Create(hotelPhoto);
            await Database.Save();
        }
        public async Task UpdateHotelPhotoAsync(HotelPhotoDTO hotelPhotoDTO)
        {
            var hotelPhoto = new HotelPhoto
            {
                HotelPhotoId  = hotelPhotoDTO.HotelPhotoId,
                HotelId = hotelPhotoDTO.HotelId,
                PhotoUrl = hotelPhotoDTO.PhotoUrl
            };
            Database.HotelPhotos.Update(hotelPhoto);
            await Database.Save();
        }
        public async Task DeleteHotelPhotoAsync(int id)
        {
            await Database.HotelPhotos.Delete(id);
            await Database.Save();
        }

        public async Task<HotelPhotoDTO> GetHotelPhotoByIdAsync(int id)
        {
            var hotelPhoto = await Database.HotelPhotos.Get(id);
            if (hotelPhoto  == null)
                throw new ValidationException("Wrong hotel photo!", "");
            return new HotelPhotoDTO
            {
                HotelPhotoId = hotelPhoto.HotelPhotoId,
                HotelId = hotelPhoto.HotelId,
                PhotoUrl = hotelPhoto.PhotoUrl,
                Hotel = hotelPhoto.Hotel?.Name
            };
        }
        // Automapper 
        public async Task<IEnumerable<HotelPhotoDTO>> GetAllHotelPhotosAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<HotelPhoto, HotelPhotoDTO>()
            .ForMember("Hotel", opt => opt.MapFrom(c => c.Hotel.Name)));
            var mapper = new Mapper(config);
            return mapper.Map<IQueryable<HotelPhoto>, IEnumerable<HotelPhotoDTO>>(await Database.HotelPhotos.GetAll());
        }
    }
}
