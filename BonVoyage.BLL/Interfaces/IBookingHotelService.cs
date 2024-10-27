using BonVoyage.BLL.DTOs;

namespace BonVoyage.BLL.Interfaces
{
    public interface IBookingHotelService
    {
        Task<IEnumerable<BookingHotelDTO>> GetAllBookingsHotelsAsync();
        Task<BookingHotelDTO> GetBookingHotelByIdAsync(int id);
        Task CreateBookingHotelAsync(BookingHotelDTO bookingHotelDTO);
        Task UpdateBookingHotelAsync(BookingHotelDTO bookingHotelDTO);
        Task DeleteBookingHotelAsync(int id);
    }
}
