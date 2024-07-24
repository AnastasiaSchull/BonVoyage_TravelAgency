using BonVoyage.BLL.Interfaces;
using BonVoyage.DAL.Entities;
using BonVoyage.DAL.Interfaces;

namespace BonVoyage.BLL.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _unitOfWork.Bookings.GetAll();
        }

        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            return await _unitOfWork.Bookings.Get(id);
        }

        public async Task CreateBookingAsync(Booking booking)
        {
            await _unitOfWork.Bookings.Create(booking);
            await _unitOfWork.Save();
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            _unitOfWork.Bookings.Update(booking);
            await _unitOfWork.Save();
        }

        public async Task DeleteBookingAsync(int id)
        {
            await _unitOfWork.Bookings.Delete(id);
            await _unitOfWork.Save();
        }
    }
}
