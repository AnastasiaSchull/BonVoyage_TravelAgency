namespace BonVoyage_TravelAgency.Models
{
    public class FlightViewModel
    {
        public string? From { get; set; } 
        public string? To { get; set; } 
        public DateTime Departure { get; set; } 
        public DateTime Arrival { get; set; } 
        public decimal Price { get; set; } 
        public string? Airline { get; set; } 
    }

}
