namespace BonVoyage.BLL.DTOs
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int TourId { get; set; }
        public int? HotelId { get; set; }
        public string? Text { get; set; }
        public int Rating { get; set; }
    }
}
