using BonVoyage.BLL.DTOs;

namespace BonVoyage.BLL.Interfaces
{
    public interface ITourPhotoService
    {
        Task<IEnumerable<TourPhotoDTO>> GetAllTourPhotosAsync();
        Task<TourPhotoDTO> GetTourPhotoByIdAsync(int id);
        Task CreateTourPhotoAsync(TourPhotoDTO tourPhotoDTO);
        Task UpdateTourPhotoAsync(TourPhotoDTO tourPhotoDTO);
        Task DeleteTourPhotoAsync(int id);
    }
}
