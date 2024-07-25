namespace BonVoyage.BLL.DTOs
{
    public class HotelPhotoDTO
    {
        public int HotelPhotoId { get; set; }
        public int HotelId { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Hotel { get; set; }

    }
}
