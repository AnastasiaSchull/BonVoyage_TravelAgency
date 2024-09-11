namespace BonVoyage_WebAPI.Models
{
    public class HotelViewModel
    {
        public int HotelId { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public decimal PricePerNight { get; set; }
        public int StarRating { get; set; }
        public bool HasSwimmingPool { get; set; }
        public int TourId { get; set; }
        public string? Tour { get; set; }
        public string? Description { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
