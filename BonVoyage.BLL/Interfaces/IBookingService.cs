using BonVoyage.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonVoyage.BLL.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingByIdAsync(int id);
        Task CreateBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(int id);
    }
}
