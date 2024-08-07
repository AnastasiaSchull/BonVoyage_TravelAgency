using BonVoyage.BLL.DTOs;

namespace BonVoyage.BLL.Interfaces
{
    public interface IHotelService
    {
        Task<IEnumerable<HotelDTO>> GetAllHotelsAsync();
        Task<HotelDTO> GetHotelByIdAsync(int id);
        Task CreateHotelAsync(HotelDTO hotelDTO);
        Task UpdateHotelAsync(HotelDTO hotelDTO);
        Task DeleteHotelAsync(int id);
    }
}
