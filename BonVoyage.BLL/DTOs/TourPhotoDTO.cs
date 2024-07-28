namespace BonVoyage.BLL.DTOs
{
    public class TourPhotoDTO
    {
        public int TourPhotoId { get; set; }
        public int TourId { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Tour { get; set; }

    }
}
