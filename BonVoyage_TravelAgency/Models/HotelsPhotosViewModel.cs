namespace BonVoyage_TravelAgency.Models
{
    public class HotelsPhotosViewModel
    {
        public IEnumerable<BonVoyage.BLL.DTOs.HotelDTO>? Hotels { get; set; }
        public IEnumerable<BonVoyage.BLL.DTOs.HotelPhotoDTO>? HotelsPhotos { get; set; }

        public string Tour { get; set; }
    }
}
