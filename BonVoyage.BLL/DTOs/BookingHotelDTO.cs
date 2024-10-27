namespace BonVoyage.BLL.DTOs
{
    public class BookingHotelDTO
    {
        public int BookingHotelId { get; set; }
        public int UserId { get; set; }
        public int HotelId { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumberOfPeople { get; set; }
        public string? Status { get; set; }
        public string? Hotel { get; set; }
        public string? User { get; set; }
        public string? UserMail { get; set; }
    }
}
