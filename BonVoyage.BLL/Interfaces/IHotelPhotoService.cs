using BonVoyage.BLL.DTOs;

namespace BonVoyage.BLL.Interfaces
{
    public interface IHotelPhotoService
    {
        Task<IQueryable<HotelPhotoDTO>> GetAllHotelPhotosAsync();
        Task<HotelPhotoDTO> GetHotelPhotoByIdAsync(int id);
        Task CreateHotelPhotoAsync(HotelPhotoDTO hotelPhotoDTO);
        Task UpdateHotelPhotoAsync(HotelPhotoDTO hotelPhotoDTO);
        Task DeleteHotelPhotoAsync(int id);
    }
}
