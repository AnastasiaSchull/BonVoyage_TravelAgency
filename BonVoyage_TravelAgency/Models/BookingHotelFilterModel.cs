using BonVoyage.BLL.DTOs;

namespace BonVoyage_TravelAgency.Models
{
    public class BookingHotelFilterModel
    {
        public IEnumerable<BookingHotelDTO>? BookingsHotels { get; set; } = new List<BookingHotelDTO>();        
        public string? Status { get; set; }        
        public string? User { get; set; }
        public BookingHotelFilterModel(IEnumerable<BookingHotelDTO> bookingsHotels, string status, string user)
        {
            BookingsHotels = bookingsHotels;
            Status = status;
            User = user;
        }
    }
}
