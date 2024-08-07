using BonVoyage.BLL.DTOs;

namespace BonVoyage.BLL.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDTO>> GetAllBookingsAsync();
        Task<BookingDTO> GetBookingByIdAsync(int id);
        Task CreateBookingAsync(BookingDTO bookingDTO);
        Task UpdateBookingAsync(BookingDTO bookingDTO);
        Task DeleteBookingAsync(int id);
    }
}
