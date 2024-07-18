using BonVoyage.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonVoyage.BLL.Interfaces
{
    public interface IHotelService
    {
        Task<IEnumerable<Hotel>> GetAllHotelsAsync();
        Task<Hotel> GetHotelByIdAsync(int id);
        Task CreateHotelAsync(Hotel hotel);
        Task UpdateHotelAsync(Hotel hotel);
        Task DeleteHotelAsync(int id);
    }
}
