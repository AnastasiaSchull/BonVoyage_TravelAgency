using BonVoyage.BLL.DTOs;

namespace BonVoyage_TravelAgency.Models
{
    public class BookingViewModel
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int TourId { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumberOfPeople { get; set; }
        public string? Status { get; set; }        
        public TourDTO? Tour { get; set; }
        public TourPhotoDTO? TourPhoto { get; set; }
        public IEnumerable<UserDTO>? Users { get; set; }

    }
}
