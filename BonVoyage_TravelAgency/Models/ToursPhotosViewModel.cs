namespace BonVoyage_TravelAgency.Models
{
    public class ToursPhotosViewModel
    {
        public IEnumerable<BonVoyage.BLL.DTOs.TourDTO>? Tours { get; set; }
        public IEnumerable<BonVoyage.BLL.DTOs.TourPhotoDTO>? TourPhotos { get; set; }
        public PageViewModel? PageViewModel { get; set; }
    }
}
