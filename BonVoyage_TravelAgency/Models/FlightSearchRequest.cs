namespace BonVoyage_TravelAgency.Models
{
    public class FlightSearchRequest
    {
        public string? CountryOfDeparture { get; set; }
        public string? CityOfDeparture { get; set; }
        public string? DestinationCity { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
