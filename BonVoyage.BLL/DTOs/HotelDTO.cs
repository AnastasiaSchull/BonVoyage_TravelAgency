using System.Collections.Generic;

namespace BonVoyage.BLL.DTOs
{
    public class HotelDTO
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public decimal PricePerNight { get; set; }
        public int StarRating { get; set; }
        public bool HasSwimmingPool { get; set; }
        public int TourId { get; set; }
        public ICollection<ReviewDTO> Reviews { get; set; }
        public ICollection<HotelPhotoDTO> HotelPhotos { get; set; }
    }
}
