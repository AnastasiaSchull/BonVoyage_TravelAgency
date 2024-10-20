using BonVoyage.BLL.DTOs;

namespace BonVoyage_TravelAgency.Models
{
    public class BookingFilterModel
    {
        public IEnumerable<BookingDTO>? Bookings { get; set; } = new List<BookingDTO>();        
        public string? Status { get; set; }        
        public string? User { get; set; }
        public BookingFilterModel(IEnumerable<BookingDTO> bookings, string status, string user)
        {
            Bookings = bookings;
            Status = status;
            User = user;
        }
    }
}
