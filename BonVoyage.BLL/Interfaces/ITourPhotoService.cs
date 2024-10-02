using BonVoyage.BLL.DTOs;

namespace BonVoyage.BLL.Interfaces
{
    public interface ITourPhotoService
    {
        Task<IEnumerable<TourPhotoDTO>> GetAllTourPhotosAsync();
        Task<TourPhotoDTO> GetTourPhotoByTourIdAsync(int id);
        Task CreateTourPhotoAsync(TourPhotoDTO tourPhotoDTO);
        Task UpdateTourPhotoAsync(TourPhotoDTO tourPhotoDTO);
        Task DeleteTourPhotoAsync(int id);
    }
}
