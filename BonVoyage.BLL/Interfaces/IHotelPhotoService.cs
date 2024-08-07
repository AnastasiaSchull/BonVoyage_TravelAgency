using BonVoyage.BLL.DTOs;
using System.Collections.Generic;

namespace BonVoyage.BLL.Interfaces
{
    public interface IHotelPhotoService
    {
        Task<IEnumerable<HotelPhotoDTO>> GetAllHotelPhotosAsync();
        Task<HotelPhotoDTO> GetHotelPhotoByIdAsync(int id);
        Task CreateHotelPhotoAsync(HotelPhotoDTO hotelPhotoDTO);
        Task UpdateHotelPhotoAsync(HotelPhotoDTO hotelPhotoDTO);
        Task DeleteHotelPhotoAsync(int id);
    }
}
