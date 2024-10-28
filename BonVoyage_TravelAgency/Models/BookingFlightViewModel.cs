namespace BonVoyage_TravelAgency.Models
{
    public class BookingFlightViewModel
    {
        public string? Airline { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public decimal Price { get; set; }
        public string? PassengerName { get; set; }
        public string? PassengerSurname { get; set; }
        public string? Email { get; set; }
    }
}
