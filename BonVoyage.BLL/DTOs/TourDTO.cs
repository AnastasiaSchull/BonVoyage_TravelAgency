namespace BonVoyage.BLL.DTOs
{
    public class TourDTO
    {
        public int TourId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public string? Country { get; set; }
        public string? Route { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }       
	}
}
