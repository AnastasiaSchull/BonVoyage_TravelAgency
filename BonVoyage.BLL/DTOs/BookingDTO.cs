namespace BonVoyage.BLL.DTOs
{
    public class BookingDTO
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int TourId { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumberOfPeople { get; set; }
        public string? Status { get; set; }
        public string? Tour { get; set; }
        public string? User { get; set; }

    }
}
