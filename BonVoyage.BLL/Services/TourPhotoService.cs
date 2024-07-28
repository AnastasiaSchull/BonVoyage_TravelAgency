using BonVoyage.BLL.Interfaces;
using BonVoyage.DAL.Interfaces;
using BonVoyage.DAL.Entities;
using BonVoyage.BLL.Infrastructure;
using BonVoyage.BLL.DTOs;
using AutoMapper;

namespace BonVoyage.BLL.Services
{
    public class TourPhotoService: ITourPhotoService
    {
        IUnitOfWork Database { get; set; }

        public TourPhotoService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task CreateTourPhotoAsync(TourPhotoDTO tourPhotoDTO)
        {
            var tourPhoto = new TourPhoto
            {
                TourPhotoId = tourPhotoDTO.TourPhotoId,
                TourId = tourPhotoDTO.TourId,
                PhotoUrl = tourPhotoDTO.PhotoUrl
            };
            await Database.TourPhotos.Create(tourPhoto);
            await Database.Save();
        }
        public async Task UpdateTourPhotoAsync(TourPhotoDTO tourPhotoDTO)
        {
            var tourPhoto = new TourPhoto
            {
                TourPhotoId = tourPhotoDTO.TourPhotoId,
                TourId = tourPhotoDTO.TourId,
                PhotoUrl = tourPhotoDTO.PhotoUrl
            };
            Database.TourPhotos.Update(tourPhoto);
            await Database.Save();
        }
        public async Task DeleteTourPhotoAsync(int id)
        {
            await Database.TourPhotos.Delete(id);
            await Database.Save();
        }

        public async Task<TourPhotoDTO> GetTourPhotoByIdAsync(int id)
        {
            var tourPhoto = await Database.TourPhotos.Get(id);
            if (tourPhoto  == null)
                throw new ValidationException("Wrong tour photo!", "");
            return new TourPhotoDTO
            {
                TourPhotoId = tourPhoto.TourPhotoId,
                TourId = tourPhoto.TourId,
                PhotoUrl = tourPhoto.PhotoUrl,
                Tour = tourPhoto.Tour?.Title
            };
        }
        // Automapper 
        public async Task<IQueryable<TourPhotoDTO>> GetAllTourPhotosAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TourPhoto, TourPhotoDTO>()
            .ForMember("Tour", opt => opt.MapFrom(c => c.Tour.Title)));
            var mapper = new Mapper(config);
            return mapper.Map<IQueryable<TourPhoto>, IQueryable<TourPhotoDTO>>(await Database.TourPhotos.GetAll());
        }
    }
}
