using BonVoyage.BLL.Interfaces;
using BonVoyage.DAL.Interfaces;
using BonVoyage.DAL.Entities;
using BonVoyage.BLL.Infrastructure;
using BonVoyage.BLL.DTOs;
using AutoMapper;

namespace BonVoyage.BLL.Services
{
    public class BookingHotelService : IBookingHotelService
    {
        IUnitOfWork Database { get; set; }

        public BookingHotelService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task CreateBookingHotelAsync(BookingHotelDTO bookingHotelDTO)
        {
            var bookingHotel = new BookingHotel
            {
                BookingHotelId = bookingHotelDTO.BookingHotelId,
                UserId = bookingHotelDTO.UserId,
                HotelId = bookingHotelDTO.HotelId,
                BookingDate = bookingHotelDTO.BookingDate,
                NumberOfPeople = bookingHotelDTO.NumberOfPeople,
                Status = bookingHotelDTO.Status
            };
            await Database.BookingsHotels.Create(bookingHotel);
            await Database.Save();
        }
        public async Task UpdateBookingHotelAsync(BookingHotelDTO bookingHotelDTO)
        {
            var bookingHotel = new BookingHotel
            {
                BookingHotelId = bookingHotelDTO.BookingHotelId,
                UserId = bookingHotelDTO.UserId,
                HotelId = bookingHotelDTO.HotelId,
                BookingDate = bookingHotelDTO.BookingDate,
                NumberOfPeople = bookingHotelDTO.NumberOfPeople,
                Status = bookingHotelDTO.Status
            };
            Database.BookingsHotels.Update(bookingHotel);
            await Database.Save();
        }
        public async Task DeleteBookingHotelAsync(int id)
        {
            await Database.BookingsHotels.Delete(id);
            await Database.Save();
        }

        public async Task<BookingHotelDTO> GetBookingHotelByIdAsync(int id)
        {
            var bookingHotel = await Database.BookingsHotels.Get(id);
            if (bookingHotel == null)
                throw new ValidationException("Wrong booking!", "");
            return new BookingHotelDTO
            {
                BookingHotelId = bookingHotel.BookingHotelId,
                UserId = bookingHotel.UserId,
                HotelId = bookingHotel.HotelId,
                BookingDate = bookingHotel.BookingDate,
                NumberOfPeople = bookingHotel.NumberOfPeople,
                Status = bookingHotel.Status
            };
        }
        // Automapper 
        public async Task<IEnumerable<BookingHotelDTO>> GetAllBookingsHotelsAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<BookingHotel, BookingHotelDTO>()
            .ForMember("Hotel", opt => opt.MapFrom(c => c.Hotel.Name)).ForMember("User", opt => opt.MapFrom(c => c.User.UserSurname)).ForMember("UserMail", opt => opt.MapFrom(c => c.User.Email)));
            var mapper = new Mapper(config);
            return mapper.Map<IQueryable<BookingHotel>, IEnumerable<BookingHotelDTO>>(await Database.BookingsHotels.GetAll());
        }
    }
}
