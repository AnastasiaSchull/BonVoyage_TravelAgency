using System.Collections.Generic;

namespace BonVoyage.BLL.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }
        public ICollection<ReviewDTO> Reviews { get; set; }
        public ICollection<BookingDTO> Bookings { get; set; }
        public ICollection<CustomerPreferenceDTO> Preferences { get; set; }
    }
}
