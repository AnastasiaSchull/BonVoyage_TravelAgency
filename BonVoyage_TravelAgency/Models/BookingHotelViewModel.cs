using BonVoyage.BLL.DTOs;

namespace BonVoyage_TravelAgency.Models
{
    public class BookingHotelViewModel
    {
        public int BookingHotelId { get; set; }
        public int UserId { get; set; }
        public int HotelId { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumberOfPeople { get; set; }
        public string? Status { get; set; }        
        public HotelDTO? Hotel { get; set; }
        public HotelPhotoDTO? HotelPhoto { get; set; }
        public IEnumerable<UserDTO>? Users { get; set; }

    }
}
