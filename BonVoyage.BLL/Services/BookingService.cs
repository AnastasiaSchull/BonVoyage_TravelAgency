using BonVoyage.BLL.Interfaces;
using BonVoyage.DAL.Interfaces;
using BonVoyage.DAL.Entities;
using BonVoyage.BLL.Infrastructure;
using BonVoyage.BLL.DTOs;
using AutoMapper;

namespace BonVoyage.BLL.Services
{
    public class BookingService : IBookingService
    {
        IUnitOfWork Database { get; set; }

        public BookingService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task CreateBookingAsync(BookingDTO bookingDTO)
        {
            var booking = new Booking
            {
                BookingId = bookingDTO.BookingId,
                UserId = bookingDTO.UserId,
                TourId = bookingDTO.TourId,
                BookingDate = bookingDTO.BookingDate,
                NumberOfPeople = bookingDTO.NumberOfPeople,
                Status = bookingDTO.Status
            };
            await Database.Bookings.Create(booking);
            await Database.Save();
        }
        public async Task UpdateBookingAsync(BookingDTO bookingDTO)
        {
            var booking = new Booking
            {
                BookingId = bookingDTO.BookingId,
                UserId = bookingDTO.UserId,
                TourId = bookingDTO.TourId,
                BookingDate = bookingDTO.BookingDate,
                NumberOfPeople = bookingDTO.NumberOfPeople,
                Status = bookingDTO.Status
            };
            Database.Bookings.Update(booking);
            await Database.Save();
        }
        public async Task DeleteBookingAsync(int id)
        {
            await Database.Bookings.Delete(id);
            await Database.Save();
        }

        public async Task<BookingDTO> GetBookingByIdAsync(int id)
        {
            var booking = await Database.Bookings.Get(id);
            if (booking == null)
                throw new ValidationException("Wrong booking!", "");
            return new BookingDTO
            {
                BookingId = booking.BookingId,
                UserId = booking.UserId,
                TourId = booking.TourId,
                BookingDate = booking.BookingDate,
                NumberOfPeople = booking.NumberOfPeople,
                Status = booking.Status
            };
        }
        // Automapper 
        public async Task<IEnumerable<BookingDTO>> GetAllBookingsAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Booking, BookingDTO>()
            .ForMember("Tour", opt => opt.MapFrom(c => c.Tour.Title)).ForMember("User", opt => opt.MapFrom(c => c.User.UserSurname)).ForMember("UserMail", opt => opt.MapFrom(c => c.User.Email)));
            var mapper = new Mapper(config);
            return mapper.Map<IQueryable<Booking>, IEnumerable<BookingDTO>>(await Database.Bookings.GetAll());
        }
    }
}
