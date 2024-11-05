using BonVoyage.BLL.DTOs;

namespace BonVoyage_TravelAgency.Models
{
    public class UserProfileViewModel
    {
        public UserDTO User { get; set; } 
        public List<BookingDTO> Bookings { get; set; } 

        public UserProfileViewModel()
        {
            User = new UserDTO();
            Bookings = new List<BookingDTO>();
        }
    }

}
