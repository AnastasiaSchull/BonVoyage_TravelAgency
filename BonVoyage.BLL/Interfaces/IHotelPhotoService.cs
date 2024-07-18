using BonVoyage.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonVoyage.BLL.Interfaces
{
    public interface IHotelPhotoService
    {
        Task<IEnumerable<HotelPhoto>> GetAllHotelPhotosAsync();
        Task<HotelPhoto> GetHotelPhotoByIdAsync(int id);
        Task CreateHotelPhotoAsync(HotelPhoto hotelPhoto);
        Task UpdateHotelPhotoAsync(HotelPhoto hotelPhoto);
        Task DeleteHotelPhotoAsync(int id);
    }
}
